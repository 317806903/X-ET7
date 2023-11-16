
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class ES_AvatarShowAwakeSystem : AwakeSystem<ES_AvatarShow,Transform> 
	{
		protected override void Awake(ES_AvatarShow self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ES_AvatarShowDestroySystem : DestroySystem<ES_AvatarShow> 
	{
		protected override void Destroy(ES_AvatarShow self)
		{
			self.DestroyWidget();
		}
	}
}
