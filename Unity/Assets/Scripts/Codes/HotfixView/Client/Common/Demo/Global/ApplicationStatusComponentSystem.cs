using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (ApplicationStatusComponent))]
    public static class ApplicationStatusComponentSystem
    {
        [ObjectSystem]
        public class ApplicationStatusComponentAwakeSystem: AwakeSystem<ApplicationStatusComponent>
        {
            protected override void Awake(ApplicationStatusComponent self)
            {
                ApplicationStatusComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class ApplicationStatusComponentDestroySystem: DestroySystem<ApplicationStatusComponent>
        {
            protected override void Destroy(ApplicationStatusComponent self)
            {
                ApplicationStatusComponent.Instance = null;
                self.OnApplicationPauseListern.Clear();
                self.OnApplicationEscapeListern.Clear();
            }
        }

        [ObjectSystem]
        public class ApplicationStatusComponentUpdateSystem: UpdateSystem<ApplicationStatusComponent>
        {
            protected override void Update(ApplicationStatusComponent self)
            {
                self.ChkEscape();
            }
        }

        public static async ETTask Init(this ApplicationStatusComponent self, Transform chkApplicationPauseTrans)
        {
            self.simulateSwitchToBackground = chkApplicationPauseTrans.gameObject.GetComponent<SimulateSwitchToBackground>();
            self.simulateSwitchToBackground.OnApplicationPauseListern = self._OnApplicationPause;

            await ETTask.CompletedTask;
        }

        public static void _OnApplicationPause(this ApplicationStatusComponent self, bool isPause)
        {
            foreach (Action<bool> action in self.OnApplicationPauseListernWaitAdd)
            {
                if (self.OnApplicationPauseListern.Contains(action))
                {
                    continue;
                }
                self.OnApplicationPauseListern.Add(action);
            }
            self.OnApplicationPauseListernWaitAdd.Clear();

            foreach (Action<bool> action in self.OnApplicationPauseListernWaitRemove)
            {
                if (self.OnApplicationPauseListern.Contains(action))
                {
                    self.OnApplicationPauseListern.Remove(action);
                }
            }
            self.OnApplicationPauseListernWaitRemove.Clear();

            foreach (Action<bool> action in self.OnApplicationPauseListern)
            {
                try
                {
                    action?.Invoke(isPause);
                }
                catch (Exception e)
                {
                    Log.Error($"OnApplicationPause {isPause} {e}");
                }
            }

            try
            {
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeApplicationStatus(){isPause = isPause});
            }
            catch (Exception e)
            {
                Log.Error($"OnApplicationPause Publish {isPause} {e}");
            }
        }

        public static void _OnApplicationEscape(this ApplicationStatusComponent self)
        {
            foreach (Action action in self.OnApplicationEscapeListernWaitAdd)
            {
                if (self.OnApplicationEscapeListern.Contains(action))
                {
                    continue;
                }
                self.OnApplicationEscapeListern.Add(action);
            }
            self.OnApplicationEscapeListernWaitAdd.Clear();

            foreach (Action action in self.OnApplicationEscapeListernWaitRemove)
            {
                if (self.OnApplicationEscapeListern.Contains(action))
                {
                    self.OnApplicationEscapeListern.Remove(action);
                }
            }
            self.OnApplicationEscapeListernWaitRemove.Clear();

            foreach (Action action in self.OnApplicationEscapeListern)
            {
                try
                {
                    action?.Invoke();
                }
                catch (Exception e)
                {
                    Log.Error($"OnApplicationEscape {e}");
                }
            }
        }

        public static void ChkEscape(this ApplicationStatusComponent self)
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            if(keyboard.escapeKey.wasPressedThisFrame)
            {
                self._OnApplicationEscape();
            }
        }

        public static void AddApplicationPauseListern(this ApplicationStatusComponent self, Action<bool> applicationPauseCall)
        {
            if (self.OnApplicationPauseListern.Contains(applicationPauseCall))
            {
                return;
            }
            if (self.OnApplicationPauseListernWaitAdd.Contains(applicationPauseCall))
            {
                return;
            }
            self.OnApplicationPauseListernWaitAdd.Add(applicationPauseCall);
        }

        public static void RemoveApplicationPauseListern(this ApplicationStatusComponent self, Action<bool> applicationPauseCall)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }
            if (self.OnApplicationPauseListernWaitRemove.Contains(applicationPauseCall))
            {
                self.OnApplicationPauseListernWaitRemove.Remove(applicationPauseCall);
            }
        }

        public static void AddApplicationEscapeListern(this ApplicationStatusComponent self, Action applicationEscapeCall)
        {
            if (self.OnApplicationEscapeListern.Contains(applicationEscapeCall))
            {
                return;
            }
            if (self.OnApplicationEscapeListernWaitAdd.Contains(applicationEscapeCall))
            {
                return;
            }
            self.OnApplicationEscapeListernWaitAdd.Add(applicationEscapeCall);
        }

        public static void RemoveApplicationEscapeListern(this ApplicationStatusComponent self, Action applicationEscapeCall)
        {
            if (self.OnApplicationEscapeListernWaitRemove.Contains(applicationEscapeCall))
            {
                self.OnApplicationEscapeListernWaitRemove.Remove(applicationEscapeCall);
            }
        }

    }
}