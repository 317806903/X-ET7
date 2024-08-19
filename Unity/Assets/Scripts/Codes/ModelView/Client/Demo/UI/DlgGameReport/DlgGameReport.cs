namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameReport : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgGameReportViewComponent View { get => this.GetComponent<DlgGameReportViewComponent>(); }

		public long dlgShowTime;

		public int limitNum;
	}
}
