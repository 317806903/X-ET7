
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARRoomViewComponentAwakeSystem : AwakeSystem<DlgARRoomViewComponent>
	{
		protected override void Awake(DlgARRoomViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARRoomViewComponentDestroySystem : DestroySystem<DlgARRoomViewComponent>
	{
		protected override void Destroy(DlgARRoomViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
