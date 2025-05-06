
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerViewComponent>
	{
		protected override void Awake(DlgBattleTowerViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerViewComponentDestroySystem : DestroySystem<DlgBattleTowerViewComponent>
	{
		protected override void Destroy(DlgBattleTowerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
