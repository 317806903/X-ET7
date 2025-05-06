
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleDeckViewComponentAwakeSystem : AwakeSystem<DlgBattleDeckViewComponent>
	{
		protected override void Awake(DlgBattleDeckViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleDeckViewComponentDestroySystem : DestroySystem<DlgBattleDeckViewComponent>
	{
		protected override void Destroy(DlgBattleDeckViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
