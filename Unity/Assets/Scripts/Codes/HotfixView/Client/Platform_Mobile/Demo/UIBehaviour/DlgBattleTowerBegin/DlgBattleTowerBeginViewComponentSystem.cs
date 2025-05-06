
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerBeginViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerBeginViewComponent>
	{
		protected override void Awake(DlgBattleTowerBeginViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerBeginViewComponentDestroySystem : DestroySystem<DlgBattleTowerBeginViewComponent>
	{
		protected override void Destroy(DlgBattleTowerBeginViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
