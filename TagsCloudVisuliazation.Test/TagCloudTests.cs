using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;
namespace TagsCloudVisuliazation.Test;

public class TagCloudTests
{
    private const string NameFile = "WithErrorFrom";
    private const string PathDir = "./../../../photos/";
    private TagCloudSettings _settings;

    [SetUp]
    public void Setup()
    {
        _settings = new TagCloudSettings()
        {
            Size = new Size(1000, 1000),
            PathDirectory = PathDir,
            NamePhoto = NameFile
        };
  }

    [Test]
    public void CreateTagCloud_WithoutIntersect()
    {
        var circularCloudLayouter = new CircularCloudLayouter(_settings);
        var rectangles = new List<Rectangle>();
        for (int i = 0; i < 100; i++)
        {
            rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(50, 50)));
        }

        CheckIntercets(rectangles);
    }

    [Test]
    public void TagCloud_StartPosition_ShouldBe_In_Image()
    {
        var size = new Size(-3, -2);
        _settings = new TagCloudSettings()
        {
            Size = new Size(-3, -2),
            PathDirectory = PathDir,
            NamePhoto = NameFile
        };
        var abstractFac = new FactoryBitMap(_settings);
        var result = abstractFac.Create();
        result.Error.Should().Be($"size of image should be with positive number, now: {size}");
    }

    [TearDown]
    public void CheckResult()
    {
        if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure &&
            TestContext.CurrentContext.Test.MethodName!.StartsWith("Create"))
        {
            TestContext.WriteLine($"Tag cloud visualization saved to file {NameFile}/" +
                                  $" {TestContext.CurrentContext.Test.MethodName}");
        }
    }

    private static void CheckIntercets(List<Rectangle> rectangles)
    {
        for (int i = 0; i < rectangles.Count; i++)
        for (int j = i + 1; j < rectangles.Count; j++)
        {
            rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
        }
    }
}