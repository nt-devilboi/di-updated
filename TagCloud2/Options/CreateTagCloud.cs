using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagCloud2.Options;

[Verb("create")]
public class CreateTagCloud
{
    [Option('w', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public required string PathToWords { get; init; }

    [Option('s', "size", Required = true, HelpText = "size of image formate WxH", Separator = 'x')]
    public required IEnumerable<string> Size { private get; init; }

    [Option('d', "pathDirectory", Required = true, HelpText = "path of directory for photos", Group = "path")]
    public required IEnumerable<string> Directory { private get; init; }

    [Option('n', "NameFile", Required = true, HelpText = "Name of photos")]
    public required string NamePhoto { get; init; }
    

    [Option('e', "emSize", Required = true, HelpText = "Max EmSize of word")]
    public required string EmSize { get; init; }

    [Option('c', "Color", Required = false, HelpText = "Color of words")]
    public Color Color { get; init; } = Color.Black;

    [Option('b', "background", Required = false, HelpText = "Color of words")]
    public Color BackgrondColor { get; init; } = Color.White;

    [Option('f', "format", Required = false, HelpText = "Format of Photo")]
    public string ImageFormatString { private get; init; } = "png";

    [Option('t', "typeFace", Required = false, HelpText = "font of words")]
    public string Font { get; init; } = "arial";

    public ImageFormat GetImageFormat()
    {
        return ImageFormatString.ToLower() switch
        {
            "png" => ImageFormat.Png,
            "jpg" => ImageFormat.Jpeg,
            "bpm" => ImageFormat.Bmp,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public Size GetSize()
    {
        var size = Size.ToArray();
        return new Size(int.Parse(size[0]), int.Parse(size[1]));
    }

    public string GetDirectory()
    {
        return string.Join(' ', Directory);
    }
}