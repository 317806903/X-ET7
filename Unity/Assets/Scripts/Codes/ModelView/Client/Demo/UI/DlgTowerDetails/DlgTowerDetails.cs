using System.Collections.Generic;
using SuperScrollView;
using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgTowerDetails : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgTowerDetailsViewComponent View { get => this.GetComponent<DlgTowerDetailsViewComponent>(); }

		public long dlgShowTime;

		public string baseTowerCfgId;
		public int curTowerIndex;
		public string curTowerCfgId;
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

	public class ShowData_DlgTowerDetails: ShowWindowData
	{
		public string towerCfgId;
		public bool isShowStatus;
		public bool isLock;
	}
}
