﻿// See https://aka.ms/new-console-template for more information

using SimpleInjector;
using TagCloud2;
using TagCloud2.Abstract;
using Tagloud2.Abstract;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
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
serviceCollection.Register<IInputData, InputData>(Lifestyle.Singleton);
serviceCollection.Register<ISizeWord, MeasureString>(Lifestyle.Singleton);
serviceCollection.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);
serviceCollection.Register<AbstractFactoryBitMap, FactoryBitMap>(Lifestyle.Singleton);
serviceCollection.Register(() =>
    new Lazy<IProcessOutputReader>(() =>
        new StemReader(serviceCollection.GetInstance<WordLoaderSettings>())), Lifestyle.Singleton);
var application = serviceCollection.GetInstance<ITagCloudController>();

application.Run();