using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class CloudBitMap : ITagCloudImage, ISizeWord
{
    private readonly Bitmap _bitmap;
    private readonly Graphics _graphics;
    private bool _isDisposed;
    private bool _isSave;
    private readonly TagCloudSettings _tagCloudSettings;

    private  Font GetFont(int emSize) => new(_tagCloudSettings.Font, emSize);

    public CloudBitMap(TagCloudSettings tagCloudSettings)
    {
        _tagCloudSettings = tagCloudSettings;
        _bitmap = new Bitmap(tagCloudSettings.Size.Width, tagCloudSettings.Size.Height);
        _graphics = Graphics.FromImage(_bitmap);
        _graphics.Clear(_tagCloudSettings.BackGround);
    }

    public Size Size()
    {
        return _bitmap.Size;
    }
    
    public void DrawString(RectangleTagCloud rec)
    {
        var brush = new SolidBrush(_tagCloudSettings.ColorWords);
        _graphics.DrawString(rec.Text, GetFont(rec.EmSize), brush, rec.Rectangle);
    }

    public Size GetSizeWord(string word, int emSize)
    {
        return _graphics.MeasureString(word, GetFont(emSize)).ToSize();
    }

    public void Save()
    {
        if (_isSave)
        {
            Console.WriteLine("уже сохранена");
            return;
        }

        var saveFilePath = string.Join("", _tagCloudSettings.PathDirectory,
            $"tagCloud-({_tagCloudSettings.NamePhoto}).{_tagCloudSettings.ImageFormat}");
        
        _bitmap.Save(saveFilePath, _tagCloudSettings.ImageFormat);
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

            _isDisposed = true;
        }
    }
}