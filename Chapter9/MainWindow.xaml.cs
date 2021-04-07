using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chapter9
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void RepeatButton_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine($"Repeat Button clicked");
		}

		private void Button_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is FrameworkElement frameworkElement)
			{
				Debug.WriteLine($"{frameworkElement.Name} is now checked");
			}
		}

		private void Button_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is FrameworkElement frameworkElement)
			{
				Debug.WriteLine($"{frameworkElement.Name} is now unchecked");
			}
		}

		private void Button_Indeterminate(object sender, RoutedEventArgs e)
		{
			if (sender is FrameworkElement frameworkElement)
			{
				Debug.WriteLine($"{frameworkElement.Name} is now indeterminate");
			}
		}
	}
}
