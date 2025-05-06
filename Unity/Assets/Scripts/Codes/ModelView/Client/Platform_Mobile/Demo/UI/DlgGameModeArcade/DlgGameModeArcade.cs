namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameModeArcade : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgGameModeArcadeViewComponent View { get => this.GetComponent<DlgGameModeArcadeViewComponent>(); }

		public long Timer;
		public bool isAR;
	}
}
