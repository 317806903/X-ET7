
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgArcadeCoinViewComponentAwakeSystem : AwakeSystem<DlgArcadeCoinViewComponent>
	{
		protected override void Awake(DlgArcadeCoinViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgArcadeCoinViewComponentDestroySystem : DestroySystem<DlgArcadeCoinViewComponent>
	{
		protected override void Destroy(DlgArcadeCoinViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
