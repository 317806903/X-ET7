
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARSceneSliderSimpleViewComponentAwakeSystem : AwakeSystem<DlgARSceneSliderSimpleViewComponent>
	{
		protected override void Awake(DlgARSceneSliderSimpleViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARSceneSliderSimpleViewComponentDestroySystem : DestroySystem<DlgARSceneSliderSimpleViewComponent>
	{
		protected override void Destroy(DlgARSceneSliderSimpleViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
