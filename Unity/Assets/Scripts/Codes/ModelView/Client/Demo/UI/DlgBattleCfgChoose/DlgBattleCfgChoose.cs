namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleCfgChoose : Entity, IAwake, IUILogic
	{
		public DlgBattleCfgChooseViewComponent View { get => this.GetComponent<DlgBattleCfgChooseViewComponent>(); }

	}
}
