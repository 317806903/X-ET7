using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
    //战斗设置面板
    [FriendOf(typeof(DlgBattleSetting))]
    public static class DlgBattleSettingSystem
    {
        //注册事件
        public static void RegisterUIEvent(this DlgBattleSetting self)
        {
            // 背景与关闭按钮
            self.View.E_BG_ClickButton.AddListener(()=>
            {
                if (self.ChkCanClickBg() == false)
                {
                    return;
                }
                self.OnBGClick();
            });
            self.View.E_BtnCloseButton.AddListener(self.OnBGClick);

            //Rescan与Quitbatte按钮
            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);
            self.View.E_ReScanButton.AddListenerAsync(self.ReScan);

            // 音乐开关
            EventTriggerListener.Get(self.View.EG_Button_MusicRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Music();
            });

            // 音效开关
            EventTriggerListener.Get(self.View.EG_Button_AudioRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_Audio();
            });

            // 伤害显示开关
            EventTriggerListener.Get(self.View.EG_Button_DamagerShowRectTransform.gameObject).onClick.AddListener((go, xx) =>
            {
                self.ChgStatus_DamageShow();
            });

            //教学视频
            self.View.E_TutorialButton.AddListenerAsync(self.OnClickTutorial);

        }

        // 显示
        public static async ETTask ShowWindow(this DlgBattleSetting self, ShowWindowData contextData = null)
        {
            self.dlgShowTime = TimeHelper.ClientNow();

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.PopUp);
            self.ShowBg();
            self.SetSwitchOnOffUI();

            self.ChkShowReScan();
        }

        public static bool ChkCanClickBg(this DlgBattleSetting self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        public static void ChkShowReScan(this DlgBattleSetting self)
        {
        //     if (ET.SceneHelper.ChkIsGameModeArcade())
        //     {
        //         self.View.E_ReScanButton.transform.SetImageGray(true);
        //         return;
        //     }
        //     GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
        //     if (gamePlayTowerDefenseComponent == null)
        //     {
        //         self.View.E_ReScanButton.transform.SetImageGray(true);
        //         return;
        //     }
        //     long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
        //
        //     if (gamePlayTowerDefenseComponent.ownerPlayerId == myPlayerId)
        //     {
        //         self.View.E_ReScanButton.transform.SetImageGray(false);
        //     }
        //     else
        //     {
        //         self.View.E_ReScanButton.transform.SetImageGray(true);
        //     }
        }

        // 检查 AR 相机是否启用后设置不同的背景
        public static void ShowBg(this DlgBattleSetting self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            if (isARCameraEnable)
            {
                self.View.EG_bgARRectTransform.SetVisible(true);
                self.View.EG_bgRectTransform.SetVisible(false);
            }
            else
            {
                self.View.EG_bgARRectTransform.SetVisible(false);
                self.View.EG_bgRectTransform.SetVisible(true);
            }
        }

        // 隐藏窗口
        public static void HideWindow(this DlgBattleSetting self)
        {
        }

        #region  控制UI控件的显隐
        public static void SetSwitchOnOffUI(this DlgBattleSetting self)
        {
            self.SetSwitchOnOff_Music();
            self.SetSwitchOnOff_Audio();
            self.SetSwitchOnOff_DamageShow();
        }
        // 设置音乐开关UI
        public static void SetSwitchOnOff_Music(this DlgBattleSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            self.View.EG_Music_OnRectTransform.SetVisible(isOn);
            self.View.EG_Music_OffRectTransform.SetVisible(!isOn);
        }
        // 设置音频开关UI
        public static void SetSwitchOnOff_Audio(this DlgBattleSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);
            self.View.EG_Audio_OnRectTransform.SetVisible(isOn);
            self.View.EG_Audio_OffRectTransform.SetVisible(!isOn);
        }
        // 设置伤害显示开关UI
        public static void SetSwitchOnOff_DamageShow(this DlgBattleSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);
            self.View.EG_DamageShow_OnRectTransform.SetVisible(isOn);
            self.View.EG_DamageShow_OffRectTransform.SetVisible(!isOn);
        }
        #endregion

        #region 事件监听函数
        // 改变音乐状态
        public static void ChgStatus_Music(this DlgBattleSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Music, !isOn);
            UIAudioManagerHelper.ResetMusicStatus(self.DomainScene());
            self.SetSwitchOnOffUI();
        }
        // 改变音效状态
        public static void ChgStatus_Audio(this DlgBattleSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Audio);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.Audio, !isOn);
            self.SetSwitchOnOffUI();
        }
        // 改变伤害显示状态
        public static void ChgStatus_DamageShow(this DlgBattleSetting self)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.DamageShow);
            GameSettingComponent.Instance.SetIsOn(GameSettingType.DamageShow, !isOn);
            self.SetSwitchOnOffUI();
        }
        // 当背景（或关闭按钮）被点击时，关闭窗口
        public static void OnBGClick(this DlgBattleSetting self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleSetting>();
        }
        //退出战斗按钮
        public static async ETTask QuitBattle(this DlgBattleSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                self._QuitBattle().Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        //真正的退出战斗逻辑
        public static async ETTask _QuitBattle(this DlgBattleSetting self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent != null && gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "LevelEnded",
                    properties = new()
                    {
                        {"finished", false},
                        {"max_wave_num", self.GetCurMonsterWave()},
                        {"tower_num", self.GetMyTowerList().Count},
                        {"coin_num", self.GetMyGold()},
                    }
                });
            }

            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        //重新扫描按钮
        public static async ETTask ReScan(this DlgBattleSetting self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);

            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Des_WhenArcade");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
                {
                }, null, null, null, null);

                return;
            }

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());

            if (gamePlayTowerDefenseComponent.ownerPlayerId == myPlayerId)
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Des");
                string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Confirm");
                string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Cancel");
                string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Title");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
                {
                    ET.Client.GamePlayTowerDefenseHelper.SendReScan(self.ClientScene()).Coroutine();
                }, null, sureTxt, cancelTxt, titleTxt);
            }
            else
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_RescanTheGame_Des_WhenNotOwner");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
                {
                }, null, null, null, null);
            }

        }

        #endregion

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleSetting self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }
        public static int GetCurMonsterWave(this DlgBattleSetting self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
            return monsterWaveCallComponent.GetWaveIndex();
        }
        public static List<long> GetMyTowerList(this DlgBattleSetting self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            List<long> towerList = gamePlayTowerDefenseComponent.GetPutTowers(myPlayerId);
            return towerList;
        }

        public static int GetMyGold(this DlgBattleSetting self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinTypeInGame.Gold);
            return curGoldValue;
        }

        public static void TrackFunctionClicked(this DlgBattleSetting self, string name)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "FunctionClicked",
                properties = new()
                {
                    {"function_name", name},
                }
            });
        }

        public static async ETTask OnClickTutorial(this  DlgBattleSetting self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindow<DlgTutorials>();
            await ETTask.CompletedTask;
        }

    }
}
