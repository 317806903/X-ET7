using UnityEngine;

namespace ET.Client
{
    // UIBaseWindow 类继承自 Entity，并实现了 IAwake 和 IDestroy 接口
    // 用于表示 UI 窗口的基本属性和行为
    [ChildOf(typeof(UIComponent))]
    public class UIBaseWindow : Entity, IAwake, IDestroy
    {
        // 是否预加载标志，用于指示该窗口是否已经被预加载
        public bool IsPreLoad
        {
            get
            {
                return this.UIPrefabGameObject != null;
            }
        }

        // 获取 UI 窗口的 Transform 组件
        public Transform uiTransform
        {
            get
            {
                if (this.UIPrefabGameObject != null)
                {
                    return this.UIPrefabGameObject.transform;
                }
                return null;
            }
        }

        // UI 窗口的唯一标识符
        public WindowID WindowID
        {
            get
            {
                if (this.m_windowID == WindowID.WindowID_Invaild)
                {
                    Debug.LogError("window id is " + WindowID.WindowID_Invaild);
                }
                return m_windowID;
            }
            set { m_windowID = value; }
        }

        // 指示该窗口是否在栈队列中
        public bool IsInStackQueue
        {
            get;
            set;
        }

        // UI 窗口的唯一标识符
        public WindowID m_windowID = WindowID.WindowID_Invaild;
        // UI 窗口的预制体 GameObject
        public GameObject UIPrefabGameObject = null;
        // UI 窗口的层级
        public UIWindowType windowType = UIWindowType.Normal;
    }
}
