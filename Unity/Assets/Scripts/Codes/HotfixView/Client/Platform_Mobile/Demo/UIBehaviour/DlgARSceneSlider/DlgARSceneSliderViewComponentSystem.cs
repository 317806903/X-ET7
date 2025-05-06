
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARSceneSliderViewComponentAwakeSystem : AwakeSystem<DlgARSceneSliderViewComponent>
	{
		protected override void Awake(DlgARSceneSliderViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARSceneSliderViewComponentDestroySystem : DestroySystem<DlgARSceneSliderViewComponent>
	{
		protected override void Destroy(DlgARSceneSliderViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
