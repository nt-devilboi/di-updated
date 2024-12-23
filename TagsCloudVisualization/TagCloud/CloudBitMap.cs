using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class CloudBitMap : ITagCloudImage
{
    private readonly Bitmap _bitmap;
    private readonly string _filePath;
    private readonly Graphics _graphics;
    private readonly string _nameFile;
    private readonly Pen _pen = new(Color.Red);
    private bool _isDisposed;
    private bool _isSave;

    public CloudBitMap(TagCloudSettings tagCloudSettings)
    {
        _nameFile = tagCloudSettings.NamePhoto;
        _bitmap = new Bitmap(tagCloudSettings.Size.Width, tagCloudSettings.Size.Height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(Color.Black);
        _filePath = tagCloudSettings.PathDirectory;
    }

    public Size Size()
    {
        return _bitmap.Size;
    }

    public void Draw(Rectangle rec)
    {
        _graphics.DrawRectangle(_pen, rec);
    }

    public void Draw(RectangleTagCloud rec)
    {
        _graphics.DrawString(rec.text, rec.font, Brushes.Blue, rec.Rectangle);

        _graphics.DrawRectangle(_pen, rec.Rectangle);
    }

    public Size GetSizeWord(string word)
    {
        return _graphics.MeasureString(word, new Font("Times New Roman", 24, FontStyle.Bold)).ToSize();
    }

    public void Save()
    {
        if (_isSave)
        {
            Console.WriteLine("уже сохранена");
            return;
        }

        var saveFilePath = string.Join("", _filePath, $"tagCloud-({_nameFile}).png");
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