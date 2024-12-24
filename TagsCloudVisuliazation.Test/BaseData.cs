using TagsCloudVisualization.Settings;

namespace TagsCloudVisuliazation.Test;

public static class BaseData
{
    public static WordLoaderSettings GetLoaderSettings() 
        => new WordLoaderSettings()
    {
        Path = "./../../../text.txt",
        PathStem = "./../../../mystem.exe"
    };
}