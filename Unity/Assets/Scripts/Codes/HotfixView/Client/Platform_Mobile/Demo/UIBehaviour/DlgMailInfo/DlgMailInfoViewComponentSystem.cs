
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgMailInfoViewComponentAwakeSystem : AwakeSystem<DlgMailInfoViewComponent>
	{
		protected override void Awake(DlgMailInfoViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgMailInfoViewComponentDestroySystem : DestroySystem<DlgMailInfoViewComponent>
	{
		protected override void Destroy(DlgMailInfoViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
