using ModernDesign.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand DiscoveryViewCommand { get; set; }

		public HomeViewModel HomeVM { get; set; }
		public DiscoveryViewModel DiscoveryVM { get; set; }

		private object m_currentView;
		public object CurrentView
		{
			get { return m_currentView; }
			set
			{
				m_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			HomeVM = new HomeViewModel();
			DiscoveryVM = new DiscoveryViewModel();

			CurrentView = HomeVM;

			HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
			DiscoveryViewCommand = new RelayCommand(o => { CurrentView = DiscoveryVM; });
		}
	}
}
