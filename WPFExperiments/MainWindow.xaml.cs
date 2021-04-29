using System;
using System.Windows;
using WPFExperiments.Data;
using WPFExperiments.Enums;
using WPFExperiments.UserControls;

namespace WPFExperiments
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ExampleId RegulatedPermit
		{
			get { return (ExampleId)GetValue(RegulatedPermitProperty); }
			set { SetValue(RegulatedPermitProperty, value); }
		}
		public static readonly DependencyProperty RegulatedPermitProperty =
				DependencyProperty.Register("RegulatedPermit", typeof(ExampleId), typeof(MainWindow), new PropertyMetadata(ExampleId.UNKNOWN));

		public string Status
		{
			get { return (string)GetValue(StatusProperty); }
			set { SetValue(StatusProperty, value); }
		}
		public static readonly DependencyProperty StatusProperty =
				DependencyProperty.Register("Status", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

		public TestObject TestObject
		{
			get { return (TestObject)GetValue(TestObjectProperty); }
			set { SetValue(TestObjectProperty, value); }
		}
		public static readonly DependencyProperty TestObjectProperty =
				DependencyProperty.Register("TestObject", typeof(TestObject), typeof(MainWindow), new PropertyMetadata(new TestObject()));

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			ContentPane.Content = new UCLEDGrid(BuildTestData());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int currentStatus = (int)RegulatedPermit;

			currentStatus = (currentStatus + 1) % (Enum.GetValues(typeof(ExampleId)).Length);
			RegulatedPermit = (ExampleId)currentStatus;

			Status = Enum.GetName(typeof(ExampleId), RegulatedPermit);
		}

		private ProfibusIODictionary BuildTestData()
		{
			ProfibusIODictionary result = new ProfibusIODictionary();

			for (ushort node = 10; node < 90; node += 10)
			{
				for (ushort bitoffset = 0; bitoffset < 20; bitoffset++)
				{
					string strKey = $"Node: {node} Offset: {bitoffset} ";
					result[strKey + "Input"] = new ProfibusIOData() { Node = node, BitOffset = bitoffset, IOType = ProfibusTypeId.INPUT };
					result[strKey + "Output"] = new ProfibusIOData() { Node = node, BitOffset = bitoffset, IOType = ProfibusTypeId.OUTPUT };
				}
			}

			return result;
		}
	}

	public class TestObject
	{
		public string SomeValue { get; set; } = "This is a cool example message";
	}
}
