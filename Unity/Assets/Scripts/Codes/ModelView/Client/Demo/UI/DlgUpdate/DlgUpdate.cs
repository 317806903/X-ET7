using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgUpdate : Entity, IAwake, IUILogic, IUIDlg
	{

		public DlgUpdateViewComponent View { get => this.GetComponent<DlgUpdateViewComponent>(); }


		public RectTransform rectTransBackground;
		public RectTransform rectTransValueImage;
		public Transform transPercentage;

		public Transform transProgress;
		public Transform transCheckUpdate;
	}
}
