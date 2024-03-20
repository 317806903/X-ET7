
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_MonstersDestroySystem : DestroySystem<Scroll_Item_Monsters> 
	{
		protected override void Destroy( Scroll_Item_Monsters self )
		{
			self.DestroyWidget();
		}
	}
}
