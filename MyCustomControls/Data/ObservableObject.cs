using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCustomControls.Data
{
	public class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		protected bool SetField<T>(ref T objField, T objValue, [CallerMemberName] string strPropertyName = null)
		{
			bool bReturn = false;

			if (false == EqualityComparer<T>.Default.Equals(objField, objValue))
			{
				objField = objValue;
				NotifyPropertyChanged(strPropertyName);
				bReturn = true;
			}

			return bReturn;
		}
	}
}
