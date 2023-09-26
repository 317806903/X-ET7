
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARHallViewComponentAwakeSystem : AwakeSystem<DlgARHallViewComponent> 
	{
		protected override void Awake(DlgARHallViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgARHallViewComponentDestroySystem : DestroySystem<DlgARHallViewComponent> 
	{
		protected override void Destroy(DlgARHallViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
