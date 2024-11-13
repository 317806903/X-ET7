
using System;
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	public partial class Scroll_Item_SkillBattleInfo
	{
		public long unitId;
		public string skillCfgId;
		public Action<string> onDownCallBack;
		public Action<string> onPressCallBack;
		public Action<string> onExitCallBack;
		public Action<string> onClickCallBack;

		public bool isPressing;
		public bool isShowDetail;
	}
}
