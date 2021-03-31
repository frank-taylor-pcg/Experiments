namespace Experiments.Messaging
{
	public class Message
	{
		public object Data { get; set; }

		public Message(object data = null)
		{
			Data = data;
		}

		public override string ToString()
		{
			return $"{Data}";
		}
	}
}
