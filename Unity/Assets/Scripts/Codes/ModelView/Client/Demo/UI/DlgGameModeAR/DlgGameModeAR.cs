namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeAR : Entity, IAwake, IUILogic
	{
		public DlgGameModeARViewComponent View { get => this.GetComponent<DlgGameModeARViewComponent>(); }

		public long Timer;
		public bool isAR;
	}
}
