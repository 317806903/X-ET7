using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using ET.AbilityConfig;

namespace ET.Client
{
    [Invoke(TimerInvokeType.DlgSkillDetailsFrameTimer)]
    public class DlgSkillDetailsTimer : ATimer<DlgSkillDetails>
    {
        protected override void Run(DlgSkillDetails self)
        {
            try
            {
                if (self.IsDisposed )
                {
                    return;
                }
                //self.Update();

            }
            catch (Exception e)
            {
                Log.Error($"DlgVideoShow timer error: {self.Id}\n{e}");
            }
        }
    }
    [FriendOf(typeof(DlgSkillDetails))]
	public static class DlgSkillDetailsSystem
	{
		public static void RegisterUIEvent(this DlgSkillDetails self)
		{
			self.View.EButton_DetailBgButton.AddListenerAsync(self.OnClickBG);

        }

		public static async ETTask ShowWindow(this DlgSkillDetails self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.DlgSkillDetailsFrameTimer, self);
            ShowData_SkillInfo  showData_SkillInfo = contextData as ShowData_SkillInfo;
            string skillCfgId = showData_SkillInfo.skillCfgId;
            PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(skillCfgId);

            await self.ShowSkillStatusUI((showData_SkillInfo.skillCfgId, showData_SkillInfo.isLearned));

            self.View.E_SkillFuncButton.AddListenerAsync(async () =>
            {
                await self.SkillGetOrUpgrade((showData_SkillInfo.skillCfgId, showData_SkillInfo.isLearned));

            });
            self.View.E_DetailsButton_VideoButton.AddListenerAsync(async () =>
            {
                DlgTutorialOne_ShowWindowData dlgTutorialOne_ShowWindowData=new DlgTutorialOne_ShowWindowData();
                dlgTutorialOne_ShowWindowData.tutorialCfgId = playerSkillCfg.TutorialCfgId;
                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgTutorialOne>(dlgTutorialOne_ShowWindowData);
            });

            await self.ShowSkillDetailsUI(showData_SkillInfo.skillCfgId);
        }

		public static bool ChkCanClickBg(this DlgSkillDetails self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgSkillDetails self)
		{
            UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            TimerComponent.Instance?.Remove(ref self.Timer);
            if(!string.IsNullOrEmpty(self.videoPath))
            {
                ResComponent.Instance.UnloadAsset(self.videoPath);
            }
            GL.Clear(false,true,Color.black);
            //移除监听事件
            self.View.E_SkillFuncButton.onClick.RemoveAllListeners();
            self.View.E_DetailsButton_VideoButton.onClick.RemoveAllListeners();
        }

		public static async ETTask OnClickBG(this DlgSkillDetails self)
		{
			if(!self.ChkCanClickBg())
			{
				return;
			}
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgSkillDetails>();
			await ETTask.CompletedTask;
		}


        //解锁或升级技能
        public static async ETTask SkillGetOrUpgrade(this DlgSkillDetails self, (string skillCfg, bool isLearned) skillItemCfgId)
        {
            if(skillItemCfgId.isLearned)
            {
                await SkillHelper.UpdatePlayerSkill(self.DomainScene(), skillItemCfgId.skillCfg);
            }
            else
            {
                await SkillHelper.LearnPlayerSkill(self.DomainScene(), skillItemCfgId.skillCfg);
            }
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgSkillDetails>();
        }

        public static async ETTask ShowSkillStatusUI(this DlgSkillDetails self, (string skillCfg, bool isLearned) skillItemCfgId)
        {
            PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(skillItemCfgId.skillCfg);
            int getOrUpgradeSkillCost = playerSkillCfg.LearnOrUpdateCost;
            if (skillItemCfgId.isLearned)
            {
                //升级
                if(!string.IsNullOrEmpty(playerSkillCfg.NextPlayerSkillCfgId))
                {
                    string upgradeSkillKey = "TextCode_Key_SkillDetails_UpgradeSkill";
                    self.View.E_Image_DiamondImage.SetVisible(true);
                    self.View.ELabel_NumTextMeshProUGUI.SetVisible(true);
                    self.View.ELabel_NumTextMeshProUGUI.text = getOrUpgradeSkillCost.ToString();
                    self.View.ELabel_TextTextMeshProUGUI.SetVisible(true);
                    self.View.ELabel_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(upgradeSkillKey);
                }
                else
                {
                    //满级
                    string maxSkillKey = "TextCode_Key_SkillDeatils_MaxSkill";
                    self.View.E_Image_DiamondImage.SetVisible(false);
                    self.View.ELabel_NumTextMeshProUGUI.SetVisible(false);
                    self.View.ELabel_NumTextMeshProUGUI.text = getOrUpgradeSkillCost.ToString();
                    self.View.ELabel_TextTextMeshProUGUI.SetVisible(true);
                    self.View.ELabel_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(maxSkillKey);
                }
            }
            else
            {
                //解锁
                string getSkillKey = "TextCode_Key_SkillDetails_GetSkill";
                self.View.E_Image_DiamondImage.SetVisible(true);
                self.View.ELabel_NumTextMeshProUGUI.SetVisible(true);
                self.View.ELabel_NumTextMeshProUGUI.text = getOrUpgradeSkillCost.ToString();
                self.View.ELabel_TextTextMeshProUGUI.SetVisible(true);
                self.View.ELabel_TextTextMeshProUGUI.text = LocalizeComponent.Instance.GetTextValue(getSkillKey);
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask ShowSkillDetailsUI(this DlgSkillDetails self, string skillCfgId)
        {
            SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
            string skillName = skillCfg.Name;
            string skillDesc = skillCfg.Desc;
            self.View.ELabel_NameTextMeshProUGUI.text = skillName;
            self.View.ELabel_DescriptionTextMeshProUGUI.text = skillDesc;
            await ETTask.CompletedTask;
        }
    }
}
