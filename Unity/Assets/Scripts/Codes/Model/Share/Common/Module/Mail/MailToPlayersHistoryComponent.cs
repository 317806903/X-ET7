using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	[ChildOf(typeof(MailHistoryManagerComponent))]
	public class MailToPlayersHistoryComponent : Entity, IAwake, IDestroy
	{
		public long createTime;
		public long historyTime;
		public MailToPlayerType mailToPlayerType;
		/// <summary>
		/// 等待发送的playerID哈希表
		/// </summary>
		public HashSet<long> waitSendPlayerList;
		/// <summary>
		/// 已经发送的playerID哈希表
		/// </summary>
		public HashSet<long> deliveredPlayerList;
	}
}