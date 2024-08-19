using System;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
	public enum BattleDragItemType
	{
		PKTower,
		PKMonster,
		PKMoveTower,
		PKMovePlayer,
		HeadQuarter,
		MonsterCall,
		Tower,
		MoveTower,
	}

	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleDragItem : Entity, IAwake, IUILogic, IUIDlg
	{
		public DlgBattleDragItemViewComponent View { get => this.GetComponent<DlgBattleDragItemViewComponent>(); }

		public long Timer;
		public bool isUpdating;

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
		public bool isClickUGUI = false;
		public bool isDragging = false;
		public bool isCliffy = false;
		public bool isRaycast = false;
		public float3 rayHitPos;

		public int tryNum = 0;
		public float tryDis = 0f;

		public bool canPutMonsterCall = true;

		//坐标在Y轴上的偏移量
		public float _YOffset = 0.1F;
		public LayerMask _groundLayerMask;

		public long newShowLineRendererTime;

		public float3 lineRendererPos;
		public bool canShowLineRendererNear;
		public bool lineRendererReqing = false;

		public float3 recordLastRayPos;
		public long recordLastChkPutRepeatTime;

		public float3 lastRayPos;
		public float3 lastDragRectifyPos;

		public long beginDragTime;
		public long canPutTime;
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
