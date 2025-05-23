﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ObjectSystem]
    public class UIComponentAwakeSystem: AwakeSystem<UIComponent>
    {
        protected override void Awake(UIComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class UIComponentDestroySystem: DestroySystem<UIComponent>
    {
        protected override void Destroy(UIComponent self)
        {
            self.Destroy();
        }
    }

    [FriendOf(typeof (ShowWindowData))]
    [FriendOf(typeof (UIPathComponent))]
    [FriendOf(typeof (UIBaseWindow))]
    [FriendOf(typeof (UIComponent))]
    public static class UIComponentSystem
    {
        public static void Awake(this UIComponent self)
        {
            self.IsPopStackWndStatus = false;
            self.AllWindowsDic?.Clear();
            self.VisibleWindowsDic?.Clear();
            self.StackWindowsQueue?.Clear();
            self.UIBaseWindowlistCached?.Clear();
        }

        /// <summary>
        /// 窗口是否是正在显示的
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <returns></returns>
        public static bool IsWindowVisible(this UIComponent self, WindowID id)
        {
            return self.VisibleWindowsDic.ContainsKey((int)id);
        }

        /// <summary>
        /// 根据泛型获得UI窗口逻辑组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNeedShowState"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDlgLogic<T>(this UIComponent self, bool isNeedShowState = false, bool isNeedShowFront = false) where T : Entity, IUILogic, IUIDlg
        {
            if (UIPathComponent.Instance == null || UIPathComponent.Instance.WindowTypeIdDict == null)
            {
                return null;
            }

            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            UIBaseWindow baseWindow = self.GetUIBaseWindow(windowsId);
            if (null == baseWindow)
            {
                //Log.Warning($"{windowsId} is not created!");
                return null;
            }

            if (!baseWindow.IsPreLoad)
            {
                Log.Warning($"{windowsId} is not loaded!");
                return null;
            }

            if (isNeedShowState)
            {
                if (!self.IsWindowVisible(windowsId))
                {
                    //Log.Warning($"{windowsId} is need show state!");
                    return null;
                }
            }

            if (isNeedShowFront)
            {
                baseWindow.uiTransform.SetAsLastSibling();
                self.DealSpec(baseWindow);
            }
            return baseWindow.GetComponent<T>();
        }

        /// <summary>
        /// 根据泛型类型获取窗口Id
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static WindowID GetWindowIdByGeneric<T>(this UIComponent self) where T : Entity
        {
            if (UIPathComponent.Instance.WindowTypeIdDict.TryGetValue(typeof (T).Name, out int windowsId))
            {
                return (WindowID)windowsId;
            }

            Log.Error($"{typeof (T).FullName} is not have any windowId!");
            return WindowID.WindowID_Invaild;
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShowStackWindow<T>(this UIComponent self) where T : Entity, IUILogic, IUIDlg
        {
            WindowID id = self.GetWindowIdByGeneric<T>();
            self.ShowStackWindow(id);
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void ShowStackWindow(this UIComponent self, WindowID id)
        {
            self.StackWindowsQueue.Enqueue(id);

            if (self.IsPopStackWndStatus)
            {
                return;
            }

            self.IsPopStackWndStatus = true;
            self.PopStackUIBaseWindow();
        }

        /// <summary>
        /// 弹出并显示一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        private static void PopStackUIBaseWindow(this UIComponent self)
        {
            if (self.StackWindowsQueue.Count > 0)
            {
                WindowID windowID = self.StackWindowsQueue.Dequeue();
                self.ShowWindow(windowID);
                UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(windowID);
                uiBaseWindow.IsInStackQueue = true;
            }
            else
            {
                self.IsPopStackWndStatus = false;
            }
        }

        /// <summary>
        /// 弹出并显示下一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        private static void PopNextStackUIBaseWindow(this UIComponent self, WindowID id)
        {
            UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(id);
            if (uiBaseWindow != null && !uiBaseWindow.IsDisposed && self.IsPopStackWndStatus && uiBaseWindow.IsInStackQueue)
            {
                uiBaseWindow.IsInStackQueue = false;
                self.PopStackUIBaseWindow();
            }
        }

        /// <summary>
        /// 根据指定Id的显示UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="showData"></OtherParam>
        private static void ShowWindow(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            try
            {
                UIBaseWindow baseWindow = self.ReadyToShowBaseWindow(id, showData);
                if (null != baseWindow)
                {
                    self.RealShowWindow(baseWindow, id, showData);
                }
            }
            catch (Exception e)
            {
                Log.Error($"ShowWindow {id.ToString()} {e}");
            }
            finally
            {
                if (showData != null)
                {
                    showData.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据泛型类型显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="showData"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShowWindow<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic, IUIDlg
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            self.ShowWindow(windowsId, showData);
        }

        /// <summary>
        /// 根据指定Id的异步加载显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <param name="showData"></param>
        public static async ETTask ShowWindowAsync(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            try
            {
                UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
                baseWindow = await self.ShowBaseWindowAsync(id, showData);
                if (null != baseWindow)
                {
                    self.RealShowWindow(baseWindow, id, showData);
                }
            }
            catch (Exception e)
            {
                Log.Error($"ShowWindowAsync {id.ToString()} {e}");
            }
            finally
            {
                if (showData != null)
                {
                    showData.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据泛型类型异步加载显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="showData"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask ShowWindowAsync<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic, IUIDlg
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            await self.ShowWindowAsync(windowsId, showData);
        }

        /// <summary>
        /// 隐藏ID指定的UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="onComplete"></OtherParam>
        public static void HideWindow(this UIComponent self, WindowID id)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int)id))
            {
                //Log.Warning($"检测关闭 WindowsID: {id} 失败！");
                return;
            }

            UIBaseWindow baseWindow = self.VisibleWindowsDic[(int)id];
            if (baseWindow == null || baseWindow.IsDisposed)
            {
                Log.Error($"UIBaseWindow is null  or isDisposed ,  WindowsID: {id} 失败！");
                return;
            }

            UIEventComponent.Instance.GetUIEventHandler(id).OnHideWindow(baseWindow, () =>
            {
                baseWindow.UIPrefabGameObject?.SetActive(false);
            });

            self.VisibleWindowsDic.Remove((int)id);

            self.PopNextStackUIBaseWindow(id);
        }

        /// <summary>
        /// 根据泛型类型隐藏UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void HideWindow<T>(this UIComponent self) where T : Entity
        {
            if (self.IsDisposed)
            {
                return;
            }

            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.HideWindow(hideWindowId);
        }

        /// <summary>
        /// 卸载指定的UI窗口实例
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        public static void UnLoadWindow(this UIComponent self, WindowID id, bool isDispose = true)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            if (null == baseWindow)
            {
                Log.Error($"UIBaseWindow WindowId {id} is null!!!");
                return;
            }

            UITextLocalizeComponent.Instance.RemoveUITextLocalizeView(baseWindow.UIPrefabGameObject);
            UIImageLocalizeComponent.Instance.RemoveUIImageLocalizeView(baseWindow.UIPrefabGameObject);
            UIRedDotHelper.RemoveUIRedDotView(self.DomainScene(), baseWindow.UIPrefabGameObject);
            UIEventComponent.Instance.GetUIEventHandler(id).BeforeUnload(baseWindow);
            if (baseWindow.IsPreLoad)
            {
                ResComponent.Instance.UnloadAsset(baseWindow.UIPrefabGameObject.name);
                //ResourcesComponent.Instance?.UnloadBundle(baseWindow.UIPrefabGameObject.name.StringToAB());
                UnityEngine.Object.Destroy(baseWindow.UIPrefabGameObject);
                baseWindow.UIPrefabGameObject = null;
            }

            if (isDispose)
            {
                self.AllWindowsDic.Remove((int)id);
                self.VisibleWindowsDic.Remove((int)id);
                baseWindow?.Dispose();
            }
        }

        /// <summary>
        /// 根据泛型类型卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void UnLoadWindow<T>(this UIComponent self) where T : Entity, IUILogic, IUIDlg
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.UnLoadWindow(hideWindowId);
        }

        private static UIBaseWindow ReadyToShowBaseWindow(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            // 如果UI不存在开始实例化新的窗口
            if (null == baseWindow)
            {
                baseWindow = self.AddChild<UIBaseWindow>();
                baseWindow.WindowID = id;
                self.LoadBaseWindows(baseWindow);
            }
            else
            {
                baseWindow.IsFirstLoad = false;
            }

            if (baseWindow.IsPreLoad == false)
            {
                self.LoadBaseWindows(baseWindow);
            }
            else
            {
                baseWindow.uiTransform.SetAsLastSibling();
            }

            self.DealSpec(baseWindow);

            return baseWindow;
        }

        private static async ETTask<UIBaseWindow> ShowBaseWindowAsync(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            CoroutineLock coroutineLock = null;
            try
            {
                coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoadUIBaseWindows, (int)id);
                UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
                if (baseWindow != null && baseWindow.UIPrefabGameObject == null)
                {
                    baseWindow.Dispose();
                    baseWindow = null;
                }

                if (null == baseWindow)
                {
                    if (UIPathComponent.Instance.WindowPrefabPath.ContainsKey((int)id))
                    {
                        baseWindow = self.AddChild<UIBaseWindow>();
                        baseWindow.WindowID = id;
                        await self.LoadBaseWindowsAsync(baseWindow);
                    }
                    else
                    {
                        baseWindow.IsFirstLoad = false;
                    }
                }
                else
                {
                    baseWindow.IsFirstLoad = false;
                }

                if (!baseWindow.IsPreLoad)
                {
                    await self.LoadBaseWindowsAsync(baseWindow);
                }
                else
                {
                    baseWindow.uiTransform.SetAsLastSibling();
                }

                self.DealSpec(baseWindow);

                return baseWindow;
            }
            catch (Exception e)
            {
                Log.Error($"[{id}][{e}]");
                return null;
            }
            finally
            {
                coroutineLock?.Dispose();
            }
        }

        private static void RealShowWindow(this UIComponent self, UIBaseWindow baseWindow, WindowID id, ShowWindowData showData = null)
        {
            ShowWindowData contextData = showData == null? null : showData;
            baseWindow.UIPrefabGameObject?.SetActive(true);
            UIEventComponent.Instance.GetUIEventHandler(id).OnShowWindow(baseWindow, contextData);

            self.VisibleWindowsDic[(int)id] = baseWindow;
#if UNITY_EDITOR
            Debug.Log("<color=magenta>### current Navigation window </color>" + baseWindow.WindowID.ToString());
#endif
        }

        public static void Destroy(this UIComponent self)
        {
            self.CloseAllWindow();
        }

        /// <summary>
        /// 根据窗口Id获取UIBaseWindow
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static UIBaseWindow GetUIBaseWindow(this UIComponent self, WindowID id)
        {
            if (self.AllWindowsDic.ContainsKey((int)id))
            {
                return self.AllWindowsDic[(int)id];
            }

            return null;
        }

        /// <summary>
        /// 根据窗口Id隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <param name="windowId"></param>
        public static void CloseWindow(this UIComponent self, WindowID windowId)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int)windowId))
            {
                if (self.AllWindowsDic.ContainsKey((int)windowId))
                {
                    self.UnLoadWindow(windowId);
                }
                return;
            }

            self.HideWindow(windowId);
            self.UnLoadWindow(windowId);
#if UNITY_EDITOR
            Debug.Log("<color=magenta>## close window without PopNavigationWindow() ##</color>");
#endif
        }

        /// <summary>
        /// 根据窗口泛型类型隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void CloseWindow<T>(this UIComponent self) where T : Entity, IUILogic, IUIDlg
        {
            if (self.IsDisposed)
            {
                return;
            }

            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.CloseWindow(hideWindowId);
        }

        /// <summary>
        /// 关闭并卸载所有的窗口实例
        /// </summary>
        /// <param name="self"></param>
        public static void CloseAllWindow(this UIComponent self)
        {
            if (self.IsDisposed)
            {
                return;
            }

            self.IsPopStackWndStatus = false;
            if (self.AllWindowsDic == null)
            {
                return;
            }

            foreach (KeyValuePair<int, UIBaseWindow> window in self.AllWindowsDic)
            {
                UIBaseWindow baseWindow = window.Value;
                if (baseWindow == null || baseWindow.IsDisposed)
                {
                    continue;
                }

                self.HideWindow(baseWindow.WindowID);
                self.UnLoadWindow(baseWindow.WindowID, false);
                baseWindow?.Dispose();
            }

            self.AllWindowsDic.Clear();
            self.VisibleWindowsDic.Clear();
            self.StackWindowsQueue.Clear();
            self.UIBaseWindowlistCached.Clear();
        }

        public static void CloseWindowType(this UIComponent self, UIWindowType uiWindowType)
        {
            if (self.IsDisposed)
            {
                return;
            }

            self.UIBaseWindowlistCached.Clear();
            foreach (KeyValuePair<int, UIBaseWindow> window in self.AllWindowsDic)
            {
                if (window.Value.windowType != uiWindowType)
                    continue;
                if (window.Value.IsDisposed)
                {
                    continue;
                }

                self.UIBaseWindowlistCached.Add((WindowID)window.Key);
            }

            if (self.UIBaseWindowlistCached.Count > 0)
            {
                for (int i = 0; i < self.UIBaseWindowlistCached.Count; i++)
                {
                    self.CloseWindow(self.UIBaseWindowlistCached[i]);
                }
                self.UIBaseWindowlistCached.Clear();
            }
        }

        /// <summary>
        /// 隐藏所有已显示的窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="includeFixed"></param>
        public static void HideAllShownWindow(this UIComponent self, bool includeFixed = false, bool includeNotice = true)
        {
            self.IsPopStackWndStatus = false;
            self.UIBaseWindowlistCached.Clear();
            foreach (KeyValuePair<int, UIBaseWindow> window in self.VisibleWindowsDic)
            {
                if ((window.Value.windowType == UIWindowType.FixedRoot ||
                        window.Value.windowType == UIWindowType.HighestFixedRoot) && !includeFixed)
                    continue;
                if ((window.Value.windowType == UIWindowType.NoticeRoot ||
                        window.Value.windowType == UIWindowType.HighestNoticeRoot) && !includeNotice)
                    continue;
                if (window.Value.IsDisposed)
                {
                    continue;
                }

                self.UIBaseWindowlistCached.Add((WindowID)window.Key);
            }

            if (self.UIBaseWindowlistCached.Count > 0)
            {
                for (int i = 0; i < self.UIBaseWindowlistCached.Count; i++)
                {
                    int id = (int)self.UIBaseWindowlistCached[i];
                    if (self.VisibleWindowsDic.ContainsKey(id))
                    {
                        UIBaseWindow uiBaseWindow = self.VisibleWindowsDic[id];
                        if (uiBaseWindow.IsDisposed == false)
                        {
                            UIEventComponent.Instance.GetUIEventHandler(uiBaseWindow.WindowID).OnHideWindow(uiBaseWindow, () =>
                            {
                                uiBaseWindow.UIPrefabGameObject?.SetActive(false);
                            });
                        }
                        self.VisibleWindowsDic.Remove(id);
                    }
                }

                self.UIBaseWindowlistCached.Clear();
            }

            self.StackWindowsQueue.Clear();
        }

        /// <summary>
        /// 同步加载UI窗口实例
        /// </summary>
        private static void LoadBaseWindows(this UIComponent self, UIBaseWindow baseWindow)
        {
            if (!UIPathComponent.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID, out string value))
            {
                Log.Error($"{baseWindow.WindowID} uiPath is not Exist!");
                return;
            }

            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(value);
            // ResourcesComponent.Instance.LoadBundle(value.StringToAB());
            // GameObject go                      = ResourcesComponent.Instance.GetAsset(value.StringToAB(), value ) as GameObject;
            baseWindow.UIPrefabGameObject = GameObject.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;
            baseWindow.IsFirstLoad = true;
            baseWindow.UIPrefabGameObject.SetActive(false);

            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            if (baseWindow.windowType == UIWindowType.World3DRoot)
            {
                GameObjectHelper.SetLayer(baseWindow.UIPrefabGameObject, LayerMask.NameToLayer("UI3D"), true);
            }
            else
            {
                GameObjectHelper.SetLayer(baseWindow.UIPrefabGameObject, LayerMask.NameToLayer("UI"), true);
            }

            baseWindow.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.windowType));
            baseWindow.uiTransform.SetAsLastSibling();

            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);
            UITextLocalizeComponent.Instance.AddUITextLocalizeView(baseWindow.UIPrefabGameObject);
            UIImageLocalizeComponent.Instance.AddUIImageLocalizeView(baseWindow.UIPrefabGameObject);
            ET.Client.UIRedDotHelper.AddUIRedDotView(self.DomainScene(), baseWindow.UIPrefabGameObject);

            self.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        }

        /// <summary>
        /// 异步加载UI窗口实例
        /// </summary>
        private static async ETTask LoadBaseWindowsAsync(this UIComponent self, UIBaseWindow baseWindow)
        {
            if (!UIPathComponent.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID, out string value))
            {
                Log.Error($"{baseWindow.WindowID} is not Exist!");
                return;
            }

            GameObject go = await ResComponent.Instance.LoadAssetAsync<GameObject>(value);
            // await ResourcesComponent.Instance.LoadBundleAsync(value.StringToAB());
            // GameObject go                      = ResourcesComponent.Instance.GetAsset(value.StringToAB(), value ) as GameObject;
            baseWindow.UIPrefabGameObject = GameObject.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;
            baseWindow.IsFirstLoad = true;

            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            if (baseWindow.windowType == UIWindowType.World3DRoot)
            {
                GameObjectHelper.SetLayer(baseWindow.UIPrefabGameObject, LayerMask.NameToLayer("UI3D"), true);
            }
            else
            {
                GameObjectHelper.SetLayer(baseWindow.UIPrefabGameObject, LayerMask.NameToLayer("UI"), true);
            }

            baseWindow?.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.windowType));
            baseWindow.uiTransform.SetAsLastSibling();

            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);
            UITextLocalizeComponent.Instance.AddUITextLocalizeView(baseWindow.UIPrefabGameObject);
            UIImageLocalizeComponent.Instance.AddUIImageLocalizeView(baseWindow.UIPrefabGameObject);
            ET.Client.UIRedDotHelper.AddUIRedDotView(self.DomainScene(), baseWindow.UIPrefabGameObject);

            self.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        }

        private static void DealSpec(this UIComponent self, UIBaseWindow baseWindow)
        {
            self.DealCanvas(baseWindow);
            self.ResetCanvasSortingOrder(baseWindow);
            self.ResetScale(baseWindow);
            self.ResetRendererSortingOrder(baseWindow);
            self.DealCanvasAfter(baseWindow);
        }

        private static void DealCanvas(this UIComponent self, UIBaseWindow baseWindow)
        {
            ET.Client.EUIRootHelper.DealCanvas(baseWindow.uiTransform.gameObject);
        }

        private static void DealCanvasAfter(this UIComponent self, UIBaseWindow baseWindow)
        {
            ET.Client.EUIRootHelper.DealCanvasAfter(baseWindow);
        }

        private static void ResetCanvasSortingOrder(this UIComponent self, UIBaseWindow baseWindow)
        {
            (bool isNeedResetSorting, bool isNeedChgSortingLayerName, string sortingLayerName) = ET.Client.EUIRootHelper.ChkIsResetSortingLayer(baseWindow.windowType);
            if (isNeedResetSorting)
            {
                Canvas canvas = baseWindow.uiTransform.gameObject.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.overrideSorting = false;
                    canvas.sortingOrder = 1;

                    if (isNeedChgSortingLayerName)
                    {
                        canvas.sortingLayerName = sortingLayerName;
                    }

                    int maxSortingOrder = EUIRootHelper.GetCanvasSortingOrderByWindowType(baseWindow.windowType);
                    foreach (KeyValuePair<int, UIBaseWindow> window in self.AllWindowsDic)
                    {
                        UIBaseWindow baseWindowTmp = window.Value;
                        if (baseWindowTmp == null || baseWindowTmp.IsDisposed)
                        {
                            continue;
                        }

                        if (baseWindowTmp.windowType != baseWindow.windowType)
                        {
                            continue;
                        }
                        if (baseWindowTmp.uiTransform.gameObject.activeSelf == false)
                        {
                            continue;
                        }
                        if (baseWindowTmp.uiTransform.gameObject == baseWindow.uiTransform.gameObject)
                        {
                            continue;
                        }
                        UIPannelSortingOrder uiPannelSortingOrderChild = baseWindowTmp.uiTransform.gameObject.GetComponent<UIPannelSortingOrder>();
                        if (uiPannelSortingOrderChild != null)
                        {
                            if (maxSortingOrder < uiPannelSortingOrderChild.sortingOrder)
                            {
                                maxSortingOrder = uiPannelSortingOrderChild.sortingOrder;
                            }
                        }
                    }

                    UIPannelSortingOrder uiPannelSortingOrder = baseWindow.uiTransform.gameObject.GetComponent<UIPannelSortingOrder>();
                    if (uiPannelSortingOrder != null)
                    {
                        uiPannelSortingOrder.sortingOrder = maxSortingOrder + 1;
                    }
                }
            }
        }

        private static bool ResetScale(this UIComponent self, UIBaseWindow baseWindow)
        {
            (bool isNeedResetScale, float scaleValue) = ET.Client.EUIRootHelper.ChkIsResetScale(baseWindow.windowType);
            if (isNeedResetScale)
            {
                if (baseWindow.uiTransform.localScale.Equals(Vector3.one * scaleValue) == false)
                {
                    baseWindow.uiTransform.localScale = Vector3.one * scaleValue;
                    baseWindow.uiTransform.localPosition *= scaleValue;
                    return true;
                }
            }

            return false;
        }

        private static void ResetRendererSortingOrder(this UIComponent self, UIBaseWindow baseWindow)
        {
            GameObject gameObject = baseWindow.UIPrefabGameObject;
            ET.Client.EUIRootHelper.ResetRendererSortingOrder(gameObject);
        }

    }
}