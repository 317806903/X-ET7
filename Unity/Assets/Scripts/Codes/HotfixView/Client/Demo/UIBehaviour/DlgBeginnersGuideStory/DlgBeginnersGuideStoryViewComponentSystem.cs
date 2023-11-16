
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBeginnersGuideStoryViewComponentAwakeSystem : AwakeSystem<DlgBeginnersGuideStoryViewComponent>
	{
		protected override void Awake(DlgBeginnersGuideStoryViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBeginnersGuideStoryViewComponentDestroySystem : DestroySystem<DlgBeginnersGuideStoryViewComponent>
	{
		protected override void Destroy(DlgBeginnersGuideStoryViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
