using System;
using System.Windows.Media;

namespace Chapter7
{
	public class Globals
	{
		public const int DONUT_SIZE = 16;
		public const int NUM_DONUTS = 30_000;

		// Global random number generator
		public static Random Rnd { get; set; } = new();

		// Simulation boundaries
		public static int XMin { get; set; }
		public static int XMax { get; set; }
		public static int YMin { get; set; }
		public static int YMax { get; set; }

		// The brush used to draw our donuts
		public static RadialGradientBrush DonutColor;

		public static void SetupGlobals(int width, int height)
		{
			// Set the boundaries for our donut simulation
			XMin = DONUT_SIZE;
			XMax = width - DONUT_SIZE;

			YMin = DONUT_SIZE;
			YMax = height - DONUT_SIZE;

			// Setup our "donut" gradient because I'm hungry
			GradientStopCollection gradientStops = new()
			{
				new GradientStop(Color.FromArgb(0, 0, 0, 0), 0),
				new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.2),
				new GradientStop(Color.FromRgb(128, 64, 0), 0.2),
				new GradientStop(Color.FromRgb(255, 192, 128), 0.5),
				new GradientStop(Color.FromRgb(255, 192, 128), 0.6),
				new GradientStop(Color.FromRgb(128, 64, 0), 1),
			};
			DonutColor = new RadialGradientBrush(gradientStops);

			// Freezing assets that support it and that won't be changing can improve performance
			DonutColor.Freeze();
		}
	}
}
