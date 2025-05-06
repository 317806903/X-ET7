
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleHomeHUDViewComponentAwakeSystem : AwakeSystem<DlgBattleHomeHUDViewComponent>
	{
		protected override void Awake(DlgBattleHomeHUDViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleHomeHUDViewComponentDestroySystem : DestroySystem<DlgBattleHomeHUDViewComponent>
	{
		protected override void Destroy(DlgBattleHomeHUDViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
