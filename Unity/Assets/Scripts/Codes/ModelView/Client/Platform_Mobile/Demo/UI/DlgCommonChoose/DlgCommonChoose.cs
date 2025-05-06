using System;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgCommonChoose : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgCommonChooseViewComponent View { get => this.GetComponent<DlgCommonChooseViewComponent>(); }

		public long dlgShowTime;

		public long Timer;

		public string timeoutMsg;
		public long timeoutTime;
		public Action confirmCallBack;
		public Action cancelCallBack;
		public Action timeOutCallBack;
		public bool isTimeOutConfirm;
		public bool isCloseAfterChoose;
	}
}
