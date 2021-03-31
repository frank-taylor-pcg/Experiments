using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFExperiments.Threads
{
	public class ImageCreator
	{
		private readonly List<Color> colors;
		private readonly BitmapPalette palette;
		private readonly byte[] pixels;
		private const int height = 128;
		private const int width = 128;
		private byte bCurrentColor = 0;
		private readonly Timer internalTimer;
		private AutoResetEvent internalTimerARE = new(false);
		private readonly ConcurrentQueue<int> messageQueue = new();

		public ManualResetEvent ImageReady = new(false);
		public BitmapSource Image { get; private set; }

		public ImageCreator()
		{
			colors = new List<Color>() { Colors.Red, Colors.Blue, Colors.Green };
			palette = new BitmapPalette(colors);
			pixels = new byte[width * height];

			internalTimer = new Timer(DoThread, null, 250, 250);
		}

		~ImageCreator()
		{
			internalTimer.Dispose();
		}

		public BitmapSource GetImage()
		{
			return BitmapSource.Create(width, height, 96, 96, PixelFormats.Indexed8, palette, pixels, width);
		}

		public void RequestSnap()
		{
			// Putting a value in the message queue to emulate a distinct "snap" message
			messageQueue.Enqueue(1);
		}

		private void DoThread(object obj)
		{
			// Every 250ms tries to Dequeue a message.  If it succeeds it sends a call to snap
			if (messageQueue.TryDequeue(out _))
			{
				Snap();
			}
		}

		private void Snap()
		{
			UpdateImageData();
			ImageReady.Set();
			UpdateColor();
		}

		private void UpdateColor()
		{
			bCurrentColor += 1;
			if (bCurrentColor >= colors.Count)
			{
				bCurrentColor = 0;
			}
		}

		private void UpdateImageData()
		{
			for (int i = 0; i < (height * width); i++)
			{
				pixels[i] = bCurrentColor;
			}
		}

	}
}
