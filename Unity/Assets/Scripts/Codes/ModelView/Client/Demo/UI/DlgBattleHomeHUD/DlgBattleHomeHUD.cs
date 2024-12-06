namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleHomeHUD : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleHomeHUDViewComponent View { get => this.GetComponent<DlgBattleHomeHUDViewComponent>(); }
		public long dlgShowTime;

		public long homeUnitId;
		public string homeCfgId;
		public bool isSelf;
	}

	public class DlgBattleHomeHUD_ShowWindowData : ShowWindowData
	{
		public long homeUnitId;
		public string homeCfgId;
	}
}
