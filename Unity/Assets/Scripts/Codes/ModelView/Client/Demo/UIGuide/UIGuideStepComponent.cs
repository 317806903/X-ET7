using System;
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

        public GameObject canvasPathGo;
        public GameObject guidePathGo;

        public UIGuidePath curUIGuidePath;

        public Action finishedCallBack;
        public Action skipCallBack;
        public CurInNextType curInNextType;

        public Vector2 lastPos2D = Vector2.zero;
        public Vector3 lastPos3D;
        public Vector3 lastCanvasSize;

        public bool isGuiding = false;

        public int curClickOutsideCount = 0;
        public int ClickOutsideMaxCount = 8;
    }
}