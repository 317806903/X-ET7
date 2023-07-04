
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TowerBuyDestroySystem : DestroySystem<Scroll_Item_TowerBuy> 
	{
		protected override void Destroy( Scroll_Item_TowerBuy self )
		{
			self.DestroyWidget();
		}
	}
}
