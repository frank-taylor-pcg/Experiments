using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiments.Messaging
{
	public class MessageSystem
	{
		private static MessageSystem _instance = null;
		private static readonly object objLock = new();

		private static readonly ConcurrentDictionary<Guid, ConcurrentQueue<Message>> processMessages = new();

		#region CONSTRUCTORS
		private MessageSystem() { }

		public static MessageSystem Instance
		{
			get
			{
				lock (objLock)
				{
					if (_instance == null)
					{
						_instance = new MessageSystem();
					}
					return _instance;
				}
			}
		}
		#endregion CONSTRUCTORS

		public static Guid RegisterProcess()
		{
			Guid result = Guid.NewGuid();
			processMessages[result] = new ConcurrentQueue<Message>();
			return result;
		}
	}
}
