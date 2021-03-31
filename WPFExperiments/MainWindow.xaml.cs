using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using WPFExperiments.Threads;

namespace WPFExperiments
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		const int NUM_ITERATIONS = 10;
		const int IMAGE_SIZE = 4096;
		public MainWindow()
		{
			InitializeComponent();
			ImageData data = new();

			//Thread takeThread = new(() => { TakeImages(data); });
			//Thread save1Thread = new(() => { SaveImages(data, "1"); });
			//Thread save2Thread = new(() => { SaveImages(data, "2"); });
			//takeThread.Start();
			//save1Thread.Start();
			//save2Thread.Start();

			TestEncodingTime();
		}

		// 11 seconds to encode and save 10 4096x4096 images.  That's 1.1 seconds per image.
		// 8 seconds to encode 10 million 4096x4096 images.  That's 0.0008ms each, so Encoding isn't the bottleneck
		private void TestEncodingTime()
		{
			//ImageCreator imageCreator = new ImageCreator();
			//imageCreator.RequestSnap();
			//imageCreator.ImageReady.WaitOne();
			//BitmapSource bitmap = imageCreator.GetImage();
			BitmapSource bitmap = CreateRandomImage();

			DateTime start = DateTime.Now;
			for (int i = 0; i < NUM_ITERATIONS; i++)
			{
				PngBitmapEncoder encoder = new();
				encoder.Frames.Add(BitmapFrame.Create(bitmap));
				Save(encoder, i);
			}
			DateTime end = DateTime.Now;
			Debug.WriteLine($"Time to encode {NUM_ITERATIONS} images: {(end - start).TotalMilliseconds}ms");

		}

		private void Save(PngBitmapEncoder encoder, int index)
		{
			using FileStream fs = new(Path.Combine(@"C:\dev\temp", $"random_{index}.png"), FileMode.Create);
			encoder.Save(fs);
			fs.Flush();
			fs.Close();
		}

		private BitmapSource CreateRandomImage()
		{
			List<Color> colors = new List<Color>() { Colors.Red, Colors.Blue, Colors.Green };
			BitmapPalette palette = new BitmapPalette(colors);
			Random rnd = new Random();

			uint[] pixels = new uint[IMAGE_SIZE * IMAGE_SIZE];
			for (int y = 0; y < IMAGE_SIZE; y++)
			{
				for (int x = 0; x < IMAGE_SIZE; x++)
				{
					pixels[(y * IMAGE_SIZE) + x] = (uint)rnd.Next(int.MaxValue);
				}
			}
			return BitmapSource.Create(IMAGE_SIZE, IMAGE_SIZE, 96, 96, PixelFormats.Bgr32, palette, pixels, IMAGE_SIZE * 4);
		}

		private void TakeImages(ImageData data)
		{
			ImageCreator imgCreator = new();
			for (int i = 0; i < 3; i++)
			{
				imgCreator.RequestSnap();
				imgCreator.ImageReady.WaitOne();
				BitmapSource bitmap = imgCreator.GetImage();
				bitmap.Freeze();
				data.Images.Add(bitmap);
			}
			data.ImagesTaken.Set();
		}

		private void SaveImages(ImageData data, string strPrefix)
		{
			int index = 0;
			Debug.WriteLine($"Waiting for image encoding to finish");
			data.ImagesTaken.WaitOne();
			foreach (BitmapSource bitmap in data.Images)
			{
				PngBitmapEncoder encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmap));
				using FileStream fs = new FileStream(Path.Combine(@"C:\dev\temp", $"{strPrefix}_{index++}.png"), FileMode.Create);
				encoder.Save(fs);
				fs.Flush();
				fs.Close();
			}
		}
	}
}
