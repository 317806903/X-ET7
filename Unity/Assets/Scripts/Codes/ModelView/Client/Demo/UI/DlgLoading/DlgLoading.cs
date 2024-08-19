using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgLoading : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgLoadingViewComponent View { get => this.GetComponent<DlgLoadingViewComponent>();}
		public float targetProcess;
		public float curProcess;
		public long Timer;


		public RectTransform rectTransBackground;
		public RectTransform rectTransValueImage;
		public Transform transPercentage;
	}
}
