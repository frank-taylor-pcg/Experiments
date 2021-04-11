using System.ComponentModel;

namespace MyCustomControls.CustomControls
{
	public enum LEDToggleTypeId
	{
		/// <summary>Display the LEDs stacked like a traffic light with Unchecked being on top, Checked on bottom and Indeterminate being both LEDs off</summary>
		[Description(@"Display the LEDs stacked like a traffic light with Unchecked being on top, Checked on bottom and Indeterminate being both LEDs off")]
		TRAFFIC_LIGHT,

		/// <summary>Display the LEDs as a horizontal slider where the order is UnChecked, Indeterminate, Checked from left to right</summary>
		[Description(@"Display the LEDs as a horizontal slider where the order is UnChecked, Indeterminate, Checked from left to right")]
		SLIDER_HORIZONTAL,

		/// <summary>Display the LEDs as a vertical slider where the order is UnChecked, Indeterminate, Checked from top to bottom</summary>
		[Description(@"Display the LEDs as a vertical slider where the order is UnChecked, Indeterminate, Checked from top to bottom")]
		SLIDER_VERTICAL,
	}
}
