namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerEnd : Entity, IAwake, IUILogic
	{
		public DlgBattleTowerEndViewComponent View { get => this.GetComponent<DlgBattleTowerEndViewComponent>(); }
	}
}
