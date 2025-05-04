using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace DesignDocument.BLL;

public class XamlToImage
{
	public static Stream XamlToImageSteam(XmlTextReader xmlReader, BitmapEncoder encoder, Stream outputStream, double dpiX, double dpiY)
	{
		Window window = new Window();
		window.WindowStyle = WindowStyle.None;
		window.ShowInTaskbar = false;
		window.ShowActivated = false;
		window.WindowState = WindowState.Minimized;
		window.SizeToContent = SizeToContent.WidthAndHeight;
		window.Content = (UIElement)XamlReader.Load(xmlReader);
		window.Show();
		BitmapSource source = visualToBitmap(window, dpiX, dpiY);
		encoder.Frames.Clear();
		encoder.Frames.Add(BitmapFrame.Create(source));
		encoder.Save(outputStream);
		outputStream.Flush();
		window.Hide();
		return outputStream;
	}

	private static BitmapSource visualToBitmap(Visual target, double dpiX, double dpiY)
	{
		if (target == null)
		{
			return null;
		}
		Rect descendantBounds = VisualTreeHelper.GetDescendantBounds(target);
		int pixelWidth = (int)(descendantBounds.Width * dpiX / 96.0);
		int pixelHeight = (int)(descendantBounds.Height * dpiX / 96.0);
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(pixelWidth, pixelHeight, dpiX, dpiY, PixelFormats.Pbgra32);
		renderTargetBitmap.Render(target);
		return renderTargetBitmap;
	}
}
