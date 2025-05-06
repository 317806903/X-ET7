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
		public Dictionary<string, (string, int)> towerCfgId2BaseTowerCfgIdAndCount = new();

		[ProtoIgnore]
		[BsonIgnore]
		public HashSet<string> towerCfgListInBattleDeck = new();

		[ProtoIgnore]
		[BsonIgnore]
		public List<string> towerCfgListInBattleDeckWhenUnLockDefault = new();

		partial void PostResolve()
		{
			foreach (var towerCfg in this.DataList)
			{
				string curTowerId = towerCfg.Id;
				if (towerCfg.IsShowInBattleDeckUI)
				{
					this.towerCfgListInBattleDeck.Add(curTowerId);
					if (towerCfg.UnLockCondition is UnLockDefault)
					{
						this.towerCfgListInBattleDeckWhenUnLockDefault.Add(curTowerId);
					}
				}
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
					this.towerCfgId2BaseTowerCfgIdAndCount.Add(curTowerCfgId, (baseTowerCfgId, costCount));
					while (this.towerCfgId2NextTowerCfgId.TryGetValue(curTowerCfgId, out nextTowerCfgId))
					{
						this.baseTowerCfgId2towerCfgIds.Add(baseTowerCfgId, nextTowerCfgId);
						TowerDefense_TowerCfg towerCfgTmp = this.Get(curTowerCfgId);
						costCount *= towerCfgTmp.NewTowerCostCount;
						this.towerCfgId2BaseTowerCfgIdAndCount.Add(nextTowerCfgId, (baseTowerCfgId, costCount));
						curTowerCfgId = nextTowerCfgId;
					}
				}
			}
		}

		public HashSet<string> GetTowerCfgListInBattleDeck()
		{
			return this.towerCfgListInBattleDeck;
		}

		public List<string> GetTowerCfgListInBattleDeckWhenUnLockDefault()
		{
			return this.towerCfgListInBattleDeckWhenUnLockDefault;
		}

		public int GetTowerMaxLevel(string towerCfgId)
		{
			int count = 1;
			string curTowerCfgId = towerCfgId;
			string nextTowerCfgId;
			while (this.towerCfgId2NextTowerCfgId.TryGetValue(curTowerCfgId, out nextTowerCfgId))
			{
				count++;
				curTowerCfgId = nextTowerCfgId;
			}
			return count;
		}

		public string GetPreTowerCfgId(string towerCfgId, int index)
		{
			while (index > 0)
			{
				index--;
				if (this.towerCfgId2PreTowerCfgId.TryGetValue(towerCfgId, out var preTowerCfgId))
				{
					return GetPreTowerCfgId(preTowerCfgId, index);
				}
				return "";
			}
			return towerCfgId;
		}

		public TowerDefense_TowerCfg GetPreTowerCfg(string towerCfgId, int index = 1)
		{
			string preTowerCfgId = GetPreTowerCfgId(towerCfgId, index);
			if (string.IsNullOrEmpty(preTowerCfgId))
			{
				return null;
			}
			return this.Get(preTowerCfgId);
		}

		public string GetNextTowerCfgId(string towerCfgId, int index)
		{
			while (index > 0)
			{
				index--;
				if (this.towerCfgId2NextTowerCfgId.TryGetValue(towerCfgId, out var nextTowerCfgId))
				{
					return GetNextTowerCfgId(nextTowerCfgId, index);
				}
				return "";
			}
			return towerCfgId;
		}

		public TowerDefense_TowerCfg GetNextTowerCfg(string towerCfgId, int index = 1)
		{
			string nextTowerCfgId = GetNextTowerCfgId(towerCfgId, index);
			if (string.IsNullOrEmpty(nextTowerCfgId))
			{
				return null;
			}
			return this.Get(nextTowerCfgId);
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
			this.towerCfgId2BaseTowerCfgIdAndCount.TryGetValue(curTowerCfgId, out var baseTowerInfo);
			return baseTowerInfo;
		}

	}
}
