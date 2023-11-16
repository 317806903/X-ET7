using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattle : Entity, IAwake, IUILogic
	{

		public DlgBattleViewComponent View { get => this.GetComponent<DlgBattleViewComponent>(); }

		public Dictionary<int, Scroll_Item_Tower> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_Tower> ScrollItemTanks;

		public List<string> towerList = new List<string>()
		{
			"TowCallMonster_1",
			"Tow5_1",
			"Tow5_2",
			"Tow5_3",
			"Tow6_1",
			"Tow6_2",
			"Tow6_3",
			"Tow7_1",
			"Tow7_2",
			"Tow7_3",
			"Tow8_1",
			"Tow8_2",
			"Tow8_3",
			"Tow1_1",
			"Tow1_2",
			"Tow1_3",
			"Tow2_1",
			"Tow2_2",
			"Tow2_3",
			"Tow3_1",
			"Tow3_2",
			"Tow3_3",
			"Tow4_1",
			"Tow4_2",
			"Tow4_3",
			"TowCallMonster_1",
			"TowCallMonster_2",
			"TowCallMonster_3",
			"TowCallMonster_4",
		};

		public List<string> monsterList = new List<string>()
		{
			"Monster1_1",
			"Monster2_1",
			"Monster3_1",
			"Monster4_1",
			"Monster5_1",
			"Monster6_1",
			"Monster7_1",
			"Monster8_1",
			"Monster9_1",
			"Monster10_1",
		};

		public long Timer;
	}
}
