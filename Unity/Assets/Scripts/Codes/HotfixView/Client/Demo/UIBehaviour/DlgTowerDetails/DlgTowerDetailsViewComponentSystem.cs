
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgTowerDetailsViewComponentAwakeSystem : AwakeSystem<DlgTowerDetailsViewComponent>
	{
		protected override void Awake(DlgTowerDetailsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgTowerDetailsViewComponentDestroySystem : DestroySystem<DlgTowerDetailsViewComponent>
	{
		protected override void Destroy(DlgTowerDetailsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
