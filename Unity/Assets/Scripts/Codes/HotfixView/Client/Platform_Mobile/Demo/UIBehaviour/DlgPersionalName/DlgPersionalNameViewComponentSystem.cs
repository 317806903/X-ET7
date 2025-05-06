
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPersionalNameViewComponentAwakeSystem : AwakeSystem<DlgPersionalNameViewComponent>
	{
		protected override void Awake(DlgPersionalNameViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPersionalNameViewComponentDestroySystem : DestroySystem<DlgPersionalNameViewComponent>
	{
		protected override void Destroy(DlgPersionalNameViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
