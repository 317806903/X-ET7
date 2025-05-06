using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgQuestionnaire : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgQuestionnaireViewComponent View { get => this.GetComponent<DlgQuestionnaireViewComponent>(); }
		public long dlgShowTime;
        public Dictionary<int, Scroll_Item_ItemShow> Scroll_Item_GiftsDict;
		public QuestionnaireCfg questionnaireCfg;
    }
}
