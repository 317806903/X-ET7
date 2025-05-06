
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgRankPowerupSeasonViewComponentAwakeSystem : AwakeSystem<DlgRankPowerupSeasonViewComponent>
	{
		protected override void Awake(DlgRankPowerupSeasonViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgRankPowerupSeasonViewComponentDestroySystem : DestroySystem<DlgRankPowerupSeasonViewComponent>
	{
		protected override void Destroy(DlgRankPowerupSeasonViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
