using TagsCloudVisualization.Result;

namespace TagsCloudVisualization;

// хочется сразу заиспользовать паттерн мост. использовать класс TagCloud, как абстракцию для создания всех деталей TagCloud
// замечу, что tagCloud не имеет интерфейса, ибо он по существу никогда не будет иметь другую реализацию, ибо вся логика внутри интерефейсов, а этот класс просто обёртка, которая ничего сама делать и не может. композиция на максиум кароче.
// здесь бы еще паттерн строитель, но это уже overhead.
public class TagCloud
{
    private readonly ICloudLayouter _cloudLayouter;
    private readonly IWordLoader _wordLoader;

    public TagCloud(ICloudLayouter cloudLayouter, IWordLoader wordLoader)
    {
        _cloudLayouter = cloudLayouter;
        _wordLoader = wordLoader;
    }

    private static Result<None> Validate(ICloudLayouter cloudLayouter, ITagCloudImage tagCloudImage)
    {
        return Result.Result.StartCheck(cloudLayouter.Start.Y <= tagCloudImage.Size().Height &&
                                        cloudLayouter.Start.X <= tagCloudImage.Size().Width,
            "the start position is abroad of image");
    }

    public ITagCloudImage
        GenerateCloud(
            ITagCloudImage tagCloudImage) // возвращать другой интерфейс, который имеет под собой фичу Save. иначе говоря, сделать два интерфейса, один рисования, второй уже на сохранение.
    {
        var result = Validate(_cloudLayouter, tagCloudImage);

        var wordPopular = _wordLoader.LoadWord();
        // todo: проверка, words пустой
        foreach (var word in wordPopular)
        {
            var size = tagCloudImage.GetSizeWord(word.Word);
            size.Width += 20;
            var rec = _cloudLayouter.PutNextRectangle(size);
            var recCloud = new RectangleTagCloud(rec, word.Word);
            tagCloudImage.Draw(recCloud);
        }

        return tagCloudImage;
    }
}