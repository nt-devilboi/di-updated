﻿// See https://aka.ms/new-console-template for more information

using SimpleInjector;
using TagCloud2;
using TagCloud2.Abstract;
using Tagloud2.Abstract;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

var serviceCollection = new Container();


serviceCollection.Register<AppSettings>(Lifestyle.Singleton);
serviceCollection.Register<ICloudLayouter, CircularCloudLayouter>(Lifestyle.Singleton);
serviceCollection.Register<IWordLoader, FileWordLoader>(Lifestyle.Singleton);
serviceCollection.Register<TagCloud>(Lifestyle.Singleton);

serviceCollection.Register<TagCloudSettings>(Lifestyle.Singleton);
serviceCollection.Register<WordLoaderSettings>(Lifestyle.Singleton);

serviceCollection.Register<ITagCloudController, TagCloudCli>(Lifestyle.Singleton);
serviceCollection.Register<IInputData>(() => new InputData(args), Lifestyle.Singleton);
serviceCollection.Register<ISizeWord, MeasureString>(Lifestyle.Singleton);
serviceCollection.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);
serviceCollection.Register<AbstractFactoryBitMap, FactoryBitMap>(Lifestyle.Singleton);
serviceCollection.Register<FactoryStem>(Lifestyle.Singleton);
var application = serviceCollection.GetInstance<ITagCloudController>();

application.Run();