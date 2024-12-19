using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagCloud2.Settings;

public class TagCloudSettings : ITagCloudSettings
{
    private string _pathDirectory;
    private string _nameFile;
    public Size Size { get; set; }

    public string PathDirectory
    {
        get => _pathDirectory;
        set
        {
            if (!Directory.Exists(value)) throw new DirectoryNotFoundException($"not correct path {value}");
            if (value[^1] == '/') throw new ArgumentException("PathShouldBeWithout \"\\\"");


            _pathDirectory = value;
        }
    }

    public string NameFile
    {
        get => _nameFile;
        set => _nameFile = value;
    }
}