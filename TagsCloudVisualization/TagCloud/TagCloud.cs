using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class TagCloud(ICloudLayouter cloudLayouter, IWordLoader wordLoader, TagCloudSettings tagCloudSettings)
{
    private static Result<None> Validate(ICloudLayouter cloudLayouter, ITagCloudImage tagCloudImage)
    {
        return Result.Result.StartCheck(cloudLayouter.Start.Y <= tagCloudImage.Size().Height &&
                                        cloudLayouter.Start.X <= tagCloudImage.Size().Width,
            "the start position is abroad of image");
    }

    public Result<ITagCloudImage> GenerateCloud(ITagCloudImage tagCloudImage, ISizeWord sizeWord) 
    {
        var result = Validate(cloudLayouter, tagCloudImage);
        if (!result.IsSuccess) return Result.Result.Fail<ITagCloudImage>(result.Error);

        var wordPopular = wordLoader.LoadWord();
        if (wordPopular.Length == 0) return Result.Result.Fail<ITagCloudImage>("Words in Text Zero");
        
        
        var emSize = tagCloudSettings.EmSize;
        foreach (var word in wordPopular)
        {
            var size = sizeWord.GetSizeWord(word.Word, emSize);
            size.Width += 20;
            
            var rec = cloudLayouter.PutNextRectangle(size);
            var recCloud = new RectangleTagCloud(rec, word.Word, emSize);
            tagCloudImage.DrawString(recCloud);
            emSize = emSize > 14 ? emSize - 1 : 24;
        }
        
        
        return tagCloudImage.AsResult();
    }
}