
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_TutorialsAwakeSystem : AwakeSystem<Scroll_Item_Tutorials> 
	{
		protected override void Awake( Scroll_Item_Tutorials self )
		{
			self.RegisterUIEvent();
		}
	}
	[ObjectSystem]
	public class Scroll_Item_TutorialsDestroySystem : DestroySystem<Scroll_Item_Tutorials> 
	{
		protected override void Destroy( Scroll_Item_Tutorials self )
		{
			self.HideItem();
			self.DestroyWidget();
		}
	}
}
