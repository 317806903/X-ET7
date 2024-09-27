using System;
using UnityEngine;
using UIGuide;

namespace ET.Client
{
    public enum GuidePriority
    {
        BattleGuide = 100,
        TutorialFirst = 200,
        BattleTowerEndGuide = 300,
    }
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
        public int priority;
        public UIGuidePathList _UIGuidePathList;
        public int nowIndex;

        public Action<Scene> finished;
        public Action<Scene, int> stepFinished;
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
        EnterGuideBattleTutorialFirst,
        EnterGuideBattlePVEFirst,
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