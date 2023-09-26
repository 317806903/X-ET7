
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgLoadingViewComponentAwakeSystem : AwakeSystem<DlgLoadingViewComponent> 
	{
		protected override void Awake(DlgLoadingViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLoadingViewComponentDestroySystem : DestroySystem<DlgLoadingViewComponent> 
	{
		protected override void Destroy(DlgLoadingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
