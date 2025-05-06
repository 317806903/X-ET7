using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragSelfUnitComponent))]
	public static class SkillShowDragSelfUnitComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragSelfUnitComponentAwakeSystem : AwakeSystem<SkillShowDragSelfUnitComponent>
		{
			protected override void Awake(SkillShowDragSelfUnitComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragSelfUnitComponentUpdateSystem : UpdateSystem<SkillShowDragSelfUnitComponent>
		{
			protected override void Update(SkillShowDragSelfUnitComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragSelfUnitComponentDestroySystem : DestroySystem<SkillShowDragSelfUnitComponent>
		{
			protected override void Destroy(SkillShowDragSelfUnitComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragSelfUnitComponent self, float3 pos, float radius, float3 direct)
		{
			self.CreateSkillShowEffect(pos, radius, direct);
		}

		public static void Update(this SkillShowDragSelfUnitComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragSelfUnitComponent self, float3 pos, float radius, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Drag_SelfUnit";
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

		public static void RemoveSkillShowEffect(this SkillShowDragSelfUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragSelfUnitComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragSelfUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragSelfUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}