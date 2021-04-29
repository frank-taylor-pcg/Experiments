using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyCustomControls.CustomControls
{
	/// <summary>
	/// A textbox that displays placeholder text when empty.
	/// <MyNamespace:PlaceholderTextbox/>
	/// </summary>
	public class PlaceholderTextbox : TextBox
	{
		/// <summary>Text to show before the user enters text into this field. Default: Enter value</summary>
		public string PlaceholderText
		{
			get { return (string)GetValue(PlaceholderTextProperty); }
			set { SetValue(PlaceholderTextProperty, value); }
		}
		public static readonly DependencyProperty PlaceholderTextProperty =
				DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextbox), new PropertyMetadata("Enter value"));

		/// <summary>The color to use when displaying placeholder text. Default: Gray</summary>
		public SolidColorBrush PlaceholderForeground
		{
			get { return (SolidColorBrush)GetValue(PlaceholderForegroundProperty); }
			set { SetValue(PlaceholderForegroundProperty, value); }
		}
		public static readonly DependencyProperty PlaceholderForegroundProperty =
				DependencyProperty.Register("PlaceholderForeground", typeof(SolidColorBrush), typeof(PlaceholderTextbox), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

		/// <summary>The font style to use when displaying placeholder text.  Default: Italic</summary>
		public FontStyle PlaceholderFontStyle
		{
			get { return (FontStyle)GetValue(PlaceholderFontStyleProperty); }
			set { SetValue(PlaceholderFontStyleProperty, value); }
		}
		public static readonly DependencyProperty PlaceholderFontStyleProperty =
				DependencyProperty.Register("PlaceholderFontStyle", typeof(FontStyle), typeof(PlaceholderTextbox), new PropertyMetadata(FontStyles.Italic));

		static PlaceholderTextbox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextbox), new FrameworkPropertyMetadata(typeof(PlaceholderTextbox)));
		}
	}
}
