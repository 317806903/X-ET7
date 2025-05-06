using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(GameSettingComponent))]
    public static class GameSettingComponentSystem
    {
        [ObjectSystem]
        public class GameSettingComponentAwakeSystem : AwakeSystem<GameSettingComponent>
        {
            protected override void Awake(GameSettingComponent self)
            {
                GameSettingComponent.Instance = self;
                self.recordSettingValue = new();
                self.Init();
            }
        }

        public static void Init(this GameSettingComponent self)
        {
            self.InitSwitchOnOff();
        }

        // 初始化开关状态
        public static void InitSwitchOnOff(this GameSettingComponent self)
        {
            bool isRecord = false;
            bool value = false;
            // 音乐
            (isRecord, value) = self.ReadIsOn(GameSettingType.Music);
            if (isRecord == false)
            {
                self.WriteIsOn(GameSettingType.Music, true);
            }
            // 音效
            (isRecord, value) = self.ReadIsOn(GameSettingType.Audio);
            if (isRecord == false)
            {
                self.WriteIsOn(GameSettingType.Audio, true);
            }
            // 伤害数字
            (isRecord, value) = self.ReadIsOn(GameSettingType.DamageShow);
            if (isRecord == false)
            {
                self.WriteIsOn(GameSettingType.DamageShow, true);
            }
        }

        // 得到开关状态(外部)
        public static bool GetIsOn(this GameSettingComponent self, GameSettingType gameSettingType)
        {
            (bool isRecord, bool value) = self.ReadIsOn(gameSettingType);
            return value;
        }

        // 设置开关状态(外部)
        public static void SetIsOn(this GameSettingComponent self, GameSettingType gameSettingType, bool isOn)
        {
            self.WriteIsOn(gameSettingType, isOn);
        }

        // 读取开关状态（内部）
        public static (bool isRecord, bool value) ReadIsOn(this GameSettingComponent self, GameSettingType gameSettingType)
        {
            long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(self.ClientScene());
            if (myPlayerId == (long)ET.PlayerId.PlayerNone)
            {
                return (false, true);
            }
            if (self.recordSettingValue.TryGetValue(myPlayerId, gameSettingType, out bool tmp))
            {
                return (true, tmp);
            }

            string key = $"GameSetting_{myPlayerId}_{gameSettingType.ToString()}";
            if (PlayerPrefs.HasKey(key) == false)
            {
                self.recordSettingValue.Add(myPlayerId, gameSettingType, true);
                return (false, true);
            }
            int value = PlayerPrefs.GetInt(key);
            bool isOn = value == 1;
            self.recordSettingValue.Add(myPlayerId, gameSettingType, isOn);
            return (true, isOn);
        }

        // 写入开关状态(内部)
        public static void WriteIsOn(this GameSettingComponent self, GameSettingType gameSettingType, bool isOn)
        {
            long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(self.ClientScene());
            string key = $"GameSetting_{myPlayerId}_{gameSettingType.ToString()}";
            int value = isOn ? 1 : 0;
            if (self.recordSettingValue.TryGetValue(myPlayerId, gameSettingType, out bool tmp))
            {
                self.recordSettingValue[myPlayerId][gameSettingType] = isOn;
            }
            else
            {
                self.recordSettingValue.Add(myPlayerId, gameSettingType, isOn);
            }
            PlayerPrefs.SetInt(key, value);
        }
    }
}
