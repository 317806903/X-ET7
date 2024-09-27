
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgTutorialsViewComponentAwakeSystem : AwakeSystem<DlgTutorialsViewComponent>
	{
		protected override void Awake(DlgTutorialsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgTutorialsViewComponentDestroySystem : DestroySystem<DlgTutorialsViewComponent>
	{
		protected override void Destroy(DlgTutorialsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
