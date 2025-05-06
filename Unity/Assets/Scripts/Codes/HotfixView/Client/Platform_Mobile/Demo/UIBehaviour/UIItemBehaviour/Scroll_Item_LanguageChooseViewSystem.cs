
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_LanguageChooseAwakeSystem : AwakeSystem<Scroll_Item_LanguageChoose> 
	{
		protected override void Awake( Scroll_Item_LanguageChoose self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_LanguageChooseDestroySystem : DestroySystem<Scroll_Item_LanguageChoose> 
	{
		protected override void Destroy( Scroll_Item_LanguageChoose self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
