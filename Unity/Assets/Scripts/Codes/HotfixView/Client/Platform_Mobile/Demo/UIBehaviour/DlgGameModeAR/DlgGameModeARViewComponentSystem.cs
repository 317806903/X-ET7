
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameModeARViewComponentAwakeSystem : AwakeSystem<DlgGameModeARViewComponent>
	{
		protected override void Awake(DlgGameModeARViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameModeARViewComponentDestroySystem : DestroySystem<DlgGameModeARViewComponent>
	{
		protected override void Destroy(DlgGameModeARViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
