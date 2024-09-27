namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_SkillDetails)]
	public class DlgSkillDetailsEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.windowType = UIWindowType.PopUp;
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgSkillDetails>().AddComponent<DlgSkillDetailsViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgSkillDetails>().RegisterUIEvent();
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, ShowWindowData contextData = null)
		{
			uiBaseWindow.GetComponent<DlgSkillDetails>().ShowWindow(contextData).Coroutine();
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<DlgSkillDetails>().HideWindow();
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
