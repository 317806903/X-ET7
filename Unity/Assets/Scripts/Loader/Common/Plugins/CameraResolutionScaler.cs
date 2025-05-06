using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    //[ExecuteInEditMode]
    public class CameraResolutionScaler : MonoBehaviour
    {
        public Camera camera;
        [Range(0.1F, 1.0F)] public float renderScale = 1.0F;
        public FilterMode filterMode = FilterMode.Bilinear;

        private Rect originalRect;
        private Rect scaledRect;

        private Rect cameraOriginalRect;
        private bool cameraSet = false;
        public static CameraResolutionScaler Instance;

        #region 目前每个场景相机是独立的，所以需要一组数据记录状态，并根据现有数据经行处理对应逻辑.

        public static bool UsedSetting;
        public static float RENDER_SCALE = 1.0f;

        public static bool IS_OPEN = true;
        public static float RENDERSCALE_DEFAULT = 1.0f;
        public static Vector2 RENDERSCALE_DEFAULT_RANGE = new Vector2(0.01f, 1f);

        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetToggle(IS_OPEN);
            SetRenderScale(0.5f);
        }

        public static void SetToggle(bool isOpen)
        {
            IS_OPEN = isOpen;
            Instance.enabled = isOpen;
        }

        public static void SetRenderScale(float v)
        {
            if (v > RENDERSCALE_DEFAULT_RANGE.y) v = RENDERSCALE_DEFAULT_RANGE.y;
            else if (v < RENDERSCALE_DEFAULT_RANGE.x) v = RENDERSCALE_DEFAULT_RANGE.x;
            RENDER_SCALE = v;
            Instance.renderScale = v;

            #region Bug #13884

            if (v == 1)
            {
                Instance.enabled = false;
            }
            else
            {
                if (IS_OPEN && !Instance.enabled)
                {
                    Instance.enabled = true;
                }
            }

            #endregion
        }


        private void Clear()
        {
            SetToggle(false);
            SetRenderScale(RENDERSCALE_DEFAULT);
            Revert();
        }

        protected void OnEnable()
        {
            ResetCamera();
        }

        protected void OnDisable()
        {
            Revert();
        }

        public void ResetCamera()
        {
            camera = GetComponent<Camera>();
            if (camera)
            {
                #region Feature #13231 避免干扰其他相机，redmine有详细说明.

                cameraOriginalRect = camera.rect;
                if (cameraOriginalRect.x == 0 && cameraOriginalRect.y == 0 && cameraOriginalRect.width == 1 &&
                    cameraOriginalRect.height == 1)
                {
                 //   camera.rect = new Rect(0.0001f, 0, 1, 1);
                    cameraSet = true;
                }

                #endregion
            }
        }

        public void Revert()
        {
            renderScale = 1.0f;
            if (camera != null)
            {
                if (cameraSet)
                {
                    camera.rect = cameraOriginalRect;
                }
                else
                {
                    camera.rect = originalRect;
                }
            }
        }

        protected void OnDestroy()
        {
            Revert();
        }


        void OnPreRender()
        {
            if (camera != null)
            {
                originalRect = camera.rect;
                scaledRect.Set(originalRect.x, originalRect.y, originalRect.width * renderScale,
                    originalRect.height * renderScale);
                camera.rect = scaledRect;
            }
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (camera != null)
            {
                camera.rect = originalRect;
                src.filterMode = filterMode;
                Graphics.Blit(src, dest);
            }
        }
    }
}
