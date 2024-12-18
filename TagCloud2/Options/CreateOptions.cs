using CommandLine;

namespace TagCloud2.Options;

[Verb("Create", HelpText = "createPhoto")]
public class CreateOptions
{
    [Option('p', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public string path { get; set; }
}