
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCameraPlayerSkillViewComponentAwakeSystem : AwakeSystem<DlgCameraPlayerSkillViewComponent>
	{
		protected override void Awake(DlgCameraPlayerSkillViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCameraPlayerSkillViewComponentDestroySystem : DestroySystem<DlgCameraPlayerSkillViewComponent>
	{
		protected override void Destroy(DlgCameraPlayerSkillViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
