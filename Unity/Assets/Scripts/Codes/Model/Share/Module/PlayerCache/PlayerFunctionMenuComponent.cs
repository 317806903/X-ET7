using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum FunctionMenuStatus
    {
        WaitChk,
        Lock,
        Openning,
        Openned,
    }

    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerFunctionMenuComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, FunctionMenuStatus> functionMenuDic;
    }
}