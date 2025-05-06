using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleDragItem : Entity, IAwake, IUILogic, IUIDlg
	{
		public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
		public DlgBattleDragItemViewComponent View { get => this.GetComponent<DlgBattleDragItemViewComponent>(); }

		public long Timer;

		public BattleDragItemType battleDragItemType;
		public string battleDragItemParam;
		public long moveTowerUnitId;
		public int countOnce;
		public string createActionIds;
		public Action<Scene> callBack;
		public Scene sceneIn;

		public bool isConfirming = false;

		//当前要被放置的对象
		public GameObject currentPlaceObj = null;
		public float3 _lastPlaceObjPos = float3.zero;
		public bool isClickUGUI = false;
		public bool isDragging = false;
		public bool redrawPathWhenClose = false;
		public bool isCliffy = false;
		public bool isRaycast = false;
		public float3 rayHitPos;

		public int tryNum = 0;
		public float tryDis = 0f;

		public bool pathfindingSuccess = true;
		public bool hasNavMeshFromHeadQuarter = false;

		//坐标在Y轴上的偏移量
		public float _YOffset = 0.02F;
		public float _CliffyYOffset = 0.5F;
		public LayerMask _groundLayerMask;

		public long newShowLineRendererTime;

		public float3 asyncCheckPos;
		public bool asyncChecked;
		public bool isAsyncChecking = false;

		public float3 recordLastRayPos;
		public long recordLastChkPutRepeatTime;

		public float3 lastRayPos;
		public float3 lastDragRectifyPos;

		public long beginDragTime;
		public long canPutTime;

		public List<float3> PointDiffs;
	}

	public class DlgBattleDragItem_ShowWindowData : ShowWindowData
	{
		public BattleDragItemType battleDragItemType;
		public string battleDragItemParam;
		public long moveTowerUnitId;
		public int countOnce;
		public string createActionIds;
		public Action<Scene> callBack;
		public Scene sceneIn;
	}
}
