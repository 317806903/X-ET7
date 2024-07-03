
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgPersionalAvatarViewComponentAwakeSystem : AwakeSystem<DlgPersionalAvatarViewComponent>
	{
		protected override void Awake(DlgPersionalAvatarViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}

	[ObjectSystem]
	public class DlgPersionalAvatarViewComponentDestroySystem : DestroySystem<DlgPersionalAvatarViewComponent>
	{
		protected override void Destroy(DlgPersionalAvatarViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
