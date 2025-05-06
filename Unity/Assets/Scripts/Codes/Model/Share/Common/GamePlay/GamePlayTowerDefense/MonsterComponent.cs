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
        public int circleNum;
        public int circleIndex;
        public int stageWaveIndex;

        public long playerId;

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