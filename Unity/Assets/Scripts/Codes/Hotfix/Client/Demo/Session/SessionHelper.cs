using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    public static class SessionHelper
    {
        public static SessionComponent GetSessionCompent(Scene scene)
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
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            SessionComponent sessionComponent = clientScene.GetComponent<SessionComponent>();
            return sessionComponent;
        }

        public static bool ChkSessionExist(Scene scene)
        {
            if (scene == null)
            {
                return false;
            }
            SessionComponent sessionComponent = GetSessionCompent(scene);
            if (sessionComponent == null || sessionComponent.IsDisposed || sessionComponent.Session == null)
            {
                return false;
            }
            return sessionComponent.Session != null;
        }

        public static Session GetSession(Scene scene)
        {
            if (scene == null)
            {
                EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                return null;
            }
            SessionComponent sessionComponent = GetSessionCompent(scene);
            if (sessionComponent == null || sessionComponent.IsDisposed || sessionComponent.Session == null)
            {
                EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                return null;
            }
            return sessionComponent.Session;
        }
    }
}