
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_Mail_InboxAwakeSystem : AwakeSystem<Scroll_Item_Mail_Inbox> 
	{
		protected override void Awake( Scroll_Item_Mail_Inbox self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_Mail_InboxDestroySystem : DestroySystem<Scroll_Item_Mail_Inbox> 
	{
		protected override void Destroy( Scroll_Item_Mail_Inbox self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
