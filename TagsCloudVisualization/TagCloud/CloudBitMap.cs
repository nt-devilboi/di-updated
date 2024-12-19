using System.Drawing;
using System.Drawing.Imaging;
using TagCloud2;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class CloudBitMap : ITagCloudImage
{
    private readonly Bitmap _bitmap;
    private readonly string _filePath;
    private readonly Graphics _graphics;
    private readonly Pen _pen = new(Color.Red);
    private bool _isDisposed;
    private bool _isSave;
    private readonly string _nameFile;

    public CloudBitMap(ITagCloudSettings tagCloudSettings)
    {
        
        _nameFile = tagCloudSettings.NameFile;
        _bitmap = new Bitmap(tagCloudSettings.Size.Width, tagCloudSettings.Size.Height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(Color.Black);
        _filePath = tagCloudSettings.PathDirectory;
    }

    public Size Size() => _bitmap.Size;

    public void Draw(Rectangle rec)
    {
        _graphics.DrawRectangle(_pen, rec);
    }

    public void Draw(RectangleTagCloud rec)
    {
        _graphics.DrawString(rec.text, rec.font, Brushes.Blue, rec.Rectangle);

        _graphics.DrawRectangle(_pen, rec.Rectangle);
    }

    public SizeF GetSizeWord(WordPopular word)
    {
        return _graphics.MeasureString(word.Word, new Font("Aria", 24, FontStyle.Bold));
    }

    public void Save()
    {
        if (_isSave)
        {
            Console.WriteLine("уже сохранена");
            return;
        }

        var saveFilePath = string.Join("", _filePath, $"/tagCloud-({_nameFile}).png");
        _bitmap.Save(saveFilePath, ImageFormat.Png);
        Console.WriteLine($"file saved in {saveFilePath}");
        _isSave = true;
    }


    private static void Validate(int width, int height) // todo: проверка на то, что файла не существуте.
    {
        if (width <= 0 || height <= 0) throw new ArgumentException("size of image should be with positive number");
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool fromMethod)
    {
        if (!_isDisposed)
        {
            if (fromMethod)
            {
                Save();
            }

            _bitmap.Dispose();
            _graphics.Dispose();
            _pen.Dispose();

            _isDisposed = true;
        }
    }
}