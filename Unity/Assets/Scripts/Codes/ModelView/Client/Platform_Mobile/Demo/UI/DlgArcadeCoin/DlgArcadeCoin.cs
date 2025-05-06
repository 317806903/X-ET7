using System;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgArcadeCoin : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgArcadeCoinViewComponent View { get => this.GetComponent<DlgArcadeCoinViewComponent>(); }

		//定义一个暂存CoinNum的数据
		public int arcadeCoinNum;
		public Action SureBtnCallBak;
	}
}
