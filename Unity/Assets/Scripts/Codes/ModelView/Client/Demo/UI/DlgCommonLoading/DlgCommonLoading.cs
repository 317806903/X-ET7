namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonLoading : Entity, IAwake, IUILogic
	{
		public DlgCommonLoadingViewComponent View { get => this.GetComponent<DlgCommonLoadingViewComponent>(); }

	}
}
