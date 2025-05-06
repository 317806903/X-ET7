
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerHUDViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerHUDViewComponent>
	{
		protected override void Awake(DlgBattleTowerHUDViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerHUDViewComponentDestroySystem : DestroySystem<DlgBattleTowerHUDViewComponent>
	{
		protected override void Destroy(DlgBattleTowerHUDViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
