using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

// хочется сразу заиспользовать паттерн мост. использовать класс TagCloud, как абстракцию для создания всех деталей TagCloud
// замечу, что tagCloud не имеет интерфейса, ибо он по существу никогда не будет иметь другую реализацию, ибо вся логика внутри интерефейсов, а этот класс просто обёртка, которая ничего сама делать и не может. композиция на максиум кароче.
// здесь бы еще паттерн строитель, но это уже overhead.
public class TagCloud
{
    private readonly ICloudLayouter _cloudLayouter;
    private readonly IWordLoader _wordLoader;
    private readonly TagCloudSettings _tagCloudSettings;

    public TagCloud(ICloudLayouter cloudLayouter, IWordLoader wordLoader, TagCloudSettings tagCloudSettings)
    {
        _cloudLayouter = cloudLayouter;
        _wordLoader = wordLoader;
        _tagCloudSettings = tagCloudSettings;
    }

    private static Result<None> Validate(ICloudLayouter cloudLayouter, ITagCloudImage tagCloudImage)
    {
        return Result.StartCheck(cloudLayouter.Start.Y <= tagCloudImage.Size().Height &&
                                 cloudLayouter.Start.X <= tagCloudImage.Size().Width,
            "the start position is abroad of image");
    }

    public Result<ITagCloudImage>
        GenerateCloud(
            ITagCloudImage tagCloudImage) // возвращать другой интерфейс, который имеет под собой фичу Save. иначе говоря, сделать два интерфейса, один рисования, второй уже на сохранение.
    {
        var result = Validate(_cloudLayouter, tagCloudImage);
        if (!result.IsSuccess) return Result.Fail<ITagCloudImage>(result.Error);

        var wordPopular = _wordLoader.LoadWord();
        var emSize = _tagCloudSettings.EmSize;
        // todo: проверка, words пустой
        foreach (var word in wordPopular)
        {
            var size = tagCloudImage.GetSizeWord(word.Word, emSize);
            size.Width += 20;
            var rec = _cloudLayouter.PutNextRectangle(size);
            var recCloud = new RectangleTagCloud(rec, word.Word, emSize);
            tagCloudImage.DrawString(recCloud);
            emSize = emSize > 14 ? emSize - 1 : 24;
        }

        return tagCloudImage.AsResult();
    }
}