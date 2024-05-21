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
            if (self.recordSettingValue.TryGetValue(gameSettingType, out bool tmp))
            {
                return (true, tmp);
            }

            string key = $"GameSetting_{gameSettingType.ToString()}";
            if (PlayerPrefs.HasKey(key) == false)
            {
                return (false, false);
            }
            int value = PlayerPrefs.GetInt(key);
            self.recordSettingValue[gameSettingType] = value == 1;
            return (true, value == 1);
        }

        // 写入开关状态(内部)
        public static void WriteIsOn(this GameSettingComponent self, GameSettingType gameSettingType, bool isOn)
        {
            string key = $"GameSetting_{gameSettingType.ToString()}";
            int value = isOn ? 1 : 0;
            self.recordSettingValue[gameSettingType] = value == 1;
            PlayerPrefs.SetInt(key, value);
        }
    }
}
