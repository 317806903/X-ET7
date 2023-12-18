
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_AvatarIconDestroySystem : DestroySystem<Scroll_Item_AvatarIcon> 
	{
		protected override void Destroy( Scroll_Item_AvatarIcon self )
		{
			self.DestroyWidget();
		}
	}
}
