namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonConfirm : Entity, IAwake, IUILogic
	{
		public DlgCommonConfirmViewComponent View { get => this.GetComponent<DlgCommonConfirmViewComponent>(); }

	}
}
