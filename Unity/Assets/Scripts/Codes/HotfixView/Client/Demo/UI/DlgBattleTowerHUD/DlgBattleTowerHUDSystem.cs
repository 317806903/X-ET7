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
			self.View.E_Sprite_BGButton.AddListener(self.OnClickBG);
			self.View.E_SaleButton.AddListener(self.OnSale);
			self.View.E_ReclaimButton.AddListener(self.OnReclaim);
			self.View.E_UpgradeButton.AddListener(self.OnUpgrade);
		}

		public static void ShowWindow(this DlgBattleTowerHUD self, ShowWindowData contextData = null)
		{
			self.playerId = ((DlgBattleTowerHUD_ShowWindowData)contextData).playerId;
			self.towerUnitId = ((DlgBattleTowerHUD_ShowWindowData)contextData).towerUnitId;
			self.towerCfgId = ((DlgBattleTowerHUD_ShowWindowData)contextData).towerCfgId;


			self.View.EG_OperatorMenuRectTransform.SetVisible(false);
			self.View.EG_MyTowerDescRectTransform.SetVisible(false);
			self.View.EG_OtherTowerDescRectTransform.SetVisible(false);

			long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
			if (myPlayerId == self.playerId)
			{
				self.ShowMyTower();
			}
			else
			{
				self.ShowOtherTower();
			}
		}

		public static void ShowMyTower(this DlgBattleTowerHUD self)
		{
			self.View.EG_OperatorMenuRectTransform.SetVisible(true);
			self.View.EG_MyTowerDescRectTransform.SetVisible(true);

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.towerCfgId);
			string towerName = towerCfg.Name;
			if (string.IsNullOrEmpty(towerName))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				towerName = unitCfg.Name;
			}
			self.View.E_TowerNameTextMeshProUGUI.text = towerName;
			int starCount = towerCfg.Level[0];
			self.View.E_IconStar1Image.gameObject.SetActive(starCount>=1);
			self.View.E_IconStar2Image.gameObject.SetActive(starCount>=2);
			self.View.E_IconStar3Image.gameObject.SetActive(starCount>=3);


			self.View.E_SaleMoney_textTextMeshProUGUI.text = $"{towerCfg.ScaleTowerCostGold}";

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			(bool bRet1, string msg1) = gamePlayTowerDefenseComponent.ChkIsUpgradeMaxPlayerTower(self.towerCfgId);
			if (bRet1)
			{
				string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_MaxLevelTower");
				self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
			}
			else
			{
				(bool bRet, string msg, Dictionary<string, int> costTowers) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerUnitId);
				if (bRet == false)
				{
					int curNum = 0;
					if (costTowers == null || costTowers.TryGetValue(self.towerCfgId, out curNum) == false)
					{
						curNum = 0;
					}
					string tipMsg = $"({curNum}/{towerCfg.NewTowerCostCount-1})";
					self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
				}
				else
				{
					string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_CanLevelUpTower");
					self.View.E_Upgrade_number_TextTextMeshProUGUI.text = $"{tipMsg}";
				}
			}

			string icon = towerCfg.Icon;
			if (string.IsNullOrEmpty(icon))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				icon = unitCfg.Icon;
			}
			ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(icon);
			icon = resIconCfg.ResName;

			Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
			self.View.E_IconImage.sprite = sprite;

			string desc = towerCfg.Desc;
			if (string.IsNullOrEmpty(desc))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				desc = unitCfg.Desc;
			}
			self.View.E_TextDescTextMeshProUGUI.text = desc;
		}

		public static void ShowOtherTower(this DlgBattleTowerHUD self)
		{
			self.View.EG_OtherTowerDescRectTransform.SetVisible(true);

			TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.towerCfgId);
			string towerName = towerCfg.Name;
			if (string.IsNullOrEmpty(towerName))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				towerName = unitCfg.Name;
			}
			self.View.E_OtherTowerNameTextMeshProUGUI.text = towerName;
			int starCount = towerCfg.Level[0];
			self.View.E_OtherIconStar1Image.gameObject.SetActive(starCount>=1);
			self.View.E_OtherIconStar2Image.gameObject.SetActive(starCount>=2);
			self.View.E_OtherIconStar3Image.gameObject.SetActive(starCount>=3);

			string icon = towerCfg.Icon;
			if (string.IsNullOrEmpty(icon))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				icon = unitCfg.Icon;
			}
			ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(icon);
			icon = resIconCfg.ResName;

			Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
			self.View.E_OtherIconImage.sprite = sprite;

			string desc = towerCfg.Desc;
			if (string.IsNullOrEmpty(desc))
			{
				UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
				desc = unitCfg.Desc;
			}
			self.View.E_TextOtherDescTextMeshProUGUI.text = desc;
		}

		public static void OnClickBG(this DlgBattleTowerHUD self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioClick(self.DomainScene());
			self.OnClose();
		}

		public static void OnClose(this DlgBattleTowerHUD self)
		{
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleTowerHUD>();
		}

		public static void OnSale(this DlgBattleTowerHUD self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

			ET.Client.GamePlayTowerDefenseHelper.SendScalePlayerTower(self.ClientScene(), self.towerUnitId);
			self.OnClose();
		}

		public static void OnReclaim(this DlgBattleTowerHUD self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkReclaimPlayerTower(self.playerId, self.towerUnitId);
			if (bRet == false)
			{
				string tipMsg = msg;
				ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
				return;
			}

			ET.Client.GamePlayTowerDefenseHelper.SendReclaimPlayerTower(self.ClientScene(), self.towerUnitId);
			self.OnClose();
		}

		public static void OnUpgrade(this DlgBattleTowerHUD self)
		{
			ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
			(bool bRet, string msg, Dictionary<string, int> costTowers) = gamePlayTowerDefenseComponent.ChkUpgradePlayerTower(self.playerId, self.towerUnitId);
			if (bRet == false)
			{
				string tipMsg = msg;
				ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
				return;
			}

			ET.Client.GamePlayTowerDefenseHelper.SendUpgradePlayerTower(self.ClientScene(), self.towerUnitId);
			self.OnClose();
		}
	}
}
