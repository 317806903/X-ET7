namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerHUDShow : Entity, IAwake, IUILogic
	{
		public DlgBattleTowerHUDShowViewComponent View { get => this.GetComponent<DlgBattleTowerHUDShowViewComponent>(); }

	}
}
