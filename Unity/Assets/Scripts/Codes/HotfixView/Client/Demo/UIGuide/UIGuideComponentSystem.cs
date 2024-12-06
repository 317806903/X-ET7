using System;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using UnityEngine.UI;
using UIGuide;

namespace ET.Client
{
    [FriendOf(typeof (UIGuideComponent))]
    public static class UIGuideComponentSystem
    {
        [ObjectSystem]
        public class UIGuideComponentAwakeSystem: AwakeSystem<UIGuideComponent>
        {
            protected override void Awake(UIGuideComponent self)
            {
                UIGuideComponent.Instance = self;

                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class UIGuideComponentDestroySystem: DestroySystem<UIGuideComponent>
        {
            protected override void Destroy(UIGuideComponent self)
            {
                if (self.nowIndex >= 0 && self.nowIndex < self._UIGuidePathList.list.Count)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.AppsFlyerTutorialCompleted()
                    {
                        isTutorialCompleted = false,
                        tutorialId = self.guideFileName
                    });
                }
                if (UIGuideComponent.Instance == self)
                {
                    UIGuideComponent.Instance = null;
                }
                if (self.CurUIGuideComponent != null)
                {
                    self.CurUIGuideComponent.Dispose();
                    self.CurUIGuideComponent = null;
                }
                self._UIGuidePathList = null;
                if (self.RootTrans != null)
                {
                    UITextLocalizeComponent.Instance.RemoveUITextLocalizeView(self.RootTrans.gameObject);
                    UIImageLocalizeComponent.Instance.RemoveUIImageLocalizeView(self.RootTrans.gameObject);
                    GameObject.DestroyImmediate(self.RootTrans.gameObject);
                    self.RootTrans = null;
                }
            }
        }

        [ObjectSystem]
        public class UIGuideComponentUpdateSystem: UpdateSystem<UIGuideComponent>
        {
            protected override void Update(UIGuideComponent self)
            {
                if (self.RootTrans == null)
                {
                    return;
                }

            }
        }

        public static async ETTask Awake(this UIGuideComponent self)
        {
            await self.CreateUIGuidePrefab();
        }

        public static async ETTask CreateUIGuidePrefab(this UIGuideComponent self)
        {
            if (self.RootTrans != null)
            {
                return;
            }

            GameObject go = ResComponent.Instance.LoadAsset<GameObject>("UIGuidePanel");
            self.RootTrans = GameObject.Instantiate(go).transform;
            self.RootTrans.name = "UIGuidePanel";

            self.RootTrans.gameObject.SetActive(false);
            GameObject.DontDestroyOnLoad(self.RootTrans.gameObject);

            UITextLocalizeComponent.Instance.AddUITextLocalizeView(self.RootTrans.gameObject);
            UIImageLocalizeComponent.Instance.AddUIImageLocalizeView(self.RootTrans.gameObject);
        }

        public static async ETTask DoUIGuideByName(this UIGuideComponent self, string guideFileName, int priority, int startIndex, Action<Scene> finished = null, Action<Scene, int> stepFinished = null)
        {
            string filePath = $"UIGuideConfig_{guideFileName}";
            UIGuidePathList _UIGuidePathList = await ResComponent.Instance.LoadAssetAsync<UIGuidePathList>(filePath);
            if (_UIGuidePathList == null)
            {
                self.DestroySelf();
                return;
            }

            for (int i = 0; i < _UIGuidePathList.list.Count; i++)
            {
                UIGuidePath _UIGuidePath = _UIGuidePathList.list[i];
                _UIGuidePath.index = i;
            }

            await self.DoUIGuide(guideFileName, priority, _UIGuidePathList, startIndex, finished, stepFinished);
            await ETTask.CompletedTask;
        }

        public static async ETTask DoUIGuide(this UIGuideComponent self, string guideFileName, int priority, UIGuidePathList _UIGuidePathList, int startIndex, Action<Scene> finished = null, Action<Scene, int> stepFinished = null)
        {
            if (_UIGuidePathList == null || _UIGuidePathList.list.Count == 0)
            {
                self.DestroySelf();
                return;
            }

            while (self.RootTrans == null)
            {
                await self.CreateUIGuidePrefab();
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
            }

            self.finished = finished;
            self.stepFinished = stepFinished;

            self.guideFileName = guideFileName;
            self.priority = priority;
            self.nowIndex = startIndex;
            self._UIGuidePathList = _UIGuidePathList;
            await self.DoGuideStep();
            await ETTask.CompletedTask;
        }

        public static async ETTask StopUIGuide(this UIGuideComponent self)
        {
            self.DestroySelf();
            await ETTask.CompletedTask;
        }

        public static async ETTask DoGuideStep(this UIGuideComponent self)
        {
            UIGuidePathList _UIGuidePathList = self._UIGuidePathList;
            if (self.nowIndex < 0)
            {
                return;
            }

            if (self.nowIndex >= _UIGuidePathList.list.Count)
            {
                if (self.finished != null)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.AppsFlyerTutorialCompleted()
                    {
                        isTutorialCompleted = true,
                        tutorialId = self.guideFileName
                    });
                    self.finished(self.DomainScene());
                }
                self.DestroySelf();
                return;
            }


            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
            {
                eventName = $"TutorialStepEnded_{self.guideFileName}",
                timerKey = self.nowIndex.ToString(),
            });
            if (self.CurUIGuideComponent != null)
            {
                self.CurUIGuideComponent.Dispose();
            }
            UIGuideStepComponent curUIGuideStepComponent = self.AddChild<UIGuideStepComponent>();
            self.CurUIGuideComponent = curUIGuideStepComponent;
            await curUIGuideStepComponent.DoGuideStepOne(self.RootTrans, _UIGuidePathList.list[self.nowIndex], self.NextStepCallBack, self.SkipCallBack);
        }

        public static void DestroySelf(this UIGuideComponent self)
        {
            self.nowIndex = -1;
            if (self.CurUIGuideComponent != null)
            {
                self.CurUIGuideComponent.Dispose();
            }
            self.RootTrans.gameObject.SetActive(false);
        }

        public static void NextStepCallBack(this UIGuideComponent self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = $"TutorialStepEnded_{self.guideFileName}",
                properties = new()
                {
                    {"step_id_code", self.nowIndex},
                },
                timerKey = self.nowIndex.ToString(),
            });

            if (self.stepFinished != null)
            {
                self.stepFinished(self.DomainScene(), self.nowIndex);
            }
            self.nowIndex++;
            self.DoGuideStep().Coroutine();
        }

        public static void SkipCallBack(this UIGuideComponent self)
        {
            self.DestroySelf();
        }

    }
}