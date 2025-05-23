﻿using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgARSceneSliderSimple : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgARSceneSliderSimpleViewComponent View { get => this.GetComponent<DlgARSceneSliderSimpleViewComponent>(); }
		public int curScaleIndex;
		public int defaultScaleIndex;
		public List<float> scaleSettingList;

		public List<(float2 disPos, string unitCfgId)> showPrefabCfgList;
		public Dictionary<GameObject, float> orgPrefabLocalScaleDic;
		public List<GameObject> showPrefabList;

		public long Timer;
	}
}
