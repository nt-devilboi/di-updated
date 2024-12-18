using System.Drawing;
using CommandLine;
using SimpleInjector;
using TagCloud2.Options;
using TagsCloudVisualization;
using Container = SimpleInjector.Container;

namespace TagCloud2;

public class TagCloudCli : ITagCloudController
{
    private TagCloud tagCloud;

    public void Run()
    {
        var data = Console.ReadLine();
        var args = data?.Split(" ");
        while (args != null)
        {
            Parser.Default.ParseArguments<SettingOptions, CreateOptions>(args).MapResult(
                (SettingOptions x) => CreateSettingsForTagCloud(x),
                (CreateOptions x) => CreatePhoto(x),
                errors => new List<Error>(errors));

            args = Console.ReadLine().Split(" ");
        }
    }

    private IEnumerable<Error> CreatePhoto(CreateOptions createOptions)
    {
        if (tagCloud == null)
        {
            Console.WriteLine("i haven't parameter of photo");
        }

        var fileLoader = new FileWordLoader();
        var words = fileLoader.LoadWord(createOptions.path);
        
        tagCloud.GenerateCloud(words);
        tagCloud.Save();
        
        return new List<Error>();
    }


    private IEnumerable<Error> CreateSettingsForTagCloud(SettingOptions se)
    {
        var serviceCollection = new Container();
        var data = new SettingsTagCloud(se.GetSize(), se.Path, se.Name);
        Console.WriteLine(data);
        serviceCollection.Register(() =>
            new SettingsTagCloud(se.GetSize(), se.Path, se.Name), Lifestyle.Singleton);
        serviceCollection.Register<ICloudLayouter, CircularCloudLayouter>(Lifestyle.Singleton);
        serviceCollection.Register<ITagCloudWordLoader, FileWordLoader>(Lifestyle.Singleton);
        serviceCollection.Register<ITagCloudImage, CloudBitMap>(Lifestyle.Singleton);
        serviceCollection.Register<TagCloud>(Lifestyle.Singleton);

        tagCloud = serviceCollection.GetInstance<TagCloud>();
        serviceCollection.Dispose();
        return new List<Error>();
    }
}