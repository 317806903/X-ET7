namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerBegin : Entity, IAwake, IUILogic, IUIDlg
	{

		public DlgBattleTowerBeginViewComponent View { get => this.GetComponent<DlgBattleTowerBeginViewComponent>(); }

		 

	}
}
