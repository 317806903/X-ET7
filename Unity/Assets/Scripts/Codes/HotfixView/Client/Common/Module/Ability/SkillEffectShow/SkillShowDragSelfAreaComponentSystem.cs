using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragSelfAreaComponent))]
	public static class SkillShowDragSelfAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragSelfAreaComponentAwakeSystem : AwakeSystem<SkillShowDragSelfAreaComponent>
		{
			protected override void Awake(SkillShowDragSelfAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragSelfAreaComponentUpdateSystem : UpdateSystem<SkillShowDragSelfAreaComponent>
		{
			protected override void Update(SkillShowDragSelfAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragSelfAreaComponentDestroySystem : DestroySystem<SkillShowDragSelfAreaComponent>
		{
			protected override void Destroy(SkillShowDragSelfAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragSelfAreaComponent self, float3 pos, float radius, float3 direct)
		{
			self.CreateSkillShowEffect(pos, radius, direct);
		}

		public static void Update(this SkillShowDragSelfAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragSelfAreaComponent self, float3 pos, float radius, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Drag_SelfArea";
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

		public static void RemoveSkillShowEffect(this SkillShowDragSelfAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragSelfAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragSelfAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragSelfAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}