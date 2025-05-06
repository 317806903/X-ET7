using ET.AbilityConfig;
using System.Collections.Generic;
using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgTutorials : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgTutorialsViewComponent View { get => this.GetComponent<DlgTutorialsViewComponent>(); }
		public long dlgShowTime;
        public string videoPath;
        public VideoPlayer videoPlayer;
        public long Timer;
        public long dragTime;
        public Dictionary<int, Scroll_Item_Tutorials> videoSelectDic;
        public List<TutorialCfg> tutorialCfgList;
        public int videoIndex;
        public int videoDefalutIndex;

        public HashSet<string> cacheVideoPath = new();

        public float rawImageWidth;
        public float rawImageHeight;
    }

	public class ShowData_DlgTutorials : ShowWindowData{
		public TutorialMenuType menuType;
	}
}
