using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class TowerDefense_TowerCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, string> towerCfgId2PreTowerCfgId = new();
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, string> towerCfgId2NextTowerCfgId = new();

		[ProtoIgnore]
		[BsonIgnore]
		public MultiMapSimple<string, string> baseTowerCfgId2towerCfgIds = new();
		[ProtoIgnore]
		[BsonIgnore]
		public MultiDictionary<string, string, int> towerCfgId2BaseTowerCfgIdAndCount = new();

		partial void PostResolve()
		{
			foreach (var towerCfg in this.DataList)
			{
				string curTowerId = towerCfg.Id;
				string nextTowerId = towerCfg.NextTowerId;
				if (string.IsNullOrEmpty(nextTowerId))
				{
					continue;
				}
				this.towerCfgId2PreTowerCfgId[nextTowerId] = curTowerId;
				this.towerCfgId2NextTowerCfgId[curTowerId] = nextTowerId;
			}

			string curTowerCfgId = "";
			string nextTowerCfgId = "";
			foreach (var towerCfg in this.DataList)
			{
				curTowerCfgId = towerCfg.Id;
				if (this.towerCfgId2PreTowerCfgId.ContainsKey(curTowerCfgId) == false)
				{
					string baseTowerCfgId = curTowerCfgId;
					int costCount = 1;

					this.baseTowerCfgId2towerCfgIds.Add(baseTowerCfgId, curTowerCfgId);
					this.towerCfgId2BaseTowerCfgIdAndCount.Add(curTowerCfgId, baseTowerCfgId, costCount);
					while (this.towerCfgId2NextTowerCfgId.TryGetValue(curTowerCfgId, out nextTowerCfgId))
					{
						this.baseTowerCfgId2towerCfgIds.Add(baseTowerCfgId, nextTowerCfgId);
						TowerDefense_TowerCfg towerCfgTmp = this.Get(curTowerCfgId);
						costCount *= towerCfgTmp.NewTowerCostCount;
						this.towerCfgId2BaseTowerCfgIdAndCount.Add(nextTowerCfgId, baseTowerCfgId, costCount);
						curTowerCfgId = nextTowerCfgId;
					}
				}
			}
		}


		public TowerDefense_TowerCfg GetPreTowerCfg(string towerCfgId)
		{
			if (this.towerCfgId2PreTowerCfgId.TryGetValue(towerCfgId, out var preTowerCfgId))
			{
				return this.Get(preTowerCfgId);
			}
			return null;
		}

		public TowerDefense_TowerCfg GetNextTowerCfg(string towerCfgId)
		{
			if (this.towerCfgId2NextTowerCfgId.TryGetValue(towerCfgId, out var nextTowerCfgId))
			{
				return this.Get(nextTowerCfgId);
			}
			return null;
		}

		public bool ChkIsSameBaseTowerCfg(string baseTowerCfgId, string towerCfgId)
		{
			if (this.baseTowerCfgId2towerCfgIds.Contains(baseTowerCfgId, towerCfgId))
			{
				return true;
			}

			return false;
		}

		public (string, int) GetBaseTowerCfgIdAndCount(string curTowerCfgId)
		{
			this.towerCfgId2BaseTowerCfgIdAndCount.TryGetDic(curTowerCfgId, out var baseTowerInfo);
			string baseTowerCfgId = baseTowerInfo.Keys.ToList()[0];
			int costCount = baseTowerInfo.Values.ToList()[0];
			return (baseTowerCfgId, costCount);
		}

	}
}
