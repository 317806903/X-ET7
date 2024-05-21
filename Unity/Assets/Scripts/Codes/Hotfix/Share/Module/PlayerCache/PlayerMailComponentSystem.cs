using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
    [FriendOf(typeof(PlayerMailComponent))]
    public static class PlayerMailComponentSystem
    {
        [ObjectSystem]
        public class PlayerMailComponentAwakeSystem : AwakeSystem<PlayerMailComponent>
        {
            protected override void Awake(PlayerMailComponent self)
            {
                self.functionMenuDic = new();
                self.Init();
            }
        }

        public static void Init(this PlayerMailComponent self)
        {
            var allFunctionMenu = FunctionMenuCfgCategory.Instance.DataList;
            foreach (var functionMenuCfg in allFunctionMenu)
            {
                if (self.functionMenuDic.TryGetValue(functionMenuCfg.Id, out var status) == false)
                {
#if UNITY_EDITOR
                    self.functionMenuDic[functionMenuCfg.Id] = FunctionMenuStatus.Openned;
#else
                    if (functionMenuCfg.OpenCondition is FunctionMenuConditionDefault)
                    {
                        self.functionMenuDic[functionMenuCfg.Id] = FunctionMenuStatus.Openned;
                    }
                    else
                    {
                        self.functionMenuDic[functionMenuCfg.Id] = FunctionMenuStatus.WaitChk;
                    }
#endif
                }
            }
        }

        public static long GetPlayerId(this PlayerMailComponent self)
        {
            return self.GetParent<PlayerDataComponent>().playerId;
        }

        public static FunctionMenuStatus GetStatus(this PlayerMailComponent self, string functionMenuId)
        {
            return self.functionMenuDic[functionMenuId];
        }

        public static List<string> GetFunctionMenuList(this PlayerMailComponent self)
        {
            List<string> lockList = ListComponent<string>.Create();
            foreach (var item in self.functionMenuDic)
            {
                lockList.Add(item.Key);
            }
            return lockList;
        }

        public static List<string> GetWaitChkFunctionMenuList(this PlayerMailComponent self)
        {
            List<string> lockList = ListComponent<string>.Create();
            foreach (var item in self.functionMenuDic)
            {
                if (item.Value == FunctionMenuStatus.WaitChk)
                {
                    lockList.Add(item.Key);
                }
            }
            return lockList;
        }

        public static List<string> GetLockFunctionMenuList(this PlayerMailComponent self)
        {
            List<string> lockList = ListComponent<string>.Create();
            foreach (var item in self.functionMenuDic)
            {
                if (item.Value == FunctionMenuStatus.Lock)
                {
                    lockList.Add(item.Key);
                }
            }
            return lockList;
        }

        public static List<string> GetOpenningFunctionMenuList(this PlayerMailComponent self)
        {
            List<string> openningList = ListComponent<string>.Create();
            foreach (var item in self.functionMenuDic)
            {
                string functionMenuCfgId = item.Key;
                if (item.Value == FunctionMenuStatus.Openning)
                {
                    FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
                    if (functionMenuCfg.IsOpenSoon)
                    {
                        continue;
                    }
                    openningList.Add(functionMenuCfgId);
                }
            }
            return openningList;
        }

        public static List<string> GetOpennedFunctionMenuList(this PlayerMailComponent self)
        {
            List<string> openningList = ListComponent<string>.Create();
            foreach (var item in self.functionMenuDic)
            {
                if (item.Value == FunctionMenuStatus.Openned)
                {
                    openningList.Add(item.Key);
                }
            }
            return openningList;
        }

        public static void ChgStatus(this PlayerMailComponent self, string functionMenuId, FunctionMenuStatus functionMenuStatus)
        {
            self.functionMenuDic[functionMenuId] = functionMenuStatus;
        }

    }
}