
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_FrameAwakeSystem : AwakeSystem<Scroll_Item_Frame> 
	{
		protected override void Awake( Scroll_Item_Frame self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_FrameDestroySystem : DestroySystem<Scroll_Item_Frame> 
	{
		protected override void Destroy( Scroll_Item_Frame self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
