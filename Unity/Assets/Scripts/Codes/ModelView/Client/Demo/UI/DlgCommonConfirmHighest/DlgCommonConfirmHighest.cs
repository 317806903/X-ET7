namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonConfirmHighest : Entity, IAwake, IUILogic
	{
		public DlgCommonConfirmHighestViewComponent View { get => this.GetComponent<DlgCommonConfirmHighestViewComponent>(); }

	}
}
