namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerHUD : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBattleTowerHUDViewComponent View { get => this.GetComponent<DlgBattleTowerHUDViewComponent>();}

		public long playerId;
		public long towerUnitId;
		public string towerCfgId;
		public bool onlyChkPool;
		public bool isSelf;
		public bool isPool;
	}

	public class DlgBattleTowerHUD_ShowWindowData : ShowWindowData
	{
		public long playerId;
		public long towerUnitId;
		public string towerCfgId;
	}
}
