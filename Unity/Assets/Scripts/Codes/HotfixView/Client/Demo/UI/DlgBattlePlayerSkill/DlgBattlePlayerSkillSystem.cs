using System.Collections;
using System.Collections.Generic;
using System;
using ET.Ability;
using ET.Ability.Client;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgBattlePlayerSkillFrameTimer)]
	public class DlgBattlePlayerSkillTimer: ATimer<DlgBattlePlayerSkill>
	{
		protected override void Run(DlgBattlePlayerSkill self)
		{
			try
			{
				if (self.IsDisposed)
				{
					return;
				}
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgBattlePlayerSkill))]
	public static class DlgBattlePlayerSkillSystem
	{
		public static void RegisterUIEvent(this DlgBattlePlayerSkill self)
		{
#if UNITY_EDITOR
			self.View.EButton_RestoreEnergyButton.SetVisible(true);
			self.View.EButton_RestoreEnergyButton.AddListener(self.RestoreEnergyWhenDebug);
#else
			self.View.EButton_RestoreEnergyButton.SetVisible(false);
#endif
		}

		public static async ETTask ShowWindow(this DlgBattlePlayerSkill self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgBattlePlayerSkillFrameTimer, self);

			await self.InitSkillList();
		}

		public static bool ChkCanClickBg(this DlgBattlePlayerSkill self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgBattlePlayerSkill self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask RefreshSkill(this DlgBattlePlayerSkill self)
		{
			Unit myUnit = self.GetUnit();
			if (myUnit == null)
			{
				return;
			}
			List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(myUnit);
			int countSkill = skillList.Count;
			for (int i = 0; i < countSkill; i++)
			{
				Scroll_Item_SkillBattleInfo itemSkill = self.ScrollItemSkills[i];

				self.UpdateSkillInfo(skillList[i].skillCfgId, itemSkill);
			}
		}

        public static async ETTask InitSkillList(this DlgBattlePlayerSkill self)
        {
	        Unit myPlayerUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
	        while (myPlayerUnit == null)
	        {
		        await TimerComponent.Instance.WaitFrameAsync();
		        if (self.IsDisposed)
		        {
			        return;
		        }
		        myPlayerUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
	        }
	        self.unitId = myPlayerUnit.Id;

            List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(myPlayerUnit);
            int countSkill = skillList.Count;
            self.AddUIScrollItems(ref self.ScrollItemSkills, countSkill);

            for (int i = 0; i < countSkill; i++)
            {
	            Transform childTrans = self.View.EGSkillRootRectTransform.GetChild(i);
	            self.AddSkillItemRefreshListener(childTrans, i);
            }

            await ETTask.CompletedTask;
        }

        public static Scroll_Item_SkillBattleInfo BindSkillItem(this DlgBattlePlayerSkill self, Transform transform, int index)
        {
	        Scroll_Item_SkillBattleInfo itemSkill = self.ScrollItemSkills[index].BindTrans(transform.GetChild(0));
	        return itemSkill;
        }

        public static void UpdateSkillInfo(this DlgBattlePlayerSkill self, string skillCfgId, Scroll_Item_SkillBattleInfo itemSkill)
        {
	        ET.Ability.SkillObj skillObj = ET.Ability.SkillHelper.GetSkillObj(self.GetUnit(), skillCfgId);

	        itemSkill.UpdateSkillInfo(skillObj);
        }

        public static void AddSkillItemRefreshListener(this DlgBattlePlayerSkill self, Transform transform, int index)
        {
            Scroll_Item_SkillBattleInfo itemSkill = self.BindSkillItem(transform, index);

            List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(self.GetUnit());
            ET.Ability.SkillObj skillObj = skillList[index];

            string skillCfgId = skillObj.skillCfgId;

            itemSkill.UpdateSkillBaseInfo(self.unitId, skillCfgId, (skillCfgId)=>
            {
	            self.CreateSkillShowEffect(skillCfgId);
            }, (skillCfgId)=>
            {
	            //self.RemoveSkillShowEffect();
            }, (skillCfgId)=>
            {
	            self.CastSkillWhenTrig(skillCfgId);
            });
            self.UpdateSkillInfo(skillCfgId, itemSkill);

        }

        public static Unit GetUnit(this DlgBattlePlayerSkill self)
        {
	        Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
	        return unit;
        }

        public static void CastSkillWhenTrig(this DlgBattlePlayerSkill self, string skillCfgId)
        {
            if (self.IsDisposed)
            {
                return;
            }

            ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(skillCfgId);

            Unit unit = self.GetUnit();

            float3 effectPosition;
            float3 efffectForward;

            if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Camera)
            {
                (float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = ET.Client.CameraHelper.GetCameraHit(self.DomainScene());

                effectPosition = unit.GetUnitClientPos();
                efffectForward = cameraDirect;
                efffectForward.y = 0;
                efffectForward = math.normalize(efffectForward);
            }
            else if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Drag)
            {
                effectPosition = unit.GetUnitClientPos();
                efffectForward = unit.Forward;
                efffectForward.y = 0;
                efffectForward = math.normalize(efffectForward);
            }
            else
            {
                return;
            }

            SelectHandle selectHandle = null;
            if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Camera_OtherUnit ||
                manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Drag_OtherUnit)
            {
                SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
                bool isResetPos = true;
                float3 resetPos = effectPosition;
                bool isResetForward = true;
                float3 resetForward = efffectForward;
                ActionContext actionContext = new();
                actionContext.skillCfgId = skillCfg.Id;
                actionContext.skillDis = skillCfg.Dis;
                SelectObjectCfg selectObjectCfg = skillCfg.SkillSelectAction_Ref;
                selectHandle = ET.Client.UnitViewHelper.CreateSelectHandle(self.GetUnit(), isResetPos, resetPos, isResetForward, resetForward, selectObjectCfg, ref actionContext);
            }
            else
            {
                selectHandle = SelectHandle.Create();
                selectHandle.position = effectPosition;
                selectHandle.direction = efffectForward;
            }

            self.CastSkill(skillCfgId, selectHandle).Coroutine();
        }

        public static async ETTask CastSkill(this DlgBattlePlayerSkill self, string skillCfgId, SelectHandle selectHandle)
        {
	        (bool bRet, string msg) = ET.Ability.SkillHelper.ChkCanUseSkill(self.GetUnit(), skillCfgId);
	        if (bRet == false)
	        {
		        UIManagerHelper.ShowTip(self.DomainScene(), msg);
		        return;
	        }

	        bRet = await SkillHelper.CastSkill(self.DomainScene(), skillCfgId, self.unitId, float3.zero, float3.zero, selectHandle);

	        Handheld.Vibrate();
        }

        public static void CreateSkillShowEffect(this DlgBattlePlayerSkill self, string skillCfgId)
        {
	        Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
	        ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(skillCfgId);
	        if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Drag actionCallShowDrag)
	        {
		        self.AddComponent<SkillControlByDragComponent>().Init(skillCfgId, self.unitId, camera, (selectHandle) =>
		        {
			        self.CastSkill(skillCfgId, selectHandle).Coroutine();
		        });
	        }
	        else if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Camera actionCallShowCamera)
	        {
		        self.AddComponent<SkillControlByCameraComponent>().Init(skillCfgId, self.unitId, false, camera, (selectHandle) =>
		        {
			        self.CastSkill(skillCfgId, selectHandle).Coroutine();
		        });
	        }
        }

        public static void RemoveSkillShowEffect(this DlgBattlePlayerSkill self)
        {
	        self.RemoveComponent<SkillControlByDragComponent>();
	        self.RemoveComponent<SkillControlByCameraComponent>();
        }

        public static void Update(this DlgBattlePlayerSkill self)
        {
	        self.UpdateSkillCD();
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattlePlayerSkill self)
        {
	        return ET.UGUIHelper.CheckUserInput();
        }

        public static void UpdateSkillCD(this DlgBattlePlayerSkill self)
        {
	        self.RefreshSkill().Coroutine();
        }

        public static void RestoreEnergyWhenDebug(this DlgBattlePlayerSkill self)
        {
	        ET.Client.SkillHelper.RestoreSkillEnergy(self.DomainScene(), self.unitId, "").Coroutine();
        }
	}
}
