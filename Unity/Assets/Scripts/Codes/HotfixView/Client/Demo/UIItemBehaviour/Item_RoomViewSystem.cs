
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_RoomDestroySystem : DestroySystem<Scroll_Item_Room> 
	{
		protected override void Destroy( Scroll_Item_Room self )
		{
			self.DestroyWidget();
		}
	}
}
