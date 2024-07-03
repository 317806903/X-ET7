
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_RankViewComponentAwakeSystem : AwakeSystem<EPage_RankViewComponent,Transform> 
	{
		protected override void Awake(EPage_RankViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_RankViewComponentDestroySystem : DestroySystem<EPage_RankViewComponent> 
	{
		protected override void Destroy(EPage_RankViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_RankAwakeSystem : AwakeSystem<EPage_Rank,Transform> 
	{
		protected override void Awake(EPage_Rank self,Transform transform)
		{
			self.AddComponent<EPage_RankViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_RankDestroySystem : DestroySystem<EPage_Rank> 
	{
		protected override void Destroy(EPage_Rank self)
		{
		}
	}
}
