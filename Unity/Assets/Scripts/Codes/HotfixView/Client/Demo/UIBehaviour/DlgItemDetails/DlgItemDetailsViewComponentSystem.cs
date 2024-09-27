
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgItemDetailsViewComponentAwakeSystem : AwakeSystem<DlgItemDetailsViewComponent>
	{
		protected override void Awake(DlgItemDetailsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgItemDetailsViewComponentDestroySystem : DestroySystem<DlgItemDetailsViewComponent>
	{
		protected override void Destroy(DlgItemDetailsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
