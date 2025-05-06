using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgVideoShowSmall : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgVideoShowSmallViewComponent View { get => this.GetComponent<DlgVideoShowSmallViewComponent>(); }

		public string videoPath;
		public VideoPlayer videoPlayer;
	}
}
