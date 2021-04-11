using System.Windows;
using System.Windows.Controls.Primitives;

namespace MyCustomControls.CustomControls
{
	public class LEDToggleButton : ToggleButton
	{
		private bool? bPreviousState;

		#region DEPENDENCY PROPERTIES
		public ushort NodeId
		{
			get { return (ushort)GetValue(NodeIdProperty); }
			set { SetValue(NodeIdProperty, value); }
		}
		public static readonly DependencyProperty NodeIdProperty =
				DependencyProperty.Register(nameof(NodeId), typeof(ushort), typeof(LEDToggleButton), new PropertyMetadata((ushort)0));

		public ushort BitOffset
		{
			get { return (ushort)GetValue(BitOffsetProperty); }
			set { SetValue(BitOffsetProperty, value); }
		}
		public static readonly DependencyProperty BitOffsetProperty =
				DependencyProperty.Register(nameof(BitOffset), typeof(ushort), typeof(LEDToggleButton), new PropertyMetadata((ushort)0));
		#endregion DEPENDENCY PROPERTIES

		public LEDToggleTypeId LEDToggleType
		{
			get { return (LEDToggleTypeId)GetValue(LEDToggleTypeProperty); }
			set { SetValue(LEDToggleTypeProperty, value); }
		}
		public static readonly DependencyProperty LEDToggleTypeProperty =
				DependencyProperty.Register("LEDToggleType", typeof(LEDToggleTypeId), typeof(LEDToggleButton), new PropertyMetadata(LEDToggleTypeId.SLIDER_HORIZONTAL));

		/// <summary>
		/// Static constructor
		/// TODO: Should IsEnabled be tied to IsThreeState?
		/// </summary>
		static LEDToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LEDToggleButton), new FrameworkPropertyMetadata(typeof(LEDToggleButton)));
			IsThreeStateProperty.OverrideMetadata(typeof(LEDToggleButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(IsThreeStatePropertyChanged)));
		}

		// I wouldn't actually use this in the final product, it's just here to test the visuals. However, IsThreeState _should_ be bound to the state of the profibus.
		static void IsThreeStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			if (sender is LEDToggleButton ledToggleButton)
			{
				if (true == ledToggleButton.IsThreeState)
				{
					// Disable clicking outputs if we're not connected to profibus
					ledToggleButton.IsEnabled = false;

					// Set the IsChecked state to Indeterminate because we have no idea what the actual state is
					ledToggleButton.IsChecked = null;
				}
			}
		}
	}
}
