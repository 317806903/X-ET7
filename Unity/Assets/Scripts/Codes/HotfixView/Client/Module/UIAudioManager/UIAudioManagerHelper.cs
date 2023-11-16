using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
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

        public static void PlayUIAudioClick(Scene scene)
        {
            string resAudioCfgId = "ResAudio_UI_Click";
            PlayUIAudio(scene, resAudioCfgId);
        }

        public static void PlayUIAudioBack(Scene scene)
        {
            string resAudioCfgId = "ResAudio_UI_Back";
            PlayUIAudio(scene, resAudioCfgId);
        }

        public static void PlayUIAudioConfirm(Scene scene)
        {
            string resAudioCfgId = "ResAudio_UI_Confirm";
            PlayUIAudio(scene, resAudioCfgId);
        }

        public static void PlayUIAudioTowerPush(Scene scene)
        {
            string resAudioCfgId = "ResAudio_TowerPush";
            PlayUIAudio(scene, resAudioCfgId);
        }

        public static void PlayUIAudio(Scene scene, string resAudioCfgId)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayUIAudio(resAudioCfgId).Coroutine();
        }

        public static void PlayUIGuideAudio(Scene scene, string audioPath)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayUIGuideAudio(audioPath).Coroutine();
        }

        public static void PlayMusicLogin(Scene scene)
        {
            List<string> musicList = new List<string>(){"ResAudio_Music_login"};
            PlayMusic(scene, musicList);
        }

        public static void PlayMusicMain(Scene scene)
        {
            List<string> musicList = new List<string>(){"ResAudio_Music_main"};
            PlayMusic(scene, musicList);
        }

        public static void PlayMusic(Scene scene, List<string> resAudioCfgIds)
        {
            UIAudioManagerComponent _UIAudioManagerComponent = GetUIAudioManagerComponent(scene);
            _UIAudioManagerComponent.PlayMusic(resAudioCfgIds);
        }
    }
}