using UnityEngine;

namespace ET.Client
{
    // UIBaseWindowAwakeSystem 类继承自 AwakeSystem，并指定泛型参数为 UIBaseWindow
    // 用于在 UIBaseWindow 被唤醒时初始化其属性
    [ObjectSystem]
    public class UIBaseWindowAwakeSystem : AwakeSystem<UIBaseWindow>
    {
        // 在 UIBaseWindow 被唤醒时调用，设置 IsInStackQueue 为 false
        protected override void Awake(UIBaseWindow self)
        {
            self.IsInStackQueue = false;
        }
    }

    // UIBaseWindowDestroySystem 类继承自 DestroySystem，并指定泛型参数为 UIBaseWindow
    // 用于在 UIBaseWindow 被销毁时清理其属性
    [ObjectSystem]
    public class UIBaseWindowDestroySystem : DestroySystem<UIBaseWindow>
    {
        // 在 UIBaseWindow 被销毁时调用，清理 WindowID、IsInStackQueue 属性，并销毁 UIPrefabGameObject
        protected override void Destroy(UIBaseWindow self)
        {
            self.WindowID = WindowID.WindowID_Invaild;
            self.IsInStackQueue = false;
            if (self.UIPrefabGameObject != null)
            {
                GameObject.Destroy(self.UIPrefabGameObject);
                self.UIPrefabGameObject = null;
            }
        }
    }

    // UIBaseWindowSystem 类定义了扩展方法用于 UIBaseWindow 类的操作
    public static class UIBaseWindowSystem
    {
        // 将 UIBaseWindow 添加到指定的根节点下，并设置其缩放为标准尺寸
        public static void SetRoot(this UIBaseWindow self, Transform rootTransform)
        {
            // 检查 UIBaseWindow 的 uiTransform 是否为空
            if (self.uiTransform == null)
            {
                Log.Error($"uibaseWindows {self.WindowID} uiTransform is null!!!");
                return;
            }
            // 检查传入的根节点是否为空
            if (rootTransform == null)
            {
                Log.Error($"uibaseWindows {self.WindowID} rootTransform is null!!!");
                return;
            }
            // 设置 UIBaseWindow 的父级为传入的根节点，并保持局部缩放不变
            self.uiTransform.SetParent(rootTransform, false);
            // 将 UIBaseWindow 的缩放重置为标准尺寸
            self.uiTransform.transform.localScale = Vector3.one;
        }
    }
}
