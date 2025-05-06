
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgBattleCfgChooseViewComponentAwakeSystem : AwakeSystem<DlgBattleCfgChooseViewComponent>
	{
		protected override void Awake(DlgBattleCfgChooseViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgBattleCfgChooseViewComponentDestroySystem : DestroySystem<DlgBattleCfgChooseViewComponent>
	{
		protected override void Destroy(DlgBattleCfgChooseViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
