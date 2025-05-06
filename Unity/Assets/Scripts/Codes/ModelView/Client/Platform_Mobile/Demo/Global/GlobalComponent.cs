using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class GlobalComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static GlobalComponent Instance;

        public Transform Global;
        public Transform Unit { get; set; }
        public Transform UIRoot { get; set; }
        public Camera MainCamera { get; set; }
        public Camera UICamera { get; set; }
        public Transform InputActionRoot { get; set; }

        public Transform ChkApplicationStatus{ get; set; }
        public Transform ClientManagerRoot{ get; set; }
        public Transform PoolRoot{ get; set; }
        public Transform ErrerLogManagerRoot{ get; set; }
        public Transform DebugRoot{ get; set; }

        public bool isUpdateFinished = false;
    }
}