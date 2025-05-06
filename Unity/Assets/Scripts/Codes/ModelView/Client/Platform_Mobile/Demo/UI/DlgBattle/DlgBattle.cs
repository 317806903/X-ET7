using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattle : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }

		public DlgBattleViewComponent View { get => this.GetComponent<DlgBattleViewComponent>(); }

		public Dictionary<int, Scroll_Item_SkillBattleInfo> ScrollItemSkills;

		public Dictionary<int, Scroll_Item_TowerBattle> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBattle> ScrollItemMonsters;

		public List<string> matchMonsterList = new();
		public List<string> matchTowerList = new();
		public List<string> towerList = new ()
		{
			"TowCallMonster_999",
			"Tower_Box",
			"Tower_BoostBox",
			"Tower_XBow1",
			"Tower_XBow2",
			"Tower_XBow3",
			"Tower_Cannon1",
			"Tower_Cannon2",
			"Tower_Cannon3",
			"Tower_Flame1",
			"Tower_Flame2",
			"Tower_Flame3",
			"Tower_AcidMist1",
			"Tower_AcidMist2",
			"Tower_AcidMist3",
			"Tower_Draco1",
			"Tower_Draco2",
			"Tower_Draco3",
			"Tower_Thunder1",
			"Tower_Thunder2",
			"Tower_Thunder3",
			"Tower_IceTower1",
			"Tower_IceTower2",
			"Tower_IceTower3",
			"Tower_SpeedTower1",
			"Tower_SpeedTower2",
			"Tower_SpeedTower3",
			"Tower_MystOrb1",
			"Tower_MystOrb2",
			"Tower_MystOrb3",
			"Tower_Alchemy1",
			"Tower_Alchemy2",
			"Tower_Alchemy3",
			"Tower_Scorpio1",
			"Tower_Scorpio2",
			"Tower_Scorpio3",
			"Tower_Crystal1",
			"Tower_Crystal2",
			"Tower_Crystal3",
			"Tower_Goblin1",
			"Tower_Goblin2",
			"Tower_Goblin3",
			"Tower_Rocket1",
			"Tower_Rocket2",
			"Tower_Rocket3",
			"Tower_Bomb1",
			"Tower_Bomb2",
			"Tower_Bomb3",
			"Tower_Golem1",
			"Tower_Golem2",
			"Tower_Golem3",
		};

		public List<string> monsterList = new ()
		{
			"TowCallMonster_999",
			"Monster_MiFeng1",
			"Monster_MiFeng2",
			"Monster_MiFeng3",
			"Monster_BianFu1",
			"Monster_BianFu2",
			"Monster_BianFu3",
			"Monster_ZhiZhu1",
			"Monster_ZhiZhu2",
			"Monster_ZhiZhu3",
			"Monster_ZhongZi1",
			"Monster_ZhongZi2",
			"Monster_ZhongZi3",
			"Monster_Gui1",
			"Monster_Gui2",
			"Monster_Gui3",
			"Monster_Dan1",
			"Monster_Dan2",
			"Monster_Dan3",
			"Monster_Niao1",
			"Monster_Niao2",
			"Monster_Niao3",
			"Monster_Rou1",
			"Monster_Rou2",
			"Monster_Rou3",
			"Monster_XueRen1",
			"Monster_XueRen2",
			"Monster_XueRen3",
			"Monster_WuGui1",
			"Monster_WuGui2",
			"Monster_WuGui3",
			"Monster_Skull1",
			"Monster_Skull2",
			"Monster_Skull3",
			"Monster_Spirit1",
			"Monster_Spirit2",
			"Monster_Spirit3",
			"Monster_FireSpirit1",
			"Monster_FireSpirit2",
			"Monster_FireSpirit3",
			"Monster_StoneGolem1",
			"Monster_StoneGolem2",
			"Monster_StoneGolem3",
			"Monster_Scorpid1",
			"Monster_Scorpid2",
			"Monster_Scorpid3",
			"Monster_Imp1",
			"Monster_Imp2",
			"Monster_Imp3",
			"Monster_Tombstone",
			"Monster_Season2_Challenge1_1_2",
			"Tow1_1",
		};

		public long Timer;
	}
}
