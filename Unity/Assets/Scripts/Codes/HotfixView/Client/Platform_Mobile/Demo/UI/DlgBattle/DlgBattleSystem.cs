﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleFrameTimer)]
    public class DlgBattleTimer: ATimer<DlgBattle>
    {
        protected override void Run(DlgBattle self)
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

    [FriendOf(typeof (DlgBattle))]
    public static class DlgBattleSystem
    {
        public static void RegisterUIEvent(this DlgBattle self)
        {
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 11");
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBattle";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTowerItemRefreshListener(transform, i));

            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBattle";
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddMonsterItemRefreshListener(transform, i));

            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);

            self.View.E_GameSettingButton.SetVisible(true);
            self.View.E_GameSettingButton.AddListenerAsync(self.GameSetting);

            self.View.E_InputFieldMatchTowerTMP_InputField.onValueChanged.AddListener(self.OnTowerValueChanged);
            self.View.E_InputFieldMatchMonsterTMP_InputField.onValueChanged.AddListener(self.OnMonsterValueChanged);

            self.View.EButton_TowerLeftButton.AddListener(self.TowerLeft);
            self.View.EButton_TowerRightButton.AddListener(self.TowerRight);

            self.View.EButton_MonsterLeftButton.AddListener(self.MonsterLeft);
            self.View.EButton_MonsterRightButton.AddListener(self.MonsterRight);

            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 22");
            self.RegisterClear().Coroutine();
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 33");
        }

        public static async ETTask RegisterClear(this DlgBattle self)
        {
            self.View.EButton_ClearMyTowerButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearMyTower(self.DomainScene(), -1).Coroutine();
            });

            self.View.EButton_ClearAllMonsterButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearAllMonster(self.DomainScene()).Coroutine();
            });
        }

        public static async ETTask ShowWindow(this DlgBattle self, ShowWindowData contextData = null)
        {
            self.DealList();

            int countTower = self.matchTowerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            int countMonster = self.monsterList.Count;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, countMonster);
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetVisible(true, countMonster);

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleFrameTimer, self);

            ET.Client.UIManagerHelper.ShowARMesh(self.DomainScene()).Coroutine();
            self.ShowPutTipMsg("");

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleCameraPlayerSkill>();
            ClientEventType.NoticeGamePlayPKStatusWhenClient _NoticeGamePlayPKStatusWhenClient = new()
            {
            };
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayPKStatusWhenClient);

            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattlePlayerSkill>();

        }

        public static void OnTowerValueChanged(this DlgBattle self, string value)
        {
            string matchKey = value.Trim().ToLower();
            self.GetMatchTowerList(matchKey);
            int countTower = self.matchTowerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);
        }

        public static void OnMonsterValueChanged(this DlgBattle self, string value)
        {
            string matchKey = value.Trim().ToLower();
            self.GetMatchMonsterList(matchKey);
            int countMonster = self.matchMonsterList.Count;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, countMonster);
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.SetVisible(true, countMonster);
        }

        public static void GetMatchMonsterList(this DlgBattle self, string matchKey)
        {
            self.matchMonsterList.Clear();
            if (string.IsNullOrEmpty(matchKey))
            {
                self.matchMonsterList.AddRange(self.monsterList);
                return;
            }
            var towerDefenseMonsterList = TowerDefense_MonsterCfgCategory.Instance.DataList;
            foreach (TowerDefense_MonsterCfg towerDefenseMonster in towerDefenseMonsterList)
            {
                string cfgId = towerDefenseMonster.Id;
                bool contains = cfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }

                if (towerDefenseMonster.UnitId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
                ItemCfg item = ItemCfgCategory.Instance.Get(cfgId);
                string nameKey = item.Name_l10n_key;
                string name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.EN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
                name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.CN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
            }
            var towerDefenseTowerList = TowerDefense_TowerCfgCategory.Instance.DataList;
            foreach (TowerDefense_TowerCfg towerDefenseTowerCfg in towerDefenseTowerList)
            {
                string cfgId = towerDefenseTowerCfg.Id;
                bool contains = cfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }

                if (towerDefenseTowerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
                {
                    contains = towerToUnitCfgId.CfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else if (towerDefenseTowerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
                {
                    foreach (string unitCfgId in towerToMonsterCfgId.CfgId)
                    {
                        contains = unitCfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (contains)
                        {
                            break;
                        }
                    }
                }
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
                ItemCfg item = ItemCfgCategory.Instance.Get(cfgId);
                string nameKey = item.Name_l10n_key;
                string name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.EN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
                name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.CN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchMonsterList.Add(cfgId);
                    continue;
                }
            }
        }

        public static void GetMatchTowerList(this DlgBattle self, string matchKey)
        {
            self.matchTowerList.Clear();
            if (string.IsNullOrEmpty(matchKey))
            {
                self.matchTowerList.AddRange(self.towerList);
                return;
            }
            var towerDefenseTowerList = TowerDefense_TowerCfgCategory.Instance.DataList;
            foreach (TowerDefense_TowerCfg towerDefenseTowerCfg in towerDefenseTowerList)
            {
                string cfgId = towerDefenseTowerCfg.Id;
                bool contains = cfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchTowerList.Add(cfgId);
                    continue;
                }

                if (towerDefenseTowerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
                {
                    contains = towerToUnitCfgId.CfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else if (towerDefenseTowerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
                {
                    foreach (string unitCfgId in towerToMonsterCfgId.CfgId)
                    {
                        contains = unitCfgId.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (contains)
                        {
                            break;
                        }
                    }
                }

                if (contains)
                {
                    self.matchTowerList.Add(cfgId);
                    continue;
                }
                ItemCfg item = ItemCfgCategory.Instance.Get(cfgId);
                string nameKey = item.Name_l10n_key;
                string name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.EN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchTowerList.Add(cfgId);
                    continue;
                }
                name = LocalizeComponent.Instance.GetTextValueByExcel(LanguageType.CN, nameKey);
                contains = name.IndexOf(matchKey, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    self.matchTowerList.Add(cfgId);
                    continue;
                }
            }
        }

        public static void DealList(this DlgBattle self)
        {
            for (int i = self.towerList.Count - 1; i >= 0; i--)
            {
                string cfgId = self.towerList[i];
                bool bRet = ItemCfgCategory.Instance.Contain(cfgId);
                if (bRet == false)
                {
                    Log.Error($"ItemCfgCategory.Instance.Contain({cfgId}) == false");
                    self.towerList.RemoveAt(i);
                    continue;
                }
                bRet = TowerDefense_TowerCfgCategory.Instance.Contain(cfgId);
                if (bRet == false)
                {
                    Log.Error($"TowerDefense_TowerCfgCategory.Instance.Contain({cfgId}) == false");
                    self.towerList.RemoveAt(i);
                    continue;
                }
            }
            for (int i = self.monsterList.Count - 1; i >= 0; i--)
            {
                string cfgId = self.monsterList[i];
                bool bRet = ItemCfgCategory.Instance.Contain(cfgId);
                if (bRet == false)
                {
                    Log.Error($"ItemCfgCategory.Instance.Contain({cfgId}) == false");
                    self.monsterList.RemoveAt(i);
                    continue;
                }
                bRet = TowerDefense_MonsterCfgCategory.Instance.Contain(cfgId);
                if (bRet == false)
                {
                    bRet = TowerDefense_TowerCfgCategory.Instance.Contain(cfgId);
                    if (bRet == false)
                    {
                        Log.Error($"TowerDefense_MonsterCfgCategory.Instance.Contain({cfgId}) == false");
                        Log.Error($"TowerDefense_TowerCfgCategory.Instance.Contain({cfgId}) == false");
                        self.monsterList.RemoveAt(i);
                        continue;
                    }
                }
            }

            self.GetMatchMonsterList("");
            self.GetMatchTowerList("");
        }

        public static void HideWindow(this DlgBattle self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleCameraPlayerSkill>();
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattlePlayerSkill>();
        }

        public static async ETTask QuitBattle(this DlgBattle self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                self._QuitBattle().Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask _QuitBattle(this DlgBattle self)
        {
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static async ETTask GameSetting(this DlgBattle self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleSetting>().Coroutine();
        }

        public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_TowerBattle itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            string towerCfgId = self.matchTowerList[index];
            itemTower.Init(towerCfgId, true).Coroutine();

            ET.EventTriggerListener.Get(itemTower.GetActionButton()).onPress.AddListener((go, xx) =>
            {
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowBattleDragItem()
                {
                    battleDragItemType = BattleDragItemType.PKTower,
                    battleDragItemParam = towerCfgId,
                    createActionIds = self.View.E_InputFieldCreateActionTowerTMP_InputField.text,
                    sceneIn = self.DomainScene(),
                    callBack = (scene) =>
                    {
                        EventSystem.Instance.Publish(scene, new ClientEventType.HideBattleDragItem());
                    },
                });
            });
        }

        public static void AddMonsterItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_TowerBattle itemMonster = self.ScrollItemMonsters[index].BindTrans(transform);

            string monsterCfgId = self.matchMonsterList[index];

            itemMonster.Init(monsterCfgId, true).Coroutine();

            ET.EventTriggerListener.Get(itemMonster.GetActionButton()).onPress.AddListener((go, xx) =>
            {
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowBattleDragItem()
                {
                    battleDragItemType = BattleDragItemType.PKMonster,
                    battleDragItemParam = monsterCfgId,
                    countOnce = int.Parse(self.View.E_InputFieldTMP_InputField.text),
                    createActionIds = self.View.E_InputFieldCreateActionTMP_InputField.text,
                    sceneIn = self.DomainScene(),
                    callBack = (scene) =>
                    {
                        EventSystem.Instance.Publish(scene, new ClientEventType.HideBattleDragItem());
                    },
                });
            });
        }

        public static void ChgScrollRectMoveStatus(this DlgBattle self, bool status)
        {
            self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect.enabled = status;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.enabled = status;
        }

        public static void Update(this DlgBattle self)
        {
            //self.View.ELoopScrollList_SkillLoopHorizontalScrollRect.RefreshCells();
        }

        public static void ShowPutTipMsg(this DlgBattle self, string tipMsg)
        {
            if (string.IsNullOrEmpty(tipMsg))
            {
                self.HidePutTipMsg();
                return;
            }
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattle self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static void TowerLeft(this DlgBattle self)
        {
            var towerScrollRect = self.View.ELoopScrollList_TowerLoopHorizontalScrollRect;
            float offset;
            int currentFirst = towerScrollRect.reverseDirection ? towerScrollRect.GetLastItem(out offset) : towerScrollRect.GetFirstItem(out offset);
            int newIndex = math.clamp(currentFirst - 6, 0, towerScrollRect.totalCount);
            towerScrollRect.SrollToCell(newIndex, 10000);
        }

        public static void TowerRight(this DlgBattle self)
        {
            var towerScrollRect = self.View.ELoopScrollList_TowerLoopHorizontalScrollRect;
            float offset;
            int currentFirst = towerScrollRect.reverseDirection ? towerScrollRect.GetLastItem(out offset) : towerScrollRect.GetFirstItem(out offset);
            int newIndex = math.clamp(currentFirst + 6, 0, towerScrollRect.totalCount);
            towerScrollRect.SrollToCell(newIndex, 10000);
        }

        public static void MonsterLeft(this DlgBattle self)
        {
            var monsterScrollRect = self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect;
            float offset;
            int currentFirst = monsterScrollRect.reverseDirection ? monsterScrollRect.GetLastItem(out offset) : monsterScrollRect.GetFirstItem(out offset);
            int newIndex = math.clamp(currentFirst - 6, 0, monsterScrollRect.totalCount);
            monsterScrollRect.SrollToCell(newIndex, 10000);
        }

        public static void MonsterRight(this DlgBattle self)
        {
            var monsterScrollRect = self.View.ELoopScrollList_MonsterLoopHorizontalScrollRect;
            float offset;
            int currentFirst = monsterScrollRect.reverseDirection ? monsterScrollRect.GetLastItem(out offset) : monsterScrollRect.GetFirstItem(out offset);
            int newIndex = math.clamp(currentFirst + 6, 0, monsterScrollRect.totalCount);
            monsterScrollRect.SrollToCell(newIndex, 10000);
        }

    }
}