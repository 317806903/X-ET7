
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgKnapsackViewComponentAwakeSystem : AwakeSystem<DlgKnapsackViewComponent>
	{
		protected override void Awake(DlgKnapsackViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgKnapsackViewComponentDestroySystem : DestroySystem<DlgKnapsackViewComponent>
	{
		protected override void Destroy(DlgKnapsackViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
