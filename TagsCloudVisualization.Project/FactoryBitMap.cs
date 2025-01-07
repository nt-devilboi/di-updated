using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;


// паттерн фабричный метод собственной персоной
public class FactoryBitMap(TagCloudSettings cloudSettings) : AbstractFactoryBitMap(cloudSettings)
{
    protected override ITagCloudImage CreateBitMap(TagCloudSettings tagCloudSettings)
    {
        return new CloudBitMap(tagCloudSettings);
    }
}