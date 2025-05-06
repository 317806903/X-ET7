using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class PlayerSkillCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, string> skillCfgId2PreSkillCfgId = new();
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, string> skillCfgId2NextSkillCfgId = new();

		[ProtoIgnore]
		[BsonIgnore]
		public MultiMapSimple<string, string> baseSkillCfgId2SkillCfgIds = new();
		[ProtoIgnore]
		[BsonIgnore]
		public Dictionary<string, string> skillCfgId2BaseSkillCfgId = new();

		[ProtoIgnore]
		[BsonIgnore]
		public HashSet<string> skillCfgListInBattleDeck = new();

		[ProtoIgnore]
		[BsonIgnore]
		public List<string> skillCfgListInBattleDeckWhenUnLockDefault = new();

		partial void PostResolve()
		{
			foreach (var skillCfg in this.DataList)
			{
				string curSkillCfgIdTmp = skillCfg.Id;
				if (skillCfg.IsShowInBattleDeckUI)
				{
					this.skillCfgListInBattleDeck.Add(curSkillCfgIdTmp);
					if (skillCfg.UnLockCondition is UnLockDefault)
					{
						this.skillCfgListInBattleDeckWhenUnLockDefault.Add(curSkillCfgIdTmp);
					}
				}
				string nextSkillCfgIdTmp = skillCfg.NextPlayerSkillCfgId;
				if (string.IsNullOrEmpty(nextSkillCfgIdTmp))
				{
					continue;
				}
				this.skillCfgId2PreSkillCfgId[nextSkillCfgIdTmp] = curSkillCfgIdTmp;
				this.skillCfgId2NextSkillCfgId[curSkillCfgIdTmp] = nextSkillCfgIdTmp;
			}

			foreach (var skillCfg in this.DataList)
			{
				string curSkillCfgId = "";
				string nextSkillCfgId = "";
				curSkillCfgId = skillCfg.Id;
				if (this.skillCfgId2PreSkillCfgId.ContainsKey(curSkillCfgId) == false)
				{
					string baseSkillCfgId = curSkillCfgId;

					this.baseSkillCfgId2SkillCfgIds.Add(baseSkillCfgId, curSkillCfgId);
					while (this.skillCfgId2NextSkillCfgId.TryGetValue(curSkillCfgId, out nextSkillCfgId))
					{
						this.baseSkillCfgId2SkillCfgIds.Add(baseSkillCfgId, nextSkillCfgId);
						curSkillCfgId = nextSkillCfgId;
					}
				}
			}

			foreach (var skillCfg in this.DataList)
			{
				string skillCfgId = skillCfg.Id;
				string curSkillCfgId = skillCfgId;
				while (this.skillCfgId2PreSkillCfgId.ContainsKey(curSkillCfgId))
				{
					curSkillCfgId = this.skillCfgId2PreSkillCfgId[curSkillCfgId];
				}
				this.skillCfgId2BaseSkillCfgId[skillCfgId] = curSkillCfgId;
			}
		}

		public HashSet<string> GetSkillCfgListInBattleDeck()
		{
			return this.skillCfgListInBattleDeck;
		}

		public List<string> GetSkillCfgListInBattleDeckWhenUnLockDefault()
		{
			return this.skillCfgListInBattleDeckWhenUnLockDefault;
		}

		public int GetSkillMaxLevel(string skillCfgId)
		{
			int count = 1;
			string curSkillCfgId = skillCfgId;
			string nextSkillCfgId;
			while (this.skillCfgId2NextSkillCfgId.TryGetValue(curSkillCfgId, out nextSkillCfgId))
			{
				count++;
				curSkillCfgId = nextSkillCfgId;
			}
			return count;
		}

		public string GetPreSkillCfgId(string skillCfgId, int index)
		{
			while (index > 0)
			{
				index--;
				if (this.skillCfgId2PreSkillCfgId.TryGetValue(skillCfgId, out var preSkillCfgId))
				{
					return GetPreSkillCfgId(preSkillCfgId, index);
				}
				return "";
			}
			return skillCfgId;
		}

		public PlayerSkillCfg GetPreSkillCfg(string skillCfgId, int index = 1)
		{
			string preSkillCfgId = GetPreSkillCfgId(skillCfgId, index);
			if (string.IsNullOrEmpty(preSkillCfgId))
			{
				return null;
			}
			return this.Get(preSkillCfgId);
		}

		public string GetNextSkillCfgId(string skillCfgId, int index)
		{
			while (index > 0)
			{
				index--;
				if (this.skillCfgId2NextSkillCfgId.TryGetValue(skillCfgId, out var nextSkillCfgId))
				{
					return GetNextSkillCfgId(nextSkillCfgId, index);
				}
				return "";
			}
			return skillCfgId;
		}

		public PlayerSkillCfg GetNextSkillCfg(string skillCfgId, int index = 1)
		{
			string nextSkillCfgId = GetNextSkillCfgId(skillCfgId, index);
			if (string.IsNullOrEmpty(nextSkillCfgId))
			{
				return null;
			}
			return this.Get(nextSkillCfgId);
		}

		public bool ChkIsSameBaseSkillCfg(string baseSkillCfgId, string skillCfgId)
		{
			if (this.baseSkillCfgId2SkillCfgIds.Contains(baseSkillCfgId, skillCfgId))
			{
				return true;
			}

			return false;
		}

		public string GetBaseSkillCfgId(string curSkillCfgId)
		{
			this.skillCfgId2BaseSkillCfgId.TryGetValue(curSkillCfgId, out var baseSkillCfgId);
			return baseSkillCfgId;
		}

		public HashSet<string> GetAllBaseSkillCfgIdList()
		{
			HashSet<string> baseSkillCfgIdList = HashSetComponent<string>.Create();
			foreach (var item in this.baseSkillCfgId2SkillCfgIds)
			{
				baseSkillCfgIdList.Add(item.Key);
			}
			return baseSkillCfgIdList;
		}

	}
}
