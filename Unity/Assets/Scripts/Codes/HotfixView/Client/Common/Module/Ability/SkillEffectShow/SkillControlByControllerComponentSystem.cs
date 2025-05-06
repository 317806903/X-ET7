using System;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillControlByControllerComponent))]
	public static class SkillControlByControllerComponentSystem
	{
		[ObjectSystem]
		public class SkillControlByControllerComponentAwakeSystem : AwakeSystem<SkillControlByControllerComponent>
		{
			protected override void Awake(SkillControlByControllerComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillControlByControllerComponentUpdateSystem : UpdateSystem<SkillControlByControllerComponent>
		{
			protected override void Update(SkillControlByControllerComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillControlByControllerComponentDestroySystem : DestroySystem<SkillControlByControllerComponent>
		{
			protected override void Destroy(SkillControlByControllerComponent self)
			{
				self.skillCfgId = "";
			}
		}

		public static void Init(this SkillControlByControllerComponent self, string skillCfgId, long unitId, bool isCameraPlayer, Camera camera, Action<SelectHandle> castSkill, bool isLeft = false)
		{
			self.skillCfgId = skillCfgId;
			self.unitId = unitId;
			self.isCameraPlayer = isCameraPlayer;
			self.camera = camera;
			self.isLeft = isLeft;

			self.InitSkillShow();
		}

		public static void InitSkillShow(this SkillControlByControllerComponent self)
		{
			float gameResScale = UnitHelper.GetGameResScale(self.DomainScene());
			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;
			if (actionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				float3 pos = cameraHitPosition;
				float radius = 2 * gameResScale;
				float3 direct = cameraDirect;
				self.AddComponent<SkillShowCameraUnitComponent>().Init(pos, radius, direct);
				self.UpdateSkillShowEffectCenterPos();
			}
			else if (actionCallParam is ActionCallShow_Camera_OtherArea actionCallShowCameraOtherArea)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				float3 pos = cameraHitPosition;
				float radius = actionCallShowCameraOtherArea.RangeRadius * gameResScale;
				float3 direct = cameraDirect;
				self.AddComponent<SkillShowCameraAreaComponent>().Init(pos, radius, direct);
			}
			else if (actionCallParam is ActionCallShow_Camera_RectangleArea actionCallShowCameraRectangleArea)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				float3 pos = cameraHitPosition;
				float width = actionCallShowCameraRectangleArea.RectangleArea.Width * gameResScale;
				//float length = actionCallShowCameraRectangleArea.RectangleArea.Length;
				float length = skillCfg.Dis * gameResScale;
				float3 direct = cameraDirect;
				self.AddComponent<SkillShowCameraRectangleAreaComponent>().Init(pos, width, length, direct);
			}
			else if (actionCallParam is ActionCallShow_Camera_UmbellateArea actionCallShowCameraUmbellateArea)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				float3 pos = cameraHitPosition;
				float angle = actionCallShowCameraUmbellateArea.UmbellateArea.Angle;
				//float radius = actionCallShowCameraUmbellateArea.UmbellateArea.Radius;
				float radius = skillCfg.Dis * gameResScale;
				float3 direct = cameraDirect;
				self.AddComponent<SkillShowCameraUmbellateAreaComponent>().Init(pos, angle, radius, direct);
			}
		}

		public static void RemoveSkillShowEffect(this SkillControlByControllerComponent self)
		{
			self.Dispose();
		}

		public static void Update(this SkillControlByControllerComponent self)
		{
			if (self.CheckUserInput())
			{
				self.UpdateSkillShowEffectCenterPos();
			}
			else
			{
				self.RemoveSkillShowEffect();
			}
		}

		public static Unit GetUnit(this SkillControlByControllerComponent self)
		{
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
			return unit;
		}

		public static Unit GetTargetUnit(this SkillControlByControllerComponent self)
		{
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.targetUnitId);
			return unit;
		}

		/// <summary>
		///检测用户当前输入
		/// </summary>
		/// <returns></returns>
		public static bool CheckUserInput(this SkillControlByControllerComponent self)
		{
			return ET.UGUIHelper.CheckUserInput();
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillControlByControllerComponent self)
		{
			float3 effectPosition;
			float3 efffectForward;

			if (self.isCameraPlayer)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				if (cameraHitPosition.Equals(float3.zero))
				{
					return;
				}

				effectPosition = cameraHitPosition;
				efffectForward = cameraDirect;
			}
			else
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				if (cameraHitPosition.Equals(float3.zero))
				{
					return;
				}

				Unit unit = self.GetUnit();
				effectPosition = unit.GetUnitClientPos();
				efffectForward = cameraDirect;
			}

			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;
			if (actionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
			{
				SelectHandle selectHandle = self.GetNearUnitSelectHandle();
				self.targetUnitId = 0;
				if (selectHandle != null && selectHandle.selectHandleType == SelectHandleType.SelectUnits)
				{
					if (selectHandle.unitIds != null && selectHandle.unitIds.Count > 0)
					{
						self.targetUnitId = selectHandle.unitIds[0];
					}
				}
				Unit targetUnit = self.GetTargetUnit();
				if (targetUnit != null)
				{
					float3 targetUnitPos = targetUnit.Position;
					float3 targetUnitForward = targetUnit.Forward;

					self.GetComponent<SkillShowCameraUnitComponent>().UpdateSkillShowEffectCenterPos(targetUnitPos, targetUnitForward);
				}
				else
				{
					self.GetComponent<SkillShowCameraUnitComponent>().UpdateSkillShowEffectCenterPos(float3.zero, float3.zero);
				}
			}
			else if (actionCallParam is ActionCallShow_Camera_OtherArea actionCallShowCameraOtherArea)
			{
				self.GetComponent<SkillShowCameraAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Camera_RectangleArea actionCallShowCameraRectangleArea)
			{
				self.GetComponent<SkillShowCameraRectangleAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Camera_UmbellateArea actionCallShowCameraUmbellateArea)
			{
				self.GetComponent<SkillShowCameraUmbellateAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}

		}

		public static SelectHandle GetNearUnitSelectHandle(this SkillControlByControllerComponent self)
		{
			Unit unit = self.GetUnit();

			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;

			SelectHandle selectHandle = null;
			if (actionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
			{
				(float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = self.GetControllerHit();
				if (cameraHitPosition.Equals(float3.zero))
				{
					return null;
				}

				bool isResetPos = true;
				float3 resetPos = cameraHitPosition;
				bool isResetForward = false;
				float3 resetForward = float3.zero;
				ActionContext actionContext = new();
				actionContext.skillCfgId = skillCfg.Id;
				actionContext.skillDis = skillCfg.Dis;
				SelectObjectCfg selectObjectCfg = skillCfg.SkillSelectAction_Ref;
				selectHandle = ET.Client.UnitViewHelper.CreateSelectHandle(unit, isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, ref actionContext);
			}

			return selectHandle;
		}

		public static SelectHandle GetSelectHandle(this SkillControlByControllerComponent self)
		{
			SelectHandle selectHandle = SelectHandle.Create();

			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;
			if (actionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
			{
				if (self.targetUnitId != 0)
				{
					selectHandle.unitIds = ListComponent<long>.Create();
					selectHandle.unitIds.Add(self.targetUnitId);
				}
			}
			else if (actionCallParam is ActionCallShow_Camera_OtherArea actionCallShowCameraOtherArea)
			{
				selectHandle.position = self.GetComponent<SkillShowCameraAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowCameraAreaComponent>().GetSkillShowEffectForward();
			}
			else if (actionCallParam is ActionCallShow_Camera_RectangleArea actionCallShowCameraRectangleArea)
			{
				selectHandle.position = self.GetComponent<SkillShowCameraRectangleAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowCameraRectangleAreaComponent>().GetSkillShowEffectForward();
			}
			else if (actionCallParam is ActionCallShow_Camera_UmbellateArea actionCallShowCameraUmbellateArea)
			{
				selectHandle.position = self.GetComponent<SkillShowCameraUmbellateAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowCameraUmbellateAreaComponent>().GetSkillShowEffectForward();
			}

			return selectHandle;
		}

		public static (float3, float3, float3) GetControllerHit(this SkillControlByControllerComponent self){
#if Platform_Quest			
			return UGUIHelper.GetControllerHit(self.isLeft);
#else
			return (float3.zero, float3.zero, float3.zero);
#endif
		}

	}
}