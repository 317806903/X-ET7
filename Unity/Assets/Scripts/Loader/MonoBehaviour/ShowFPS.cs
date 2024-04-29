using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ShowFPS : MonoBehaviour
    {
        /// <summary>
        /// 上一次更新帧率的时间
        /// </summary>
        private float m_lastUpdateShowTime = 0f;
        /// <summary>
        /// 更新显示帧率的时间间隔
        /// </summary>
        private readonly float m_updateTime = 0.05f;
        /// <summary>
        /// 帧数
        /// </summary>
        private int m_frames = 0;
        /// <summary>
        /// 帧间间隔
        /// </summary>
        private float m_frameDeltaTime = 0;
        private float m_FPS = 0;
        private Rect m_fps, m_dtime, m_pose;
        private GUIStyle m_styleRed = new GUIStyle();
        private GUIStyle m_styleGreen = new GUIStyle();
        private string m_exShow;
        private string m_exShow2;
        public string ExShow
        {
            get
            {
                return this.m_exShow;
            }
            set
            {
                this.m_exShow = value;
            }
        }
        public string ExShow2
        {
            get
            {
                return this.m_exShow2;
            }
            set
            {
                this.m_exShow2 = value;
            }
        }

        void Awake()
        {
        }

        void Start()
        {
            m_lastUpdateShowTime = Time.realtimeSinceStartup;
            m_fps = new Rect(50, 100, 100, 100);    // first line
            m_dtime = new Rect(300, 100, 100, 100); // first line 
            m_pose = new Rect(50, 150, 300, 100);   // second line
            this.m_styleGreen.fontSize = 50;
            this.m_styleGreen.normal.textColor = Color.green;
            this.m_styleRed.fontSize = 50;
            this.m_styleRed.normal.textColor = Color.red;
        }

        void Update()
        {
            m_frames++;
            if (Time.realtimeSinceStartup - m_lastUpdateShowTime >= m_updateTime)
            {
                m_FPS = (int)(m_frames / (Time.realtimeSinceStartup - m_lastUpdateShowTime));
                m_frameDeltaTime = (Time.realtimeSinceStartup - m_lastUpdateShowTime) / m_frames;
                m_frames = 0;
                m_lastUpdateShowTime = Time.realtimeSinceStartup;
                //Debug.Log("FPS: " + m_FPS + "，间隔: " + m_FrameDeltaTime);
            }
        }

        void OnGUI()
        {
            if (m_FPS > 20)
            {
                GUI.Label(m_fps, "FPS: " + m_FPS, this.m_styleGreen);
            }
            else
            {
                GUI.Label(m_fps, "FPS: " + m_FPS, this.m_styleRed);
            }

            if (string.IsNullOrEmpty(m_exShow) == false)
            {
                GUI.Label(m_dtime, m_exShow, this.m_styleGreen);
            }
            if (string.IsNullOrEmpty(m_exShow2) == false)
            {
                GUI.Label(m_pose, m_exShow2, this.m_styleGreen);
            }
        }
    }
}
