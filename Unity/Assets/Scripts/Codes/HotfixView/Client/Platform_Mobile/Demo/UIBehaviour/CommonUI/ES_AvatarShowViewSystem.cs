
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class ES_AvatarShowViewComponentAwakeSystem : AwakeSystem<ES_AvatarShowViewComponent,Transform> 
	{
		protected override void Awake(ES_AvatarShowViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class ES_AvatarShowViewComponentDestroySystem : DestroySystem<ES_AvatarShowViewComponent> 
	{
		protected override void Destroy(ES_AvatarShowViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class ES_AvatarShowAwakeSystem : AwakeSystem<ES_AvatarShow,Transform> 
	{
		protected override void Awake(ES_AvatarShow self,Transform transform)
		{
			self.AddComponent<ES_AvatarShowViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class ES_AvatarShowDestroySystem : DestroySystem<ES_AvatarShow> 
	{
		protected override void Destroy(ES_AvatarShow self)
		{
		}
	}
}
