
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgVideoShowViewComponentAwakeSystem : AwakeSystem<DlgVideoShowViewComponent>
	{
		protected override void Awake(DlgVideoShowViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgVideoShowViewComponentDestroySystem : DestroySystem<DlgVideoShowViewComponent>
	{
		protected override void Destroy(DlgVideoShowViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
