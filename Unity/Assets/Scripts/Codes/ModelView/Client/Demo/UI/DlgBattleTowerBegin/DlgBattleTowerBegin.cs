namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerBegin : Entity, IAwake, IUILogic
	{

		public DlgBattleTowerBeginViewComponent View { get => this.GetComponent<DlgBattleTowerBeginViewComponent>(); }

		 

	}
}
