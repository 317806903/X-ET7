using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	public class EPage_BattleDeckSkill : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
	{
		public EPage_BattleDeckSkillViewComponent View { get => this.GetComponent<EPage_BattleDeckSkillViewComponent>(); }

		public long dlgShowTime;

		public Dictionary<int, Scroll_Item_BattleDeckSkill> ScrollBattleDeckItem;
		public Dictionary<int, Scroll_Item_BattleDeckSkill> ScrollBagItem;
		public Scroll_Item_BattleDeckSkill moveBagItem;

		public Vector2 lastScreenPos;
		public string moveItemCfgId;
		public int replaceIndex;
		public long Timer;
	}
}
