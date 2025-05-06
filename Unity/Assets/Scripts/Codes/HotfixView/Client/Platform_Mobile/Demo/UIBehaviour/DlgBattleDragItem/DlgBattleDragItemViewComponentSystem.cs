
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleDragItemViewComponentAwakeSystem : AwakeSystem<DlgBattleDragItemViewComponent>
	{
		protected override void Awake(DlgBattleDragItemViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleDragItemViewComponentDestroySystem : DestroySystem<DlgBattleDragItemViewComponent>
	{
		protected override void Destroy(DlgBattleDragItemViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
