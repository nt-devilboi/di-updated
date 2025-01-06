namespace TagsCloudVisualization;

public static class Errors
{
    
    public static ImageErrors Image => new();
    public static StemErrors Stem => new();
}

public class StemErrors
{
    public string NotFoundInEnvVar()
        => $"Stem not set in variable 'Path'";
}