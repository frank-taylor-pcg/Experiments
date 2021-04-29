using MyCustomControls.Converters;
using MyCustomControls.CustomControls;
using MyCustomControls.Data;
using MyCustomControls.Enums;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MyCustomControls
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<IOPoint> LEDStates
		{
			get { return (ObservableCollection<IOPoint>)GetValue(LEDStatesProperty); }
			set { SetValue(LEDStatesProperty, value); }
		}
		public static readonly DependencyProperty LEDStatesProperty =
				DependencyProperty.Register("LEDStates", typeof(ObservableCollection<IOPoint>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<IOPoint>()));

		public MainWindow()
		{
			CreateTestData();
			InitializeComponent();
			DataContext = this;
		}

		private void CreateTestData()
		{
			foreach (ProfibusInputId input in Enum.GetValues(typeof(ProfibusInputId)))
			{
				IOPoint point = new IOPoint()
				{
					NodeId = 20,
					Name = Enum.GetName(typeof(ProfibusInputId), input).Replace("_", " "),
					BitOffset = (ushort)input,
					State = null,
					IsOutput = true
				};

				if (input.ToString().Contains("INPUT"))
				{
					point.IsOutput = false;
				}

				LEDStates.Add(point);
			}
		}

		private void LEDToggleButton_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is LEDToggleButton button)
			{
				Debug.WriteLine($"Set the status of output {button.Content} at node {button.NodeId} and offset {button.BitOffset} to ON");
			}
		}

		private void LEDToggleButton_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is LEDToggleButton button)
			{
				Debug.WriteLine($"Set the status of output {button.Content} at node {button.NodeId} and offset {button.BitOffset} to OFF");
			}
		}

		private void LEDToggleButton_Indeterminate(object sender, RoutedEventArgs e)
		{
			// The indeterminate state (IsThreeState) should be disabled when we successfully connect to Profibus
			if (sender is LEDToggleButton button)
			{
				Debug.WriteLine($"Set the status of output {button.Content} at node {button.NodeId} and offset {button.BitOffset} to Indeterminate");
			}
		}

		private void VerifyButton_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			foreach (IOPoint point in LEDStates)
			{
				sb.AppendLine(point.ToString());
			}
			Debug.WriteLine(sb.ToString());
		}

		private void RandomizeButton_Click(object sender, RoutedEventArgs e)
		{
			Random rnd = new Random();
			foreach (IOPoint point in LEDStates)
			{
				switch (rnd.Next(0, 3))
				{
					case 0: { point.State = null; break; }
					case 1: { point.State = false; break; }
					case 2: { point.State = true; break; }
				}
			}
		}

		//private void Window_Loaded(object sender, RoutedEventArgs e)
		//{
		//	Random rnd = new Random();
		//	Style darkStyle = this.FindResource("dark") as Style;
		//	int i = 0;
		//	char c1 = 'A';

		//	Binding binding = new Binding("IsChecked");
		//	binding.Source = cbProfibusConnected;

		//	Binding inverseBinding = new Binding("IsChecked");
		//	inverseBinding.Source = cbProfibusConnected;
		//	inverseBinding.Converter = new InverseBooleanConverter();

		//	for (ushort node = 0; node < 80; node += 10)
		//	{
		//		char c2 = 'M';
		//		for (ushort offset = 0; offset < 10; offset += 2)
		//		{
		//			LEDToggleButton button = new LEDToggleButton()
		//			{
		//				Name = $"{node}_{offset}",
		//				NodeId = node,
		//				BitOffset = offset,
		//				Content = $"Output {c1}_{c2++}",
		//				Width = 100,
		//				Height = 40
		//			};

		//			//  Bind the three-state property of the button to the profibus connected checkbox
		//			button.SetBinding(LEDToggleButton.IsThreeStateProperty, inverseBinding);

		//			// Bind the is enabled property of the button too the profibus connected checkbox
		//			button.SetBinding(LEDToggleButton.IsEnabledProperty, binding);

		//			// Subscribe to the events
		//			button.Checked += LEDToggleButton_Checked;
		//			button.Unchecked += LEDToggleButton_Unchecked;
		//			button.Indeterminate += LEDToggleButton_Indeterminate;

		//			// Set one of the two styles (alternating)
		//			if (i % 2 == 0)
		//			{
		//				button.Style = darkStyle;
		//			}
		//			i++;

		//			// Pick a random starting state for the button
		//			switch (rnd.Next(0, 3))
		//			{
		//				case 0:
		//				{
		//					button.IsChecked = null;
		//					break;
		//				}
		//				case 1:
		//				{
		//					button.IsChecked = false;
		//					break;
		//				}
		//				case 2:
		//				{
		//					button.IsChecked = true;
		//					break;
		//				}
		//			}

		//			ugButtons.Children.Add(button);
		//		}
		//		c1++;
		//	}
		//}
	}
}
