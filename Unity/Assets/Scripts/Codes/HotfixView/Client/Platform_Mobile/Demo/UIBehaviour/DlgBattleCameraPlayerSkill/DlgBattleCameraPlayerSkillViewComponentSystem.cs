
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleCameraPlayerSkillViewComponentAwakeSystem : AwakeSystem<DlgBattleCameraPlayerSkillViewComponent>
	{
		protected override void Awake(DlgBattleCameraPlayerSkillViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleCameraPlayerSkillViewComponentDestroySystem : DestroySystem<DlgBattleCameraPlayerSkillViewComponent>
	{
		protected override void Destroy(DlgBattleCameraPlayerSkillViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
