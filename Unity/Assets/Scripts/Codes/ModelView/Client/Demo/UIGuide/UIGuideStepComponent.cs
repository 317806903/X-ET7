using System;
using System.Collections.Generic;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
    [ChildOf(typeof(UIGuideComponent))]
    public class UIGuideStepComponent: Entity, IAwake, IDestroy, IUpdate
    {

        public float diaphaneityBlack = 0.75f;
        public float diaphaneityTransparent = 0.05f;

        public Transform RootTrans;
        public Transform guideMaskTrans;
        public Transform maskWhenDown;

        public GameObject canvasPathGo;
        public GameObject guidePathGo;

        public UIGuidePath curUIGuidePath;

        public Action finishedCallBack;
        public Action skipCallBack;
        public CurInNextType curInNextType;

        public bool islastInit = false;
        public Vector3 lastGuideRectPos = Vector3.zero;
        public Vector3 lastGuideRectlossyScale = Vector3.zero;
        public Vector2 lastGuideRectSize = Vector2.zero;

        public Vector3 lastPos3D;
        public Vector2 lastCanvasSize;

        public bool isGuiding = false;

        public int curClickOutsideCount = 0;
        public int ClickOutsideMaxCount = 8;

        public Dictionary<GuideConditionStaticMethodType, bool> guideConditionStatus;
    }
}