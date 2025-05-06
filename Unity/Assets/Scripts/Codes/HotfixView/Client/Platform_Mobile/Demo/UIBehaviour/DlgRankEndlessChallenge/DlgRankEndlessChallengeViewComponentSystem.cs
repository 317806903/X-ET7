
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgRankEndlessChallengeViewComponentAwakeSystem : AwakeSystem<DlgRankEndlessChallengeViewComponent>
	{
		protected override void Awake(DlgRankEndlessChallengeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgRankEndlessChallengeViewComponentDestroySystem : DestroySystem<DlgRankEndlessChallengeViewComponent>
	{
		protected override void Destroy(DlgRankEndlessChallengeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
