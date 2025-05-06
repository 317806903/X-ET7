
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgMailViewComponentAwakeSystem : AwakeSystem<DlgMailViewComponent>
	{
		protected override void Awake(DlgMailViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgMailViewComponentDestroySystem : DestroySystem<DlgMailViewComponent>
	{
		protected override void Destroy(DlgMailViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
