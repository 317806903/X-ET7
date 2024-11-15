﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattle : Entity, IAwake, IUILogic, IUIDlg
	{

		public DlgBattleViewComponent View { get => this.GetComponent<DlgBattleViewComponent>(); }

		public Dictionary<int, Scroll_Item_SkillBattleInfo> ScrollItemSkills;

		public Dictionary<int, Scroll_Item_TowerBattle> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBattle> ScrollItemMonsters;

		public List<string> matchMonsterList = new();
		public List<string> matchTowerList = new();
		public List<string> towerList = new ()
		{
			"Tow26_1",
			"Tow26_2",
			"Tow26_3",
			"Tow25_1",
			"Tow25_2",
			"Tow25_3",
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
			"Tow9_1",
			"Tow9_2",
			"Tow9_3",
			"Tow10_1",
			"Tow10_2",
			"Tow10_3",
			"Tow11_1",
			"Tow11_2",
			"Tow11_3",
			"Tow12_1",
			"Tow12_2",
			"Tow12_3",
			"Tow13_1",
			"Tow13_2",
			"Tow13_3",
			"Tow14_1",
			"Tow14_2",
			"Tow14_3",
			"Tow15_1",
			"Tow15_2",
			"Tow15_3",
			"Tow16_1",
			"Tow16_2",
			"Tow16_3",
			"Tow17_1",
			"Tow17_2",
			"Tow17_3",
			"Tow18_1",
			"Tow18_2",
			"Tow18_3",
			"Tow19_1",
			"Tow19_2",
			"Tow19_3",
			"Tow20_1",
			"Tow20_2",
			"Tow20_3",
			"Tow21_1",
			"Tow21_2",
			"Tow21_3",
			"Tow22_1",
			"Tow22_2",
			"Tow22_3",
			"Tow23_1",
			"Tow23_2",
			"Tow23_3",
			"Tow24_1",
			"Tow24_2",
			"Tow24_3",
		};

		public List<string> monsterList = new ()
		{
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
