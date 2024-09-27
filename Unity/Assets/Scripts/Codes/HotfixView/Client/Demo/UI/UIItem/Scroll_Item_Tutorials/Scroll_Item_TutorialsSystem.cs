using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_Tutorials))]
	public static class Scroll_Item_TutorialsSystem
	{
		public static void Init(this Scroll_Item_Tutorials self, int index, Action<int> callBack)
		{
            self.EButton_VideoSelectButton.AddListener(() =>
            {              
                    callBack?.Invoke(index);
            });
            self.SetItemStatus(false);
        }

     

        /// <summary>
        /// 设置按钮的选中状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="status"></param>
        public static void SetItemStatus(this Scroll_Item_Tutorials self, bool status)
        {
            self.E_SelectedImage.SetVisible(status);
        }
    }
}
