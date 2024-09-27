using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgQuestionnaire : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgQuestionnaireViewComponent View { get => this.GetComponent<DlgQuestionnaireViewComponent>(); }
		public long dlgShowTime;
        public Dictionary<int, Scroll_Item_TowerBuy> Scroll_Item_GiftsDict;
		public QuestionnaireCfg questionnaireCfg;
    }
}
