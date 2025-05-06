using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ET.Client
{

    public enum UIWindowType
    {
        World3DRoot,    // 3DUI
        WorldCameraRoot,    // 3D摄像机下UI
        WorldHubRoot,    // 战斗Hub
        NormalRoot,    // 普通主界面
        FixedRoot,     // 固定窗口
        PopUpRoot,     // 弹出窗口
        NoticeRoot,      //通知窗口
        LoadingRoot,      //loading窗口
        HighestFixedRoot,      //最高固定窗口
        HighestNoticeRoot,      //最高通知窗口
    }

    public class ShowWindowData : Entity
    {

    }
}