
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_RoomMemberDestroySystem : DestroySystem<Scroll_Item_RoomMember> 
	{
		protected override void Destroy( Scroll_Item_RoomMember self )
		{
			self.DestroyWidget();
		}
	}
}
