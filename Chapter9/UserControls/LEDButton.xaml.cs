using System.Windows;
using System.Windows.Controls;

namespace Chapter9.UserControls
{
	/// <summary>
	/// Interaction logic for LEDButton.xaml
	/// </summary>
	public partial class LEDButton : UserControl
	{
		public short NodeId
		{
			get { return (short)GetValue(NodeIdProperty); }
			set { SetValue(NodeIdProperty, value); }
		}
		public static readonly DependencyProperty NodeIdProperty =
				DependencyProperty.Register("NodeId", typeof(short), typeof(LEDButton), new PropertyMetadata((short)0));

		public short BitOffset
		{
			get { return (short)GetValue(BitOffsetProperty); }
			set { SetValue(BitOffsetProperty, value); }
		}
		public static readonly DependencyProperty BitOffsetProperty =
				DependencyProperty.Register("BitOffset", typeof(short), typeof(LEDButton), new PropertyMetadata((short)0));

		public LEDButton()
		{
			InitializeComponent();
		}
	}
}
