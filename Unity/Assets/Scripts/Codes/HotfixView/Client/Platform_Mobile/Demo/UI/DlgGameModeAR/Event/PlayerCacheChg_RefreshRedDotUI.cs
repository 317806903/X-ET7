﻿using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshRedDotUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType == PlayerModelType.OtherInfo)
            {
                await ET.Client.UIManagerHelper.DealPlayerUIRedDotType(scene, false);
            }
            await ETTask.CompletedTask;
        }
    }
}