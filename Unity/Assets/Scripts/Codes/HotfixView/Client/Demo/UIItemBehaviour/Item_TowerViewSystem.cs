
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TowerDestroySystem : DestroySystem<Scroll_Item_Tower> 
	{
		protected override void Destroy( Scroll_Item_Tower self )
		{
			self.DestroyWidget();
		}
	}
}
