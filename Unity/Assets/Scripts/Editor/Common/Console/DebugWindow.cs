using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XGame
{
    public abstract class DebugWindowBase
    {
        protected Rect _windowRect;
        protected bool _isInEditor;
        protected float _scale = 1.5f;

        private bool _controlSize;
        private Vector2 _startPos;
        private Vector2 _startSize;

        public void Init(Rect winRect, bool isInEditor)
        {
            this._windowRect = winRect;
            this._isInEditor = isInEditor;
        }

        public void Draw()
        {

            if (this._isInEditor)
            {
                OnDrawWindow(0);
            }
            else
            {
                GUI.matrix = Matrix4x4.Scale(new Vector3(_scale, _scale, 1f));
                _windowRect = GUI.Window(0, _windowRect, OnDrawWindow, "Debug");

                GUI.matrix = Matrix4x4.Scale(new Vector3(1, 1, 1f));

                var e = Event.current;
                var windowRealRect = new Rect(_windowRect.min, _windowRect.size * _scale);
                var dragArea =
                        new Rect(windowRealRect.max.x - 20, windowRealRect.max.y - 20, 40, 40);
                var cur = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

                switch (e.type)
                {
                    case EventType.MouseDown:
                        _controlSize = dragArea.Contains(cur);
                        _startPos = cur;
                        _startSize = _windowRect.size;
                        if (_controlSize)
                            e.Use();
                        break;
                    case EventType.MouseDrag:
                        if (_controlSize)
                        {
                            _windowRect.size = _startSize + (cur - _startPos) / _scale;
                            e.Use();
                        }

                        break;
                    case EventType.MouseUp:
                        if (_controlSize)
                            e.Use();
                        _controlSize = false;
                        break;
                }
            }
        }

        protected abstract void OnDrawWindow(int id);
    }

    public partial class DebugWindow : MonoBehaviour
    {
        private float _fps;
        private float _lastRefresh;
        private bool _beginDrag;
        private Vector2 _beginPos;
        private Vector2 _dragPos;
        private bool _isClick;
        private float _scale = 1.5f;

        private Vector2 minBtnPos;

        private Image _block;

        private DebugWindowBase[] _windows;

        private int _showIndex = 0;

        private Type[] _windowTypes = new[]
        {
            typeof (DebugWindowLog),
            typeof (DebugWindowTools),
            typeof (DebugWindowServerCMD)
        };

        private string[] _showNames = new[] { "X", "Log", "工具", "协议" };
        private void Awake()
        {
            Application.logMessageReceived += OnlogMessageReceived;
        }

        private void OnlogMessageReceived(string condition, string stacktrace, LogType type)
        {
            ConsoleLogs.Instance.Add(condition, stacktrace, type);
        }

        private void Start()
        {
            this._windows = new DebugWindowBase[this._windowTypes.Length];
            for (int i = 0; i < this._windowTypes.Length; i++)
            {
                Type windowType = this._windowTypes[i];
                this._windows[i] = Activator.CreateInstance(windowType) as DebugWindowBase;
                this._windows[i].Init(new Rect(20, 20, (Screen.width) / _scale - 40, (Screen.height) / _scale - 40), false);
            }

            this.minBtnPos = new Vector2(Screen.width / _scale - 25, 0) / _scale;

            var can = new GameObject("BlockCanvas").AddComponent<Canvas>();
            can.transform.parent = this.transform;
            can.worldCamera = GameObject.Find("/Init/GlobalRoot/UICamera").GetComponent<Camera>();
            can.renderMode = RenderMode.ScreenSpaceCamera;
            can.sortingOrder = 30000;

            can.gameObject.AddComponent<GraphicRaycaster>();

            this._block = new GameObject("Block").AddComponent<Image>();
            this._block.color = Color.clear;
            this._block.transform.SetParent(can.transform, false);
            this._block.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        }

        private void OnGUI()
        {
            GUI.matrix = Matrix4x4.Scale(new Vector3(_scale, _scale, 1f));
            this._block.gameObject.SetActive(this._showIndex != 0);
            if (this._showIndex != 0)
            {
                var win = this._windows[this._showIndex - 1];
                win.Draw();
                this.DrawTab();
            }
            else
            {
                var area = new Rect(this.minBtnPos, new Vector2(50, 20));
                GUI.Box(area, this._fps.ToString("f0"));
                Vector2 pos = Event.current.mousePosition;
                switch (Event.current.type)
                {
                    case EventType.MouseDown:
                        if (Event.current.button == 0 && area.Contains(pos))
                        {
                            this._beginDrag = true;
                            this._beginPos = pos;
                            this._dragPos = this.minBtnPos;
                            this._isClick = true;
                        }
                        break;
                    case EventType.MouseDrag:
                        if (this._beginDrag)
                        {
                            this.minBtnPos = this._dragPos + (pos - this._beginPos);
                            this._isClick = false;
                        }
                        break;
                    case EventType.MouseUp:
                        if (this._isClick)
                        {
                            this._showIndex = 1;
                            this._isClick = false;
                        }

                        this._beginDrag = false;
                        break;
                }
            }
        }

        private void DrawTab()
        {
            GUI.matrix = Matrix4x4.Scale(new Vector3(_scale, _scale, 1f));
            var half = 60 * (this._windowTypes.Length);
            this._showIndex = GUI.Toolbar(new Rect((Screen.width / _scale - half) / _scale, 0, 60 * this._windows.Length, 20),
                this._showIndex, this._showNames);
        }

        private void Update()
        {
            if (Time.time - this._lastRefresh > 1)
            {
                this._fps = 1 / Time.deltaTime;
                this._lastRefresh = Time.time;
            }

#if UNITY_EDITOR

            List<RaycastResult> m_RaycastResult = new List<RaycastResult>();
            if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftControl))
            {
                PointerEventData data = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
                data.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                UnityEngine.EventSystems.EventSystem.current.RaycastAll(data, m_RaycastResult);
                if (m_RaycastResult.Count > 0)
                {
                    UnityEditor.EditorGUIUtility.PingObject(m_RaycastResult[0].gameObject);

                }
            }
#endif
        }

    }
}