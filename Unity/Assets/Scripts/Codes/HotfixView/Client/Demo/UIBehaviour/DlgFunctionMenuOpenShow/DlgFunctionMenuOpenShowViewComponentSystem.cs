
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgFunctionMenuOpenShowViewComponentAwakeSystem : AwakeSystem<DlgFunctionMenuOpenShowViewComponent>
	{
		protected override void Awake(DlgFunctionMenuOpenShowViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgFunctionMenuOpenShowViewComponentDestroySystem : DestroySystem<DlgFunctionMenuOpenShowViewComponent>
	{
		protected override void Destroy(DlgFunctionMenuOpenShowViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
