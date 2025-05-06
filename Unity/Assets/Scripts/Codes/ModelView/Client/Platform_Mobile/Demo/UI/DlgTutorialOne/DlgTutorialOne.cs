using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgTutorialOne : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgTutorialOneViewComponent View { get => this.GetComponent<DlgTutorialOneViewComponent>(); }
		public long dlgShowTime;

		public long dragTime;
		public VideoPlayer videoPlayer;
		public string videoPath;
		public string tutorialCfgId;
		public long Timer;
		public float rawImageWidth;
		public float rawImageHeight;
		public bool isDrag;
	}

	public class DlgTutorialOne_ShowWindowData : ShowWindowData
	{
		public string tutorialCfgId;
	}
}
