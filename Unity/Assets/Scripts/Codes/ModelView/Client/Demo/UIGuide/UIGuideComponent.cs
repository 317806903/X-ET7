using System;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class UIGuideComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static UIGuideComponent Instance;

        public EntityRef<UIGuideStepComponent> _curUIGuideComponent;
        public UIGuideStepComponent CurUIGuideComponent
        {
            get
            {
                return this._curUIGuideComponent;
            }
            set
            {
                this._curUIGuideComponent = value;
            }
        }

        public Transform RootTrans;

        public string guideFileName;
        public UIGuidePathList _UIGuidePathList;
        public int nowIndex;

        public Action finished;
    }

    public enum GuideConditionStaticMethodType
    {
        None,
        //判断塔 是否被放置
        ChkTowerPutSuccess,
        ChkTowerScaleSuccess,
        ChkTowerReclaimSuccess,
        ChkTowerUpgradeSuccess,
        ChkTowerMoveSuccess,
        ChkIsNotShowStory,
        ChkIsNotShowVideo,
        ChkWaitTime,
        ChkARMeshShow,
    }

    public enum GuideExecuteStaticMethodType
    {
        None,
        //进入故事展示
        ShowStory,
        ShowVideo,
        //进入新手关卡
        EnterGuideBattle,
        ShowPointTower,
        HidePointTower,
        HideTowerInfo,
        ShowBattleTowerReady,
        ShowBattleTowerQuit,
        ShowScanQuit,
        ShowScanVideo,
        BackToGameModeAR,
    }
}