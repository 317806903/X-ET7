namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameJudgeChoose : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgGameJudgeChooseViewComponent View { get => this.GetComponent<DlgGameJudgeChooseViewComponent>(); }

		public int limitNum;
	}
}
