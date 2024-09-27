
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleSkillViewComponentAwakeSystem : AwakeSystem<DlgBattleSkillViewComponent>
	{
		protected override void Awake(DlgBattleSkillViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleSkillViewComponentDestroySystem : DestroySystem<DlgBattleSkillViewComponent>
	{
		protected override void Destroy(DlgBattleSkillViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
