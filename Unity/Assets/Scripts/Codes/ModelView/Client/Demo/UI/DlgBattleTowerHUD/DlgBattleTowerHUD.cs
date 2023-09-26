namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerHUD : Entity, IAwake, IUILogic
	{
		public DlgBattleTowerHUDViewComponent View { get => this.GetComponent<DlgBattleTowerHUDViewComponent>();}

		public long playerId;
		public long towerUnitId;
		public string towerCfgId;
	}

	public class DlgBattleTowerHUD_ShowWindowData : ShowWindowData
	{
		public long playerId;
		public long towerUnitId;
		public string towerCfgId;
	}
}
