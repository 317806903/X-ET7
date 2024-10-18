using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragOtherUnitComponent))]
	public static class SkillShowDragOtherUnitComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragOtherUnitComponentAwakeSystem : AwakeSystem<SkillShowDragOtherUnitComponent>
		{
			protected override void Awake(SkillShowDragOtherUnitComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragOtherUnitComponentUpdateSystem : UpdateSystem<SkillShowDragOtherUnitComponent>
		{
			protected override void Update(SkillShowDragOtherUnitComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragOtherUnitComponentDestroySystem : DestroySystem<SkillShowDragOtherUnitComponent>
		{
			protected override void Destroy(SkillShowDragOtherUnitComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragOtherUnitComponent self, float3 pointRangePos, float pointRangeValue, float3 outRangePos, float outRangeValue, float3 direct)
		{
			self.CreateSkillShowEffect(pointRangePos, pointRangeValue, outRangePos, outRangeValue, direct);
		}

		public static void Update(this SkillShowDragOtherUnitComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragOtherUnitComponent self, float3 pointRangePos, float pointRangeValue, float3 outRangePos, float outRangeValue, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Drag_OtherUnit";
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

		public static void RemoveSkillShowEffect(this SkillShowDragOtherUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragOtherUnitComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectOutRangeTrans != null)
			{
				self.skillShowEffectOutRangeTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectOutRangeTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectOutRangeTrans.forward = direct;
			}
		}

		public static void UpdateSkillShowEffectPointPos(this SkillShowDragOtherUnitComponent self, float3 pointPos)
		{
			if (self.skillShowEffectPointRangeTrans != null)
			{
				self.skillShowEffectPointRangeTrans.DOMove(pointPos, 0.2f);
				//self.skillShowEffectPointRangeTrans.position = centerPos;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragOtherUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragOtherUnitComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}