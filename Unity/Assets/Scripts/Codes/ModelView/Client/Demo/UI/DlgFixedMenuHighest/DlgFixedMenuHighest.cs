using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgFixedMenuHighest : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgFixedMenuHighestViewComponent View { get => this.GetComponent<DlgFixedMenuHighestViewComponent>(); }

		public long Timer;
		public bool isMoving;
		public Dictionary<RectTransform, Vector2> transPos = new();
	}
}
