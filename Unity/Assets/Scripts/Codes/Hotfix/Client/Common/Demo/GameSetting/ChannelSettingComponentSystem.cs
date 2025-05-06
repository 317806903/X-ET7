using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Client
{
    [FriendOf(typeof(ChannelSettingComponent))]
    public static class ChannelSettingComponentSystem
    {
        [ObjectSystem]
        public class ChannelSettingComponentAwakeSystem : AwakeSystem<ChannelSettingComponent>
        {
            protected override void Awake(ChannelSettingComponent self)
            {
                ChannelSettingComponent.Instance = self;
            }
        }

        public static void Init(this ChannelSettingComponent self, string channelId)
        {
            self.channelId = channelId;
        }

        public static bool ChkChannelCfg(this ChannelSettingComponent self)
        {
            if (ChannelCfgCategory.Instance.Contain(self.channelId) == false)
            {
                return false;
            }
            ChannelCfg channelCfg = ChannelCfgCategory.Instance.Get(self.channelId);
            if (channelCfg.DeviceGameMode == DeviceGameMode.Editor)
            {
                return false;
            }
            return true;
        }

        public static ChannelCfg GetChannelCfg(this ChannelSettingComponent self)
        {
            return ChannelCfgCategory.Instance.GetOrDefault(self.channelId);
        }

        public static DeviceGameMode GetDeviceGameMode(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().DeviceGameMode;
        }

        public static string GetResHostServerIP(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().ResHostServerIP;
        }

        public static string GetResGameVersion(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().ResGameVersion;
        }

        public static bool ChkIsActivity(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsActivity;
        }

        public static string GetRouterHttpHost(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().RouterHttpHost;
        }

        public static int GetRouterHttpPort(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().RouterHttpPort;
        }

        public static string GetAreaType(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().AreaType;
        }

        public static string GetLanguageType(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().LanguageType;
        }

        public static List<LanguageType> GetLanguageTypeList(this ChannelSettingComponent self)
        {
            List<LanguageType> languageTypeList = new();
            List<string> list = self.GetChannelCfg().LanguageTypeList;
            for (int i = 0; i < list.Count; i++)
            {
                if (System.Enum.TryParse(list[i], out LanguageType languageType))
                {
                    languageTypeList.Add(languageType);
                }
            }
            return languageTypeList;
        }

        public static bool ChkIsNeedSendEventLog(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsNeedSendEventLog;
        }

        public static string GetEventLogURL(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().EventLogURL;
        }

        public static string GetEventLogKey(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().EventLogKey;
        }

        public static bool ChkIsGetMeshFromClient(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsGetMeshFromClient;
        }

        public static bool ChkIsGetDiamondWhenClick(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsGetDiamondWhenClick;
        }

        public static string GetGameJudgeURL(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().GameJudgeURL;
        }

        public static bool ChkIsGameJudgeUseWebView(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsGameJudgeUseWebView;
        }

        public static string GetGameDownLoadURL(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().GameDownLoadURL;
        }

        public static string GetPrivacyPolicyURL(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().PrivacyPolicyURL;
        }

        public static string GetDiscordURL(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().DiscordURL;
        }

        public static bool ChkIsGameDownLoadWebView(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsGameDownLoadWebView;
        }

        public static bool ChkIsNeedAppsflyer(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsNeedAppsflyer;
        }

        public static string GetAppsflyerKey(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().AppsflyerKey;
        }

        public static string GetAppsflyerAppID(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().AppsflyerAppID;
        }

        public static bool ChkIsNeedQuestionnaire(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsNeedQuestionnaire;
        }

        public static List<QuestionnaireCfg> GetQuestionnaireCfgIdList(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().QuestionnaireCfgId_Ref;
        }

        public static bool ChkIsNeedSDKLogin(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().IsNeedSDKLogin;
        }

        public static string GetAliyunOSS(this ChannelSettingComponent self)
        {
            return self.GetChannelCfg().AliyunOSS;
        }

    }
}
