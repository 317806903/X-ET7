
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgVideoShowSmallViewComponentAwakeSystem : AwakeSystem<DlgVideoShowSmallViewComponent>
	{
		protected override void Awake(DlgVideoShowSmallViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgVideoShowSmallViewComponentDestroySystem : DestroySystem<DlgVideoShowSmallViewComponent>
	{
		protected override void Destroy(DlgVideoShowSmallViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
