namespace TagsCloudVisualization.Settings;

public class AppSettings(TagCloudSettings tagCloudSettings, WordLoaderSettings wordLoaderSettings)
{
    public TagCloudSettings TagCloudSettings { get; } = tagCloudSettings;
    public WordLoaderSettings WordLoaderSettings { get; } = wordLoaderSettings;
}