using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgARRoom))]
    public static class DlgARRoomSystem
    {
        public static void RegisterUIEvent(this DlgARRoom self)
        {
        }

        public static void ShowWindow(this DlgARRoom self, Entity contextData = null)
        {
            ARSessionComponent arSessionComponent = self.ClientScene().GetComponent<ARSessionComponent>();
            if (arSessionComponent == null)
            {
                arSessionComponent = self.ClientScene().AddComponent<ARSessionComponent>();
            }

            arSessionComponent.Init(() => { self.OnClose(); }, () => { self.OnFinished(); });
        }

        public static async ETTask OnClose(this DlgARRoom self)
        {
            self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ARRoom);
            await self.ClientScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_GameMode);
        }

        public static async ETTask OnFinished(this DlgARRoom self)
        {
        }
    }
}