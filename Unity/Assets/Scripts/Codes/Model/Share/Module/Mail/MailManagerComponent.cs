using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	/// <summary>
	/// 邮件管理器
	/// </summary>
	[ComponentOf(typeof(Scene))]
	public class MailManagerComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public int waitFrameChk = 3000;
		public int curFrameChk = 0;
	}
}