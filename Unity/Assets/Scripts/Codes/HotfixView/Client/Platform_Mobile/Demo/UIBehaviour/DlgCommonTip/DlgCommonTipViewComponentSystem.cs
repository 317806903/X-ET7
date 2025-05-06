
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonTipViewComponentAwakeSystem : AwakeSystem<DlgCommonTipViewComponent>
	{
		protected override void Awake(DlgCommonTipViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonTipViewComponentDestroySystem : DestroySystem<DlgCommonTipViewComponent>
	{
		protected override void Destroy(DlgCommonTipViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
