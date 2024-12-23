using System.Drawing;
using CommandLine;

namespace TagCloud2.Options;

[Verb("create")]
public class CreateTagCloud
{
    [Option('w', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public required string PathToWords { get; set; }

    [Option('s', "size", Required = true, HelpText = "size of image formate WxH")]
    public required string Size { get; set; }

    [Option('d', "pathDirectory", Required = true, HelpText = "path of directory for photos")]
    public required string Directory { get; set; }

    [Option('n', "NameFile", Required = true, HelpText = "Name Of Photos")]
    public required string NamePhoto { get; set; }

    [Option('a', "StemPath", Required = true, HelpText = "Stem Dir")]
    public required string StemPath { get; set; }
    public Size GetSize()
    {
        var sizeString = Size.Split('x');
        // todo: добавить проверки 
        return new Size(int.Parse(sizeString[0]), int.Parse(sizeString[1]));
    }
}