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

        private bool currentIsLandscape = false;
        protected T CurrentConfig
        {
            get
            {
                return IsLandscape()? LandscapeConfig : PortraitConfig;
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
            base.Awake();
            currentIsLandscape = IsLandscape();
            LoadConfig();
            //AdaptManager.Instance.Register(this);
        }

        private void Update()
        {
            bool newState = IsLandscape();
            if (newState != currentIsLandscape)
            {
                currentIsLandscape = newState;
                LoadConfig();
            }
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
        public static bool IsLandscape()
        {
            var mouseOverWindow = UnityEditor.EditorWindow.mouseOverWindow;
            System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
            System.Type type = assembly.GetType("UnityEditor.PlayModeView");

            Vector2 size = (Vector2) type.GetMethod(
                "GetMainPlayModeViewTargetSize",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static
            ).Invoke(mouseOverWindow, null);
            return size.y < size.x;
        }
#else
        public static bool IsLandscape()
        {
            return Screen.height < Screen.width;
        }
#endif
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

        public override void LoadConfig()
        {
            if (this.mComponent == null)
            {
                ClearComponentDict();
            }
            if (IsLandscape())
            {
                this.LoadLandscapeConfig();
            }
            else
            {
                this.LoadPortraitConfig();
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
                this.SaveLandscapeConfig();
            }
            else
            {
                this.SavePortraitConfig();
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

        protected virtual void _LoadConfig(T config)
        {
        }

        protected virtual void _SaveConfig(T config)
        {
        }
    }
}