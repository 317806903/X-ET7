
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerNoticeViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerNoticeViewComponent>
	{
		protected override void Awake(DlgBattleTowerNoticeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleTowerNoticeViewComponentDestroySystem : DestroySystem<DlgBattleTowerNoticeViewComponent>
	{
		protected override void Destroy(DlgBattleTowerNoticeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
