
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPersonalInformationViewComponentAwakeSystem : AwakeSystem<DlgPersonalInformationViewComponent>
	{
		protected override void Awake(DlgPersonalInformationViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPersonalInformationViewComponentDestroySystem : DestroySystem<DlgPersonalInformationViewComponent>
	{
		protected override void Destroy(DlgPersonalInformationViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
