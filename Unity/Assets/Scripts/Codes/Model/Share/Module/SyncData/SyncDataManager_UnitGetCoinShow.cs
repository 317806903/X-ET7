using System.Collections.Generic;
using System.Diagnostics;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [ComponentOf(typeof (SyncDataManager))]
    public class SyncDataManager_UnitGetCoinShow : Entity, IAwake, IDestroy
    {
        public MultiMapSimple<long, (Unit unit, CoinType coinType, int chgValue)> player2SyncUnit;
        public List<(long playerId, Unit unit, CoinType coinType, int chgValue)> NeedSyncGetCoinShowList;

        public Dictionary<long, int> waitFrameSync = new();
        public Dictionary<long, int> curFrameSync = new();
    }
}