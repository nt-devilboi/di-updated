using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public class FactoryStem(WordLoaderSettings wordLoaderSettings)
{
    private IStemReader CreateStem(WordLoaderSettings cloudSettings)
    {
        return new StemReader(cloudSettings);
    }

    public virtual Result<IStemReader> Create()
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
    {
        return settings.Validate(x => File.Exists(x.PathStem), x => $"this not stem {x}");
    }
}