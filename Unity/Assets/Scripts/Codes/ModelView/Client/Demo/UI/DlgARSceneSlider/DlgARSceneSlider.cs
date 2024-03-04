using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARSceneSlider : Entity, IAwake, IUILogic
	{
		public DlgARSceneSliderViewComponent View { get => this.GetComponent<DlgARSceneSliderViewComponent>(); }
		public float curScale;
		public float defaultScale;
		public float minScale;
		public float maxScale;
		public List<(float2 disPos, string unitCfgId)> showPrefabCfgList;
		public List<GameObject> showPrefabList;

		public long Timer;
	}
}
