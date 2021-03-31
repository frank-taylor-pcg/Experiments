using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Imaging;

namespace WPFExperiments.Threads
{
	public class ImageData
	{
		public List<BitmapSource> Images = new();
		public ManualResetEvent ImagesTaken = new(false);
	}
}
