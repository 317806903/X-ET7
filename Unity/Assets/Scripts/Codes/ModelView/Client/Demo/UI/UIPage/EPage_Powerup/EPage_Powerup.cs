using System.Collections.Generic;

namespace ET.Client
{
	public class EPage_Powerup : Entity, IAwake<UnityEngine.Transform>, IDestroy, IUILogic
	{
		public EPage_PowerupViewComponent View { get => this.GetComponent<EPage_PowerupViewComponent>(); }

        /// <summary>
        /// Item的字典
        /// </summary>
        public Dictionary<int, Scroll_Item_PowerUps> ScrollItemDic;

        /// <summary>
        /// 当前底部ITem的配置
        /// </summary>
        public string BottomtItemCfg;

        /// <summary>
        /// 当前选中的Item的索引
        /// </summary>
        public int CurrentItemIndex;

        /// <summary>
        /// 当前是否正在升级
        /// </summary>
        public bool isUpadaeting;
    }
}
