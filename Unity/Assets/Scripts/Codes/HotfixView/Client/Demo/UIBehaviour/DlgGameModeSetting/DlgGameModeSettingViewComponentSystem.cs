
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameModeSettingViewComponentAwakeSystem : AwakeSystem<DlgGameModeSettingViewComponent>
	{
		protected override void Awake(DlgGameModeSettingViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgGameModeSettingViewComponentDestroySystem : DestroySystem<DlgGameModeSettingViewComponent>
	{
		protected override void Destroy(DlgGameModeSettingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
