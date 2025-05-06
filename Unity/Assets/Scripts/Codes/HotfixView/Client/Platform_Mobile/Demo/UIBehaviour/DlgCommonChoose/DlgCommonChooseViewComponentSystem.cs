
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCommonChooseViewComponentAwakeSystem : AwakeSystem<DlgCommonChooseViewComponent>
	{
		protected override void Awake(DlgCommonChooseViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgCommonChooseViewComponentDestroySystem : DestroySystem<DlgCommonChooseViewComponent>
	{
		protected override void Destroy(DlgCommonChooseViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
