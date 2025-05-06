
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattlePlayerSkillViewComponentAwakeSystem : AwakeSystem<DlgBattlePlayerSkillViewComponent>
	{
		protected override void Awake(DlgBattlePlayerSkillViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattlePlayerSkillViewComponentDestroySystem : DestroySystem<DlgBattlePlayerSkillViewComponent>
	{
		protected override void Destroy(DlgBattlePlayerSkillViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
