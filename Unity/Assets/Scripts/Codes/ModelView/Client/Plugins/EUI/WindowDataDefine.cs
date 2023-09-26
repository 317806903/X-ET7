using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ET.Client
{

    public enum UIWindowType
    {
        Normal,    // 普通主界面
        Fixed,     // 固定窗口
        PopUp,     // 弹出窗口
        NoticeRoot,      //其他窗口
        LoadingRoot,      //其他窗口
        HighestNoticeRoot,      //其他窗口
    }

    public class ShowWindowData : Entity
    {

    }
}