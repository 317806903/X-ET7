using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	public class EPage_BattleDeckTower : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
	{
		public EPage_BattleDeckTowerViewComponent View { get => this.GetComponent<EPage_BattleDeckTowerViewComponent>(); }

		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_BattleDeckTower> ScrollBattleDeckItem;
		public Dictionary<int, Scroll_Item_BattleDeckTower> ScrollBagItem;
		public Scroll_Item_BattleDeckTower moveBagItem;

		public long Timer;

		public string moveItemCfgId;
		public int replaceIndex;
		public Vector2 lastScreenPos;
	}
}
