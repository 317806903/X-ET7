
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonLoadingViewComponentAwakeSystem : AwakeSystem<DlgCommonLoadingViewComponent>
	{
		protected override void Awake(DlgCommonLoadingViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonLoadingViewComponentDestroySystem : DestroySystem<DlgCommonLoadingViewComponent>
	{
		protected override void Destroy(DlgCommonLoadingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
