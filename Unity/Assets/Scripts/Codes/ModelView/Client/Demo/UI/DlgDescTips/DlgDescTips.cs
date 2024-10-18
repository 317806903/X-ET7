using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgDescTips : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgDescTipsViewComponent View { get => this.GetComponent<DlgDescTipsViewComponent>(); }

	}

	public class DlgDescTips_ShowWindowData : ShowWindowData
    {
        public string Desc;
        public Vector3 Pos;
        public bool tipTextAlignmentMid;
        public bool notNeedClickBg;
    }
}
