
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgChallengeModeViewComponentAwakeSystem : AwakeSystem<DlgChallengeModeViewComponent>
	{
		protected override void Awake(DlgChallengeModeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgChallengeModeViewComponentDestroySystem : DestroySystem<DlgChallengeModeViewComponent>
	{
		protected override void Destroy(DlgChallengeModeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
