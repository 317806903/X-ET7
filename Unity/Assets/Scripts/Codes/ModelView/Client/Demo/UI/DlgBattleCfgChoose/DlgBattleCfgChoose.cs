using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleCfgChoose : Entity, IAwake, IUILogic
	{
		public DlgBattleCfgChooseViewComponent View { get => this.GetComponent<DlgBattleCfgChooseViewComponent>(); }

		public Dictionary<int, Scroll_Item_GameCfgItem> ScrollItemGameCfgItems;

		public bool isGlobalMode = false;
		public bool isAR = false;
		public List<GamePlayBattleLevelCfg> curlist = new();
		public int curChooseIndex;

		public List<string[]> gameModeList = new ()
		{
			new string[]{"-1", "TextCode_Key_GameMode_All"},
			new string[]{"GamePlayTowerDefenseNormal", "TextCode_Key_GameMode_GamePlayTowerDefenseNormal"},
			new string[]{"GamePlayTowerDefenseTutorialFirst", "TextCode_Key_GamePlayTowerDefenseTutorialFirst"},
			new string[]{"GamePlayTowerDefensePVE", "TextCode_Key_GamePlayTowerDefensePVE"},
			new string[]{"GamePlayTowerDefenseEndlessChallenge", "TextCode_Key_GamePlayTowerDefenseEndlessChallenge"},
			new string[]{"GamePlayTowerDefensePVP", "TextCode_Key_GamePlayTowerDefensePVP"},
			new string[]{"GamePlayPKNormal", "TextCode_Key_GameMode_GamePlayPKNormal"},
		};
		public List<string[]> teamModeList = new ()
		{
			new string[]{"-1", "TextCode_Key_TeamMode_All"},
			new string[]{"AllPlayersOneGroup", "TextCode_Key_TeamMode_AllPlayersOneGroup"},
			new string[]{"PlayerAlone", "TextCode_Key_TeamMode_PlayerAlone"},
			new string[]{"PlayerTeam", "TextCode_Key_TeamMode_PlayerTeam"},
		};
	}

	public class DlgBattleCfgChoose_ShowWindowData : ShowWindowData
	{
		public bool isGlobalMode;
		public bool isAR;
	}
}
