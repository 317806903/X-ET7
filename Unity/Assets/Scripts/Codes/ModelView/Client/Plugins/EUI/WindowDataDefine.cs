using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ET.Client
{

    public enum UIWindowType
    {
        WorldHub,    // 战斗Hub
        Normal,    // 普通主界面
        Fixed,     // 固定窗口
        PopUp,     // 弹出窗口
        NoticeRoot,      //通知窗口
        LoadingRoot,      //loading窗口
        HighestFixedRoot,      //最高固定窗口
        HighestNoticeRoot,      //最高通知窗口
    }

    public class ShowWindowData : Entity
    {

    }
}