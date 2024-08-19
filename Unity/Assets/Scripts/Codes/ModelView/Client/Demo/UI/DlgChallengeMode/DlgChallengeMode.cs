using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(UIBaseWindow))]
    public class DlgChallengeMode : Entity, IAwake, IUILogic, IUIDlg
    {
        public DlgChallengeModeViewComponent View { get => this.GetComponent<DlgChallengeModeViewComponent>(); }

        //当前选择的页面索引：
        //0代表常规界面；
        //1代表赛季界面
        public int pageIndex = 0;
    }
}
