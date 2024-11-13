
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_MonstersAwakeSystem : AwakeSystem<Scroll_Item_Monsters> 
	{
		protected override void Awake( Scroll_Item_Monsters self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_MonstersDestroySystem : DestroySystem<Scroll_Item_Monsters> 
	{
		protected override void Destroy( Scroll_Item_Monsters self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
