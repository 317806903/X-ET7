
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_RoomMemberAwakeSystem : AwakeSystem<Scroll_Item_RoomMember> 
	{
		protected override void Awake( Scroll_Item_RoomMember self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_RoomMemberDestroySystem : DestroySystem<Scroll_Item_RoomMember> 
	{
		protected override void Destroy( Scroll_Item_RoomMember self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
