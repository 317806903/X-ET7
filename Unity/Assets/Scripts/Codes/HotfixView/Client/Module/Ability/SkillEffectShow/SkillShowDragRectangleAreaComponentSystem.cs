using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragRectangleAreaComponent))]
	public static class SkillShowDragRectangleAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragRectangleAreaComponentAwakeSystem : AwakeSystem<SkillShowDragRectangleAreaComponent>
		{
			protected override void Awake(SkillShowDragRectangleAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragRectangleAreaComponentUpdateSystem : UpdateSystem<SkillShowDragRectangleAreaComponent>
		{
			protected override void Update(SkillShowDragRectangleAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragRectangleAreaComponentDestroySystem : DestroySystem<SkillShowDragRectangleAreaComponent>
		{
			protected override void Destroy(SkillShowDragRectangleAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragRectangleAreaComponent self, float3 pos, float width, float length, float3 direct)
		{
			self.CreateSkillShowEffect(pos, width, length, direct);
		}

		public static void Update(this SkillShowDragRectangleAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragRectangleAreaComponent self, float3 pos, float width, float length, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Camera_RectangleArea";
			ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
			GameObject skillShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
			skillShowGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
			skillShowGo.transform.localPosition = Vector3.zero;
			skillShowGo.transform.localScale = Vector3.one;

			self.skillShowEffectTrans = skillShowGo.transform;

			self.skillShowEffectTrans.gameObject.SetActive(true);
			self.skillShowEffectTrans.position = pos;

			self.skillShowEffectTrans.localScale = new Vector3(width, 1, length);
			direct.y = 0;
			self.skillShowEffectTrans.forward = direct;
		}

		public static void RemoveSkillShowEffect(this SkillShowDragRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragRectangleAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}