
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgHallViewComponentAwakeSystem : AwakeSystem<DlgHallViewComponent> 
	{
		protected override void Awake(DlgHallViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgHallViewComponentDestroySystem : DestroySystem<DlgHallViewComponent> 
	{
		protected override void Destroy(DlgHallViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
