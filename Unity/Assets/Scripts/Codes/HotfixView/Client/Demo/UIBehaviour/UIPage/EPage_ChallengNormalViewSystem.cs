
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_ChallengNormalViewComponentAwakeSystem : AwakeSystem<EPage_ChallengNormalViewComponent,Transform>
	{
		protected override void Awake(EPage_ChallengNormalViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_ChallengNormalViewComponentDestroySystem : DestroySystem<EPage_ChallengNormalViewComponent>
	{
		protected override void Destroy(EPage_ChallengNormalViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_ChallengNormalAwakeSystem : AwakeSystem<EPage_ChallengNormal,Transform>
	{
		protected override void Awake(EPage_ChallengNormal self,Transform transform)
		{
			self.AddComponent<EPage_ChallengNormalViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_ChallengNormalDestroySystem : DestroySystem<EPage_ChallengNormal>
	{
		protected override void Destroy(EPage_ChallengNormal self)
		{
		}
	}
}
