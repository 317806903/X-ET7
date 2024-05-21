using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ShowFPS : MonoBehaviour
    {
        private Rect m_fps, m_dtime, m_pose;
        private GUIStyle m_styleRed = new GUIStyle();
        private GUIStyle m_styleGreen = new GUIStyle();
        private int m_exFPSShow;
        private string m_exShow;
        private string m_exShow2;
        public int ExFPSShow
        {
            get
            {
                return this.m_exFPSShow;
            }
            set
            {
                this.m_exFPSShow = value;
            }
        }
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
            m_fps = new Rect(50, 100, 100, 100);    // first line
            m_dtime = new Rect(300, 100, 100, 100); // first line
            m_pose = new Rect(50, 150, 300, 100);   // second line
            this.m_styleGreen.fontSize = 50;
            this.m_styleGreen.normal.textColor = Color.green;
            this.m_styleRed.fontSize = 50;
            this.m_styleRed.normal.textColor = Color.red;
        }

        void OnGUI()
        {
            if (ExFPSShow > 20)
            {
                GUI.Label(m_fps, "FPS: " + ExFPSShow, this.m_styleGreen);
            }
            else
            {
                GUI.Label(m_fps, "FPS: " + ExFPSShow, this.m_styleRed);
            }

            if (string.IsNullOrEmpty(ExShow) == false)
            {
                GUI.Label(m_dtime, ExShow, this.m_styleGreen);
            }
            if (string.IsNullOrEmpty(ExShow2) == false)
            {
                GUI.Label(m_pose, ExShow2, this.m_styleGreen);
            }
        }
    }
}
