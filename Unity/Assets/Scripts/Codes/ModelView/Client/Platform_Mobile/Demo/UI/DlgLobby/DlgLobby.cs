namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgLobby : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }

		public DlgLobbyViewComponent View { get => this.GetComponent<DlgLobbyViewComponent>(); }

		public string gamePlayBattleLevelCfgId;

	}
}
