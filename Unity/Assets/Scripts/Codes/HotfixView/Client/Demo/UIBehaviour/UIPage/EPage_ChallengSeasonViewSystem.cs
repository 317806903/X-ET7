
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_ChallengSeasonViewComponentAwakeSystem : AwakeSystem<EPage_ChallengSeasonViewComponent,Transform>
	{
		protected override void Awake(EPage_ChallengSeasonViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_ChallengSeasonViewComponentDestroySystem : DestroySystem<EPage_ChallengSeasonViewComponent>
	{
		protected override void Destroy(EPage_ChallengSeasonViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_ChallengSeasonAwakeSystem : AwakeSystem<EPage_ChallengSeason,Transform>
	{
		protected override void Awake(EPage_ChallengSeason self,Transform transform)
		{
			self.AddComponent<EPage_ChallengSeasonViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_ChallengSeasonDestroySystem : DestroySystem<EPage_ChallengSeason>
	{
		protected override void Destroy(EPage_ChallengSeason self)
		{
		}
	}
}
