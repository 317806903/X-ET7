
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_FrameDestroySystem : DestroySystem<Scroll_Item_Frame> 
	{
		protected override void Destroy( Scroll_Item_Frame self )
		{
			self.DestroyWidget();
		}
	}
}
