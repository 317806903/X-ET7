using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum LoginType
    {
        Editor,
        UnitySDK,
        GoogleSDK,
        AppleSDK,
        Robot,
    }

    [ChildOf(typeof(AccountManagerComponent))]
    public class AccountComponent : Entity, IAwake, IDestroy
    {
        public string accountId;
        public string bindAccountId;
        public string password;
        public long playerId;
        public long createTime;
        public long loginTime;
        public LoginType accountType;
        public LoginType loginType;
        public string createIP;
        public string loginIP;
    }
}