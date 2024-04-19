
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgFixedMenuViewComponentAwakeSystem : AwakeSystem<DlgFixedMenuViewComponent>
	{
		protected override void Awake(DlgFixedMenuViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgFixedMenuViewComponentDestroySystem : DestroySystem<DlgFixedMenuViewComponent>
	{
		protected override void Destroy(DlgFixedMenuViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
