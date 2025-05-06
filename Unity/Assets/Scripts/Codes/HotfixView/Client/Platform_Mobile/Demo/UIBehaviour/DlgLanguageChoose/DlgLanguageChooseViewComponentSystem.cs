
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgLanguageChooseViewComponentAwakeSystem : AwakeSystem<DlgLanguageChooseViewComponent>
	{
		protected override void Awake(DlgLanguageChooseViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgLanguageChooseViewComponentDestroySystem : DestroySystem<DlgLanguageChooseViewComponent>
	{
		protected override void Destroy(DlgLanguageChooseViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
