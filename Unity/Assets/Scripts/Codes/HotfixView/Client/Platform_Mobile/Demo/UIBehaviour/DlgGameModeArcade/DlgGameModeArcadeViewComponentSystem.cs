
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameModeArcadeViewComponentAwakeSystem : AwakeSystem<DlgGameModeArcadeViewComponent>
	{
		protected override void Awake(DlgGameModeArcadeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameModeArcadeViewComponentDestroySystem : DestroySystem<DlgGameModeArcadeViewComponent>
	{
		protected override void Destroy(DlgGameModeArcadeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
