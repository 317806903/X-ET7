
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerARViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerARViewComponent>
	{
		protected override void Awake(DlgBattleTowerARViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerARViewComponentDestroySystem : DestroySystem<DlgBattleTowerARViewComponent>
	{
		protected override void Destroy(DlgBattleTowerARViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
