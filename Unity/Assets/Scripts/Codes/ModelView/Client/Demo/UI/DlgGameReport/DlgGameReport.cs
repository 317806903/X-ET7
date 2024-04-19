namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameReport : Entity, IAwake, IUILogic
	{
		public DlgGameReportViewComponent View { get => this.GetComponent<DlgGameReportViewComponent>(); }

		public int limitNum;
	}
}
