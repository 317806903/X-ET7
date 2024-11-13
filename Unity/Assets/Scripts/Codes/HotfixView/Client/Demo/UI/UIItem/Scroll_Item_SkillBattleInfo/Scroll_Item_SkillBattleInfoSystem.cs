using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using ET.Ability;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_SkillBattleInfo))]
	public static class Scroll_Item_SkillBattleInfoSystem
	{
		public static void RegisterUIEvent(this Scroll_Item_SkillBattleInfo self)
		{

		}

		public static void HideItem(this Scroll_Item_SkillBattleInfo self)
		{

		}


        public static void UpdateSkillBaseInfo(this Scroll_Item_SkillBattleInfo self, long unitId, string skillCfgId, Action<string> onDownCallBack, Action<string> onPressCallBack, Action<string> onExitCallBack, Action<string> onClickCallBack)
        {
	        self.unitId = unitId;
	        self.skillCfgId = skillCfgId;
	        self.onDownCallBack = onDownCallBack;
	        self.onPressCallBack = onPressCallBack;
	        self.onExitCallBack = onExitCallBack;
	        self.onClickCallBack = onClickCallBack;

	        self.isPressing = false;
	        self.isShowDetail = true;

	        self.EImage_IconImage.SetImageByItemCfgId(self, skillCfgId).Coroutine();
	        self.EButton_ShowDetailButton.SetVisible(false);
	        self.EG_BuyEnergyRectTransform.SetVisible(true);
	        self.ELabel_AddTimesTipsTextMeshProUGUI.SetVisible(false);
	        self.ELabel_TimesTextMeshProUGUI.text = "";

	        self.EButton_BuyEnergyButton.AddListener(async () =>
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
                self.EG_BuyEnergyRectTransform.SetVisible(false);

                self.CheckUserInput().Coroutine();
                onDownCallBack?.Invoke(skillCfgId);

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

            ET.EventTriggerListener.Get(self.EGBuyEnergyPressRectTransform.gameObject).onDown.AddListener((go, xx) =>
            {
	            //Log.Error($"====onDown");

	            self.EButton_ShowDetailButton.SetVisible(true);
	            self.CheckUserInput().Coroutine();

            });
            ET.EventTriggerListener.Get(self.EButton_ShowDetailButton.gameObject).onEnter.AddListener(async (go, xx) =>
            {
	            //Log.Error($"====onEnter");

	            if (self.isPressing)
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

	            self.isPressing = true;

	            if (self.isShowDetail)
	            {
		            await self.ShowSkillDetail(self.uiTransform, unitId, skillCfgId, skillObj);
	            }
	            else
	            {
		            await self.ShowSkillDetailTips(self.uiTransform, unitId, skillCfgId, skillObj);
	            }
            });
        }

        public static async ETTask CheckUserInput(this Scroll_Item_SkillBattleInfo self)
        {
	        while (ET.UGUIHelper.CheckUserInput())
	        {
		        await TimerComponent.Instance.WaitFrameAsync();
		        if (self.IsDisposed)
		        {
			        return;
		        }
	        }

	        self.isPressing = false;
	        self.EButton_ShowDetailButton.SetVisible(false);
	        self.EG_BuyEnergyRectTransform.SetVisible(true);

	        if (self.isShowDetail)
	        {
	        }
	        else
	        {
		        await TimerComponent.Instance.WaitAsync(500);
		        if (self.IsDisposed)
		        {
			        return;
		        }
		        if (self.isPressing)
		        {
			        return;
		        }
		        self.RemoveSkillDetailTips();
	        }
        }

        public static void UpdateSkillInfo(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        self._UpdateSkillInfo_CD(skillObj);
	        self._UpdateSkillInfo_EnergyCD(skillObj);
	        self._UpdateSkillInfo_Times(skillObj);
	        self._UpdateSkillInfo_Buy(skillObj);
        }

        public static void _UpdateSkillInfo_CD(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        if (skillObj.cdTotal == 0)
	        {
		        self.EImage_CDImage.fillAmount = 1;
		        return;
	        }
	        self.EImage_CDImage.fillAmount = 1 - skillObj.cdCountDown / skillObj.cdTotal;
        }

        public static void _UpdateSkillInfo_EnergyCD(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillObj.skillCfgId);

	        if (skillConsumeCfg.ConsumeCommonEnergy > 0)
	        {
		        Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.unitId);
		        float totalCommonEnergy = unit.model.TotalCommonEnergy;
		        int consumeCommonEnergy = skillConsumeCfg.ConsumeCommonEnergy;
		        int realTotalCommonEnergy = (int)(totalCommonEnergy / consumeCommonEnergy) * consumeCommonEnergy;

		        float curCommonEnergyNum = skillObj.GetCurCommonEnergyNum();

		        if (curCommonEnergyNum >= realTotalCommonEnergy)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 0;
			        return;
		        }
		        if (unit.model.RestoreCommonEnergyByTime <= 0)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 0;
			        return;
		        }
		        float progress = (curCommonEnergyNum % consumeCommonEnergy) / (float)consumeCommonEnergy;
		        if (self.EImage_EnergyCDImage.fillAmount < 1 - progress)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 1 - progress;
		        }
		        else
		        {
			        self.EImage_EnergyCDImage.DOFillAmount(1 - progress, 0.5f);
		        }
	        }
	        if (skillConsumeCfg.ConsumeEnergy > 0)
	        {
		        float totalEnergy = skillConsumeCfg.TotalEnergy;
		        int consumeEnergy = skillConsumeCfg.ConsumeEnergy;
		        int realTotalEnergy = (int)(totalEnergy / consumeEnergy) * consumeEnergy;

		        float curEnergyNum = skillObj.curEnergyNum;

		        if (curEnergyNum >= realTotalEnergy)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 0;
			        return;
		        }
		        if (skillConsumeCfg.RestoreEnergyByTime <= 0)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 0;
			        return;
		        }
		        float progress = (curEnergyNum % consumeEnergy) / (float)consumeEnergy;
		        if (self.EImage_EnergyCDImage.fillAmount < 1 - progress)
		        {
			        self.EImage_EnergyCDImage.fillAmount = 1 - progress;
		        }
		        else
		        {
			        self.EImage_EnergyCDImage.DOFillAmount(1 - progress, 0.5f);
		        }
	        }
        }

        public static void _UpdateSkillInfo_Times(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillObj.skillCfgId);

	        if (skillConsumeCfg.ConsumeCommonEnergy > 0)
	        {
		        string text = self.ELabel_TimesTextMeshProUGUI.text;
		        int newText = (int)Math.Floor(skillObj.GetCurCommonEnergyNum()/skillConsumeCfg.ConsumeCommonEnergy);
		        self.EImage_TimeBg3Image.SetVisible(newText == 0);
		        if (text.IsNullOrEmpty())
		        {
			        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
		        }
		        else
		        {
			        int lastText = int.Parse(self.ELabel_TimesTextMeshProUGUI.text);
			        if (lastText < newText)
			        {
				        self.ShowAddTimesTips(newText - lastText).Coroutine();
				        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
			        }
			        else if (lastText > newText)
			        {
				        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
			        }
		        }
	        }
	        if (skillConsumeCfg.ConsumeEnergy > 0)
	        {
		        string text = self.ELabel_TimesTextMeshProUGUI.text;
		        int newText = (int)Math.Floor(skillObj.curEnergyNum/skillConsumeCfg.ConsumeEnergy);
		        self.EImage_TimeBg3Image.SetVisible(newText == 0);
		        if (text.IsNullOrEmpty())
		        {
			        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
		        }
		        else
		        {
			        int lastText = int.Parse(self.ELabel_TimesTextMeshProUGUI.text);
			        if (lastText < newText)
			        {
				        self.ShowAddTimesTips(newText - lastText).Coroutine();
				        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
			        }
			        else if (lastText > newText)
			        {
				        self.ELabel_TimesTextMeshProUGUI.text = $"{newText}";
			        }
		        }
	        }
        }

        public static void _UpdateSkillInfo_Buy(this Scroll_Item_SkillBattleInfo self, ET.Ability.SkillObj skillObj)
        {
	        bool needShowBuy = false;
	        SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillObj.skillCfgId);

	        if (skillConsumeCfg.ConsumeCommonEnergy > 0 && skillObj.GetCurCommonEnergyNum() < skillConsumeCfg.ConsumeCommonEnergy)
	        {
		        needShowBuy = true;
	        }
	        if (skillConsumeCfg.ConsumeEnergy > 0 && skillObj.curEnergyNum < skillConsumeCfg.ConsumeEnergy)
	        {
		        needShowBuy = true;
	        }
	        self.EButton_BuyEnergyButton.SetVisible(needShowBuy);
	        self.EGBuyEnergyPressRectTransform.SetVisible(needShowBuy);
        }

        public static async ETTask ShowAddTimesTips(this Scroll_Item_SkillBattleInfo self, int addTimes)
        {
	        // 可以添加额外的视觉效果，例如缩放动画
	        self.ELabel_TimesTextMeshProUGUI.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
	        {
		        if (self.IsDisposed)
		        {
			        return;
		        }
		        self.ELabel_TimesTextMeshProUGUI.transform.DOScale(1f, 0.2f);
	        });

	        self.ELabel_AddTimesTipsTextMeshProUGUI.SetVisible(true);
	        self.ELabel_AddTimesTipsTextMeshProUGUI.text = $"+{addTimes}";
	        await TimerComponent.Instance.WaitAsync(3000);
	        if (self.IsDisposed)
	        {
		        return;
	        }
	        self.ELabel_AddTimesTipsTextMeshProUGUI.SetVisible(false);
        }

        public static async ETTask ShowSkillDetail(this Scroll_Item_SkillBattleInfo self, Transform trans, long unitId, string skillCfgId,
        ET.Ability.SkillObj skillObj)
        {
	        await ETTask.CompletedTask;
	        ET.Client.UIManagerHelper.ShowSkillDetails(self.DomainScene(), skillCfgId, false, false);
        }

        public static async ETTask ShowSkillDetailTips(this Scroll_Item_SkillBattleInfo self, Transform trans, long unitId, string skillCfgId, ET.Ability.SkillObj skillObj)
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

	        string name = ET.ItemHelper.GetItemName(skillCfgId);
	        string desc = ET.ItemHelper.GetItemDesc(skillCfgId);
	        string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattleSkillTips_Show", name, desc, restoreEnergyDesc);
	        await ET.Client.UIManagerHelper.ShowDescTips(self.DomainScene(), txt, pos, false, true);
        }

        public static void RemoveSkillDetailTips(this Scroll_Item_SkillBattleInfo self)
        {
	        ET.Client.UIManagerHelper.HideDescTips(self.DomainScene());
        }
	}
}
