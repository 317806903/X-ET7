using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Client
{
    public static class PlayerStatusHelper
    {
        public static PlayerComponent GetMyPlayerComponent(Scene scene)
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
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            PlayerComponent playerComponent = clientScene.GetComponent<PlayerComponent>();
            return playerComponent;
        }

        public static Player GetMyPlayer(Scene scene)
        {
            PlayerComponent playerComponent = GetMyPlayerComponent(scene);
            if (playerComponent == null)
            {
                return null;
            }
            foreach (Entity entity in playerComponent.Children.Values)
            {
                Player player = entity as Player;
                return player;
            }

            return null;
        }

        public static PlayerStatusComponent GetMyPlayerStatusComponent(Scene scene)
        {
            Player player = GetMyPlayer(scene);
            return player?.GetComponent<PlayerStatusComponent>();
        }

        public static void RefreshMyPlayer(Scene scene, Entity entity)
        {
            PlayerComponent playerComponent = GetMyPlayerComponent(scene);
            if (playerComponent == null)
            {
                return;
            }
            Player player = GetMyPlayer(scene);
            if (player != null)
            {
                player.Dispose();
            }
            playerComponent.AddChild(entity);
        }

        public static void RefreshMyPlayerStatus(Scene scene, Entity entity)
        {
            Player player = GetMyPlayer(scene);
            player.RemoveComponent<PlayerStatusComponent>();
            player.AddComponent(entity);

            EventSystem.Instance.Publish(scene, new EventType.NoticePlayerStatusChg());
        }

        public static long GetMyPlayerId(Scene scene)
        {
            Player player = GetMyPlayer(scene);
            return player.Id;
        }

        public static string GetAvatarIcon(int index)
        {
            var list = GlobalSettingCfgCategory.Instance.AvatarIcons;
            if (index < list.Count)
            {
                string iconKey = list[index];
                string iconPath = ResIconCfgCategory.Instance.Get(iconKey).ResName;
                return iconPath;
            }
            return "";
        }

        public static List<string> GetAvatarIconList()
        {
            List<string> avatarIconList = new();
            var list = GlobalSettingCfgCategory.Instance.AvatarIcons;
            for (int i = 0; i < list.Count; i++)
            {
                string iconKey = list[i];
                string iconPath = ResIconCfgCategory.Instance.Get(iconKey).ResName;
                avatarIconList.Add(iconPath);
            }
            return avatarIconList;
        }

        public static async ETTask SendGetPlayerStatus(Scene scene)
        {
            G2C_GetPlayerStatus _G2C_GetPlayerStatus = await ET.Client.SessionHelper.GetSession(scene).Call(new C2G_GetPlayerStatus()) as G2C_GetPlayerStatus;

            if (_G2C_GetPlayerStatus.Error != ET.ErrorCode.ERR_Success)
            {
                EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
                {
                    tipMsg = _G2C_GetPlayerStatus.Message,
                });
            }
            else
            {
                byte[] byts = _G2C_GetPlayerStatus.PlayerStatusComponentBytes;
                Entity entity = MongoHelper.Deserialize<Entity>(byts);
                PlayerStatusHelper.RefreshMyPlayerStatus(scene, entity);
            }

            await ETTask.CompletedTask;
        }
    }
}