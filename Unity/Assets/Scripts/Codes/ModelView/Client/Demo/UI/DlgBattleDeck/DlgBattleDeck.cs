namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleDeck : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleDeckViewComponent View { get => this.GetComponent<DlgBattleDeckViewComponent>(); }
		public long dlgShowTime;
		public int pageIndex = 0;
	}
}
