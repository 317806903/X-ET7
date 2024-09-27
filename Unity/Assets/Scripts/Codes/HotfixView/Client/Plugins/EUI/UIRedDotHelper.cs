using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (UIRedDotComponent))]
    public static class UIRedDotHelper
    {
        public static UIRedDotComponent GetUIRedDotComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = scene.ClientScene();
            }

            CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
            UIRedDotComponent _UIRedDotComponent = currentScenesComponent.GetComponent<UIRedDotComponent>();
            if (_UIRedDotComponent == null)
            {
                _UIRedDotComponent = currentScenesComponent.AddComponent<UIRedDotComponent>();
            }

            return _UIRedDotComponent;
        }

        /// <summary>
        /// 增加逻辑红点节点
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="parent"></param>
        /// <param name="target"></param>
        /// <param name="isNeedShowNum"></param>
        public static void AddRedDotNode(Scene scene, UIRedDotType parentRedDotType, UIRedDotType uiRedDotType, bool isNeedShowNum)
        {
            AddRedDotNode(scene, parentRedDotType.ToString(), uiRedDotType.ToString(), isNeedShowNum);
        }
        public static void AddRedDotNode(Scene scene, string parent, string target, bool isNeedShowNum)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(parent) && !uiRedDotComponent.RedDotNodeParentsDict.ContainsKey(parent))
            {
                Log.Warning("Runtime动态添加的红点，其父节点是新节点： " + parent);
            }

            uiRedDotComponent.AddRedDotNode(parent, target, isNeedShowNum);
        }

        /// <summary>
        /// 移除逻辑红点
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <param name="isRemoveView"></param>
        public static void RemoveRedDotNode(Scene scene, UIRedDotType uiRedDotType, bool isRemoveView = true)
        {
            RemoveRedDotNode(scene, uiRedDotType.ToString(), isRemoveView);
        }
        public static void RemoveRedDotNode(Scene scene, string target, bool isRemoveView = true)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            uiRedDotComponent.RemoveRedDotNode(target);
            if (isRemoveView)
            {
                uiRedDotComponent.RemoveRedDotView(target, out UIRedDotMonoView redDotMonoView);
            }
        }

        /// <summary>
        /// 增加红点节点显示层
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <param name="monoView"></param>
        public static void AddRedDotNodeView(Scene scene, UIRedDotType uiRedDotType, GameObject gameObject, Vector3 RedDotScale, Vector2 PositionOffset)
        {
            AddRedDotNodeView(scene, uiRedDotType.ToString(), gameObject, RedDotScale, PositionOffset);
        }
        public static void AddRedDotNodeView(Scene scene, string target, GameObject gameObject, Vector3 RedDotScale, Vector2 PositionOffset)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            UIRedDotMonoView monoView = gameObject.GetComponent<UIRedDotMonoView>() ?? gameObject.AddComponent<UIRedDotMonoView>();
            monoView.RedDotScale = RedDotScale;
            monoView.PositionOffset = PositionOffset;
            uiRedDotComponent.AddRedDotView(target, monoView);
        }

        /// <summary>
        /// 增加红点节点显示层
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <param name="monoView"></param>
        public static void AddRedDotNodeView(Scene scene, UIRedDotType uiRedDotType, UIRedDotMonoView monoView)
        {
            AddRedDotNodeView(scene, uiRedDotType.ToString(), monoView);
        }
        public static void AddRedDotNodeView(Scene scene, string target, UIRedDotMonoView monoView)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            uiRedDotComponent.AddRedDotView(target, monoView);
        }

        /// <summary>
        /// 移除红点节点显示层
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <param name="monoView"></param>
        public static void RemoveRedDotView(Scene scene, UIRedDotType uiRedDotType, out UIRedDotMonoView monoView)
        {
            RemoveRedDotView(scene, uiRedDotType.ToString(), out monoView);
        }
        public static void RemoveRedDotView(Scene scene, string target, out UIRedDotMonoView monoView)
        {
            monoView = null;
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            uiRedDotComponent.RemoveRedDotView(target, out monoView);
        }

        /// <summary>
        /// 隐藏逻辑红点
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool HideRedDotNode(Scene scene, UIRedDotType uiRedDotType)
        {
            return HideRedDotNode(scene, uiRedDotType.ToString());
        }
        public static bool HideRedDotNode(Scene scene, string target)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return false;
            }

            return uiRedDotComponent.HideRedDotNode(target);
        }

        /// <summary>
        /// 显示逻辑红点
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool ShowRedDotNode(Scene scene, UIRedDotType uiRedDotType)
        {
            return ShowRedDotNode(scene, uiRedDotType.ToString());
        }
        public static bool ShowRedDotNode(Scene scene, string target)
        {
            if (IsLogicAlreadyShow(scene, target))
            {
                return false;
            }

            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return false;
            }

            return uiRedDotComponent.ShowRedDotNode(target);
        }

        /// <summary>
        /// 逻辑红点是否已经处于显示状态
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsLogicAlreadyShow(Scene scene, UIRedDotType uiRedDotType)
        {
            return IsLogicAlreadyShow(scene, uiRedDotType.ToString());
        }
        public static bool IsLogicAlreadyShow(Scene scene, string target)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                Log.Error("uiRedDotComponent is not exist!");
                return false;
            }

            if (!uiRedDotComponent.RedDotNodeRetainCount.ContainsKey(target))
            {
                return false;
            }

            return uiRedDotComponent.RedDotNodeRetainCount[target] >= 1;
        }

        /// <summary>
        /// 刷新红点显示层的文本数量
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="target"></param>
        /// <param name="Count"></param>
        public static void RefreshRedDotViewCount(Scene scene, UIRedDotType uiRedDotType, int Count)
        {
            RefreshRedDotViewCount(scene, uiRedDotType.ToString(), Count);
        }
        public static void RefreshRedDotViewCount(Scene scene, string target, int Count)
        {
            UIRedDotComponent uiRedDotComponent = GetUIRedDotComponent(scene);
            if (uiRedDotComponent == null)
            {
                return;
            }

            uiRedDotComponent.RefreshRedDotViewCount(target, Count);
        }


        public static void AddUIRedDotView(Scene scene, GameObject go)
        {
            UIRedDotMonoView[] list = go.GetComponentsInChildren<UIRedDotMonoView>();
            foreach (UIRedDotMonoView _UIRedDotMonoView in list)
            {
                if (_UIRedDotMonoView == null)
                {
                    continue;
                }
                UIRedDotHelper.AddRedDotNodeView(scene, _UIRedDotMonoView.UIRedDotType, _UIRedDotMonoView);
            }
        }

        public static void RemoveUIRedDotView(Scene scene, GameObject go)
        {
            if (go == null || scene == null || scene.IsDisposed)
            {
                return;
            }
            UIRedDotMonoView[] list = go.GetComponentsInChildren<UIRedDotMonoView>(true);
            foreach (UIRedDotMonoView _UIRedDotMonoView in list)
            {
                if (_UIRedDotMonoView == null)
                {
                    continue;
                }
                UIRedDotHelper.RemoveRedDotView(scene, _UIRedDotMonoView.UIRedDotType, out var tmp);
            }
        }

    }
}