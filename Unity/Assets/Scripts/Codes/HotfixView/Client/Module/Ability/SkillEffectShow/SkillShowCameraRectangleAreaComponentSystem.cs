using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowCameraRectangleAreaComponent))]
	public static class SkillShowCameraRectangleAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowCameraRectangleAreaComponentAwakeSystem : AwakeSystem<SkillShowCameraRectangleAreaComponent>
		{
			protected override void Awake(SkillShowCameraRectangleAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowCameraRectangleAreaComponentUpdateSystem : UpdateSystem<SkillShowCameraRectangleAreaComponent>
		{
			protected override void Update(SkillShowCameraRectangleAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowCameraRectangleAreaComponentDestroySystem : DestroySystem<SkillShowCameraRectangleAreaComponent>
		{
			protected override void Destroy(SkillShowCameraRectangleAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowCameraRectangleAreaComponent self, float3 pos, float width, float length, float3 direct)
		{
			self.CreateSkillShowEffect(pos, width, length, direct);
		}

		public static void Update(this SkillShowCameraRectangleAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowCameraRectangleAreaComponent self, float3 pos, float width, float length, float3 direct)
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

		public static void RemoveSkillShowEffect(this SkillShowCameraRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowCameraRectangleAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowCameraRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowCameraRectangleAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}