namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeAR : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgGameModeARViewComponent View { get => this.GetComponent<DlgGameModeARViewComponent>(); }

		public long Timer;
		public bool isAR;
	}
}
