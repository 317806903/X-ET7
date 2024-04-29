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
        public class GameSettingComponentAwakeSystem: AwakeSystem<GameSettingComponent>
        {
            protected override void Awake(GameSettingComponent self)
            {
                GameSettingComponent.Instance = self;
                self.Init();
            }
        }

        public static void Init(this GameSettingComponent self)
        {
            self.InitSwitchOnOff();
        }

        public static void InitSwitchOnOff(this GameSettingComponent self)
        {
            bool isRecord = false;
            bool value = false;
            (isRecord, value) = self.ReadIsOn(GameSettingType.Music);
            if (isRecord == false)
            {
                self.RecordIsOn(GameSettingType.Music, true);
            }
            (isRecord, value) = self.ReadIsOn(GameSettingType.Audio);
            if (isRecord == false)
            {
                self.RecordIsOn(GameSettingType.Audio, true);
            }
            (isRecord, value) = self.ReadIsOn(GameSettingType.DamageShow);
            if (isRecord == false)
            {
                self.RecordIsOn(GameSettingType.DamageShow, true);
            }
        }

        public static bool GetIsOn(this GameSettingComponent self, GameSettingType gameSettingType)
        {
            (bool isRecord, bool value) = self.ReadIsOn(gameSettingType);
            return value;
        }

        public static void SetIsOn(this GameSettingComponent self, GameSettingType gameSettingType, bool isOn)
        {
            self.RecordIsOn(gameSettingType, isOn);
        }

        public static (bool isRecord, bool value) ReadIsOn(this GameSettingComponent self, GameSettingType gameSettingType)
        {
            string key = $"GameSetting_{gameSettingType.ToString()}";
            if (PlayerPrefs.HasKey(key) == false)
            {
                return (false, false);
            }
            int value = PlayerPrefs.GetInt(key);
            return (true, value == 1);
        }

        public static void RecordIsOn(this GameSettingComponent self, GameSettingType gameSettingType, bool isOn)
        {
            string key = $"GameSetting_{gameSettingType.ToString()}";
            int value = isOn? 1 : 0;
            PlayerPrefs.SetInt(key, value);
        }
    }
}