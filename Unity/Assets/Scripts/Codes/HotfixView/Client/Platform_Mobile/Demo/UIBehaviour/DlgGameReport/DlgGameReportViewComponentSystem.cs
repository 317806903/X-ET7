
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameReportViewComponentAwakeSystem : AwakeSystem<DlgGameReportViewComponent>
	{
		protected override void Awake(DlgGameReportViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameReportViewComponentDestroySystem : DestroySystem<DlgGameReportViewComponent>
	{
		protected override void Destroy(DlgGameReportViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
