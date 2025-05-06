using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public enum SoundEffectType
    {
        Click,
        Back,
        Confirm,
        PopUp,
        JoinRoom,
        Quit,
        Reward,
        Scan,
        ReadyGo,
        NextWave,
        Upgradation,
        Reclaim,
        Buy,
        Sell,
        DefeatOneWave,
        Attacked,
        Forbidden,
        BattleForbidden,
        TowerPush,
        BringUp,
        GetItem,
        Unlock,
        Revive,

    }

    public enum MusicType
    {
        Login,
        Main,
        None,
        ARStart,
        ARScan,
        GameEndFinish,
        GameEndWin,
        GameEndFail
    }

    [FriendOf(typeof (Unit))]
    public static class UIAudioManagerHelper
    {
        public static UIAudioManagerComponent GetUIAudioManagerComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = scene.ClientScene();
            }

            UIAudioManagerComponent _UIAudioManagerComponent = clientScene.GetComponent<UIAudioManagerComponent>();
            if (_UIAudioManagerComponent == null)
            {
                _UIAudioManagerComponent = clientScene.AddComponent<UIAudioManagerComponent>();
            }
            return _UIAudioManagerComponent;
        }

        public static void PlayUIAudio(Scene scene, SoundEffectType soundEffectType)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            string resAudioCfgId = "";
            switch (soundEffectType)
            {
                case SoundEffectType.Click:
                    resAudioCfgId = "ResAudio_UI_Click";
                    break;
                case SoundEffectType.Back:
                    resAudioCfgId = "ResAudio_UI_Back";
                    break;
                case SoundEffectType.Confirm:
                    resAudioCfgId = "ResAudio_UI_Confirm";
                    break;
                case SoundEffectType.PopUp:
                    resAudioCfgId = "ResAudio_UI_Pop-up";
                    break;
                case SoundEffectType.JoinRoom:
                    resAudioCfgId = "ResAudio_UI_Join";
                    break;
                case SoundEffectType.Quit:
                    resAudioCfgId = "ResAudio_UI_Quit";
                    break;
                case SoundEffectType.Reward:
                    resAudioCfgId = "ResAudio_UI_Reward";
                    break;
                case SoundEffectType.ReadyGo:
                    resAudioCfgId = "ResAudio_UI_ready_go";
                    break;
                case SoundEffectType.NextWave:
                    resAudioCfgId = "ResAudio_UI_NextWave";
                    break;
                case SoundEffectType.Upgradation:
                    resAudioCfgId = "ResAudio_UI_Upgradation";
                    break;
                case SoundEffectType.Reclaim:
                    resAudioCfgId = "ResAudio_UI_Recovery";
                    break;
                case SoundEffectType.Buy:
                    resAudioCfgId = "ResAudio_UI_Buy";
                    break;
                case SoundEffectType.Sell:
                    resAudioCfgId = "ResAudio_UI_Sell";
                    break;
                case SoundEffectType.DefeatOneWave:
                    resAudioCfgId = "ResAudio_UI_Victory";
                    break;
                case SoundEffectType.Attacked:
                    resAudioCfgId = "ResAudio_UI_Attacked";
                    break;
                case SoundEffectType.Forbidden:
                    resAudioCfgId = "ResAudio_UI_Forbidden";
                    break;
                case SoundEffectType.BattleForbidden:
                    resAudioCfgId = "ResAudio_UI_Battle_forbidden";
                    break;
                case SoundEffectType.TowerPush:
                    resAudioCfgId = "ResAudio_TowerPush";
                    break;
                case SoundEffectType.BringUp:
                    resAudioCfgId = "ResAudio_UI_BringUp";
                    break;
                case SoundEffectType.GetItem:
                    resAudioCfgId = "ResAudio_UI_GetItem";
                    break;
                case SoundEffectType.Unlock:
                    resAudioCfgId = "ResAudio_UI_Unlock";
                    break;
                case SoundEffectType.Revive:
                    resAudioCfgId = "ResAudio_UI_Revive";
                    break;
                default:
                    break;
            }
            _UIAudioManagerComponent.PlayUIAudio(resAudioCfgId).Coroutine();
        }

        public static void PlayUIGuideAudio(Scene scene, string audioPath)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayUIAudioByPath(audioPath).Coroutine();
        }

        public static void PlayMusic(Scene scene, MusicType musicType)
        {
            Dictionary<string, float> audioList = null;
            switch (musicType)
            {
                case MusicType.Login:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_login", 1);
                    break;
                case MusicType.Main:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_main", 1);
                    break;
                case MusicType.ARStart:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_ARStarted", 1);
                    break;
                case MusicType.ARScan:
                    Log.Error($"PlayMusic cannot MusicType.ARScan ");
                    return;
                    break;
                case MusicType.GameEndFinish:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_Finish", 1);
                    break;
                case MusicType.GameEndWin:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_Win", 1);
                    break;
                case MusicType.GameEndFail:
                    audioList = new ();
                    audioList.Add("ResAudio_Music_Fail", 1);
                    break;
                default:
                    break;
            }

            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayMusic(audioList);
        }

        public static void PlayHighestMusic(Scene scene, MusicType musicType, bool isLoop = true)
        {
            Dictionary<string, float> audioList = null;
            switch (musicType)
            {
                case MusicType.None:
                    audioList = new ();
                    break;
                case MusicType.ARScan:
                    audioList = new ();
                    audioList.Add("ResAudio_UI_Scan", 1);
                    break;
                default:
                    Log.Error($"PlayHighestMusic cannot other ");
                    return;
            }

            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayHighestMusic(audioList, isLoop);
        }

        public static void ResetMusicStatus(Scene scene)
        {
            bool isOn = GameSettingComponent.Instance.GetIsOn(GameSettingType.Music);
            if (isOn)
            {
                ResumeMusic(scene);
            }
            else
            {
                StopMusic(scene);
            }
        }

        public static void StopMusic(Scene scene)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.StopMusic();
        }

        public static void ResumeMusic(Scene scene)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.ResumeMusic();
        }

    }
}