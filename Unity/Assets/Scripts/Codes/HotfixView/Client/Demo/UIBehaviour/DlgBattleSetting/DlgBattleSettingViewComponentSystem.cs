
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleSettingViewComponentAwakeSystem : AwakeSystem<DlgBattleSettingViewComponent>
	{
		protected override void Awake(DlgBattleSettingViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleSettingViewComponentDestroySystem : DestroySystem<DlgBattleSettingViewComponent>
	{
		protected override void Destroy(DlgBattleSettingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
