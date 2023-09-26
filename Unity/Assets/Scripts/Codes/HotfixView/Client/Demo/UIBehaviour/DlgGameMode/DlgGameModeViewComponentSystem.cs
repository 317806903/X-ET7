
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameModeViewComponentAwakeSystem : AwakeSystem<DlgGameModeViewComponent>
	{
		protected override void Awake(DlgGameModeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameModeViewComponentDestroySystem : DestroySystem<DlgGameModeViewComponent>
	{
		protected override void Destroy(DlgGameModeViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
