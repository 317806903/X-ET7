using System;

namespace ET.Client
{
	public class ES_CommonItem : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
	{
		public ES_CommonItemViewComponent View { get => this.GetComponent<ES_CommonItemViewComponent>(); }

		public string itemCfgId;
		public Action extendClickAction;
	}
}
