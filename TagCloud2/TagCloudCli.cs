using CommandLine;
using TagCloud2.Options;
using TagsCloudVisualization;

namespace TagCloud2;

public class TagCloudCli : ITagCloudController
{
    private TagCloud _tagCloud;
    private AppSettings _appSettings;

    public TagCloudCli(TagCloud tagCloud, AppSettings appSettings)
    {
        _tagCloud = tagCloud;
        _appSettings = appSettings;
    }

    public void Run()
    {
        var data = Console.ReadLine();
        var args = data?.Split(" ");
        while (args != null)
        {
            Parser.Default.ParseArguments<SettingOptions, CreateOptions, InfoConfigOptions, CreateTagCloud>(args)
                .MapResult(
                    (SettingOptions x) => CreateSettingsForTagCloud(x),
                    (CreateOptions x) => SetPathTextFile(x),
                    (InfoConfigOptions x) => DisplayConfig(),
                    (CreateTagCloud x) => CreateCloud(),
                errors => new List<Error>(errors));

            args = Console.ReadLine().Split(" ");
        }
    }

    private IEnumerable<Error> CreateCloud()
    {
        _tagCloud.CreateFilePhotoInBuffer();
        
        _tagCloud.LoadTextFile();
        _tagCloud.GenerateCloud();

        _tagCloud.Save();

        return new List<Error>();
    }

    private IEnumerable<Error> DisplayConfig()
    {
        Console.WriteLine("Config Photo");
        Console.WriteLine($"Directory for photo <{_appSettings.TagCloudSettings.PathDirectory}>");
        Console.WriteLine($"Name Of PhotoFile <{_appSettings.TagCloudSettings.NameFile}>");
        Console.WriteLine($"Size Of PhotoFile <{_appSettings.TagCloudSettings.Size}>");
        Console.WriteLine("-----------------------------");

        Console.WriteLine($"Path of FileText \n <{_appSettings.WordLoaderSettings.Path}>");

        return new List<Error>();
    }

    private IEnumerable<Error> SetPathTextFile(CreateOptions createOptions)
    {
        _appSettings.WordLoaderSettings.Path = createOptions.path;

        return new List<Error>();
    }


    private IEnumerable<Error> CreateSettingsForTagCloud(SettingOptions se)
    {
        _appSettings.TagCloudSettings.Size = se.GetSize();
        _appSettings.TagCloudSettings.NameFile = se.Name;
        _appSettings.TagCloudSettings.PathDirectory = se.Directory;

        return new List<Error>();
    }
}