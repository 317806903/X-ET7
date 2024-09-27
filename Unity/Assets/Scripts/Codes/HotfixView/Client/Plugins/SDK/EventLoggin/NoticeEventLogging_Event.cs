using System;
using System.Collections.Generic;
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
                    properties["RoomId"] = playerStatusComponent.RoomId.ToString();
                    properties["LastBattleCfgId"] = playerStatusComponent.LastBattleCfgId;
                    properties["LastBattleResult"] = playerStatusComponent.LastBattleResult.ToString();
                    if (playerStatusComponent.RoomTypeInfo != null)
                    {
                        properties["RoomType"] = playerStatusComponent.RoomTypeInfo.roomType.ToString();
                        properties["SubRoomType"] = playerStatusComponent.RoomTypeInfo.subRoomType.ToString();
                        properties["seasonIndex"] = playerStatusComponent.RoomTypeInfo.seasonIndex.ToString();
                        properties["seasonCfgId"] = playerStatusComponent.RoomTypeInfo.seasonCfgId.ToString();
                        properties["pveIndex"] = playerStatusComponent.RoomTypeInfo.pveIndex.ToString();
                        properties["gamePlayBattleLevelCfgId"] = playerStatusComponent.RoomTypeInfo.gamePlayBattleLevelCfgId;
                    }
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
