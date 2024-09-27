using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgLanguageChoose : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgLanguageChooseViewComponent View { get => this.GetComponent<DlgLanguageChooseViewComponent>(); }
		public long dlgShowTime;
		public List<LanguageType> languages;
		public Dictionary<int, Scroll_Item_LanguageChoose> languageItemDic;
		public int frontLanguageIndex;

	}
}
