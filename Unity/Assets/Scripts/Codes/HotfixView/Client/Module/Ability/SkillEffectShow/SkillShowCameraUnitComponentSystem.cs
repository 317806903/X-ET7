using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowCameraUnitComponent))]
	public static class SkillShowCameraUnitComponentSystem
	{
		[ObjectSystem]
		public class SkillShowCameraUnitComponentAwakeSystem : AwakeSystem<SkillShowCameraUnitComponent>
		{
			protected override void Awake(SkillShowCameraUnitComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowCameraUnitComponentUpdateSystem : UpdateSystem<SkillShowCameraUnitComponent>
		{
			protected override void Update(SkillShowCameraUnitComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowCameraUnitComponentDestroySystem : DestroySystem<SkillShowCameraUnitComponent>
		{
			protected override void Destroy(SkillShowCameraUnitComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowCameraUnitComponent self, float3 pos, float radius, float3 direct)
		{
			self.CreateSkillShowEffect(pos, radius, direct);
		}

		public static void Update(this SkillShowCameraUnitComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowCameraUnitComponent self, float3 pos, float radius, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Camera_OtherUnit";
			ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
			GameObject skillShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
			skillShowGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
			skillShowGo.transform.localPosition = Vector3.zero;
			skillShowGo.transform.localScale = Vector3.one;

			self.skillShowEffectTrans = skillShowGo.transform;

			self.skillShowEffectTrans.gameObject.SetActive(true);
			self.skillShowEffectTrans.position = pos;

			self.skillShowEffectTrans.localScale = Vector3.one * radius;
			direct.y = 0;
			self.skillShowEffectTrans.forward = direct;
		}

		public static void RemoveSkillShowEffect(this SkillShowCameraUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowCameraUnitComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}
	}
}