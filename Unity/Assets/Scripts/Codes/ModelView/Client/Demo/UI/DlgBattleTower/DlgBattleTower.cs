using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	public enum UISelectCfgType
	{
		HeadQuarter,
		MonsterCall,
		Tower,
		Monster,
	}
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTower : Entity, IAwake, IUILogic
	{
		public DlgBattleTowerViewComponent View { get => this.GetComponent<DlgBattleTowerViewComponent>();}

		public Dictionary<int, Scroll_Item_Tower> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemTowerBuy;

		public bool needResetMyOwnTowList;
		public List<string> myOwnTowerList = new();
		public Dictionary<string, int> myOwnTowerDic = new();

		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus;
		public long curTipTime;

		public long curLeftTime;
		public string curLeftTimeMsg;

		public long Timer;
	}
}
