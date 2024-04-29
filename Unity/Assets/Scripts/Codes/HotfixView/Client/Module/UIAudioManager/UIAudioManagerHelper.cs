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
        Upgradation,
        Reclaim,
        Buy,
        Sell,
        DefeatOneWave,
        Attacked,
        Forbidden,
        BattleForbidden,
        TowerPush,
        GameEndFinish,
        GameEndWin,
        GameEndFail
    }

    public enum MusicType
    {
        Login,
        Main,
        ARStart,
        Game
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

        public static void PlayUIAudio(Scene scene, SoundEffectType soundEffectType, bool needLoopPlay = false)
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
                case SoundEffectType.Scan:
                    resAudioCfgId = "ResAudio_UI_Scan";
                    _UIAudioManagerComponent.PlayScanAudio(resAudioCfgId, needLoopPlay);
                    return;
                    break;
                case SoundEffectType.ReadyGo:
                    resAudioCfgId = "ResAudio_UI_ready_go";
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
                case SoundEffectType.GameEndFinish:
                    resAudioCfgId = "ResAudio_Music_Finish";
                    break;
                case SoundEffectType.GameEndWin:
                    resAudioCfgId = "ResAudio_Music_Win";
                    break;
                case SoundEffectType.GameEndFail:
                    resAudioCfgId = "ResAudio_Music_Fail";
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

        // public static void PlayMusicLogin(Scene scene)
        // {
        //     List<string> musicList = new List<string>(){"ResAudio_Music_login"};
        //     PlayMusic(scene, musicList);
        // }

        // public static void PlayMusicMain(Scene scene)
        // {
        //     List<string> musicList = new List<string>(){"ResAudio_Music_main"};
        //     PlayMusic(scene, musicList);
        // }

        // public static void PlayMusicARStart(Scene scene)
        // {
        //     List<string> musicList = new List<string>(){"ResAudio_Music_ARStarted"};
        //     PlayMusic(scene, musicList);
        // }

        // public static void PlayMusicGame(Scene scene)
        // {
        //     GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
        //     List<string> musicList = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
        //     PlayMusic(scene, musicList);
        // }

        public static void PlayMusic(Scene scene, MusicType musicType)
        {
            List<string> resAudioCfgIds = null;
            switch (musicType)
            {
                case MusicType.Login:
                    resAudioCfgIds = new List<string>(){"ResAudio_Music_main"};
                    break;
                case MusicType.Main:
                    resAudioCfgIds = new List<string>(){"ResAudio_Music_main"};
                    break;
                case MusicType.ARStart:
                    resAudioCfgIds = new List<string>(){"ResAudio_Music_ARStarted"};
                    break;
                case MusicType.Game:
                    GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
                    resAudioCfgIds = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
                    break;
                default:
                    break;
            }

            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayMusic(resAudioCfgIds);
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