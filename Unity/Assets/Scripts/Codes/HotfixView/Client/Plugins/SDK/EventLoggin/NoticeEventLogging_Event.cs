using System;
using System.Collections.Generic;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLogging_Event: AEvent<Scene, EventType.NoticeEventLogging>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeEventLogging args)
        {
            string eventName = args.eventName;
            Dictionary<string, object> properties = args.properties;
            if (properties != null)
            {
                PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
                if (playerStatusComponent != null)
                {
                    properties["PlayerStatus"] = playerStatusComponent.PlayerStatus.ToString();
                    properties["RoomType"] = playerStatusComponent.RoomType.ToString();
                    properties["SubRoomType"] = playerStatusComponent.SubRoomType.ToString();
                    properties["RoomId"] = playerStatusComponent.RoomId.ToString();
                    properties["LastBattleCfgId"] = playerStatusComponent.LastBattleCfgId.ToString();
                    properties["LastBattleResult"] = playerStatusComponent.LastBattleResult.ToString();
                    int challengeIndex = ET.AbilityConfig.TowerDefense_ChallengeLevelCfgCategory.Instance.GetChallengeIndex(playerStatusComponent.LastBattleCfgId);
                    properties["LastBattleChallengeIndex"] = challengeIndex.ToString();
                }
            }
            string timerKey = args.timerKey;
            if (string.IsNullOrEmpty(timerKey))
            {
                EventLoggingSDKComponent.Instance.Track(eventName, properties);
            }
            else
            {
                EventLoggingSDKComponent.Instance.Track(eventName, properties, timerKey);
            }

            string props = "";
            if (properties != null)
            {
                props = string.Join(",", properties);
            }
            Log.Debug($"EventLogging tracked {eventName} {timerKey} {props}");
            await ETTask.CompletedTask;
        }
    }
}
