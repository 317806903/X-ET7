
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonTipNodeViewComponentAwakeSystem : AwakeSystem<DlgCommonTipNodeViewComponent>
	{
		protected override void Awake(DlgCommonTipNodeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonTipNodeViewComponentDestroySystem : DestroySystem<DlgCommonTipNodeViewComponent>
	{
		protected override void Destroy(DlgCommonTipNodeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
