
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgSeasonNoticeViewComponentAwakeSystem : AwakeSystem<DlgSeasonNoticeViewComponent>
	{
		protected override void Awake(DlgSeasonNoticeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgSeasonNoticeViewComponentDestroySystem : DestroySystem<DlgSeasonNoticeViewComponent>
	{
		protected override void Destroy(DlgSeasonNoticeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
