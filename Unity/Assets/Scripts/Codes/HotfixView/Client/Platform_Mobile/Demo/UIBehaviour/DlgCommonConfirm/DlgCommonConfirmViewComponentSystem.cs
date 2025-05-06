
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonConfirmViewComponentAwakeSystem : AwakeSystem<DlgCommonConfirmViewComponent>
	{
		protected override void Awake(DlgCommonConfirmViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonConfirmViewComponentDestroySystem : DestroySystem<DlgCommonConfirmViewComponent>
	{
		protected override void Destroy(DlgCommonConfirmViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
