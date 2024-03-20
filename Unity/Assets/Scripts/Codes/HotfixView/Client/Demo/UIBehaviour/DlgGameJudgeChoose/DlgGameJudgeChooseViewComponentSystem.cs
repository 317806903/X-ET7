
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameJudgeChooseViewComponentAwakeSystem : AwakeSystem<DlgGameJudgeChooseViewComponent>
	{
		protected override void Awake(DlgGameJudgeChooseViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameJudgeChooseViewComponentDestroySystem : DestroySystem<DlgGameJudgeChooseViewComponent>
	{
		protected override void Destroy(DlgGameJudgeChooseViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
