
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgMailSettlementViewComponentAwakeSystem : AwakeSystem<DlgMailSettlementViewComponent>
	{
		protected override void Awake(DlgMailSettlementViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgMailSettlementViewComponentDestroySystem : DestroySystem<DlgMailSettlementViewComponent>
	{
		protected override void Destroy(DlgMailSettlementViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
