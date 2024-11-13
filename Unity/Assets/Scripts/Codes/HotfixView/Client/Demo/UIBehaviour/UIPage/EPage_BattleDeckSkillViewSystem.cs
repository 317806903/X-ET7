
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class EPage_BattleDeckSkillViewComponentAwakeSystem : AwakeSystem<EPage_BattleDeckSkillViewComponent,Transform> 
	{
		protected override void Awake(EPage_BattleDeckSkillViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class EPage_BattleDeckSkillViewComponentDestroySystem : DestroySystem<EPage_BattleDeckSkillViewComponent> 
	{
		protected override void Destroy(EPage_BattleDeckSkillViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class EPage_BattleDeckSkillAwakeSystem : AwakeSystem<EPage_BattleDeckSkill,Transform> 
	{
		protected override void Awake(EPage_BattleDeckSkill self,Transform transform)
		{
			self.AddComponent<EPage_BattleDeckSkillViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class EPage_BattleDeckSkillDestroySystem : DestroySystem<EPage_BattleDeckSkill> 
	{
		protected override void Destroy(EPage_BattleDeckSkill self)
		{
		}
	}
}
