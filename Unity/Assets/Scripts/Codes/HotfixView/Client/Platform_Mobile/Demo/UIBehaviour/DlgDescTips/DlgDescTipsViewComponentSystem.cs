
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgDescTipsViewComponentAwakeSystem : AwakeSystem<DlgDescTipsViewComponent>
	{
		protected override void Awake(DlgDescTipsViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgDescTipsViewComponentDestroySystem : DestroySystem<DlgDescTipsViewComponent>
	{
		protected override void Destroy(DlgDescTipsViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
