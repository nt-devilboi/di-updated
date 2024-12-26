using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagCloud2.Options;

[Verb("create")]
public class CreateTagCloud
{
    [Option('w', "PathWordsFile", Required = true, HelpText = "path of file with words")]
    public required string PathToWords { get; set; }

    [Option('s', "size", Required = true, HelpText = "size of image formate WxH", Separator = 'x')]
    public required IEnumerable<string> Size { get; set; }

    [Option('d', "pathDirectory", Required = true, HelpText = "path of directory for photos")]
    public required string Directory { get; set; }

    [Option('n', "NameFile", Required = true, HelpText = "Name of photos")]
    public required string NamePhoto { get; set; }

    [Option('a', "StemPath", Required = true, HelpText = "stem dir")]
    public required string StemPath { get; set; }

    [Option('e', "emSize", Required = true, HelpText = "Max EmSize of word")]
    public required string EmSize { get; set; }

    [Option('c', "Color", Required = false, HelpText = "Color of words")]
    public Color Color { get; set; } = Color.Black;

    [Option('b', "background", Required = false, HelpText = "Color of words")]
    public Color BackgrondColor { get; set; } = Color.White;

    [Option('f', "format", Required = false, HelpText = "Format of Photo")]
    public string ImageFormatString { get; set; } = "png";

    [Option('t', "typeFace", Required = false, HelpText = "font of words")]
    public string Font { get; set; } = "arial";

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
}