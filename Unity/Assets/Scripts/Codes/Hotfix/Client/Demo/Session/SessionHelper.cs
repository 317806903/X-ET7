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

        public static Session GetSession(Scene scene)
        {
            return GetSessionCompent(scene).Session;
        }
    }
}