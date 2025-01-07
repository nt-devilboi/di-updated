using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public class FactoryStem(WordLoaderSettings wordLoaderSettings)
{
    private static IStemReader CreateStem(WordLoaderSettings cloudSettings)
    {
        return new StemReader(cloudSettings);
    }

    public virtual Result<IStemReader> Create() // virtual для тестов
    {
        return ValidateMyStem(wordLoaderSettings)
            .Then(ValidTextFile)
            .Then(CreateStem);
    }

    private Result<WordLoaderSettings> ValidTextFile(WordLoaderSettings settings)
    {
        return settings.Validate(x => File.Exists(x.PathTextFile), x => $"this not file with text  {x.PathTextFile}");
    }

    private Result<WordLoaderSettings> ValidateMyStem(WordLoaderSettings settings)
        => settings.Validate(x => StemExists(), x => Errors.Stem.NotFoundInEnvVar());


    private static bool StemExists()
    {
        return Environment.GetEnvironmentVariable("Path")!
            .Split(";")
            .Any(x => File.Exists(Path.Combine(x, "mystem.exe")));
    }
}