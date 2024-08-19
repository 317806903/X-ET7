namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgPassword : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgPasswordViewComponent View { get => this.GetComponent<DlgPasswordViewComponent>(); }

		
    }
}
