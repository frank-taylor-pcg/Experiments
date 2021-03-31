using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Chapter7_472
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private BufferedCanvas bufferedCanvas;

		DispatcherTimer timer = new DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();

			// Trying to improve performance, no luck
			VisualBitmapScalingMode = BitmapScalingMode.LowQuality;

			timer.Tick += Timer_Tick;
			timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000.0 / 60.0));
			timer.Start();
		}

		private void wndMain_Loaded(object sender, RoutedEventArgs e)
		{
			// Add the buffered canvas.  This is where all of the work is done
			bufferedCanvas = new BufferedCanvas((int)canvas.ActualWidth, (int)canvas.ActualHeight);

			// Add the buffered canvas to our backdrop
			canvas.Children.Add(bufferedCanvas);
		}

		// WPF operates in retained mode so it decides when to redraw the screen
		private void Timer_Tick(object sender, EventArgs e)
		{
			bufferedCanvas.Update();
		}
	}
}
