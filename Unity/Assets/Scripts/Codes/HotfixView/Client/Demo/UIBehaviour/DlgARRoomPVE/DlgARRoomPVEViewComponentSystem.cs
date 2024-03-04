
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARRoomPVEViewComponentAwakeSystem : AwakeSystem<DlgARRoomPVEViewComponent>
	{
		protected override void Awake(DlgARRoomPVEViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARRoomPVEViewComponentDestroySystem : DestroySystem<DlgARRoomPVEViewComponent>
	{
		protected override void Destroy(DlgARRoomPVEViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
