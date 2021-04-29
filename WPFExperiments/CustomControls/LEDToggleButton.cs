using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFExperiments.CustomControls
{
	/// <summary>
	/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
	///
	/// Step 1a) Using this custom control in a XAML file that exists in the current project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:WPFExperiments.CustomControls"
	///
	///
	/// Step 1b) Using this custom control in a XAML file that exists in a different project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:WPFExperiments.CustomControls;assembly=WPFExperiments.CustomControls"
	///
	/// You will also need to add a project reference from the project where the XAML file lives
	/// to this project and Rebuild to avoid compilation errors:
	///
	///     Right click on the target project in the Solution Explorer and
	///     "Add Reference"->"Projects"->[Browse to and select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:LEDToggleButton/>
	///
	/// </summary>
	public class LEDToggleButton : ToggleButton
	{
		#region DEPENDENCY PROPERTIES
		public ushort NodeId
		{
			get { return (ushort)GetValue(NodeIdProperty); }
			set { SetValue(NodeIdProperty, value); }
		}
		public static readonly DependencyProperty NodeIdProperty =
				DependencyProperty.Register(nameof(NodeId), typeof(ushort), typeof(LEDToggleButton), new PropertyMetadata((ushort)0));

		public string NodeName
		{
			get { return (string)GetValue(NodeNameProperty); }
			set { SetValue(NodeNameProperty, value); }
		}
		public static readonly DependencyProperty NodeNameProperty =
				DependencyProperty.Register("NodeName", typeof(string), typeof(LEDToggleButton), new PropertyMetadata(string.Empty));

		public ushort BitOffset
		{
			get { return (ushort)GetValue(BitOffsetProperty); }
			set { SetValue(BitOffsetProperty, value); }
		}
		public static readonly DependencyProperty BitOffsetProperty =
				DependencyProperty.Register(nameof(BitOffset), typeof(ushort), typeof(LEDToggleButton), new PropertyMetadata((ushort)0));
		#endregion DEPENDENCY PROPERTIES

		/// <summary>
		/// Static constructor
		/// TODO: Should IsEnabled be tied to IsThreeState?
		/// </summary>
		static LEDToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LEDToggleButton), new FrameworkPropertyMetadata(typeof(LEDToggleButton)));
		}
	}
}
