using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragOtherAreaComponent))]
	public static class SkillShowDragOtherAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragOtherAreaComponentAwakeSystem : AwakeSystem<SkillShowDragOtherAreaComponent>
		{
			protected override void Awake(SkillShowDragOtherAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragOtherAreaComponentUpdateSystem : UpdateSystem<SkillShowDragOtherAreaComponent>
		{
			protected override void Update(SkillShowDragOtherAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragOtherAreaComponentDestroySystem : DestroySystem<SkillShowDragOtherAreaComponent>
		{
			protected override void Destroy(SkillShowDragOtherAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragOtherAreaComponent self, float3 pointRangePos, float pointRangeValue, float3 outRangePos, float outRangeValue, float3 direct)
		{
			self.CreateSkillShowEffect(pointRangePos, pointRangeValue, outRangePos, outRangeValue, direct);
		}

		public static void Update(this SkillShowDragOtherAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragOtherAreaComponent self, float3 pointRangePos, float pointRangeValue, float3 outRangePos, float outRangeValue, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Drag_OtherArea";
			ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
			GameObject skillShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
			skillShowGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
			skillShowGo.transform.localPosition = Vector3.zero;
			skillShowGo.transform.localScale = Vector3.one;

			self.skillShowEffectTrans = skillShowGo.transform;
			self.skillShowEffectPointRangeTrans = skillShowGo.transform.Find("PointRange");
			self.skillShowEffectOutRangeTrans = skillShowGo.transform.Find("OutRange");

			self.skillShowEffectPointRangeTrans.position = pointRangePos;
			self.skillShowEffectOutRangeTrans.position = outRangePos;

			self.skillShowEffectPointRangeTrans.localScale = Vector3.one * pointRangeValue;
			self.skillShowEffectOutRangeTrans.localScale = Vector3.one * outRangeValue;

			direct.y = 0;
			self.skillShowEffectOutRangeTrans.forward = direct;
		}

		public static void RemoveSkillShowEffect(this SkillShowDragOtherAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragOtherAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectOutRangeTrans != null)
			{
				self.skillShowEffectOutRangeTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectOutRangeTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectOutRangeTrans.forward = direct;
			}
		}

		public static void UpdateSkillShowEffectPointPos(this SkillShowDragOtherAreaComponent self, float3 pointPos)
		{
			if (self.skillShowEffectPointRangeTrans != null)
			{
				self.skillShowEffectPointRangeTrans.DOMove(pointPos, 0.2f);
				//self.skillShowEffectPointRangeTrans.position = pointPos;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragOtherAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragOtherAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}