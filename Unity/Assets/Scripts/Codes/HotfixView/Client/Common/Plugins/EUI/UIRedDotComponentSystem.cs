﻿using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (UIRedDotComponent))]
    public static class UIRedDotComponentSystem
    {
        [ObjectSystem]
        public class UIRedDotComponentAwakeSystem: AwakeSystem<UIRedDotComponent>
        {
            protected override void Awake(UIRedDotComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIRedDotComponentDestroySystem: DestroySystem<UIRedDotComponent>
        {
            protected override void Destroy(UIRedDotComponent self)
            {
                foreach (var List in self.RedDotNodeParentsDict.Values)
                {
                    List.Dispose();
                }

                self.RedDotNodeParentsDict.Clear();
                self.ToParentDict.Clear();
                self.RedDotNodeRetainCount.Clear();
                self.RedDotMonoViewDict.Clear();
                self.RedDotNodeNeedShowSet.Clear();
                self.RetainViewCount.Clear();
            }
        }

        public static void AddRedDotNode(this UIRedDotComponent self, string parent, string target, bool isNeedShowNum)
        {
            if (string.IsNullOrEmpty(target))
            {
                Log.Error($"target is null");
                return;
            }

            if (string.IsNullOrEmpty(parent))
            {
                Log.Error($"parent is null");
                return;
            }

            if (self.ToParentDict.ContainsKey(target))
            {
                Log.Error($"{target} is already exist!");
                return;
            }

            self.ToParentDict.Add(target, parent);

            if (!self.RedDotNodeRetainCount.ContainsKey(target))
            {
                self.RedDotNodeRetainCount.Add(target, 0);
            }

            if (!self.RedDotNodeNeedShowSet.Contains(parent) && isNeedShowNum)
            {
                self.RedDotNodeNeedShowSet.Add(parent);
            }

            if (!self.RetainViewCount.ContainsKey(target))
            {
                self.RetainViewCount.Add(target, 0);
            }

            if (!self.RedDotNodeRetainCount.ContainsKey(parent))
            {
                self.RedDotNodeRetainCount.Add(parent, 0);
            }

            if (self.RedDotNodeParentsDict.TryGetValue(parent, out ListComponent<string> list))
            {
                list.Add(target);
                return;
            }

            var listComponent = ListComponent<string>.Create();
            listComponent.Add(target);
            self.RedDotNodeParentsDict.Add(parent, listComponent);
        }

        public static void RemoveRedDotNode(this UIRedDotComponent self, string target)
        {
            if (!self.ToParentDict.TryGetValue(target, out string parent))
            {
                return;
            }

            if (!self.IsLeafNode(target))
            {
                Log.Error("can not remove parent node!");
                return;
            }

            self.UpdateLogicNodeRetainCount(target, false);
            self.ToParentDict.Remove(target);
            if (!string.IsNullOrEmpty(parent))
            {
                self.RedDotNodeParentsDict[parent].Remove(target);
                if (self.RedDotNodeParentsDict[parent].Count <= 0)
                {
                    self.RedDotNodeParentsDict[parent].Dispose();
                    self.RedDotNodeParentsDict.Remove(parent);
                    self.RedDotNodeNeedShowSet.Remove(parent);
                }
            }

            self.RedDotNodeRetainCount.Remove(target);
        }

        public static void AddRedDotView(this UIRedDotComponent self, string target, UIRedDotMonoView monoView)
        {
            if (!self.RedDotNodeRetainCount.TryGetValue(target, out int retainCount))
            {
                Log.Error("redDot Node never added :" + target);
                return;
            }

            monoView.UIRedDotType = target;
            self.RedDotMonoViewDict[target] = monoView;

            if (retainCount == 0)
            {
                return;
            }

            monoView.Show(self.GetRedDotGameObjectFromPool());
        }

        public static void RemoveRedDotView(this UIRedDotComponent self, string target, out UIRedDotMonoView monoView)
        {
            if (self.RedDotMonoViewDict.TryGetValue(target, out monoView))
            {
                self.RedDotMonoViewDict.Remove(target);
            }

            if (monoView == null || !monoView.isRedDotActive)
            {
                return;
            }

            self.RecycleRedDotGameObject(monoView.Recovery());
        }

        public static bool IsLeafNode(this UIRedDotComponent self, string target)
        {
            return !self.RedDotNodeParentsDict.ContainsKey(target);
        }

        public static bool HideRedDotNode(this UIRedDotComponent self, string target)
        {
            if (!self.IsLeafNode(target))
            {
#if UNITY_EDITOR
                Log.Error("can not hide parent node!" + target);
#endif
                return false;
            }

            self.UpdateLogicNodeRetainCount(target, false);
            return true;
        }

        public static bool ShowRedDotNode(this UIRedDotComponent self, string target)
        {
            if (!self.IsLeafNode(target))
            {
#if UNITY_EDITOR
                Log.Error("can not show parent node : " + target);
#endif
                return false;
            }

            self.UpdateLogicNodeRetainCount(target);
            return true;
        }

        private static void UpdateLogicNodeRetainCount(this UIRedDotComponent self, string target, bool isRaiseRetainCount = true)
        {
            if (!self.RedDotNodeRetainCount.ContainsKey(target))
            {
#if UNITY_EDITOR
                Log.Error($"redDot logic node [{target}] is not exist!");
#endif
                return;
            }

            if (!self.IsLeafNode(target))
            {
#if UNITY_EDITOR
                Log.Error($"redDot logic node [{target}] is not leaf node!");
#endif
                return;
            }

            if (isRaiseRetainCount)
            {
                if (self.RedDotNodeRetainCount[target] == 1)
                {
#if UNITY_EDITOR
                    Log.Error($"redDot logic node [{target}] RetainCount is already one!");
#endif
                    return;
                }

                self.RedDotNodeRetainCount[target] += 1;
                if (self.RedDotNodeRetainCount[target] != 1)
                {
#if UNITY_EDITOR
                    Log.Error($"redDot logic node [{target}] RetainCount is {self.RedDotNodeRetainCount[target]}, number error!");
#endif
                    return;
                }
            }
            else
            {
                if (self.RedDotNodeRetainCount[target] != 1)
                {
                    //Log.Error($"redDot logic node [{target}] is not show status, RetainCount is {self.RedDotNodeRetainCount[target]}");
                    return;
                }

                self.RedDotNodeRetainCount[target] += -1;
            }

            int curr = self.RedDotNodeRetainCount[target];

            if (curr < 0 || curr > 1)
            {
#if UNITY_EDITOR
                Log.Error("count is error, redDot node is logic error!");
#endif
                return;
            }

            if (self.RedDotMonoViewDict.TryGetValue(target, out UIRedDotMonoView redDotMonoView))
            {
                if (isRaiseRetainCount)
                {
                    redDotMonoView.Show(self.GetRedDotGameObjectFromPool());
                }
                else
                {
                    self.RecycleRedDotGameObject(redDotMonoView.Recovery());
                }
            }

            bool isParentExist = self.ToParentDict.TryGetValue(target, out string parent);
            while (isParentExist)
            {
                self.RedDotNodeRetainCount[parent] += isRaiseRetainCount? 1 : -1;

                if (self.RedDotNodeRetainCount[parent] >= 1 && isRaiseRetainCount)
                {
                    if (self.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                    {
                        if (!redDotMonoView.isRedDotActive)
                        {
                            redDotMonoView.Show(self.GetRedDotGameObjectFromPool());
                        }
                    }
                }

                if (self.RedDotNodeRetainCount[parent] == 0 && !isRaiseRetainCount)
                {
                    if (self.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                    {
                        self.RecycleRedDotGameObject(redDotMonoView.Recovery());
                    }
                }

                isParentExist = self.ToParentDict.TryGetValue(parent, out parent);
            }
        }

        public static void RefreshRedDotViewCount(this UIRedDotComponent self, string target, int Count)
        {
            if (!self.IsLeafNode(target))
            {
#if UNITY_EDITOR
                Log.Error("can not refresh parent node view count");
#endif
                return;
            }

            self.RedDotMonoViewDict.TryGetValue(target, out UIRedDotMonoView redDotMonoView);

            self.RetainViewCount[target] = Count;

            if (self.RedDotNodeNeedShowSet.Contains(target) && redDotMonoView != null)
            {
                redDotMonoView.RefreshRedDotCount(self.RetainViewCount[target]);
            }

            bool isParentExist = self.ToParentDict.TryGetValue(target, out string parent);

            while (isParentExist)
            {
                var viewCount = 0;

                foreach (var childNode in self.RedDotNodeParentsDict[parent])
                {
                    viewCount += self.RetainViewCount[childNode];
                }

                self.RetainViewCount[parent] = viewCount;

                if (self.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                {
                    if (self.RedDotNodeNeedShowSet.Contains(parent))
                    {
                        redDotMonoView.RefreshRedDotCount(self.RetainViewCount[parent]);
                    }
                }

                isParentExist = self.ToParentDict.TryGetValue(parent, out parent);
            }
        }

        public static GameObject GetRedDotGameObjectFromPool(this UIRedDotComponent self)
        {
            return GameObjectPoolHelper.GetObjectFromPool("RedDot", true, 5);
        }

        public static void RecycleRedDotGameObject(this UIRedDotComponent self, GameObject go)
        {
            GameObjectPoolHelper.ReturnTransformToPool(go.transform);
        }
    }
}