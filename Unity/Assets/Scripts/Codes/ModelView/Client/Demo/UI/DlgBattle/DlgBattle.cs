using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgBattle : Entity, IAwake, IUILogic
	{

		public DlgBattleViewComponent View { get => this.GetComponent<DlgBattleViewComponent>(); }

		public Dictionary<int, Scroll_Item_Tower> ScrollItemTowers;
		public Dictionary<int, Scroll_Item_Tower> ScrollItemTanks;

		public List<string> towerList = new List<string>()
		{
			"TowCallMonster_1",
			"Tow5_1",
			"Tow5_2",
			"Tow5_3",
			"Tow6_1",
			"Tow6_2",
			"Tow6_3",
			"Tow7_1",
			"Tow7_2",
			"Tow7_3",
			"Tow8_1",
			"Tow8_2",
			"Tow8_3",
			"Tow1_1",
			"Tow1_2",
			"Tow1_3",
			"Tow2_1",
			"Tow2_2",
			"Tow2_3",
			"Tow3_1",
			"Tow3_2",
			"Tow3_3",
			"Tow4_1",
			"Tow4_2",
			"Tow4_3",
		};

		public List<string> monsterList = new List<string>()
		{
			"Monster1_1",
			"Monster2_1",
			"Monster3_1",
			"Monster4_1",
			"Monster5_1",
			"Monster6_1",
			"Monster7_1",
			"Monster8_1",
			"Monster9_1"
		};

		public long Timer;
		public string selectCfgId;
		public UISelectCfgType selectCfgType;

		public long curTipTime;

		//对象的缩放系数
		public float _scaleFactor = 1.2f;
		//地面层级
		public LayerMask _groundLayerMask;
		public int touchID;
		public bool isDragging = false;
		public bool isTouchInput = false;

		public bool isRaycast = false;
		public bool isCliffy = false;
		//当前要被放置的对象
		public GameObject currentPlaceObj = null;
		//坐标在Y轴上的偏移量
		public float _YOffset = 0.1F;
		public bool isClickUGUI = false;
		public List<RaycastResult> results = new List<RaycastResult>();
		public PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
	}
}
