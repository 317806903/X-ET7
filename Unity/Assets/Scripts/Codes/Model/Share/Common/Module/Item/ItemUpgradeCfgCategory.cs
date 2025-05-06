using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET.AbilityConfig
{
	public partial class ItemUpgradeCfgCategory
	{
		[ProtoIgnore]
		[BsonIgnore]
		Dictionary<string, SortedDictionary<int, ItemUpgradeCfg>> ItemUpgradeDic = new();

		public SortedDictionary<int, ItemUpgradeCfg> GetItemUpgrades(string itemCfgId)
		{
			if(this.ItemUpgradeDic.TryGetValue(itemCfgId, out var itemUpgrades))
			{
				return itemUpgrades;
			}
			else
			{
				return null;
			}
		}

		public int GetMaxUpgradeLevel(string itemCfgId)
		{
			var itemUpgrades = this.GetItemUpgrades(itemCfgId);
			if (itemUpgrades == null)
			{
				return 0;
			}

			return itemUpgrades.Count;
		}

		public ItemUpgradeCfg GetItemUpgradeByLevel(string itemCfgId, int level)
		{
			var itemUpgrades = this.GetItemUpgrades(itemCfgId);
			if (itemUpgrades == null)
			{
				return null;
			}

			if (itemUpgrades.TryGetValue(level, out var itemUpgradeCfg))
			{
				return itemUpgradeCfg;
			}

			return null;
		}

		partial void PostResolve()
		{
			foreach (var itemUpgradeCfg in this.DataList)
			{
				string itemCfgId = itemUpgradeCfg.ItemCfgId;
				if (this.ItemUpgradeDic.TryGetValue(itemCfgId, out var itemUpgradeCfgs) == false)
				{
					itemUpgradeCfgs = new SortedDictionary<int, ItemUpgradeCfg>();
					this.ItemUpgradeDic.Add(itemCfgId, itemUpgradeCfgs);
				}
				itemUpgradeCfgs.Add(itemUpgradeCfg.Level, itemUpgradeCfg);
			}
		}
	}
}
