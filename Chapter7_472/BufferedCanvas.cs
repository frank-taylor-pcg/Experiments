using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Chapter7_472
{
	public class BufferedCanvas : FrameworkElement
	{
		private readonly List<Donut> donuts = new List<Donut>();

		DrawingGroup drawingGroup;

		public BufferedCanvas(int width, int height)
		{
			Globals.SetupGlobals(width, height);
			Initialize();
		}

		private void Initialize()
		{
			// Initialize the data (and visual representation) for the list of donuts
			for (int i = 0; i < Globals.NUM_DONUTS; i++)
			{
				donuts.Add(new Donut());
			}

			Render();
		}

		// When the OnRender event occurs, draw whatever is in the drawingGroup.  This should hopefully smooth out the animations
		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);
			drawingContext.DrawDrawing(drawingGroup);
		}

		// Render the current state of the donuts to the drawingGroup.
		public void Render()
		{
			drawingGroup = new DrawingGroup();

			foreach (Donut donut in donuts)
			{
				donut.Update();

				GeometryDrawing gd = new GeometryDrawing(Globals.DonutColor, null, donut.Geometry);
				drawingGroup.Children.Add(gd);
			}
		}

		// Once the donuts are all drawn to the group, all we have to do is update their positions and they'll automatically redraw.
		// This is probably where the slowdown is occurring.  Rather than batching the calls WPF is probably updating the screen for
		// each one that changes.  I don't know what's going on in the background yet, so can't optimize this.  In immediate mode
		// using DirectX I can draw 100s of thousands of bouncing donuts at 100+ frames per second.
		public void Update()
		{
			foreach (Donut donut in donuts)
			{
				donut.Update();
			}
		}
	}
}
