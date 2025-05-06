
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonTipTopShowViewComponentAwakeSystem : AwakeSystem<DlgCommonTipTopShowViewComponent>
	{
		protected override void Awake(DlgCommonTipTopShowViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonTipTopShowViewComponentDestroySystem : DestroySystem<DlgCommonTipTopShowViewComponent>
	{
		protected override void Destroy(DlgCommonTipTopShowViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
