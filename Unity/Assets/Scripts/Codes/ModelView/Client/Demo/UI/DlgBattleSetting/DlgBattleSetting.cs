﻿namespace ET.Client
{
	//战斗设置面板
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleSetting : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleSettingViewComponent View { get => this.GetComponent<DlgBattleSettingViewComponent>(); }

		public long dlgShowTime;

	}
}