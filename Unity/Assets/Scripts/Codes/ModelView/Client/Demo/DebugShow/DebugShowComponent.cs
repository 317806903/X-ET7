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


        /// <summary>
        /// 上一次更新帧率的时间
        /// </summary>
        public float m_lastUpdateShowTime = 0f;
        /// <summary>
        /// 更新显示帧率的时间间隔
        /// </summary>
        public readonly float m_updateTime = 0.05f;
        /// <summary>
        /// 帧数
        /// </summary>
        public int m_frames = 0;
        /// <summary>
        /// 帧间间隔
        /// </summary>
        public float m_frameDeltaTime = 0;
        public int m_FPS = 0;

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

        public Vector3 arCameraPosition;
        public Vector3 arCameraEulerAngles;
    }
}