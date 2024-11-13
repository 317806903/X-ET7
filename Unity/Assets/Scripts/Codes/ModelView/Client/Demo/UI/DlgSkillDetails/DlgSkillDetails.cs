using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgSkillDetails : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgSkillDetailsViewComponent View { get => this.GetComponent<DlgSkillDetailsViewComponent>(); }
		public long dlgShowTime;

		public string baseSkillCfgId;
		public int curSkillIndex;
		public string curSkillCfgId;
		public bool isShowStatus;
		public bool isLock;

		public long dragTime;
		public VideoPlayer videoPlayer;
		public string videoPath;
		public string tutorialCfgId;
		public long Timer;
		public float rawImageWidth;
		public float rawImageHeight;
		public bool isDrag;
    }

	public class ShowData_DlgSkillDetails: ShowWindowData
	{
		public string skillCfgId;
		public bool isShowStatus;
		public bool isLock;
	}
}
