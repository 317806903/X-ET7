using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class MonsterComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public string monsterCfgId;
        [BsonIgnore]
        public MonsterType monsterType
        {
            get
            {
                return this.model.Type;
            }
        }
        public int rewardGold;
        public int waveIndex;
        public int circleWaveIndex;

        [BsonIgnore]
        public TowerDefense_MonsterCfg model
        {
            get
            {
                return TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            }
        }
    }
}