
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgTutorialOneViewComponentAwakeSystem : AwakeSystem<DlgTutorialOneViewComponent>
	{
		protected override void Awake(DlgTutorialOneViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgTutorialOneViewComponentDestroySystem : DestroySystem<DlgTutorialOneViewComponent>
	{
		protected override void Destroy(DlgTutorialOneViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
