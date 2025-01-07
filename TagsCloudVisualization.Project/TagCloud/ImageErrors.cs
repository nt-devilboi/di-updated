using System.Drawing;

namespace TagsCloudVisualization;

public class ImageErrors
{
    public string SizeLessThanZero(Size size)
        => $"size of image should be with positive number, now {size}";

    public string FontNotExists(string font)
        => $"Font: '{font}' not exists";
}