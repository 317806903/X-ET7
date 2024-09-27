using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(GlobalComponent))]
    public class MainQualitySettingComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static MainQualitySettingComponent Instance;

        public int minHeight = 720;
        public int maxHeight = 1080;
        public int qualitySettingSize = 3;
        public int[] qualityHeightList;
        public int[] qualityWidthList;
        //public int safeAreaOffset;
        public int orgScreenHeight;
        public int orgScreenWidth;
        public float orgRatio;

        [HideInInspector] public int quality_screen_width = 0;
        [HideInInspector] public int quality_screen_height = 0;
        public bool initQuality = false;
        [Range(0, 100)]
        public int chgLenScale = 0;

        public bool keepChgLenScale = false;

        public bool forceResetResoution = false;
        public int lastHeight;
        public float lastChgLenScale;

        public int waitFrameChk = 100;
        public int curFrameChk = 0;
    }
}