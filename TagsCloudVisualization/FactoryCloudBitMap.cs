using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class FactoryCloudBitMap
{
    private ITagCloudSettings _cloudSettings;

    public FactoryCloudBitMap(ITagCloudSettings cloudSettings)
    {
        _cloudSettings = cloudSettings;
    }

    public ITagCloudImage Create()
    {
        Validate(_cloudSettings.Size.Width, _cloudSettings.Size.Height, _cloudSettings.PathDirectory, _cloudSettings.NameFile);

        return new CloudBitMap(_cloudSettings);
    }
    
    private static void Validate(int width, int height, string cloudSettingsPathDirectory, string cloudSettingsNameFile) // todo: проверка на то, что файла не существуте.
    {
        if (width <= 0 || height <= 0) throw new ArgumentException("size of image should be with positive number");
        if (!Directory.Exists(cloudSettingsPathDirectory)) throw new DirectoryNotFoundException($"not correct path {cloudSettingsPathDirectory}");
        if (cloudSettingsNameFile[^1] == '/') throw new ArgumentException(@"Path Should Be Without \");
    }
}