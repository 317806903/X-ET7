using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleTowerHUD))]
	public static class DlgBattleTowerHUDSystem
	{
		public static void RegisterUIEvent(this DlgBattleTowerHUD self)
		{
			EventTriggerListener.Get(self.View.E_Sprite_BGImage.gameObject).onDown.AddListener((go, xx) =>
			{
				self.playerId = (long)ET.PlayerId.PlayerNone;
				self.View.EGRootRectTransform.SetVisible(false);
			});
			EventTriggerListener.Get(self.View.E_Sprite_BGImage.gameObject).onUp.AddListener((go, xx) =>
			{
				self.OnClickBG();
			});

			self.View.EButton_SaleButton.AddListener(self.OnSale);
			self.View.E_ReclaimButton.AddListener(self.OnReclaim);
			self.View.E_UpgradeButton.AddListener(self.OnUpgrade);
			self.View.E_ShowDetailButton.AddListener(self.OnShowDetail);
			self.View.E_HideDetailButton.AddListener(self.OnHideDetail);

			if (ET.Client.UIManagerHelper.ChkIsDebug())
			{
				self.View.E_UpgradeItemButton.SetVisible(true);
				self.View.E_UpgradeItemButton.AddListenerAsync(self.TestUpgrade);
			}
			else
			{
				self.View.E_UpgradeItemButton.SetVisible(false);
			}
		}

		public static async ETTask ShowWindow(this DlgBattleTowerHUD self, ShowWindowData contextData = null)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

			self.playerId = ((DlgBattleTowerHUD_ShowWindowData)contextData).playerId;
			self.towerUnitId = ((DlgBattleTowerHUD_ShowWindowData)contextData).towerUnitId;
			self.towerCfgId = ((DlgBattleTowerHUD_ShowWindowData)contextData).towerCfgId;


			self.isSelf = false;
			self.isPool = false;
			long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
			if (myPlayerId == self.playerId)
			{
				self.isSelf = true;
				if (self.towerUnitId == 0)
				{
					self.isPool = true;
				}
			}

			if (self.isPool)
			{
				self.onlyChkPool = true;
			}
			else
			{
				self.onlyChkPool = false;
			}

			self._ShowWindow();
			self.ShowTowerInfo();

			//升级按钮UI状态
			await self.SetUpgradeUIStatus();
		}

		public static void HideWindow(this DlgBattleTowerHUD self)
		{

		}

		public static void _ShowWindow(this DlgBattleTowerHUD self)
		{
			self.View.EGRootRectTransform.SetVisible(true);
			self.View.EG_OperatorMenuRectTransform.SetVisible(true);
			self.View.EG_MyTowerDescRectTransform.SetVisible(true);

			if (self.isSelf)
			{
				self.View.EG_OperatorRootRectTransform.SetVisible(true);
			}
			else
			{
				self.View.EG_OperatorRootRectTransform.SetVisible(false);
			}

			self.View.EButton_SaleButton.gameObject.SetActive(true);
			self.View.EButton_ConfirmButton.gameObject.SetActive(false);
			self.View.E_UpgradeButton.gameObject.SetActive(true);
			if (self.isPool)
			{
				self.View.E_ReclaimButton.gameObject.SetActive(false);
			}
			else
			{
				self.View.E_ReclaimButton.gameObject.SetActive(true);
			}

		}

		public static void ShowTowerInfo(this DlgBattleTowerHUD self)
		{
			self.ShowTowerInfo_Base();
			self.ShowTowerInfo_Attr();
		}

		public static void ShowTowerInfo_Base(this DlgBattleTowerHUD self)
		{
			string itemCfgId = self.towerCfgId;
			string towerName = ET.ItemHelper.GetItemName(itemCfgId);

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.towerCfgId);

			self.View.E_TowerNameTextMeshProUGUI.text = towerName;
			QualityRank qualityRank = ET.ItemHelper.GetTowerItemQualityRank(itemCfgId);
			if (qualityRank == QualityRank.None)
			{
				self.View.EG_IconStarRectTransform.SetVisible(false);
			}
			else
			{
				self.View.EG_IconStarRectTransform.SetVisible(true);
				int starCount = (int)qualityRank;
				self.View.E_IconStar1Image.gameObject.SetActive(starCount>=1);
				self.View.E_IconStar2Image.gameObject.SetActive(starCount>=2);
				self.View.E_IconStar3Image.gameObject.SetActive(starCount>=3);
			}

			self.View.E_SaleMoney_textTextMeshProUGUI.text = $"{towerCfg.ScaleTowerCostGold}";

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			if (gamePlayTowerDefenseComponent != null)
			{
				(bool bRet1, string msg1) = gamePlayTowerDefenseComponent.ChkIsUpgradeMaxPlayerTower(self.towerCfgId);
				if (bRet1)
				{
					string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxLevelTower");
					self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
					self.View.E_UpgradeImage.SetImageGray(true);
				}
				else
				{
					(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds)Result;
					if (self.isPool)
					{
						Result = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerCfgId, self.onlyChkPool);
					}
					else
					{
						Result = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerUnitId, self.onlyChkPool);
					}
					if (Result.bRet == false)
					{
						int curNum = 0;
						if (Result.costTowers == null || Result.costTowers.TryGetValue(self.towerCfgId, out curNum) == false)
						{
							curNum = 0;
						}
						string tipMsg = $"({curNum}/{towerCfg.NewTowerCostCount-1})";
						self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
						self.View.E_UpgradeImage.SetImageGray(true);
					}
					else
					{
						string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CanLevelUpTower");
						self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
						self.View.E_UpgradeImage.SetImageGray(false);
					}
				}
			}

			string icon = ET.ItemHelper.GetItemIcon(itemCfgId);
			if (string.IsNullOrEmpty(icon) == false)
			{
				Sprite sprite = UIManagerHelper.LoadSprite(icon);
				self.View.E_IconImage.sprite = sprite;
			}

			string desc = ET.ItemHelper.GetItemDesc(itemCfgId);
			self.SetDetailText(desc);
		}

		public static void SetDetailText(this DlgBattleTowerHUD self, string detailText)
		{
			self.View.ELabel_DescriptionSimpleTextMeshProUGUI.SetVisible(true);
			self.View.ELabel_DescriptionSimpleTextMeshProUGUI.text = detailText;
			LayoutRebuilder.ForceRebuildLayoutImmediate(self.View.ELabel_DescriptionSimpleTextMeshProUGUI.rectTransform);
			float sizeRootY = self.View.EGDescRootRectTransform.sizeDelta.y;
			float sizeTextY = self.View.ELabel_DescriptionSimpleTextMeshProUGUI.preferredHeight;
			if (sizeRootY < sizeTextY)
			{
				self.View.ELabel_DescriptionSimpleTextMeshProUGUI.SetVisible(false);
				self.View.EGDescScrollViewScrollRect.SetVisible(true);
				self.View.ELabel_DescriptionTextMeshProUGUI.text = detailText;
			}
			else
			{
				self.View.ELabel_DescriptionSimpleTextMeshProUGUI.SetVisible(true);
				self.View.EGDescScrollViewScrollRect.SetVisible(false);
			}
		}

		public static void ShowTowerInfo_Attr(this DlgBattleTowerHUD self)
		{
			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.towerCfgId);
			string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(self.towerCfgId);
			int unitCfgLevel = ET.ItemHelper.GetTowerItemLevel(self.towerCfgId);
			string propertyType = UnitCfgCategory.Instance.Get(unitCfgId).PropertyType;

			var attributeList = ET.ItemHelper.GetAttributeProperty(propertyType, unitCfgLevel);
			self.View.ENode_Attribute1Image.SetVisible(attributeList.Count >= 1);
			self.View.ENode_Attribute2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_Attribute3Image.SetVisible(attributeList.Count >= 3);
			self.View.ENode_AttributeLine2Image.SetVisible(attributeList.Count >= 2);
			self.View.ENode_AttributeLine3Image.SetVisible(attributeList.Count >= 3);
			if (attributeList.Count >= 1)
			{
				self.View.Elabel_Attribute1TextMeshProUGUI.text = attributeList[0].title;
				self.View.Elabel_AttributeValue1TextMeshProUGUI.text = attributeList[0].content;
			}
			if (attributeList.Count >= 2)
			{
				self.View.Elabel_Attribute2TextMeshProUGUI.text = attributeList[1].title;
				self.View.Elabel_AttributeValue2TextMeshProUGUI.text = attributeList[1].content;
			}
			if (attributeList.Count >= 3)
			{
				self.View.Elabel_Attribute3TextMeshProUGUI.text = attributeList[2].title;
				self.View.Elabel_AttributeValue3TextMeshProUGUI.text = attributeList[2].content;
			}
		}

		public static void OnClickBG(this DlgBattleTowerHUD self)
		{
			//UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
			if (self.playerId == (long)ET.PlayerId.PlayerNone)
			{
				self.OnClose();
			}
		}

		public static void OnClose(this DlgBattleTowerHUD self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleTowerHUD>();
		}

		public static void OnSale(this DlgBattleTowerHUD self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

			self.View.EButton_SaleButton.gameObject.SetActive(false);
			self.View.EButton_ConfirmButton.gameObject.SetActive(true);
			self.View.EButton_ConfirmButton.AddListener(self.OnConfirmSale);

			/*string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_SaleTower_Des", towerCfg.ScaleTowerCostGold);
			string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_SaleTower_Confirm");
			string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_SaleTower_Cancel");
			string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_SaleTower_Title");
			ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
			{
				ET.Client.GamePlayTowerDefenseHelper.SendScalePlayerTower(self.ClientScene(), self.towerUnitId).Coroutine();
				self.OnClose();
			}, null, sureTxt, cancelTxt, titleTxt);*/
		}

		public static void OnConfirmSale(this DlgBattleTowerHUD self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Sell);
			if (GamePlayHelper.GetGamePlayPK(self.DomainScene()) != null)
			{
				ET.Client.GamePlayPKHelper.SendClearMyTower(self.DomainScene(), self.towerUnitId).Coroutine();
				self.OnClose();
				return;
			}
			if (GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene()) != null)
			{
				if (self.isPool)
				{
					ET.Client.GamePlayTowerDefenseHelper.SendScalePlayerTowerCard(self.ClientScene(), self.towerCfgId).Coroutine();
				}
				else
				{
					ET.Client.GamePlayTowerDefenseHelper.SendScalePlayerTower(self.ClientScene(), self.towerUnitId).Coroutine();
				}
			}
			self.OnClose();
		}

		public static void OnReclaim(this DlgBattleTowerHUD self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Reclaim);

			if (GamePlayHelper.GetGamePlayPK(self.DomainScene()) != null)
			{
				ET.Client.GamePlayPKHelper.SendClearMyTower(self.DomainScene(), self.towerUnitId).Coroutine();
				self.OnClose();
				return;
			}
			if (GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene()) != null)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
				(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkReclaimPlayerTower(self.playerId, self.towerUnitId);
				if (bRet == false)
				{
					string tipMsg = msg;
					ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
					return;
				}

				ET.Client.GamePlayTowerDefenseHelper.SendReclaimPlayerTower(self.ClientScene(), self.towerUnitId).Coroutine();
			}

			self.OnClose();
		}

		public static void OnUpgrade(this DlgBattleTowerHUD self)
		{
			if (GamePlayHelper.GetGamePlayPK(self.DomainScene()) != null)
			{
				return;
			}
			if (GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene()) != null)
			{
			}

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			if (self.isPool)
			{
				(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerCfgId, self.onlyChkPool);
				if (bRet == false)
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
					string tipMsg = msg;
					ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
					return;
				}
			}
			else
			{
				(bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerUnitId, self.onlyChkPool);
				if (bRet == false)
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
					string tipMsg = msg;
					ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
					return;
				}
			}

			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Upgradation);
			ET.Client.GamePlayTowerDefenseHelper.SendUpgradePlayerTower(self.ClientScene(), self.towerUnitId, self.towerCfgId, self.onlyChkPool).Coroutine();
			self.OnClose();
		}

		public static void OnShowDetail(this DlgBattleTowerHUD self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
			self.View.E_ShowDetailButton.SetVisible(false);
			self.View.EG_ViewInfoRectTransform.SetVisible(true);
		}

		public static void OnHideDetail(this DlgBattleTowerHUD self)
		{
			UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
			self.View.E_ShowDetailButton.SetVisible(true);
			self.View.EG_ViewInfoRectTransform.SetVisible(false);
		}

		public static Unit GetUnit(this DlgBattleTowerHUD self)
		{
			Unit unit = ET.Client.UnitHelper.GetUnit(self.DomainScene(), self.towerUnitId);
			return unit;
		}

		public static async ETTask TestUpgrade(this DlgBattleTowerHUD self)
		{
			string itemGiftCfgId = self.View.E_InputFieldUpgradeTextTMP_InputField.text;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			if (gamePlayTowerDefenseComponent != null)
			{
				Unit unit = self.GetUnit();
				ItemUpgradeComponent itemUpgradeComponent = unit.GetComponent<ItemUpgradeComponent>();
				int curLevel = itemUpgradeComponent.curLevel;
				int maxUpgradeLevel = itemUpgradeComponent.maxUpgradeLevel;
				if (curLevel == maxUpgradeLevel)
				{
					UIManagerHelper.ShowTip(self.DomainScene(), "maxUpgradeLevel");
					return;
				}
				SortedDictionary<int, string> chooseItemGiftDic = itemUpgradeComponent.chooseItemGiftDic;

				int nextLevel = 1;
				if (curLevel == 1 && chooseItemGiftDic.ContainsKey(curLevel) == false)
				{
					nextLevel = 1;
				}
				else
				{
					nextLevel = curLevel + 1;
				}

				(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkUpgradeItemUnit(self.playerId, self.towerUnitId, nextLevel, itemGiftCfgId);

				Log.Error($"bRet[{bRet}], msg[{msg}]");
				if (bRet == false)
				{
					UIManagerHelper.ShowTip(self.DomainScene(), msg);

					return;
				}
				await ET.Client.GamePlayTowerDefenseHelper.SendUpgradeItemUnit(self.DomainScene(), self.towerUnitId, self.towerCfgId, nextLevel, itemGiftCfgId);
			}
			else
			{
				await ET.Client.GamePlayPKHelper.SendCallTowerActions(self.DomainScene(), self.towerUnitId, itemGiftCfgId);
			}
		}

		public static async ETTask SetUpgradeUIStatus(this DlgBattleTowerHUD self)
		{
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			if (gamePlayTowerDefenseComponent == null)
				return;
            if (self.isPool)
            {
                (bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerCfgId, self.onlyChkPool);
                if (bRet == false)
                {
					if(msg == LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxLevelTower"))
					{
                        self.View.E_UpgradeButton.SetVisible(false);
						self.View.E_MaxUpgradeButton.SetVisible(true);
                    }
					else
					{
                        UIManagerHelper.SetImageGray(self.View.E_UpgradeButton.transform, true);
                        self.View.E_MaxUpgradeButton.SetVisible(false);
                        self.View.E_UpgradeButton.SetVisible(true);
                    }

                }
				else
				{
                    self.View.E_UpgradeButton.SetVisible(true);
                    self.View.E_MaxUpgradeButton.SetVisible(false);
                    UIManagerHelper.SetImageGray(self.View.E_UpgradeButton.transform, false);
                }
            }
            else
            {
                (bool bRet, string msg, Dictionary<string, int> costTowers, List<long> existTowerUnitIds) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerUnitId, self.onlyChkPool);
                if (bRet == false)
                {
                    if (msg == LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxLevelTower"))
                    {
                        self.View.E_UpgradeButton.SetVisible(false);
                        self.View.E_MaxUpgradeButton.SetVisible(true);
                    }
                    else
                    {
                        UIManagerHelper.SetImageGray(self.View.E_UpgradeButton.transform, true);
                        self.View.E_MaxUpgradeButton.SetVisible(false);
                        self.View.E_UpgradeButton.SetVisible(true);
                    }
                }
                else
                {
                    self.View.E_UpgradeButton.SetVisible(true);
                    self.View.E_MaxUpgradeButton.SetVisible(false);
                    UIManagerHelper.SetImageGray(self.View.E_UpgradeButton.transform, false);
                }
            }

			await ETTask.CompletedTask;
		}

	}
}
