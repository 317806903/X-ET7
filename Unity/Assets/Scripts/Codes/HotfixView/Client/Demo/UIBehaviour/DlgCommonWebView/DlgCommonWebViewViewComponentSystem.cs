
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonWebViewViewComponentAwakeSystem : AwakeSystem<DlgCommonWebViewViewComponent>
	{
		protected override void Awake(DlgCommonWebViewViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonWebViewViewComponentDestroySystem : DestroySystem<DlgCommonWebViewViewComponent>
	{
		protected override void Destroy(DlgCommonWebViewViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
