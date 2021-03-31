using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiments.Messaging
{
	public class MessageProcess
	{
		private Guid guid;

		public MessageProcess()
		{
			guid = MessageSystem.RegisterProcess();
		}
	}
}
