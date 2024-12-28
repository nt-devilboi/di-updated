using System.Drawing;

namespace TagsCloudVisualization;

public class ImageErrors
{
    public string SizeLessThanZero(Size size)
        => $"size of image should be with positive number, now {size}";
}