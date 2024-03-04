
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgDetailsViewComponentAwakeSystem : AwakeSystem<DlgDetailsViewComponent>
	{
		protected override void Awake(DlgDetailsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgDetailsViewComponentDestroySystem : DestroySystem<DlgDetailsViewComponent>
	{
		protected override void Destroy(DlgDetailsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
