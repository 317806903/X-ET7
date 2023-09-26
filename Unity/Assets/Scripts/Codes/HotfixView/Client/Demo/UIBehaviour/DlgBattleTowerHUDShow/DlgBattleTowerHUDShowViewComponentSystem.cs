
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerHUDShowViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerHUDShowViewComponent>
	{
		protected override void Awake(DlgBattleTowerHUDShowViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerHUDShowViewComponentDestroySystem : DestroySystem<DlgBattleTowerHUDShowViewComponent>
	{
		protected override void Destroy(DlgBattleTowerHUDShowViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
