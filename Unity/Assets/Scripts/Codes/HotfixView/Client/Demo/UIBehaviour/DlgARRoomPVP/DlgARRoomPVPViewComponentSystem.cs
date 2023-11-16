
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARRoomPVPViewComponentAwakeSystem : AwakeSystem<DlgARRoomPVPViewComponent>
	{
		protected override void Awake(DlgARRoomPVPViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARRoomPVPViewComponentDestroySystem : DestroySystem<DlgARRoomPVPViewComponent>
	{
		protected override void Destroy(DlgARRoomPVPViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
