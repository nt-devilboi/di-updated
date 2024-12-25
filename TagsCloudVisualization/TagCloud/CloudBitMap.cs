using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class CloudBitMap : ITagCloudImage, ISizeWord
{
    private readonly Bitmap _bitmap;
    private readonly Graphics _graphics;
    private readonly Pen _pen = new(Color.Red);
    private bool _isDisposed;
    private bool _isSave;
    private TagCloudSettings _tagCloudSettings;

    private static Font DefaultFont(int emSize) => new Font("arial", emSize);

    public CloudBitMap(TagCloudSettings tagCloudSettings)
    {
        _tagCloudSettings = tagCloudSettings;
        _bitmap = new Bitmap(tagCloudSettings.Size.Width, tagCloudSettings.Size.Height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(Color.Black);
    }

    public Size Size()
    {
        return _bitmap.Size;
    }

    public void Draw(Rectangle rec)
    {
        _graphics.DrawRectangle(_pen, rec);
    }

    public void DrawString(RectangleTagCloud rec)
    {
        _graphics.DrawString(rec.Text, DefaultFont(rec.EmSize), Brushes.Blue, rec.Rectangle);
    }

    public Size GetSizeWord(string word, int emSize)
    {
        return _graphics.MeasureString(word, DefaultFont(emSize)).ToSize();
    }

    public void Save()
    {
        if (_isSave)
        {
            Console.WriteLine("уже сохранена");
            return;
        }

        var saveFilePath = string.Join("", _tagCloudSettings.PathDirectory,
            $"tagCloud-({_tagCloudSettings.NamePhoto}).png");
        _bitmap.Save(saveFilePath, ImageFormat.Png);
        Console.WriteLine($"file saved in {saveFilePath}");
        _isSave = true;
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool fromMethod)
    {
        if (!_isDisposed)
        {
            if (fromMethod) Save();

            _bitmap.Dispose();
            _graphics.Dispose();
            _pen.Dispose();

            _isDisposed = true;
        }
    }
}