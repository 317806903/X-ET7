using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerHUDShow : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleTowerHUDShowViewComponent View { get => this.GetComponent<DlgBattleTowerHUDShowViewComponent>(); }

		public Dictionary<RectTransform, float> HomeHealthBarDictionary = new();
		public long Timer;

		public int curFrame = 0;
		public int waitFrame = 3;
	}
}
