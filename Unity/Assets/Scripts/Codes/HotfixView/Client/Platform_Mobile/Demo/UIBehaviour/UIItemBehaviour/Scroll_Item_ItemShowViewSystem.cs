
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_ItemShowAwakeSystem : AwakeSystem<Scroll_Item_ItemShow> 
	{
		protected override void Awake( Scroll_Item_ItemShow self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_ItemShowDestroySystem : DestroySystem<Scroll_Item_ItemShow> 
	{
		protected override void Destroy( Scroll_Item_ItemShow self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
