
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgZpbTestViewComponentAwakeSystem : AwakeSystem<DlgZpbTestViewComponent>
	{
		protected override void Awake(DlgZpbTestViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgZpbTestViewComponentDestroySystem : DestroySystem<DlgZpbTestViewComponent>
	{
		protected override void Destroy(DlgZpbTestViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
