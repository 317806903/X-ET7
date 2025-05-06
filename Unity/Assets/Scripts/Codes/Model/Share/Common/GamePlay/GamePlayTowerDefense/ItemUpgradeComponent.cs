using System.Collections.Generic;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class ItemUpgradeComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public long playerId;
        public string itemCfgId;
        public int curLevel;
        public int maxUpgradeLevel;

        public int upgradeCostGoldWhenInGame;
        public int upgradeCostCardWhenInGame;

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public SortedDictionary<int, string> chooseItemGiftDic;

    }
}