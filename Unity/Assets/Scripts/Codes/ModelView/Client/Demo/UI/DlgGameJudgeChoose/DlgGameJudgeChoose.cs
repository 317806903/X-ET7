namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgGameJudgeChoose : Entity, IAwake, IUILogic
	{
		public DlgGameJudgeChooseViewComponent View { get => this.GetComponent<DlgGameJudgeChooseViewComponent>(); }

		public int limitNum;
	}
}
