using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	public enum UISelectCfgType
	{
		HeadQuarter,
		MonsterCall,
		Tower,
		Tanker,
	}
	 [ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattleTower :Entity,IAwake,IUILogic
	{
		public DlgBattleTowerViewComponent View { get => this.GetComponent<DlgBattleTowerViewComponent>();} 

		public Dictionary<int, Scroll_Item_Tower> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_TowerBuy> ScrollItemTowerBuy;

		public Dictionary<string, int> myOwnTowList = new();
		
		public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus;
		public long curLeftTime;
		public string curLeftTimeMsg;
		
		public long Timer;
		public string selectCfgId;
		public UISelectCfgType selectCfgType;
		
		//物体z轴距摄像机的长度
		public float _zDistance = 50f;
		//对象的缩放系数
		public float _scaleFactor = 1.2f;
		//地面层级
		public LayerMask _groundLayerMask;
		public int touchID;
		public bool isDragging = false;
		public bool isTouchInput = false;
		//是否是有效的放置（如果放置在地面上返回True,否则为False）
		public bool isPlaceSuccess = false;
		//当前要被放置的对象
		public GameObject currentPlaceObj = null;
		//坐标在Y轴上的偏移量
		public float _YOffset = 0.5F;
		public bool isClickUGUI = false;
		public List<RaycastResult> results = new List<RaycastResult>();
		public PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
		 

	}
}
