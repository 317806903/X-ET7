using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using ET.Ability;
using ET.Ability.Client;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.DlgBattleCameraPlayerSkillFrameTimer)]
	public class DlgBattleCameraPlayerSkillTimer: ATimer<DlgBattleCameraPlayerSkill>
	{
		protected override void Run(DlgBattleCameraPlayerSkill self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}

	[FriendOf(typeof(DlgBattleCameraPlayerSkill))]
	public static class DlgBattleCameraPlayerSkillSystem
	{
		public static void RegisterUIEvent(this DlgBattleCameraPlayerSkill self)
		{
			self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.prefabSource.prefabName = "Item_SkillBattleInfo";
			self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.prefabSource.poolSize = 5;
			self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.prefabSource.prefabScale = 0.6f;
			self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddSkillItemRefreshListener(transform, i);
				self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.SetSrcollMiddle();
			});

#if UNITY_EDITOR
			self.View.EButton_RestoreEnergyButton.SetVisible(true);
			self.View.EButton_RestoreEnergyButton.AddListener(self.RestoreEnergyWhenDebug);
#else
			self.View.EButton_RestoreEnergyButton.SetVisible(false);
#endif
		}

		public static async ETTask ShowWindow(this DlgBattleCameraPlayerSkill self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();

			self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgBattleCameraPlayerSkillFrameTimer, self);
			self.ShowOrHide(false, false);
			self.View.EGAimRootRectTransform.SetVisible(false);
			await self.InitSkillList();
		}

		public static void ShowOrHide(this DlgBattleCameraPlayerSkill self, bool isShow, bool isHigh)
		{
			self.View.EGRootRectTransform.SetVisible(isShow);
			if (isShow)
			{
				if (isHigh)
				{
					self.View.EGSkillRootRectTransform.transform.DOLocalMove(new Vector3(0, 100, 0), 0.2f);
				}
				else
				{
					self.View.EGSkillRootRectTransform.transform.DOLocalMove(Vector3.zero, 0.2f);
				}
			}
		}

		public static bool ChkCanClickBg(this DlgBattleCameraPlayerSkill self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgBattleCameraPlayerSkill self)
		{
			TimerComponent.Instance?.Remove(ref self.Timer);
		}

		public static async ETTask RefreshSkill(this DlgBattleCameraPlayerSkill self)
		{
			Unit myCameraPlayerUnit = self.GetUnit();
			if (myCameraPlayerUnit == null)
			{
				return;
			}
			List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(myCameraPlayerUnit);
			int countSkill = skillList.Count;

			if (self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.totalCount != countSkill)
			{
				await self.InitSkillList();
			}
			else
			{
				for (int i = 0; i < countSkill; i++)
				{
					Scroll_Item_SkillBattleInfo itemSkill = self.ScrollItemSkills[i];
					if (itemSkill.uiTransform == null)
					{
						self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.RefreshCells();
						return;
					}
				}
				for (int i = 0; i < countSkill; i++)
				{
					Scroll_Item_SkillBattleInfo itemSkill = self.ScrollItemSkills[i];
					self.UpdateSkillInfo(skillList[i].skillCfgId, itemSkill);
				}
			}
		}

        public static async ETTask InitSkillList(this DlgBattleCameraPlayerSkill self)
        {
            Unit myCameraPlayerUnit = UnitHelper.GetMyCameraPlayerUnit(self.DomainScene());
            while (myCameraPlayerUnit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
	                return;
                }
                myCameraPlayerUnit = UnitHelper.GetMyCameraPlayerUnit(self.DomainScene());
            }
            self.unitId = myCameraPlayerUnit.Id;

            List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(myCameraPlayerUnit);
            int countSkill = skillList.Count;
            self.AddUIScrollItems(ref self.ScrollItemSkills, countSkill);
            self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.SetVisible(true, countSkill);

            await ETTask.CompletedTask;
        }

        public static void UpdateSkillInfo(this DlgBattleCameraPlayerSkill self, string skillCfgId, Scroll_Item_SkillBattleInfo itemSkill)
        {
	        ET.Ability.SkillObj skillObj = ET.Ability.SkillHelper.GetSkillObj(self.GetUnit(), skillCfgId);

	        itemSkill.UpdateSkillInfo(skillObj);
        }

        public static void AddSkillItemRefreshListener(this DlgBattleCameraPlayerSkill self, Transform transform, int index)
        {
            Scroll_Item_SkillBattleInfo itemSkill = self.ScrollItemSkills[index].BindTrans(transform);

            List<ET.Ability.SkillObj> skillList = ET.Ability.SkillHelper.GetManualSkillList(self.GetUnit());
            ET.Ability.SkillObj skillObj = skillList[index];

            string skillCfgId = skillObj.skillCfgId;
            itemSkill.UpdateSkillBaseInfo(self.unitId, skillCfgId, (skillCfgId)=>
            {
	            self.View.EGAimRootRectTransform.SetVisible(true);
            },(skillCfgId)=>
            {
	            self.CreateSkillShowEffect(skillCfgId);
	            self.View.EGAimRootRectTransform.SetVisible(false);
            }, (skillCfgId)=>
            {
	            self.RemoveSkillShowEffect();
	            self.View.EGAimRootRectTransform.SetVisible(false);
            }, (skillCfgId)=>
            {
	            self.CastSkillWhenTrig(skillCfgId);
            });
            self.UpdateSkillInfo(skillCfgId, itemSkill);
        }

        public static Unit GetUnit(this DlgBattleCameraPlayerSkill self)
        {
	        Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
	        return unit;
        }

        public static void CastSkillWhenTrig(this DlgBattleCameraPlayerSkill self, string skillCfgId)
        {
	        if (self.IsDisposed)
	        {
		        return;
	        }

	        (float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = ET.Client.CameraHelper.GetCameraHit(self.DomainScene());
	        if (cameraHitPosition.Equals(float3.zero))
	        {
		        return;
	        }

	        float3 effectPosition = cameraHitPosition;
	        float3 efffectForward = cameraDirect;
	        efffectForward.y = 0;
	        efffectForward = math.normalize(efffectForward);

	        ManualSkillCfg manualSkillCfg = ManualSkillCfgCategory.Instance.Get(skillCfgId);
	        SelectHandle selectHandle = null;
	        if (manualSkillCfg.SkillSelectAction_Ref.ActionCallParam is ActionCallShow_Camera_OtherUnit actionCallShowCameraOtherUnit)
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

        public static async ETTask CastSkill(this DlgBattleCameraPlayerSkill self, string skillCfgId, SelectHandle selectHandle)
        {
	        (bool bRet, string msg) = ET.Ability.SkillHelper.ChkCanUseSkill(self.GetUnit(), skillCfgId);
	        if (bRet == false)
	        {
		        UIManagerHelper.ShowTip(self.DomainScene(), msg);
		        return;
	        }

	        (float3 cameraPosition, float3 cameraDirect, float3 cameraHitPosition) = ET.Client.CameraHelper.GetCameraHit(self.DomainScene());

	        bRet = await SkillHelper.CastSkill(self.DomainScene(), skillCfgId, self.unitId, cameraPosition, cameraDirect, selectHandle);

	        ET.Ability.Client.AudioPlayHelper.PlayVibrate(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        }

        public static void CreateSkillShowEffect(this DlgBattleCameraPlayerSkill self, string skillCfgId)
        {
	        Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
	        self.AddComponent<SkillControlByCameraComponent>().Init(skillCfgId, self.unitId, true, camera, (selectHandle) =>
	        {
		        self.CastSkill(skillCfgId, selectHandle).Coroutine();
	        });
        }

        public static void RemoveSkillShowEffect(this DlgBattleCameraPlayerSkill self)
        {
			self.RemoveComponent<SkillControlByCameraComponent>();
        }

        public static void Update(this DlgBattleCameraPlayerSkill self)
        {
	        self.UpdateSkillCD();

        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattleCameraPlayerSkill self)
        {
	        return ET.UGUIHelper.CheckUserInput();
        }

        public static void UpdateSkillCD(this DlgBattleCameraPlayerSkill self)
        {
	        self.RefreshSkill().Coroutine();
        }

        public static void RestoreEnergyWhenDebug(this DlgBattleCameraPlayerSkill self)
        {
	        ET.Client.SkillHelper.RestoreSkillEnergy(self.DomainScene(), self.unitId, "").Coroutine();
        }

	}
}
