using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	/// <summary>
	/// 邮件发送给玩家种类枚举
	/// </summary>
	public enum MailToPlayerType
	{
		/// <summary>
		/// 所有玩家
		/// </summary>
		AllPlayer,
		/// <summary>
		/// 部分玩家
		/// </summary>
		PlayerList,
	}

	[ChildOf(typeof(MailManagerComponent))]
	public class MailToPlayersComponent : Entity, IAwake, IDestroy
	{
		public long createTime;
		public MailToPlayerType mailToPlayerType;
		/// <summary>
		/// 等待发送的playerID哈希表
		/// </summary>
		public HashSet<long> waitSendPlayerList;
		/// <summary>
		/// 已经发送的playerID哈希表
		/// </summary>
		public HashSet<long> deliveredPlayerList;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, string> playerParam;
	}
}