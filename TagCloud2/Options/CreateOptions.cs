using CommandLine;

namespace TagCloud2.Options;

[Verb("SetConfigTextFile", HelpText = "Settings")]
public class CreateOptions
{
    [Option('w', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public string path { get; set; }
}