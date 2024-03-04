using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgKnapsack : Entity, IAwake, IUILogic
	{
		public DlgKnapsackViewComponent View { get => this.GetComponent<DlgKnapsackViewComponent>(); }
		
		public Dictionary<int, Scroll_Item_Card> ScrollItemCrad;
		public Dictionary<int, Scroll_Item_Card> ScrollItemMyCrad;

	}
}
