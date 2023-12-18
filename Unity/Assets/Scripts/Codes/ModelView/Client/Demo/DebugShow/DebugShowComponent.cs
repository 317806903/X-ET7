using UnityEngine;

namespace ET.Client
{
    public class DebugShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static DebugShowComponent Instance;

        public int waitFrameUpdate = 3;
        public int curFrameUpdate = 0;

        public Transform Root;
        public ShowFPS showFPS;
        public EntityRef<PingComponent> _pingComponent;
        public PingComponent pingComponent
        {
            get
            {
                return this._pingComponent;
            }
            set
            {
                this._pingComponent = value;
            }
        }
    }
}