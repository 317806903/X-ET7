using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgSkillDetails : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgSkillDetailsViewComponent View { get => this.GetComponent<DlgSkillDetailsViewComponent>(); }
		public long dlgShowTime;

        public long dragTime;
        public VideoPlayer videoPlayer;
        public string videoPath;
        public string tutorialCfgId;
        public long Timer;
        public float rawImageWidth;
		public float rawImageHeight;

    }

	public class ShowData_SkillInfo: ShowWindowData
	{
		public string skillCfgId;
		public bool isLearned;
	}
}
