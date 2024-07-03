using DG.Tweening;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonLoading : Entity, IAwake, IUILogic
	{
		public DlgCommonLoadingViewComponent View { get => this.GetComponent<DlgCommonLoadingViewComponent>(); }

		public int showNum;
		public Sequence quence;
	}
}
