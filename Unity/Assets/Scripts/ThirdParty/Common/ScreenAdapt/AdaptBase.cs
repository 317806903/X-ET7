using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenAdapt
{
    [ExecuteInEditMode]
    public class AdaptBase<T, T1>: AdaptBehaviour, IAdaptBase where T : new() where T1 : Component
    {
        [SerializeField]
        protected T LandscapeConfig;

        [SerializeField]
        protected T PortraitConfig;

        [SerializeField]
        protected T LandscapeConfigPad;

        [SerializeField]
        protected T PortraitConfigPad;

        public bool currentIsLandscape = false;
        public bool currentIsPad = false;
        protected T CurrentConfig
        {
            get
            {
                if (IsLandscape())
                {
                    if (IsPad())
                    {
                        return this.LandscapeConfigPad;
                    }
                    else
                    {
                        return this.LandscapeConfig;
                    }
                }
                else
                {
                    if (IsPad())
                    {
                        return this.PortraitConfigPad;
                    }
                    else
                    {
                        return this.PortraitConfig;
                    }
                }
            }
        }

        private T1 _mComponent;
        protected T1 mComponent
        {
            get
            {
                if (null == _mComponent)
                {
                    _mComponent = GetBehaviour<T1>();
                }

                return _mComponent;
            }
        }

        private Dictionary<System.Type, Component> ComponentDict = new Dictionary<System.Type, Component>();

        protected T2 GetBehaviour<T2>() where T2 : Component
        {
            System.Type T1Type = typeof (T2);
            if (!ComponentDict.ContainsKey(T1Type))
            {
                ComponentDict[T1Type] = GetComponent<T2>();
            }

            return ComponentDict[T1Type] as T2;
        }

        protected void ClearComponentDict()
        {
            ComponentDict.Clear();
        }

        protected override void Awake()
        {
            if (this.enabled == false)
            {
                return;
            }
            base.Awake();
            currentIsLandscape = IsLandscape();
            currentIsPad = IsPad();
            LoadConfig();
            //AdaptManager.Instance.Register(this);
        }

        void OnEnable()
        {
            if (GetComponent<T1>() == null)
            {
                Debug.LogError($"{typeof(T1)} component is required for this script to function properly.");
                // 这里可以添加更多的错误处理逻辑，比如禁用脚本等
                this.enabled = false; // 禁用脚本以防止进一步的错误
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            bool isLandscapeNew = IsLandscape();
            bool isPadNew = IsPad();
            if (isLandscapeNew != currentIsLandscape || isPadNew != this.currentIsPad)
            {
                currentIsLandscape = isLandscapeNew;
                currentIsPad = isPadNew;
                LoadConfig();
            }
#else
            bool isLandscapeNew = IsLandscape();;
            if (isLandscapeNew != currentIsLandscape)
            {
                currentIsLandscape = isLandscapeNew;
                LoadConfig();
            }
#endif
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            // if (null == AdaptManager.Instance)
            // {
            //     return;
            // }
            //
            // AdaptManager.Instance.Remove(this);
        }

#if UNITY_EDITOR
        public static (float height, float width) GetSceneSize()
        {
            var mouseOverWindow = UnityEditor.EditorWindow.mouseOverWindow;
            System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
            System.Type type = assembly.GetType("UnityEditor.PlayModeView");

            Vector2 size = (Vector2) type.GetMethod(
                "GetMainPlayModeViewTargetSize",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static
            ).Invoke(mouseOverWindow, null);
            return (size.y, size.x);
        }
#else
        public static (float height, float width) GetSceneSize()
        {
            return (Screen.height, Screen.width);
        }
#endif
        public static bool IsLandscape()
        {
            (float height, float width) = GetSceneSize();
            return height < width;
        }

        public static bool IsPad()
        {
            (float height, float width) = GetSceneSize();
            if (height > width)
            {
                (height, width) = (width, height);
            }
            return width / height < 1.7f;
        }

        public virtual void OnChange()
        {
            LoadConfig();
        }

        public virtual void OnLandscape()
        {
        }

        public virtual void OnPortrait()
        {
        }

        protected virtual void Reset()
        {
            SaveConfig();
        }

        public void LoadLandscapeConfig()
        {
            if (null == LandscapeConfig)
            {
                return;
            }

            this._LoadConfig(LandscapeConfig);
        }

        public void LoadPortraitConfig()
        {
            if (null == PortraitConfig)
            {
                return;
            }

            this._LoadConfig(PortraitConfig);
        }

        public void LoadLandscapeConfigPad()
        {
            if (null == LandscapeConfigPad)
            {
                return;
            }

            this._LoadConfig(LandscapeConfigPad);
        }

        public void LoadPortraitConfigPad()
        {
            if (null == PortraitConfigPad)
            {
                return;
            }

            this._LoadConfig(PortraitConfigPad);
        }

        public override void LoadConfig()
        {
            if (this.mComponent == null)
            {
                ClearComponentDict();
            }
            if (IsLandscape())
            {
                if (IsPad())
                {
                    this.LoadLandscapeConfigPad();
                }
                else
                {
                    this.LoadLandscapeConfig();
                }
            }
            else
            {
                if (IsPad())
                {
                    this.LoadPortraitConfigPad();
                }
                else
                {
                    this.LoadPortraitConfig();
                }
            }
        }

        public override void SaveConfig()
        {
            if (this.mComponent == null)
            {
                ClearComponentDict();
            }
            if (IsLandscape())
            {
                if (IsPad())
                {
                    this.SaveLandscapeConfigPad();
                }
                else
                {
                    this.SaveLandscapeConfig();
                }
            }
            else
            {
                if (IsPad())
                {
                    this.SavePortraitConfigPad();
                }
                else
                {
                    this.SavePortraitConfig();
                }
            }
        }

        public void SaveLandscapeConfig()
        {
            if (null == LandscapeConfig)
            {
                LandscapeConfig = new T();
            }

            this._SaveConfig(LandscapeConfig);
        }

        public void SavePortraitConfig()
        {
            if (null == PortraitConfig)
            {
                PortraitConfig = new T();
            }

            this._SaveConfig(PortraitConfig);
        }

        public void SaveLandscapeConfigPad()
        {
            if (null == LandscapeConfigPad)
            {
                LandscapeConfigPad = new T();
            }

            this._SaveConfig(LandscapeConfigPad);
        }

        public void SavePortraitConfigPad()
        {
            if (null == PortraitConfigPad)
            {
                PortraitConfigPad = new T();
            }

            this._SaveConfig(PortraitConfigPad);
        }

        protected virtual void _LoadConfig(T config)
        {
        }

        protected virtual void _SaveConfig(T config)
        {
        }
    }
}