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
        private Rect m_fps, m_dtime;
        private GUIStyle m_styleRed = new GUIStyle();
        private GUIStyle m_styleGreen = new GUIStyle();

        void Awake()
        {
        }

        void Start()
        {
            m_lastUpdateShowTime = Time.realtimeSinceStartup;
            m_fps = new Rect(50, 100, 100, 100);
            m_dtime = new Rect(50, 150, 100, 100);
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
            //GUI.Label(m_dtime, "间隔: " + m_frameDeltaTime, m_style);
        }
    }
}
