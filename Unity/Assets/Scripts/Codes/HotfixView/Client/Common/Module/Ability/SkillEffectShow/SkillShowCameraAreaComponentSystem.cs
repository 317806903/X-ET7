using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowCameraAreaComponent))]
	public static class SkillShowCameraAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowCameraAreaComponentAwakeSystem : AwakeSystem<SkillShowCameraAreaComponent>
		{
			protected override void Awake(SkillShowCameraAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowCameraAreaComponentUpdateSystem : UpdateSystem<SkillShowCameraAreaComponent>
		{
			protected override void Update(SkillShowCameraAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowCameraAreaComponentDestroySystem : DestroySystem<SkillShowCameraAreaComponent>
		{
			protected override void Destroy(SkillShowCameraAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowCameraAreaComponent self, float3 pos, float radius, float3 direct)
		{
			self.CreateSkillShowEffect(pos, radius, direct);
		}

		public static void Update(this SkillShowCameraAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowCameraAreaComponent self, float3 pos, float radius, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Camera_OtherArea";
			ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
			GameObject skillShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
			skillShowGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
			skillShowGo.transform.localPosition = Vector3.zero;
			skillShowGo.transform.localScale = Vector3.one;

			self.skillShowEffectTrans = skillShowGo.transform;

			self.skillShowEffectTrans.position = pos;

			self.skillShowEffectTrans.localScale = Vector3.one * radius;
			direct.y = 0;
			self.skillShowEffectTrans.forward = direct;
		}

		public static void RemoveSkillShowEffect(this SkillShowCameraAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowCameraAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowCameraAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowCameraAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}