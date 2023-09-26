using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTowerAR : Entity, IAwake, IUILogic
	{
		public DlgBattleTowerARViewComponent View { get => this.GetComponent<DlgBattleTowerARViewComponent>(); }

		public Dictionary<int, Scroll_Item_Tower> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemTowerBuy;

		public Dictionary<string, int> myOwnTowList = new();

		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus;
		public long curTipTime;

		public long curLeftTime;
		public string curLeftTimeMsg;

		public long Timer;
		public string selectCfgId;
		public UISelectCfgType selectCfgType;

		//对象的缩放系数
		public float _scaleFactor = 1.2f;
		//地面层级
		public LayerMask _groundLayerMask;
		public int touchID;
		public bool isDragging = false;
		public bool isTouchInput = false;

		public bool isRaycast = false;
		public bool isCliffy = false;
		public bool canPutMonsterCall = true;

		public long newShowLineRendererTime;

		public float3 lineRendererPos;
		public bool canShowLineRendererNear;
		public bool lineRendererReqing = false;

		//当前要被放置的对象
		public GameObject currentPlaceObj = null;
		//坐标在Y轴上的偏移量
		public float _YOffset = 0.1F;
		public bool isClickUGUI = false;
		public List<RaycastResult> results = new List<RaycastResult>();
		public PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);


	}
}
