
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgUpdateViewComponentAwakeSystem : AwakeSystem<DlgUpdateViewComponent> 
	{
		protected override void Awake(DlgUpdateViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgUpdateViewComponentDestroySystem : DestroySystem<DlgUpdateViewComponent> 
	{
		protected override void Destroy(DlgUpdateViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
