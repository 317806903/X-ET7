
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_RoomAwakeSystem : AwakeSystem<Scroll_Item_Room> 
	{
		protected override void Awake( Scroll_Item_Room self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_RoomDestroySystem : DestroySystem<Scroll_Item_Room> 
	{
		protected override void Destroy( Scroll_Item_Room self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
