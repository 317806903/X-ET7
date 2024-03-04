using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARSceneSliderSimple : Entity, IAwake, IUILogic
	{
		public DlgARSceneSliderSimpleViewComponent View { get => this.GetComponent<DlgARSceneSliderSimpleViewComponent>(); }
		public int curScaleIndex;
		public int defaultScaleIndex;
		public List<float> scaleSettingList;

		public List<(float2 disPos, string unitCfgId)> showPrefabCfgList;
		public List<GameObject> showPrefabList;

		public long Timer;
	}
}
