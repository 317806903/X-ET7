using System.Collections.Generic;

namespace ET
{
	public enum ActionFromHttpStatus
	{
		SendMails,
		AddItem,
		DeleteItem,
		SetItemNum,
	}

	[ComponentOf(typeof(Scene))]
	public class ActionFromHttpManagerComponent : Entity, IAwake, IDestroy
	{
		public Dictionary<ActionFromHttpStatus, IActionFromHttpHandler> dic;
	}
}