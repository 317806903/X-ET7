
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TowerIconDestroySystem : DestroySystem<Scroll_Item_TowerIcon> 
	{
		protected override void Destroy( Scroll_Item_TowerIcon self )
		{
			self.DestroyWidget();
		}
	}
}
