using System;
using DG.Tweening;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillShowDragUmbellateAreaComponent))]
	public static class SkillShowDragUmbellateAreaComponentSystem
	{
		[ObjectSystem]
		public class SkillShowDragUmbellateAreaComponentAwakeSystem : AwakeSystem<SkillShowDragUmbellateAreaComponent>
		{
			protected override void Awake(SkillShowDragUmbellateAreaComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillShowDragUmbellateAreaComponentUpdateSystem : UpdateSystem<SkillShowDragUmbellateAreaComponent>
		{
			protected override void Update(SkillShowDragUmbellateAreaComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillShowDragUmbellateAreaComponentDestroySystem : DestroySystem<SkillShowDragUmbellateAreaComponent>
		{
			protected override void Destroy(SkillShowDragUmbellateAreaComponent self)
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static void Init(this SkillShowDragUmbellateAreaComponent self, float3 pos, float angle, float radius, float3 direct)
		{
			self.CreateSkillShowEffect(pos, angle, radius, direct);
		}

		public static void Update(this SkillShowDragUmbellateAreaComponent self)
		{
		}

		public static void CreateSkillShowEffect(this SkillShowDragUmbellateAreaComponent self, float3 pos, float angle, float radius, float3 direct)
		{
			string resName = "ResEffect_SkillShow_Camera_UmbellateArea";
			ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
			GameObject skillShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
			skillShowGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
			skillShowGo.transform.localPosition = Vector3.zero;
			skillShowGo.transform.localScale = Vector3.one;

			self.skillShowEffectTrans = skillShowGo.transform;

			self.skillShowEffectTrans.gameObject.SetActive(true);
			self.skillShowEffectTrans.position = pos;

			self.skillShowEffectTrans.localScale = new Vector3(radius, 1, radius);
			direct.y = 0;
			self.skillShowEffectTrans.forward = direct;

			Werewolf.StatusIndicators.Components.Cone cone = self.skillShowEffectTrans.gameObject.GetComponentInChildren<Werewolf.StatusIndicators.Components.Cone>();
			if (cone != null)
			{
				cone.Angle = angle;
			}
		}

		public static void RemoveSkillShowEffect(this SkillShowDragUmbellateAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				GameObjectPoolHelper.ReturnObjectToPool(self.skillShowEffectTrans.gameObject);
				self.skillShowEffectTrans = null;
			}
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillShowDragUmbellateAreaComponent self, float3 centerPos, float3 direct)
		{
			if (self.skillShowEffectTrans != null)
			{
				self.skillShowEffectTrans.DOMove(centerPos, 0.2f);
				//self.skillShowEffectTrans.position = centerPos;
				direct.y = 0;
				self.skillShowEffectTrans.forward = direct;
			}
		}

		public static Vector3 GetSkillShowEffectCenterPos(this SkillShowDragUmbellateAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.position;
			}
			return Vector3.zero;
		}

		public static Vector3 GetSkillShowEffectForward(this SkillShowDragUmbellateAreaComponent self)
		{
			if (self.skillShowEffectTrans != null)
			{
				return self.skillShowEffectTrans.forward;
			}
			return Vector3.zero;
		}
	}
}
