namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonWebView : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgCommonWebViewViewComponent View { get => this.GetComponent<DlgCommonWebViewViewComponent>(); }
	}
}
