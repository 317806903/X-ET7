
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgARRoomPVESeasonViewComponentAwakeSystem : AwakeSystem<DlgARRoomPVESeasonViewComponent>
	{
		protected override void Awake(DlgARRoomPVESeasonViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgARRoomPVESeasonViewComponentDestroySystem : DestroySystem<DlgARRoomPVESeasonViewComponent>
	{
		protected override void Destroy(DlgARRoomPVESeasonViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
