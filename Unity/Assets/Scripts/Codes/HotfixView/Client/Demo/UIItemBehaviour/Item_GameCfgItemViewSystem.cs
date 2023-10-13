
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_GameCfgItemDestroySystem : DestroySystem<Scroll_Item_GameCfgItem> 
	{
		protected override void Destroy( Scroll_Item_GameCfgItem self )
		{
			self.DestroyWidget();
		}
	}
}
