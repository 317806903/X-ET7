using DG.Tweening;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonLoading : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgCommonLoadingViewComponent View { get => this.GetComponent<DlgCommonLoadingViewComponent>(); }

		public bool isForceShow;
		public int showNum;
		public Sequence quence;
	}
}
