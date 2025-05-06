
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgQuestionnaireViewComponentAwakeSystem : AwakeSystem<DlgQuestionnaireViewComponent>
	{
		protected override void Awake(DlgQuestionnaireViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgQuestionnaireViewComponentDestroySystem : DestroySystem<DlgQuestionnaireViewComponent>
	{
		protected override void Destroy(DlgQuestionnaireViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
