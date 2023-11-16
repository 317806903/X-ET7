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
        int designWidthOrg = 1920; //开发时分辨率宽
        int designHeightOrg = 1080; //开发时分辨率高
        private int lastWidth;
        private int lastHeight;

        // Start is called before the first frame update
        void Start()
        {
            this.lastWidth = Screen.width;
            this.lastHeight = Screen.height;
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
            float s1 = (float)designWidth / (float)designHeight;
            float s2 = (float)width / (float)height;
            if (s1 < s2)
            {
                designWidth = width;
                designHeight = (int)Mathf.FloorToInt(designWidth / s1);
            }
            else
            {
                designHeight = height;
                designWidth = (int)Mathf.FloorToInt(designHeight * s1);
            }

            if (rectTransform != null)
            {
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.localPosition = Vector3.zero;
                rectTransform.sizeDelta = new Vector2(designWidth, designHeight);
            }
        }

        public void Update()
        {
#if UNITY_EDITOR
            //editor模式下测试用
            Scaler();
#else
            if (this.lastWidth != Screen.width || this.lastHeight != Screen.height)
            {
                this.lastWidth = Screen.width;
                this.lastHeight = Screen.height;
                Scaler();
            }
#endif
        }
    }
}