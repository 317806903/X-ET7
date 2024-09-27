namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonWebView : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgCommonWebViewViewComponent View { get => this.GetComponent<DlgCommonWebViewViewComponent>(); }
	}
}
