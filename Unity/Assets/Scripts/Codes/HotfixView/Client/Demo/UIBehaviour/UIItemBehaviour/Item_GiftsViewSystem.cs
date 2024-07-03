
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_GiftsDestroySystem : DestroySystem<Scroll_Item_Gifts> 
	{
		protected override void Destroy( Scroll_Item_Gifts self )
		{
			self.DestroyWidget();
		}
	}
}
