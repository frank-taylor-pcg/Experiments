using WPFExperiments.Enums;

namespace WPFExperiments.Data
{
	public class ProfibusIOData
	{
		public ProfibusTypeId IOType { get; set; }
		public ushort Node { get; set; }
		public ushort BitOffset { get; set; }
	}
}
