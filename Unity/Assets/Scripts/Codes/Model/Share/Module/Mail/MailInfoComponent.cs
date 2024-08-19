using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	/// <summary>
	/// 邮件
	/// </summary>
	[ChildOf()]
	public class MailInfoComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
		public string mailType;
		public string mailTitle;
		public string mailContent;
		public Dictionary<string, int> itemCfgList;
		public long receiveTime;
		public long limitTime;
	}
}