using System.Drawing;
using CommandLine;

namespace TagCloud2.Options;


[Verb("settings")]
public class SettingOptions
{
    [Option('s', "size", Required = true, HelpText = "size of image formate WxH")]
    public string Size { get; set; }

    [Option('d', "pathDirectory", Required = true, HelpText = "path of directory for photos")]
    public string Path { get; set; }

    [Option('n', "NameFile", Required = true, HelpText = "Name Of Photos")]
    public string Name { get; set; }


    public Size GetSize()
    {
        var sizeString = Size.Split('x');
        // todo: добавить проверки 
        return new Size(int.Parse(sizeString[0]), int.Parse(sizeString[1]));
    }
}