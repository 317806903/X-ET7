
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPhysicalStrengthTipViewComponentAwakeSystem : AwakeSystem<DlgPhysicalStrengthTipViewComponent>
	{
		protected override void Awake(DlgPhysicalStrengthTipViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPhysicalStrengthTipViewComponentDestroySystem : DestroySystem<DlgPhysicalStrengthTipViewComponent>
	{
		protected override void Destroy(DlgPhysicalStrengthTipViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
