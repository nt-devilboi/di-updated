using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public sealed class FactoryStem(WordLoaderSettings wordLoaderSettings)
{
    private static IStemReader CreateStem(WordLoaderSettings cloudSettings)
    {
        return new StemReader(cloudSettings);
    }

    public Result<IStemReader> Create()
    {
        return ValidatePathTextFile(wordLoaderSettings)
            .Then(ValidPathStem)
            .Then(CreateStem);
    }

    private Result<WordLoaderSettings> ValidPathStem(WordLoaderSettings settings)
    {
        return settings.Validate(x => File.Exists(x.PathStem), x => $"this not file with text  {x}");
    }

    private Result<WordLoaderSettings> ValidatePathTextFile(WordLoaderSettings settings)
        => settings.Validate(x => StemExists(), x => Errors.Stem.NotFoundInEnvVar());


    private static bool StemExists()
    {
        return Environment.GetEnvironmentVariable("Path")!
            .Split(";")
            .Any(x => File.Exists(Path.Combine(x, "mystem.exe")));
    }
}