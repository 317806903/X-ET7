
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class ES_CommonItemViewComponentAwakeSystem : AwakeSystem<ES_CommonItemViewComponent,Transform> 
	{
		protected override void Awake(ES_CommonItemViewComponent self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}

	[ObjectSystem]
	public class ES_CommonItemViewComponentDestroySystem : DestroySystem<ES_CommonItemViewComponent> 
	{
		protected override void Destroy(ES_CommonItemViewComponent self)
		{
			self.DestroyWidget();
		}
	}

	[ObjectSystem]
	public class ES_CommonItemAwakeSystem : AwakeSystem<ES_CommonItem,Transform> 
	{
		protected override void Awake(ES_CommonItem self,Transform transform)
		{
			self.AddComponent<ES_CommonItemViewComponent,Transform>(transform);
			self.RegisterUIEvent();
		}
	}


	[ObjectSystem]
	public class ES_CommonItemDestroySystem : DestroySystem<ES_CommonItem> 
	{
		protected override void Destroy(ES_CommonItem self)
		{
		}
	}
}
