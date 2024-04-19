using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleDeck : Entity, IAwake, IUILogic
	{
		public DlgBattleDeckViewComponent View { get => this.GetComponent<DlgBattleDeckViewComponent>(); }

		public Dictionary<int, Scroll_Item_TowerBuy> ScrollBattleDeckItem;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollBagItem;
		public Scroll_Item_TowerBuy moveBagItem;

		public long Timer;

		public string moveItemCfgId;
		public int replaceIndex;
		public Vector2 lastScreenPos;
	}
}
