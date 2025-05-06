
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgSkillDetailsViewComponentAwakeSystem : AwakeSystem<DlgSkillDetailsViewComponent>
	{
		protected override void Awake(DlgSkillDetailsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgSkillDetailsViewComponentDestroySystem : DestroySystem<DlgSkillDetailsViewComponent>
	{
		protected override void Destroy(DlgSkillDetailsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
