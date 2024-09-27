using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_SkillInfo))]
	public static class Scroll_Item_SkillInfoSystem
	{
		public static void Init(this Scroll_Item_SkillInfo self, (string skillCfg,bool isLearned) skillItemCfgId, bool isShowRedDot=false)
		{
			EventTriggerListener.Get(self.EButton_SelectButton.gameObject).RemoveAllListeners();
			EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
			{
				if(string.IsNullOrEmpty(skillItemCfgId.skillCfg))
				{
					Debug.LogWarning("skillItemCfgId为空");
					return;
				}
                if (isShowRedDot)
                {
                    UIManagerHelper.HideUIRedDot(self.DomainScene(),UIRedDotType.None, "", skillItemCfgId.skillCfg).Coroutine();
					self.E_RedDotImage.gameObject.SetActive(false);
					isShowRedDot = false;
                }
				UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
				//显示详情介绍面板
				ShowData_SkillInfo skillInfoData= new ShowData_SkillInfo();
				skillInfoData.skillCfgId = skillItemCfgId.skillCfg;
				skillInfoData.isLearned = skillItemCfgId.isLearned;
				UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindow<DlgSkillDetails>(skillInfoData);

			});
			//没有物品信息显示槽位
			if(string.IsNullOrEmpty(skillItemCfgId.skillCfg))
			{
				self.E_NoneImage.SetVisible(true);
				self.EG_RootRectTransform.gameObject.SetActive(false);
				return;
			}
			else
			{
                self.E_NoneImage.SetVisible(false);
                self.EG_RootRectTransform.gameObject.SetActive(true);
            }
			if(isShowRedDot)
			{
				self.E_RedDotImage.SetVisible(true);
			}
			else
			{
				self.E_RedDotImage.SetVisible(false);
			}
			//未解锁
			if(skillItemCfgId.isLearned)
			{
				self.E_LockImage.SetVisible(false);
			}
			else
			{
                self.E_LockImage.SetVisible(true);
            }
			//icon
			PlayerSkillCfg playerSkillCfg = PlayerSkillCfgCategory.Instance.Get(skillItemCfgId.skillCfg);
			string icon_Res = playerSkillCfg.Icon;
			self.EButton_IconImage.SetImageByResIconCfgId(self, icon_Res).Coroutine();
			//name
			string name=playerSkillCfg.Name;
			self.EButton_nameTextMeshProUGUI.text = name;
			//level
			int skillLevel = playerSkillCfg.Level;
			self.EG_IconStarRectTransform.SetVisible(true);
			self.E_IconStar1Image.SetVisible(skillLevel > 0);
			self.E_IconStar2Image.SetVisible(skillLevel > 1);
			self.E_IconStar3Image.SetVisible(skillLevel > 2);
			//label
			self.EImage_Label1Image.SetVisible(false);
			self.EImage_Label2Image.SetVisible(false);
		}

	}
}
