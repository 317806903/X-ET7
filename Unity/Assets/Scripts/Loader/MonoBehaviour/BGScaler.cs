using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class BGScaler: MonoBehaviour
    {
        public bool isShowBlack = false;
        public int designWidthOrg = 1920; //开发时分辨率宽
        public int designHeightOrg = 1080; //开发时分辨率高
        private int lastWidth;
        private int lastHeight;
        private float lastHeight2Width;

        // Start is called before the first frame update
        void Start()
        {
            this.lastWidth = Screen.width;
            this.lastHeight = Screen.height;
            this.lastHeight2Width = this.height2Width();
            Scaler();
        }

        //适配
        void Scaler()
        {
            RectTransform rectTransform = this.transform as RectTransform;
            if (rectTransform == null)
            {
                return;
            }
            Vector2 canvasSize = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.size;

            // int width = Screen.width;
            // int height = Screen.height;
            float width = canvasSize.x;
            float height = canvasSize.y;

            float designWidth = this.designWidthOrg;
            float designHeight = this.designHeightOrg;
            float s1 = (float)designWidth / designHeight;
            float s2 = (float)width / height;
            if (this.isShowBlack == false)
            {
                if (s1 < s2)
                {
                    designWidth = width;
                    designHeight = designWidth / s1;
                }
                else
                {
                    designHeight = height;
                    designWidth = designHeight * s1;
                }
            }
            else
            {
                if (s1 < s2)
                {
                    designHeight = height;
                    designWidth = designHeight * s1;
                }
                else
                {
                    designWidth = width;
                    designHeight = designWidth / s1;
                }
            }

            if (rectTransform != null)
            {
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.localPosition = Vector3.zero;
                rectTransform.localScale = Vector3.one;
                rectTransform.sizeDelta = new Vector2(designWidth, designHeight);
            }
        }

        public void Update()
        {
#if UNITY_EDITOR
            //if (this.lastHeight2Width != this.height2Width())
            {
                this.lastHeight2Width = this.height2Width();

                //editor模式下测试用
                Scaler();
            }
#else
            if (this.lastWidth != Screen.width || this.lastHeight != Screen.height)
            {
                this.lastWidth = Screen.width;
                this.lastHeight = Screen.height;
                Scaler();
            }
#endif
        }

#if UNITY_EDITOR
        public float height2Width()
        {
            var mouseOverWindow = UnityEditor.EditorWindow.mouseOverWindow;
            System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
            System.Type type = assembly.GetType("UnityEditor.PlayModeView");

            Vector2 size = (Vector2) type.GetMethod(
                "GetMainPlayModeViewTargetSize",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static
            ).Invoke(mouseOverWindow, null);
            return size.y / size.x;
        }
#else
        public float height2Width()
        {
            return Screen.height / Screen.width;
        }
#endif
    }
}