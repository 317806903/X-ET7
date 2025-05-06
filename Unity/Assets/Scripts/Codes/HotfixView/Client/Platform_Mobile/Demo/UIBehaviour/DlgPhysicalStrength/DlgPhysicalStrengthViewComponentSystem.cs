
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPhysicalStrengthViewComponentAwakeSystem : AwakeSystem<DlgPhysicalStrengthViewComponent>
	{
		protected override void Awake(DlgPhysicalStrengthViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPhysicalStrengthViewComponentDestroySystem : DestroySystem<DlgPhysicalStrengthViewComponent>
	{
		protected override void Destroy(DlgPhysicalStrengthViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
