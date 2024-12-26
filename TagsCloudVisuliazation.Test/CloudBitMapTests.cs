using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisuliazation.Test;

public class CloudBitMapTests
{
    [Test]
    public void CloudBitMap_DirectoryShouldBeExist()
    {
        var filePath = "./../../../OuterWild/";
        var settings = new TagCloudSettings()
        {
            Size = new Size(5, 5),
            PathDirectory = filePath,
            NamePhoto = "notIntersect-50.png"
        };

        var fac = new FactoryCloudBitMap(settings);

        fac.Create().Error.Should().Be($"This Directory Not Exists  {settings.PathDirectory}");
    }
 
    [Test]
    public void CloudBitMap_ShouldBe_SizeWithPositiveNumbers()
    {
        var filePath = "./../../../photos/notIntersect-50.png";
        var settings = new TagCloudSettings()
        {
            Size = new Size(-5, 5),
            PathDirectory = filePath,
            NamePhoto = "notIntersect-50.png"
        };
        Action action = () => new CloudBitMap(settings);
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void CloudBitMap_ShouldBe_CreatePhotoPng()
    {
        var filePath = "./../../../photos/";
        var settings = new TagCloudSettings()
        {
            Size = new Size(30, 300),
            PathDirectory = filePath,
            NamePhoto = "notIntersect",
            ImageFormat = ImageFormat.Png
        };
        var cloudBitMap = new CloudBitMap(settings);
        
        cloudBitMap.Save();
        
        File.Exists(filePath + "tagCloud-(notIntersect).png").Should().BeTrue();
        File.Delete(filePath + "tagCloud-(notIntersect).png");
    }
    [Test]
    public void CloudBitMap_ShouldBe_CreatePhotoBmp()
    {
        var filePath = "./../../../photos/";
        var settings = new TagCloudSettings()
        {
            Size = new Size(30, 300),
            PathDirectory = filePath,
            NamePhoto = "notIntersect",
            ImageFormat = ImageFormat.Bmp
        };
        var cloudBitMap = new CloudBitMap(settings);
        
        cloudBitMap.Save();
        
        File.Exists(filePath + "tagCloud-(notIntersect).Bmp").Should().BeTrue();
        File.Delete(filePath + "tagCloud-(notIntersect).Bmp");
    }
}