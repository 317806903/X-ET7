
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonConfirmHighestViewComponentAwakeSystem : AwakeSystem<DlgCommonConfirmHighestViewComponent>
	{
		protected override void Awake(DlgCommonConfirmHighestViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonConfirmHighestViewComponentDestroySystem : DestroySystem<DlgCommonConfirmHighestViewComponent>
	{
		protected override void Destroy(DlgCommonConfirmHighestViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
