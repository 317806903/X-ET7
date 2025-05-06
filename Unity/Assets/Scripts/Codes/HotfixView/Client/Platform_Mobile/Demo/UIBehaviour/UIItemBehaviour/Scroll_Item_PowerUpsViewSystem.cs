
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_PowerUpsAwakeSystem : AwakeSystem<Scroll_Item_PowerUps> 
	{
		protected override void Awake( Scroll_Item_PowerUps self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_PowerUpsDestroySystem : DestroySystem<Scroll_Item_PowerUps> 
	{
		protected override void Destroy( Scroll_Item_PowerUps self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
