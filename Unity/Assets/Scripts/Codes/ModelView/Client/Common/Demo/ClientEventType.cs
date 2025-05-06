using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET
{
    namespace ClientEventType
    {
        public struct LoadingProgress
        {
            public float curProgress;
        }

        public struct OnPatchDownloadProgress
        {
            public int CurrentDownloadCount;

            public int TotalDownloadCount;

            public long CurrentDownloadSizeBytes;

            public long TotalDownloadSizeBytes;
        }

        public struct OnPatchDownlodFailed
        {
            public string FileName;

            public string Error;
        }

        public struct ShowBattleDragItem
        {
            public BattleDragItemType battleDragItemType;
            public string battleDragItemParam;
            public long moveTowerUnitId;
            public int countOnce;
            public string createActionIds;
            public Scene sceneIn;
            public System.Action<Scene> callBack;
        }

        public struct HideBattleDragItem
        {
        }

        public struct ShowBattleTowerHUD
        {
            public long playerId;
            public long towerUnitId;
            public string towerCfgId;
            public GameObject followObj;
        }

        public struct HideBattleTowerHUD
        {
            public bool isNotHideWhenWithUnitId;
        }

        public struct ShowBattleHomeHUD
        {
            public long homeUnitId;
            public string homeCfgId;
        }

        public struct HideBattleHomeHUD
        {
        }

        public struct ShowBattleMonsterCallHUD
        {
            public long monsterCallUnitId;
        }

        public struct HideBattleMonsterCallHUD
        {
        }

        public struct BattleTowerListShowOrHide
        {
            public bool isShow;
            public bool isLeft;
        }

        public struct ControllerGripPerform
        {
            public bool isLeft;
        }

        public struct ControllerGripCancel
        {
            public bool isLeft;
        }

        public struct LeftControllerMenuClick
        {
        }

        public struct BlockSkill
        {
        }
    }
}