// See https://aka.ms/new-console-template for more information

using SimpleInjector;
using TagCloud2;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;
using WeCantSpell.Hunspell;

var serviceCollection = new Container();


serviceCollection.Register<AppSettings>(Lifestyle.Singleton);
serviceCollection.Register<ICloudLayouter, CircularCloudLayouter>(Lifestyle.Singleton);
serviceCollection.Register<IWordLoader, FileWordLoader>(Lifestyle.Singleton);
serviceCollection.Register<TagCloud>(Lifestyle.Singleton);

serviceCollection.Register<TagCloudSettings>(Lifestyle.Singleton);
serviceCollection.Register<WordLoaderSettings>(Lifestyle.Singleton);

serviceCollection.Register<ITagCloudController, TagCloudCli>(Lifestyle.Singleton);

serviceCollection.Register<FactoryCloudBitMap>(Lifestyle.Singleton);
serviceCollection.Register(() =>
    new Lazy<IProcessOutputReader>(() =>
        new StemReader(serviceCollection.GetInstance<WordLoaderSettings>())), Lifestyle.Singleton);

serviceCollection.Register(() => WordList.CreateFromFiles("./../../../ru_RU.dic"), Lifestyle.Singleton);
var application = serviceCollection.GetInstance<ITagCloudController>();

application.Run();