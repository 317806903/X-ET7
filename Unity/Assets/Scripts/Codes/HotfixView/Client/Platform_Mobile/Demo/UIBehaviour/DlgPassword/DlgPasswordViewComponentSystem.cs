
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPasswordViewComponentAwakeSystem : AwakeSystem<DlgPasswordViewComponent>
	{
		protected override void Awake(DlgPasswordViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPasswordViewComponentDestroySystem : DestroySystem<DlgPasswordViewComponent>
	{
		protected override void Destroy(DlgPasswordViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
