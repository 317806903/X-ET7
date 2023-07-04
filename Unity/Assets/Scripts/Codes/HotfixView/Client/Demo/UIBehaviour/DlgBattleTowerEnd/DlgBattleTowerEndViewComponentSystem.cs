
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleTowerEndViewComponentAwakeSystem : AwakeSystem<DlgBattleTowerEndViewComponent> 
	{
		protected override void Awake(DlgBattleTowerEndViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgBattleTowerEndViewComponentDestroySystem : DestroySystem<DlgBattleTowerEndViewComponent> 
	{
		protected override void Destroy(DlgBattleTowerEndViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
