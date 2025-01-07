using System.Collections.Immutable;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class TagCloud(
    ICloudLayouter cloudLayouter,
    IWordLoader wordLoader,
    TagCloudSettings tagCloudSettings,
    ISizeWord sizeWord)
{
    private static Result<ITagCloudImage> Validate(ICloudLayouter cloudLayouter, ITagCloudImage tagCloudImage)
    {
        if (cloudLayouter.Start.Y > tagCloudImage.Size().Height ||
            cloudLayouter.Start.X > tagCloudImage.Size().Width)
        {
            return new Result<ITagCloudImage>("the start position is abroad of image");
        }

        return tagCloudImage.AsResult();
    }

    public Result<ITagCloudSave> GenerateCloud(ITagCloudImage tagCloudImage)
    {
        return Validate(cloudLayouter, tagCloudImage)
            .Then(LoadWord)
            .Then(AddWordInCloud);
    }

    private Result<ITagCloudSave> AddWordInCloud(
        (ITagCloudImage tagCloudImage, IEnumerable<FrequencyWord> words) data)
    {
        var emSize = tagCloudSettings.EmSize;
        foreach (var word in data.words)
        {
            var size = sizeWord.GetSizeWord(word.Word, emSize);
            size.Width += 20;

            var rec = cloudLayouter.PutNextRectangle(size);
            var recCloud = new RectangleTagCloud(rec, word.Word, emSize);
            data.tagCloudImage.DrawString(recCloud);
            emSize = emSize > 14 ? emSize - 1 : 24;
        }

        return ((ITagCloudSave)data.tagCloudImage).AsResult();
    }


    private Result<(ITagCloudImage, IEnumerable<FrequencyWord>)> LoadWord(ITagCloudImage tagCloudImage)
    {
        var words = wordLoader.LoadWords().ToList();

        if (words.Count == 0)
        {
            return Result.Result.Fail<(ITagCloudImage, IEnumerable<FrequencyWord>)>(
                Errors.Stem.TextIsEmptyOrOnlyBoringWords());
        }

        words.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return (tagCloudImage, words);
    }
}