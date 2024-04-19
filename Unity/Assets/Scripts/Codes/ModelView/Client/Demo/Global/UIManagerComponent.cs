using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(GlobalComponent))]
    public class UIManagerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static UIManagerComponent Instance;

        public int designWidth = 1920;
        public int designHeight = 1080;

        public Camera UICamera { get; set; }
        public Transform UIRoot { get; set; }

        public bool isInitUI;
        public bool isLandscape;
        public Transform WorldHubRoot{ get; set; }
        public Transform NormalRoot{ get; set; }
        public Transform PopUpRoot{ get; set; }
        public Transform FixedRoot{ get; set; }
        public Transform NoticeRoot{ get; set; }
        public Transform LoadingRoot{ get; set; }
        public Transform HighestFixedRoot{ get; set; }
        public Transform HighestNoticeRoot{ get; set; }

        public HashSet<Transform> UIRootRotationTranList = new();
    }
}