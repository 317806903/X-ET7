using System;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(SkillControlByDragComponent))]
	public static class SkillControlByDragComponentSystem
	{
		[ObjectSystem]
		public class SkillControlByDragComponentAwakeSystem : AwakeSystem<SkillControlByDragComponent>
		{
			protected override void Awake(SkillControlByDragComponent self)
			{
			}
		}

		[ObjectSystem]
		public class SkillControlByDragComponentUpdateSystem : UpdateSystem<SkillControlByDragComponent>
		{
			protected override void Update(SkillControlByDragComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class SkillControlByDragComponentDestroySystem : DestroySystem<SkillControlByDragComponent>
		{
			protected override void Destroy(SkillControlByDragComponent self)
			{
				self.skillCfgId = "";
			}
		}

		public static void Init(this SkillControlByDragComponent self, string skillCfgId, long unitId, Camera camera, Action<SelectHandle> castSkill)
		{
			self.skillCfgId = skillCfgId;
			self.unitId = unitId;
			self.camera = camera;
			self.castSkill = castSkill;


			self.InitSkillShow();
		}

		public static void InitSkillShow(this SkillControlByDragComponent self)
		{
			float gameResScale = UnitHelper.GetGameResScale(self.DomainScene());
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
			float3 unitPos = unit.Position;
			float3 unitForward = unit.Forward;

			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;
			if (actionCallParam is ActionCallShow_Drag_SelfUnit actionCallShowDragSelfUnit)
			{
				float3 pos = unitPos;
				float radius = 2 * gameResScale;
				float3 direct = unitForward;
				self.AddComponent<SkillShowDragSelfUnitComponent>().Init(pos, radius, direct);
				self.UpdateSkillShowEffectCenterPos();
			}
			else if (actionCallParam is ActionCallShow_Drag_SelfArea actionCallShowDragSelfArea)
			{
				float3 pos = unitPos;
				float radius = skillCfg.Dis * gameResScale;
				float3 direct = unitForward;
				self.AddComponent<SkillShowDragSelfAreaComponent>().Init(pos, radius, direct);
				self.UpdateSkillShowEffectCenterPos();
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherUnit actionCallShowDragOtherUnit)
			{
				float3 pointRangePos = unitPos;
				float pointRangeValue = 2 * gameResScale;
				float3 outRangePos = unitPos;
				float outRangeValue = skillCfg.Dis * gameResScale;
				float3 direct = unitForward;
				self.AddComponent<SkillShowDragOtherUnitComponent>().Init(pointRangePos, pointRangeValue, outRangePos, outRangeValue, direct);
				self.UpdateSkillShowEffectCenterPos();
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherArea actionCallShowDragOtherArea)
			{
				float3 pointRangePos;
				float pointRangeValue = actionCallShowDragOtherArea.RangeRadius * gameResScale;
				float3 outRangePos = unitPos;
				float outRangeValue = skillCfg.Dis * gameResScale;
				float3 direct = unitForward;
				pointRangePos = outRangePos + direct * outRangeValue;
				self.AddComponent<SkillShowDragOtherAreaComponent>().Init(pointRangePos, pointRangeValue, outRangePos, outRangeValue, direct);
			}
			else if (actionCallParam is ActionCallShow_Drag_RectangleArea actionCallShowDragRectangleArea)
			{
				float3 pos = unitPos;
				float width = actionCallShowDragRectangleArea.RectangleArea.Width * gameResScale;
				//float length = actionCallShowDragRectangleArea.RectangleArea.Length * gameResScale;
				float length = skillCfg.Dis * gameResScale;
				float3 direct = unitForward;
				self.AddComponent<SkillShowDragRectangleAreaComponent>().Init(pos, width, length, direct);
			}
			else if (actionCallParam is ActionCallShow_Drag_UmbellateArea actionCallShowDragUmbellateArea)
			{
				float3 pos = unitPos;
				float angle = actionCallShowDragUmbellateArea.UmbellateArea.Angle;
				//float radius = actionCallShowDragUmbellateArea.UmbellateArea.Radius * gameResScale;
				float radius = skillCfg.Dis * gameResScale;
				float3 direct = unitForward;
				self.AddComponent<SkillShowDragUmbellateAreaComponent>().Init(pos, angle, radius, direct);
			}
		}

		public static void RemoveSkillShowEffect(this SkillControlByDragComponent self)
		{
			self.Dispose();
		}

		public static void Update(this SkillControlByDragComponent self)
		{
			if (self.CheckUserInput())
			{
				self.isClickUGUI = ET.UGUIHelper.IsClickUGUI();
				// if (self.isClickUGUI)
				// {
				//     return;
				// }

				self.UpdateSkillShowEffectCenterPos();
			}
			else
			{
				if (self.isClickUGUI == false)
				{
					self.castSkill(self.GetSelectHandle());
				}
				self.RemoveSkillShowEffect();
			}
		}

		public static Unit GetUnit(this SkillControlByDragComponent self)
		{
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
			return unit;
		}

		public static Unit GetTargetUnit(this SkillControlByDragComponent self)
		{
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.targetUnitId);
			return unit;
		}

		/// <summary>
		///检测用户当前输入
		/// </summary>
		/// <returns></returns>
		public static bool CheckUserInput(this SkillControlByDragComponent self)
		{
			return ET.UGUIHelper.CheckUserInput();
		}

		public static void UpdateSkillShowEffectCenterPos(this SkillControlByDragComponent self)
		{
			Unit unit = self.GetUnit();

			float3 effectPosition = unit.GetUnitClientPos();
			float3 efffectForward = unit.Forward;

			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;

			if (actionCallParam is ActionCallShow_Drag_SelfUnit actionCallShowDragSelfUnit)
			{
				self.GetComponent<SkillShowDragSelfUnitComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Drag_SelfArea actionCallShowDragSelfArea)
			{
				self.GetComponent<SkillShowDragSelfAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherUnit actionCallShowDragOtherUnit)
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

					self.GetComponent<SkillShowDragOtherUnitComponent>().UpdateSkillShowEffectPointPos(targetUnitPos);
				}
				else
				{
					self.GetComponent<SkillShowDragOtherUnitComponent>().UpdateSkillShowEffectPointPos(effectPosition);
				}
				self.GetComponent<SkillShowDragOtherUnitComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherArea actionCallShowDragOtherArea)
			{
				float3 outRangePos = effectPosition;
				float outRangeValue = skillCfg.Dis;
				float3 direct = efffectForward;
				float3 pointRangePos = outRangePos + direct * outRangeValue;
				self.GetComponent<SkillShowDragOtherAreaComponent>().UpdateSkillShowEffectPointPos(pointRangePos);
				self.GetComponent<SkillShowDragOtherAreaComponent>().UpdateSkillShowEffectCenterPos(outRangePos, direct);
			}
			else if (actionCallParam is ActionCallShow_Drag_RectangleArea actionCallShowDragRectangleArea)
			{
				self.GetComponent<SkillShowDragRectangleAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
			else if (actionCallParam is ActionCallShow_Drag_UmbellateArea actionCallShowDragUmbellateArea)
			{
				self.GetComponent<SkillShowDragUmbellateAreaComponent>().UpdateSkillShowEffectCenterPos(effectPosition, efffectForward);
			}
		}

		public static SelectHandle GetNearUnitSelectHandle(this SkillControlByDragComponent self)
		{
			Unit unit = self.GetUnit();

			SkillCfg skillCfg = SkillCfgCategory.Instance.Get(self.skillCfgId);
			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;

			SelectHandle selectHandle = null;
			if (actionCallParam is ActionCallShow_Drag_OtherUnit actionCallShowDragOtherUnit)
			{
				bool isResetPos = false;
				float3 resetPos = float3.zero;
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

		public static SelectHandle GetSelectHandle(this SkillControlByDragComponent self)
		{
			SelectHandle selectHandle = SelectHandle.Create();

			ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(self.skillCfgId);
			ActionCallParam actionCallParam = manualSkillCfg.SkillSelectAction_Ref.ActionCallParam;
			if (actionCallParam is ActionCallShow_Drag_SelfUnit actionCallShowDragSelfUnit)
			{
				selectHandle = SelectHandleHelper.CreateUnitSelfSelectHandle(self.GetUnit());
			}
			else if (actionCallParam is ActionCallShow_Drag_SelfArea actionCallShowDragSelfArea)
			{
				selectHandle.position = self.GetComponent<SkillShowDragSelfAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowDragSelfAreaComponent>().GetSkillShowEffectForward();
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherUnit actionCallShowDragOtherUnit)
			{
				if (self.targetUnitId != 0)
				{
					selectHandle.unitIds = ListComponent<long>.Create();
					selectHandle.unitIds.Add(self.targetUnitId);
				}
			}
			else if (actionCallParam is ActionCallShow_Drag_OtherArea actionCallShowDragOtherArea)
			{
				selectHandle.position = self.GetComponent<SkillShowDragOtherAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowDragOtherAreaComponent>().GetSkillShowEffectForward();
			}
			else if (actionCallParam is ActionCallShow_Drag_RectangleArea actionCallShowDragRectangleArea)
			{
				selectHandle.position = self.GetComponent<SkillShowDragRectangleAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowDragRectangleAreaComponent>().GetSkillShowEffectForward();
			}
			else if (actionCallParam is ActionCallShow_Drag_UmbellateArea actionCallShowDragUmbellateArea)
			{
				selectHandle.position = self.GetComponent<SkillShowDragUmbellateAreaComponent>().GetSkillShowEffectCenterPos();
				selectHandle.direction = self.GetComponent<SkillShowDragUmbellateAreaComponent>().GetSkillShowEffectForward();
			}

			return selectHandle;
		}

	}
}