using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.Ability.Client;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Tower";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTowerItemRefreshListener(transform, i));
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddMonsterItemRefreshListener(transform, i));
            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);

            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 22");
            self.RegisterClear().Coroutine();
            self.RegisterSkill().Coroutine();
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 33");
        }

        public static async ETTask RegisterClear(this DlgBattle self)
        {
            Unit myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            while (myUnit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            }
            UnitCfg unitCfg = myUnit.model;

            self.View.EButton_ClearMyTowerButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearMyTower(self.DomainScene()).Coroutine();
            });

            self.View.EButton_ClearAllMonsterButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearAllMonster(self.DomainScene()).Coroutine();
            });
        }

        public static async ETTask RegisterSkill(this DlgBattle self)
        {
            Unit myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            while (myUnit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            }
            UnitCfg unitCfg = myUnit.model;
            int count = unitCfg.SkillList.Count;
            List<string> skillList = unitCfg.SkillList.Keys.ToList();
            if (count > 0)
            {
                self.View.EButton_Skill1Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), skillList[0]).Coroutine();
                });
            }
            if (count > 1)
            {
                self.View.EButton_Skill2Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), skillList[1]).Coroutine();
                });
            }
            if (count > 2)
            {
                self.View.EButton_Skill3Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), skillList[2]).Coroutine();
                });
            }
            if (count > 3)
            {
                self.View.EButton_Skill4Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), skillList[3]).Coroutine();
                });
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgBattle self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Game);
            int countTower = self.towerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            int countTank = self.monsterList.Count;
            self.AddUIScrollItems(ref self.ScrollItemMonsters, countTank);
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.SetVisible(true, countTank);

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleFrameTimer, self);

            ET.Client.UIManagerHelper.ShowARMesh(self.DomainScene()).Coroutine();
            self.ShowPutTipMsg("");

            //self.PlayMusic();
        }

        // public static void PlayMusic(this DlgBattle self)
        // {
        //     GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
        //     List<string> musicList = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
        //     UIAudioManagerHelper.PlayMusic(self.DomainScene(), musicList);
        // }

        public static void HideWindow(this DlgBattle self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
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

        public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            string itemCfgId = self.towerList[index];
            itemTower.ShowBagItem(itemCfgId, true);

            itemTower.ELabel_NumTextMeshProUGUI.text = "1";

            ET.EventTriggerListener.Get(itemTower.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                DlgBattleDragItem_ShowWindowData showWindowData = new()
                {
                    battleDragItemType = BattleDragItemType.PKTower,
                    battleDragItemParam = itemCfgId,
                    createActionIds = self.View.E_InputFieldCreateActionTowerTMP_InputField.text,
                    callBack = (scene) =>
                    {
                    },
                };
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
            });
        }

        public static void AddMonsterItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemMonster = self.ScrollItemMonsters[index].BindTrans(transform);

            string itemCfgId = self.monsterList[index];
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(itemCfgId);

            itemMonster.ShowBagItem(itemCfgId, true);
            itemMonster.ELabel_NumTextMeshProUGUI.text = $"1";

            ET.EventTriggerListener.Get(itemMonster.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                DlgBattleDragItem_ShowWindowData showWindowData = new()
                {
                    battleDragItemType = BattleDragItemType.PKMonster,
                    battleDragItemParam = monsterCfg.Id,
                    countOnce = int.Parse(self.View.E_InputFieldTMP_InputField.text),
                    createActionIds = self.View.E_InputFieldCreateActionTMP_InputField.text,
                    callBack = (scene) =>
                    {
                    },
                };
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
            });
        }

        public static void ChgScrollRectMoveStatus(this DlgBattle self, bool status)
        {
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.enabled = status;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.enabled = status;
        }

        public static void Update(this DlgBattle self)
        {
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

    }
}