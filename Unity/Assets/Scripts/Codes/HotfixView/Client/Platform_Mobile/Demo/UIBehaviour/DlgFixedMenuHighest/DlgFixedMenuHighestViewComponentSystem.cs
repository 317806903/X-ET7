
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgFixedMenuHighestViewComponentAwakeSystem : AwakeSystem<DlgFixedMenuHighestViewComponent>
	{
		protected override void Awake(DlgFixedMenuHighestViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgFixedMenuHighestViewComponentDestroySystem : DestroySystem<DlgFixedMenuHighestViewComponent>
	{
		protected override void Destroy(DlgFixedMenuHighestViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
