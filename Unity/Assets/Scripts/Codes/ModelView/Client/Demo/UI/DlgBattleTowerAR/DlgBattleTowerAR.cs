﻿using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerAR : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleTowerARViewComponent View { get => this.GetComponent<DlgBattleTowerARViewComponent>(); }

		public Dictionary<int, Scroll_Item_TowerBattle> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBattleBuy> ScrollItemTowerBuy;

		public bool needResetMyOwnTowList;
		public List<string> myOwnTowerList = new();
		public Dictionary<string, int> myOwnTowerDic = new();

		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus;
		public long curTipTime;

		public long curLeftTime;
		public string curLeftTimeMsg;

		public int oldGold;
		public long Timer;
	}
}