using System.Collections;
using System.Collections.Generic;
using System;
using ET.Ability;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_SkillBattleInfo))]
	public static class Scroll_Item_SkillBattleInfoSystem
	{
		public static void Init(this Scroll_Item_SkillBattleInfo self)
		{
		}

        public static void UpdateSkillBaseInfo(this Scroll_Item_SkillBattleInfo self, long unitId, string skillCfgId, Action<string> onPressCallBack, Action<string> onExitCallBack, Action<string> onClickCallBack)
        {
	        SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
	        self.EButton_IconImage.SetImageByResIconCfgId(self, skillCfg.Icon).Coroutine();
	        self.EButton_nameTextMeshProUGUI.text = skillCfg.Name;
	        self.EButton_ShowDetailButton.SetVisible(false);

	        ET.EventTriggerListener.Get(self.EButton_BuyEnergyButton.gameObject).onClick.AddListener(async (go, xx) =>
	        {
		        if (self.IsDisposed)
		        {
			        return;
		        }

		        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillCfgId);
		        int needDiamond = skillConsumeCfg.ResetFullEnergyByCostDiamond;
		        bool bRet = await ET.Client.UIManagerHelper.ChkDiamondAndShowtip(self.DomainScene(), needDiamond, false);
		        if (self.IsDisposed)
		        {
			        return;
		        }
		        if (bRet)
		        {
			        int curDiamond = await PlayerCacheHelper.GetTokenDiamond(self.DomainScene());
			        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkill_RestoreEnergyByDiamond", curDiamond, needDiamond);
			        UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
			        {
				        if (self.IsDisposed)
				        {
					        return;
				        }
				        SkillHelper.BuySkillEnergy(self.DomainScene(), unitId, skillCfgId).Coroutine();
			        }, null);
		        }
		        else
		        {
			        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkill_RestoreEnergyByDiamond_NotEnough", needDiamond);
			        UIManagerHelper.ShowTip(self.DomainScene(), msg);
		        }
	        });

	        bool isPressing = false;

            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).isNeedClickWhenPress = true;
            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onPress.AddListener(async (go, xx) =>
            {
	            //Log.Error($"====onPress");
	            onPressCallBack?.Invoke(skillCfgId);
            });
            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onDown.AddListener((go, xx) =>
            {
                //Log.Error($"====onDown");
                self.EG_RootRectTransform.localScale = Vector3.one * 1.1f;
                self.EButton_ShowDetailButton.SetVisible(true);
                self.CheckUserInput(async () =>
                {
	                isPressing = false;
	                self.EButton_ShowDetailButton.SetVisible(false);


	                await TimerComponent.Instance.WaitAsync(500);
	                if (self.IsDisposed)
	                {
		                return;
	                }
	                if (isPressing)
	                {
		                return;
	                }
	                self.RemoveSkillDetailTips();
                }).Coroutine();

            });
            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onUp.AddListener((go, xx) =>
            {
                //Log.Error($"====onUp");
                self.EG_RootRectTransform.localScale = Vector3.one;
            });
            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onExit.AddListener(async (go, xx) =>
            {
                //Log.Error($"====onExit");
                self.EG_RootRectTransform.localScale = Vector3.one;
                onExitCallBack?.Invoke(skillCfgId);
            });
            ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
            {
                if (self.IsDisposed)
                {
                    return;
                }

                onClickCallBack?.Invoke(skillCfgId);
            });

            ET.EventTriggerListener.Get(self.EButton_ShowDetailButton.gameObject).onEnter.AddListener(async (go, xx) =>
            {
	            //Log.Error($"====onEnter");

	            if (isPressing)
	            {
		            return;
	            }

	            Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), unitId);
	            if (unit == null)
	            {
		            return;
	            }
	            ET.Ability.SkillObj skillObj = ET.Ability.SkillHelper.GetSkillObj(unit, skillCfgId);
	            if (skillObj == null)
	            {
		            return;
	            }

	            isPressing = true;

	            await self.CreateSkillDetailTips(self.uiTransform, unitId, skillCfgId, skillObj);
            });
        }

        public static async ETTask CheckUserInput(this Scroll_Item_SkillBattleInfo self, Action resetPressing)
        {
	        while (ET.UGUIHelper.CheckUserInput())
	        {
		        await TimerComponent.Instance.WaitFrameAsync();
		        if (self.IsDisposed)
		        {
			        return;
		        }
	        }
	        resetPressing();
        }

        public static void UpdateSkillInfo(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        self.ELabel_CDTextMeshProUGUI.text = $"{Math.Round(skillObj.cdCountDown, 2)}";
	        self.ELabel_EnergyTextMeshProUGUI.text = $"{Math.Round(skillObj.curEnergyNum, 2)}/{skillObj.GetEnergyFullNum()}";
	        self.ELabel_CommonEnergyTextMeshProUGUI.text = $"{Math.Round(skillObj.GetCurCommonEnergyNum(), 2)}/{skillObj.GetCommonEnergyFullNum()}";

	        bool needShowBuy = false;
	        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillObj.skillCfgId);
	        if (skillObj.GetCurCommonEnergyNum() < skillConsumeCfg.ConsumeCommonEnergy)
	        {
		        needShowBuy = true;
	        }
	        if (skillObj.curEnergyNum < skillConsumeCfg.ConsumeEnergy)
	        {
		        needShowBuy = true;
	        }
	        self.EButton_BuyEnergyButton.SetVisible(needShowBuy);
        }

        public static async ETTask CreateSkillDetailTips(this Scroll_Item_SkillBattleInfo self, Transform trans, long unitId, string skillCfgId, ET.Ability.SkillObj skillObj)
        {
	        Vector3 pos = ET.Client.EUIHelper.GetRectTransformMidTop(trans.GetComponent<RectTransform>());
	        SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
	        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillCfgId);
	        string restoreEnergyDesc = "";
	        if (skillConsumeCfg.ConsumeEnergy > 0)
	        {
		        if (skillConsumeCfg.RestoreEnergyByWave >= skillConsumeCfg.ConsumeEnergy)
		        {
			        int count = (int)Mathf.Floor(skillConsumeCfg.RestoreEnergyByWave/skillConsumeCfg.ConsumeEnergy);
			        //restoreEnergyDesc = $"每回合恢复{count}次";
			        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy1", count);
		        }
		        else
		        {
			        if (skillConsumeCfg.ConsumeEnergy <= skillObj.curEnergyNum)
			        {
				        int count = (int)Mathf.Ceil(skillConsumeCfg.ConsumeEnergy/skillConsumeCfg.RestoreEnergyByWave);
				        //restoreEnergyDesc = $"每{count}回合恢复1次";
				        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy2", count);
			        }
			        else
			        {
				        int countCur = (int)Mathf.Ceil((skillConsumeCfg.ConsumeEnergy - skillObj.curEnergyNum)/skillConsumeCfg.RestoreEnergyByWave);
				        int count = (int)Mathf.Ceil(skillConsumeCfg.ConsumeEnergy/skillConsumeCfg.RestoreEnergyByWave);
				        //restoreEnergyDesc = $"{0}回合后将恢复(每{1}回合恢复1次)";
				        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy2_NotEnough", countCur, count);
			        }
		        }
	        }
	        if (skillConsumeCfg.ConsumeCommonEnergy > 0)
	        {
		        Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), unitId);
		        float restoreCommonEnergyByWave = unit.model.RestoreCommonEnergyByWave;
		        if (restoreCommonEnergyByWave >= skillConsumeCfg.ConsumeCommonEnergy)
		        {
			        int count = (int)Mathf.Floor(restoreCommonEnergyByWave/skillConsumeCfg.ConsumeCommonEnergy);
			        //restoreEnergyDesc = $"每回合恢复{count}次";
			        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy1", count);
		        }
		        else
		        {
			        if (skillConsumeCfg.ConsumeCommonEnergy <= skillObj.GetCurCommonEnergyNum())
			        {
				        int count = (int)Mathf.Ceil(skillConsumeCfg.ConsumeCommonEnergy/restoreCommonEnergyByWave);
				        //restoreEnergyDesc = $"每{count}回合恢复1次";
				        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy2", count);
			        }
			        else
			        {
				        int countCur = (int)Mathf.Ceil((skillConsumeCfg.ConsumeCommonEnergy - skillObj.GetCurCommonEnergyNum())/restoreCommonEnergyByWave);
				        int count = (int)Mathf.Ceil(skillConsumeCfg.ConsumeCommonEnergy/restoreCommonEnergyByWave);
				        //restoreEnergyDesc = $"{0}回合后将恢复(每{1}回合恢复1次)";
				        restoreEnergyDesc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_RestoreEnergy2_NotEnough", countCur, count);
			        }
		        }
	        }
	        string desc = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_Show", skillCfg.Name, skillCfg.Desc, restoreEnergyDesc);
	        await ET.Client.UIManagerHelper.ShowDescTips(self.DomainScene(), desc, pos, false, true);
        }

        public static void RemoveSkillDetailTips(this Scroll_Item_SkillBattleInfo self)
        {
	        ET.Client.UIManagerHelper.HideDescTips(self.DomainScene());
        }
	}
}
