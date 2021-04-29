using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using WPFExperiments.Converters;
using WPFExperiments.CustomControls;
using WPFExperiments.Data;
using WPFExperiments.Enums;

namespace WPFExperiments.UserControls
{
	/// <summary>
	/// Interaction logic for UCLEDGrid.xaml
	/// </summary>
	public partial class UCLEDGrid : UserControl
	{
		#region DEPENDENCY PROPERTIES
		/// <summary>The number of LEDs to display in the IO panels</summary>
		public int LEDsPerRow
		{
			get { return (int)GetValue(LEDsPerRowProperty); }
			set { SetValue(LEDsPerRowProperty, value); }
		}
		public static readonly DependencyProperty LEDsPerRowProperty =
				DependencyProperty.Register("LEDsPerRow", typeof(int), typeof(UCLEDGrid), new PropertyMetadata(5));

		/// <summary>The background color used for the title bars of the IO panels</summary>
		public SolidColorBrush TitleBackground
		{
			get { return (SolidColorBrush)GetValue(TitleBackgroundProperty); }
			set { SetValue(TitleBackgroundProperty, value); }
		}
		public static readonly DependencyProperty TitleBackgroundProperty =
				DependencyProperty.Register("TitleBackground", typeof(SolidColorBrush), typeof(UCLEDGrid), new PropertyMetadata(new SolidColorBrush(Colors.Orange)));

		/// <summary>The foreground color used for the title bars of the IO panels</summary>
		public SolidColorBrush TitleForeground
		{
			get { return (SolidColorBrush)GetValue(TitleForegroundProperty); }
			set { SetValue(TitleForegroundProperty, value); }
		}
		public static readonly DependencyProperty TitleForegroundProperty =
				DependencyProperty.Register("TitleForeground", typeof(SolidColorBrush), typeof(UCLEDGrid), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

		/// <summary>The FontSize used for the text in the title bars of the IO panels</summary>
		public int TitleFontSize
		{
			get { return (int)GetValue(TitleFontSizeProperty); }
			set { SetValue(TitleFontSizeProperty, value); }
		}
		public static readonly DependencyProperty TitleFontSizeProperty =
				DependencyProperty.Register("TitleFontSize", typeof(int), typeof(UCLEDGrid), new PropertyMetadata(18));

		/// <summary>The height of the splitter bar between the IO panels</summary>
		public int SplitterHeight
		{
			get { return (int)GetValue(SplitterHeightProperty); }
			set { SetValue(SplitterHeightProperty, value); }
		}
		public static readonly DependencyProperty SplitterHeightProperty =
				DependencyProperty.Register("SplitterHeight", typeof(int), typeof(UCLEDGrid), new PropertyMetadata(8));

		/// <summary>The color of the splitter bar</summary>
		public SolidColorBrush SplitterBackground
		{
			get { return (SolidColorBrush)GetValue(SplitterBackgroundProperty); }
			set { SetValue(SplitterBackgroundProperty, value); }
		}
		public static readonly DependencyProperty SplitterBackgroundProperty =
				DependencyProperty.Register("SplitterBackground", typeof(SolidColorBrush), typeof(UCLEDGrid), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

		/// <summary>Flag indicating if we're connected to the Profibus network.  Reflected in the LEDToggleButtons.</summary>
		public bool IsProfibusConnected
		{
			get { return (bool)GetValue(IsProfibusConnectedProperty); }
			set { SetValue(IsProfibusConnectedProperty, value); }
		}
		public static readonly DependencyProperty IsProfibusConnectedProperty =
				DependencyProperty.Register("IsProfibusConnected", typeof(bool), typeof(UCLEDGrid), new PropertyMetadata(false));

		#endregion DEPENDENCY PROPERTIES

		private readonly TabControl inputTabControl = new TabControl();
		private readonly TabControl outputTabControl = new TabControl();

		/// ProfibusProcess instead? That'll give me access to things like SetBit, but then it's tied to the UI.
		/// What about MasterMessagePumpMachineBase?  An Action that takes Node, BitOffset and value?
		public UCLEDGrid(ProfibusIODictionary profibusDictionary)
		{
			InitializeComponent();
			PopulateTabControl(profibusDictionary);
		}

		private void PopulateTabControl(ProfibusIODictionary profibusDictionary)
		{
			PopulateIOPanel(profibusDictionary, pnlOutputs, outputTabControl, ProfibusTypeId.OUTPUT);
			PopulateIOPanel(profibusDictionary, pnlInputs, inputTabControl, ProfibusTypeId.INPUT);
		}

		private void PopulateIOPanel(ProfibusIODictionary profibusDictionary, Grid gridPanel, TabControl tabControl, ProfibusTypeId ioType)
		{
			tabControl = new TabControl();

			// Get the list of nodes to create the tabs
			List<ushort> lstNodes = profibusDictionary.Values.Select(x => x.Node).Distinct().ToList();
			foreach (ushort nodeId in lstNodes)
			{
				// Get the list of IO point in the current tab that matches the requested ProfibusTypeId
				List<KeyValuePair<string, ProfibusIOData>> lstIOPoints = profibusDictionary.Where(x => x.Value.Node == nodeId && x.Value.IOType == ioType).ToList();

				// Don't create empty tabs
				if (lstIOPoints.Count > 0)
				{
					// TODO: Get the enum description for the second part of this
					string strHeader = $"({ nodeId }) { nodeId }";
					TabItem tabItem = new TabItem() { Header = strHeader };
					//tabItem.Tag = nodeId; // This shouldn't be needed because the buttons have NodeId as a property

					ScrollViewer scrollViewer = new ScrollViewer()
					{
						VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
						HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
					};

					UniformGrid uniformGrid = new UniformGrid() { Columns = LEDsPerRow };

					foreach (KeyValuePair<string, ProfibusIOData> entry in lstIOPoints)
					{
						LEDToggleButton button = new LEDToggleButton()
						{
							Content = entry.Key,
							NodeId = nodeId,
							BitOffset = entry.Value.BitOffset,
						};

						// Only the outputs should be clickable
						if (ioType == ProfibusTypeId.OUTPUT /* && shouldThisEvenBeAllowed */ )
						{
							// Bind the IsEnabled property of the button to the ProfibusConnectedStatus because we only want to allow clicking them when connected
							Binding binding = new Binding("IsEnabled");
							binding.Source = IsProfibusConnected;
							button.SetBinding(IsEnabledProperty, binding);

							// Subscribe to the click event
							button.Click += Button_LEDToggleButtonClick;
						}
						else
						{
							button.IsEnabled = false;
						}

						// Bind the IsThreeState property of the button to the inverse of the ProfibusConnected status
						Binding inverseBinding = new Binding("IsThreeState");
						inverseBinding.Source = IsProfibusConnected;
						inverseBinding.Converter = new InverseBooleanConverter();
						button.SetBinding(ToggleButton.IsThreeStateProperty, inverseBinding);

						// Add the button to the content grid for the current tab
						uniformGrid.Children.Add(button);
					}

					// Finish assembling the current tab
					scrollViewer.Content = uniformGrid;
					tabItem.Content = scrollViewer;
					tabControl.Items.Add(tabItem);
				}
			}

			// Add the populated tab control to the parent grid
			gridPanel.Children.Add(tabControl);
		}

		/// <summary>Handles an LED button being clicked (only outputs will respond)</summary>
		private void Button_LEDToggleButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender is LEDToggleButton button)
			{
				// Set the digital output -- see comment above constructor for ideas
			}
		}

		// Something like this
		public bool UpdateIO(GuiData guiData)
		{
			bool bSuccess = false;

			bSuccess &= UpdateIOPoints(inputTabControl, guiData.DigitalInputs);
			bSuccess &= UpdateIOPoints(outputTabControl, guiData.DigitalOutputs);

			return bSuccess;
		}

		private void UpdateIOPoints(TabControl tabControl, List<bool> ioPoints)
		{
			bool bSuccess = false;
			if (tabControl.SelectedItem is TabItem tabItem)
			{
				if (tabItem.Content is ScrollViewer scrollViewer)
				{
					if (scrollViewer.Content is UniformGrid uniformGrid)
					{
						bSuccess = true;
						foreach (LEDToggleButton button in uniformGrid.Children)
						{
							if (button.BitOffset < ioPoints.Count)
							{
								// TODO: Determine the correct mechanism here
								button.IsChecked = ioPoints[button.BitOffset];
							}
						}
					}
				}
			}
			return bSuccess;
		}
	}
}
