namespace MyCustomControls.Data
{
	public class IOPoint : ObservableObject
	{
		private ushort _NodeId;
		public ushort NodeId
		{
			get => _NodeId;
			set { SetField(ref _NodeId, value); }
		}

		private string _Name;
		public string Name
		{
			get => _Name;
			set { SetField(ref _Name, value); }
		}

		private ushort _BitOffset;
		public ushort BitOffset
		{
			get => _BitOffset;
			set { SetField(ref _BitOffset, value); }
		}

		private bool? _State;
		public bool? State
		{
			get => _State;
			set { SetField(ref _State, value); }
		}

		private bool _IsOutput;
		public bool IsOutput
		{
			get => _IsOutput;
			set { SetField(ref _IsOutput, value); }
		}

		public override string ToString()
		{
			string strState = "Unknown";
			if (true == State) strState = "On";
			else if (false == State) strState = "Off";

			return $"{Name} => {strState}";
		}
	}
}
