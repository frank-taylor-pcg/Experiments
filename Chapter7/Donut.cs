using System.Windows;
using System.Windows.Media;

namespace Chapter7
{
	public class Donut
	{
		public EllipseGeometry Geometry { get; set; }

		private Point m_position;

		private int m_deltaX;
		private int m_deltaY;

		public Donut()
		{
			// Calculate the starting velocities
			m_deltaX = Globals.Rnd.Next(-2, 2);
			m_deltaY = Globals.Rnd.Next(-2, 2);

			// Calculate the starting position
			m_position = new Point(Globals.Rnd.Next(Globals.XMin, Globals.XMax), Globals.Rnd.Next(Globals.YMin, Globals.YMax));

			// Create the visual representation of the donut
			Geometry = new(m_position, Globals.DONUT_SIZE, Globals.DONUT_SIZE);
		}

		public void Update()
		{
			// Update the position based on the current velocities
			m_position.X += m_deltaX;
			m_position.Y += m_deltaY;

			// If the update crossed one of the X boundaries, reverse the X direction and re-apply the X update
			if (m_position.X < Globals.XMin || m_position.X > Globals.XMax)
			{
				m_deltaX *= -1;
				m_position.X += m_deltaX;
			}

			// If the update crossed one of the Y boundaries, reverse the Y direction and re-apply the Y update
			if (m_position.Y < Globals.YMin || m_position.Y > Globals.YMax)
			{
				m_deltaY *= -1;
				m_position.Y += m_deltaY;
			}

			// Update the position of the visual representation
			Geometry.Center = m_position;
		}
	}
}
