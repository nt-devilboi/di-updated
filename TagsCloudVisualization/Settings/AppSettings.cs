using System.Drawing;
using TagCloud2.Settings;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class AppSettings(ITagCloudSettings tagCloudSettings, IWordLoaderSettings wordLoaderSettings)
{
    public ITagCloudSettings TagCloudSettings { get; } = tagCloudSettings;
    public IWordLoaderSettings WordLoaderSettings { get; } = wordLoaderSettings;
}