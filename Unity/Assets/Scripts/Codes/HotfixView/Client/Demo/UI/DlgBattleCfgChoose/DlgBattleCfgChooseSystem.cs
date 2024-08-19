using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBattleCfgChoose))]
	public static class DlgBattleCfgChooseSystem
	{
		public static void RegisterUIEvent(this DlgBattleCfgChoose self)
		{
			self.View.E_BackButton.AddListenerAsync(self.OnBack);
			self.View.E_SureButton.AddListenerAsync(self.OnSure);

			self.View.E_InputFieldTMP_InputField.onValueChanged.AddListener(self.OnValueChanged);

			self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_GameCfgItem";
			self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.poolSize = 6;
			self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
				self.AddItemRefreshListener(transform, i));

            //清空默认节点
            self.View.E_DropdownGameModeTMP_Dropdown.options.Clear();
            //初始化
            for (int i = 0; i < self.gameModeList.Count; i++)
            {
                var tempData = new TMP_Dropdown.OptionData();
                string textKey = self.gameModeList[i][1];
                tempData.text = LocalizeComponent.Instance.GetTextValue(textKey);
                //tempData.image = gameModeList[i][2];
                self.View.E_DropdownGameModeTMP_Dropdown.options.Add(tempData);
            }
            //初始选项的显示
            self.View.E_DropdownGameModeTMP_Dropdown.value = 0;
            self.View.E_DropdownGameModeTMP_Dropdown.AddListener(self.OnGameModeChoose);

            //清空默认节点
            self.View.E_DropdownTeamModeTMP_Dropdown.options.Clear();

            //初始化
            for (int i = 0; i < self.teamModeList.Count; i++)
            {
                var tempData = new TMP_Dropdown.OptionData();
                string textKey = self.teamModeList[i][1];
                tempData.text = LocalizeComponent.Instance.GetTextValue(textKey);
                //tempData.image = teamModeList[i][2];
                self.View.E_DropdownTeamModeTMP_Dropdown.options.Add(tempData);
            }
            //初始选项的显示
            self.View.E_DropdownTeamModeTMP_Dropdown.value = 0;
            self.View.E_DropdownTeamModeTMP_Dropdown.AddListener(self.OnTeamModeChoose);

		}

		public static async ETTask ShowWindow(this DlgBattleCfgChoose self, ShowWindowData contextData = null)
		{
			DlgBattleCfgChoose_ShowWindowData dlgBattleCfgChooseShowWindowData = contextData as DlgBattleCfgChoose_ShowWindowData;
			self.isGlobalMode = dlgBattleCfgChooseShowWindowData.isGlobalMode;
			self.isAR = dlgBattleCfgChooseShowWindowData.isAR;

			Log.Debug($"self.isAR {self.isAR}");
			self.View.E_InputFieldTMP_InputField.text = "";
			self.matchKey = "";

			self.ShowBattleCfgList();
		}

		public static void ShowBattleCfgList(this DlgBattleCfgChoose self)
		{
			List<GamePlayBattleLevelCfg> list = self.GetBattleCfgItemList();
			int count = list.Count;
			if (count > 0)
			{
				self.curChooseIndex = 0;
			}
			else
			{
				self.curChooseIndex = -1;
			}
			self.AddUIScrollItems(ref self.ScrollItemGameCfgItems, count);
			self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.SetVisible(true, count);

		}

        public static void AddItemRefreshListener(this DlgBattleCfgChoose self, Transform transform, int index)
        {
	        List<GamePlayBattleLevelCfg> list = self.curlist;

            Scroll_Item_GameCfgItem itemGameCfgItem = self.ScrollItemGameCfgItems[index].BindTrans(transform);

            itemGameCfgItem.E_Text_NameTextMeshProUGUI.text = list[index].Name;
            if (self.curChooseIndex == index)
            {
	            itemGameCfgItem.E_CheckBoxImage.gameObject.SetActive(true);
	            itemGameCfgItem.E_ClickButton.gameObject.SetActive(false);
            }
            else
            {
	            itemGameCfgItem.E_CheckBoxImage.gameObject.SetActive(false);
	            itemGameCfgItem.E_ClickButton.gameObject.SetActive(true);
	            itemGameCfgItem.E_ClickButton.AddListener(() =>
	            {
		            self.OnChoose(index).Coroutine();
	            });
            }
        }

        public static async ETTask OnChoose(this DlgBattleCfgChoose self, int newIndex)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.curChooseIndex = newIndex;
            self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.RefreshCells();
        }

        public static async ETTask OnBack(this DlgBattleCfgChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleCfgChoose>();
        }

        public static async ETTask OnSure(this DlgBattleCfgChoose self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string gamePlayBattleLevelCfgId = self.GetCurChooseBattleCfgId();
            if (string.IsNullOrEmpty(gamePlayBattleLevelCfgId))
            {
	            string msg = "请选择";
				UIManagerHelper.ShowTip(self.DomainScene(), msg);
	            return;
            }
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.BattleCfgIdChoose()
            {
	            gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId,
            });
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleCfgChoose>();
        }

        public static void OnGameModeChoose(this DlgBattleCfgChoose self, int index)
        {
	        self.RefreshUI().Coroutine();
        }

        public static void OnTeamModeChoose(this DlgBattleCfgChoose self, int index)
        {
	        self.RefreshUI().Coroutine();
        }

        public static async ETTask RefreshUI(this DlgBattleCfgChoose self)
        {
	        self.ShowBattleCfgList();

	        await ETTask.CompletedTask;
        }

        public static List<GamePlayBattleLevelCfg> GetBattleCfgItemList(this DlgBattleCfgChoose self)
        {
	        int gameModeIndex = self.View.E_DropdownGameModeTMP_Dropdown.value;
	        int teamModeIndex = self.View.E_DropdownTeamModeTMP_Dropdown.value;

	        self.curlist.Clear();
	        List<GamePlayBattleLevelCfg> dataList = GamePlayBattleLevelCfgCategory.Instance.DataList;
	        for (int i = 0; i < dataList.Count; i++)
	        {
		        GamePlayBattleLevelCfg gamePlayBattleLevelCfg = dataList[i];
		        if (gamePlayBattleLevelCfg.IsGlobalMode != self.isGlobalMode)
		        {
			        continue;
		        }

		        if (self.isAR)
		        {
			        if (gamePlayBattleLevelCfg.SceneMap != "ARMap")
			        {
				        continue;
			        }
		        }
		        else
		        {
			        if (gamePlayBattleLevelCfg.SceneMap == "ARMap")
			        {
				        continue;
			        }
		        }
		        if (self.ChkGameMode(gamePlayBattleLevelCfg, gameModeIndex) == false)
		        {
			        continue;
		        }
		        if (self.ChkTeamMode(gamePlayBattleLevelCfg, teamModeIndex) == false)
		        {
			        continue;
		        }

		        if (string.IsNullOrEmpty(self.matchKey))
		        {
			        self.curlist.Add(gamePlayBattleLevelCfg);
		        }
		        else
		        {
			        bool isMatch = false;
			        if (isMatch == false)
			        {
				        isMatch = gamePlayBattleLevelCfg.Id.ToLower().Contains(self.matchKey.ToLower());
			        }
			        if (isMatch == false)
			        {
				        isMatch = gamePlayBattleLevelCfg.Name.ToLower().Contains(self.matchKey.ToLower());
				        isMatch = gamePlayBattleLevelCfg.Name.Contains(self.matchKey);
			        }
			        if (isMatch == false)
			        {
				        isMatch = gamePlayBattleLevelCfg.Desc.ToLower().Contains(self.matchKey.ToLower());
			        }

			        if (isMatch)
			        {
				        self.curlist.Add(gamePlayBattleLevelCfg);
			        }
		        }
	        }

	        return self.curlist;
        }

        public static string GetCurChooseBattleCfgId(this DlgBattleCfgChoose self)
        {
	        if (self.curChooseIndex == -1)
	        {
		        return "";
	        }
	        return self.curlist[self.curChooseIndex].Id;
        }

        public static bool ChkGameMode(this DlgBattleCfgChoose self, GamePlayBattleLevelCfg gamePlayBattleLevelCfg, int gameModeIndex)
        {
	        bool isMatching = false;
	        var gameMode = self.gameModeList[gameModeIndex];
	        if (gameMode[0] == "-1")
	        {
		        isMatching = true;
	        }
	        else if (gameMode[0] == "GamePlayTowerDefenseNormal" && gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseNormal)
	        {
		        isMatching = true;
	        }
	        else if (gameMode[0] == "GamePlayTowerDefenseEndlessChallengeMonster" && gamePlayBattleLevelCfg.GamePlayMode is GamePlayTowerDefenseEndlessChallengeMonster)
	        {
		        isMatching = true;
	        }
	        else if (gameMode[0] == "GamePlayPKNormal" && gamePlayBattleLevelCfg.GamePlayMode is GamePlayPKNormal)
	        {
		        isMatching = true;
	        }
	        else
	        {
		        isMatching = false;
	        }

	        return isMatching;
        }

        public static bool ChkTeamMode(this DlgBattleCfgChoose self, GamePlayBattleLevelCfg gamePlayBattleLevelCfg, int teamModeIndex)
        {
	        bool isMatching = false;
	        var teamMode = self.teamModeList[teamModeIndex];
	        if (teamMode[0] == "-1")
	        {
		        isMatching = true;
	        }
	        else if (teamMode[0] == "AllPlayersOneGroup" && gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup)
	        {
		        isMatching = true;
	        }
	        else if (teamMode[0] == "PlayerAlone" && gamePlayBattleLevelCfg.TeamMode is PlayerAlone)
	        {
		        isMatching = true;
	        }
	        else if (teamMode[0] == "PlayerTeam" && gamePlayBattleLevelCfg.TeamMode is PlayerTeam)
	        {
		        isMatching = true;
	        }
	        else
	        {
		        isMatching = false;
	        }

	        return isMatching;
        }

        public static void OnValueChanged(this DlgBattleCfgChoose self, string name)
        {
	        self.matchKey = self.View.E_InputFieldTMP_InputField.text;
	        self.RefreshUI().Coroutine();
        }

	}
}
