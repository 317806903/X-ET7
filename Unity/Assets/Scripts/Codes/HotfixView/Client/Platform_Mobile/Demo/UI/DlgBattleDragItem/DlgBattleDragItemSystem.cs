﻿using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleDragItemFrameTimer)]
    public class DlgBattleDragItemTimer: ATimer<DlgBattleDragItem>
    {
        protected override void Run(DlgBattleDragItem self)
        {
            try
            {
                if (self.IsDisposed)
                {
                    TimerComponent.Instance?.Remove(ref self.Timer);
                    return;
                }
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
                TimerComponent.Instance?.Remove(ref self.Timer);
            }
        }
    }

    [FriendOf(typeof (DlgBattleDragItem))]
    public static class DlgBattleDragItemSystem
    {
        public static void RegisterUIEvent(this DlgBattleDragItem self)
        {
        }

        public static async ETTask ShowWindow(this DlgBattleDragItem self, ShowWindowData contextData = null)
        {
            DlgBattleDragItem_ShowWindowData showWindowData = contextData as DlgBattleDragItem_ShowWindowData;
            if (showWindowData == null)
            {
                return;
            }
            self.beginDragTime = TimeHelper.ClientNow();
            self.canPutTime = (long)(0.3f * 1000);
            self.battleDragItemType = showWindowData.battleDragItemType;
            self.battleDragItemParam = showWindowData.battleDragItemParam;
            if (self.battleDragItemType == BattleDragItemType.HeadQuarter)
            {
                self.battleDragItemParam = "Unit_HeadQuarter";
                NavMeshRendererComponent.Instance.SetNavMesh(null);
            }
            else if (self.battleDragItemType == BattleDragItemType.MonsterCall)
            {
                self.battleDragItemParam = "Unit_MonsterCall";
            }
            self.moveTowerUnitId = showWindowData.moveTowerUnitId;
            self.countOnce = showWindowData.countOnce;
            self.createActionIds = showWindowData.createActionIds;
            self.callBack = showWindowData.callBack;
            self.sceneIn = showWindowData.sceneIn;

            self.lastRayPos = float3.zero;
            self.lastDragRectifyPos = float3.zero;

            self.isConfirming = false;
            self.isClickUGUI = false;
            self.isDragging = false;
            self.isCliffy = false;
            self.isRaycast = false;
            self.pathfindingSuccess = true;
            self.hasNavMeshFromHeadQuarter = true;
            self.tryNum = 50;
            self.tryDis = 0.2f;
            self._YOffset = 0.02f;
            self._CliffyYOffset = 0.5f;

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            self.tryDis *= clientResScale;
            self._YOffset *= clientResScale;
            self._CliffyYOffset *= clientResScale;
            self.PointDiffs = new List<float3>(){
                new float3(0.5f, 0, 0) * clientResScale,
                new float3(-0.5f, 0, 0) * clientResScale,
                new float3(0, 0, 0.5f) * clientResScale,
                new float3(0, 0, -0.5f) * clientResScale,
            };

            self._groundLayerMask = LayerMask.GetMask("Map");
            TimerComponent.Instance?.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleDragItemFrameTimer, self);

            self.View.E_TipNodeImage.SetVisible(false);
            self.View.EG_ConfirmRootRectTransform.SetVisible(false);

            //UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
        }

        public static void InitPrefab(this DlgBattleDragItem self)
        {
            switch (self.battleDragItemType)
            {
                case BattleDragItemType.PKTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMonster:
                    self.OnSelectMonster(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMoveTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMovePlayer:
                    self.OnSelectPlayer(self.battleDragItemParam);
                    break;
                case BattleDragItemType.HeadQuarter:
                    self.OnSelectHeadQuarter(self.battleDragItemParam);
                    break;
                case BattleDragItemType.MonsterCall:
                    self.OnSelectMonsterCall(self.battleDragItemParam);
                    break;
                case BattleDragItemType.Tower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.MoveTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                default:
                    self.Close();
                    break;
            }
        }

        public static void LookAtMainCamera(this DlgBattleDragItem self)
        {
            Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
            self.currentPlaceObj.transform.LookAt(mainCamera.transform);
            var forward = self.currentPlaceObj.transform.forward;
            self.currentPlaceObj.transform.forward = new Vector3(forward.x, 0, forward.z);
        }

        public static void Update(this DlgBattleDragItem self)
        {
            NavMeshRendererComponent.Instance.ShowMesh(false);
            if (self.currentPlaceObj == null)
            {
                self.InitPrefab();
                return;
            }

            if (self.isConfirming)
            {
                return;
            }

            if (self.battleDragItemType == BattleDragItemType.Tower)
            {
                if (self.isDragging)
                {
                    ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), ARSessionComponent.ArMeshVisibility.Visible);
                }
            }
            else if (self.battleDragItemType == BattleDragItemType.MonsterCall || self.battleDragItemType == BattleDragItemType.HeadQuarter)
            {
                ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), ARSessionComponent.ArMeshVisibility.Visible);
                NavMeshRendererComponent.Instance.ShowMesh(true);
            }
            else
            {
                ARSessionHelper.ShowARMesh(self.DomainScene(), ARSessionComponent.ArMeshVisibility.Visible);
            }

            if (self.CheckUserInput())
            {
                self.isClickUGUI = ET.UGUIHelper.IsClickUGUI();
                self.isDragging = true;
                self.MoveCurrentPlaceObj();

                (bool canPut, bool isLimitRule, bool isObstacle) = self.ChkCanPut(self.rayHitPos);
                if (!canPut && !isLimitRule && self.IsNeedChkCanPutRepeat(self.rayHitPos))
                {
                    canPut = self.ChkCanPutWhenRepeat(self.rayHitPos, false);
                }

                if (canPut && (self.battleDragItemType == BattleDragItemType.MonsterCall || isObstacle))
                {
                    if (self.pathfindingSuccess == false)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPut_IsNotReachHome");
                        self.ShowPutTipMsg(tipMsg);
                        canPut = false;
                    } else if (self.ChkIsNearHeadQuarter(GlobalSettingCfgCategory.Instance.MinDisWhenMonsterCall2HeadQuarter) && self.battleDragItemType == BattleDragItemType.MonsterCall)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMonsterCall_IsNearHeadQuarter");
                        self.ShowPutTipMsg(tipMsg);
                        canPut = false;
                    }
                }

                if (canPut && self.battleDragItemType == BattleDragItemType.HeadQuarter)
                {
                    if (!self.hasNavMeshFromHeadQuarter)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutHome_RecahableAreaSmall");
                        self.ShowPutTipMsg(tipMsg);
                        canPut = false;
                    }
                }

                if (canPut)
                {
                    self.View.E_TipNodeImage.SetVisible(false);
                    self.ChgCurrentPlaceObj(self.beginDragTime + self.canPutTime <= TimeHelper.ClientNow());
                }
                else
                {
                    self.ChgCurrentPlaceObj(false);
                }

                // Start async check now, after doing possible adjustments.
                self.DrawReachableNavmesh().Coroutine();
                self.DrawMonsterCall2HeadQuarter(false).Coroutine();
                self.TryMoveUnitAndDrawAllMonsterCall2HeadQuarter(false).Coroutine();
            }
            else if (self.isDragging)
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
                if (self.beginDragTime + self.canPutTime > TimeHelper.ClientNow())
                {
                    self.ChgCurrentPlaceObj(false);
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_DragItem_TooFast");
                    UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return;
                }

                self.isClickUGUI = ET.UGUIHelper.IsClickUGUI(true);
                if (self.isClickUGUI == false)
                {
                    (bool canPut, bool isLimitRule, bool isObstacle) = self.ChkCanPut(self.rayHitPos);
                    if (canPut == false && isLimitRule == false)
                    {
                        canPut = self.ChkCanPutWhenRepeat(self.rayHitPos, true);
                    }
                    if (canPut)
                    {
                        if (self.battleDragItemType != BattleDragItemType.MonsterCall && !isObstacle)
                        {
                            self.View.E_TipNodeImage.SetVisible(false);
                            self.ChgCurrentPlaceObj(true);
                        }
                    }
                    else
                    {
                        self.ChgCurrentPlaceObj(false);
                    }
                    if (self.isRaycast == false)
                    {
                        //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                        //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.Close();
                        return;
                    }
                    if (self.isCliffy)
                    {
                        //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                        //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.Close();
                        return;
                    }

                    var position = self.currentPlaceObj.transform.position;
                    var forward = self.currentPlaceObj.transform.forward;
                    if (self.battleDragItemType == BattleDragItemType.PKTower)
                    {
                        self.DoPutPKTower(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMonster)
                    {
                        self.DoPutPKMonster(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMoveTower)
                    {
                        self.DoPutPKMoveTower(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMovePlayer)
                    {
                        self.DoPutPKMovePlayer(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.HeadQuarter)
                    {
                        self.DoPutHome(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.MonsterCall)
                    {
                        self.DoPutMonsterCall(position, forward).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.Tower)
                    {
                        self.DoPutOwnTower(position, forward, self.redrawPathWhenClose).Coroutine();
                        self.redrawPathWhenClose = false;
                    }
                    else if (self.battleDragItemType == BattleDragItemType.MoveTower)
                    {
                        self.DoPutMoveTower(position, forward, self.redrawPathWhenClose).Coroutine();
                        self.redrawPathWhenClose = false;
                    }
                }
                self.Close();
            }
            else if (self.currentPlaceObj != null)
            {
                self.Close();
            }
        }

        public static void OnSelectHeadQuarter(this DlgBattleDragItem self, string headQuarterUnitCfgId)
        {
            self.currentPlaceObj = new GameObject("currentPlaceObj");
            self.currentPlaceObj.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            self.currentPlaceObj.transform.up = Vector3.up;

            string unitCfgId = headQuarterUnitCfgId;
            string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);
            go.transform.SetParent(self.currentPlaceObj.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * resScale;
            go.transform.localEulerAngles = Vector3.zero;
            self.currentPlaceObj.gameObject.SetActive(false);

        }

        public static void OnSelectMonsterCall(this DlgBattleDragItem self, string monsterCallUnitCfgId)
        {
            self.currentPlaceObj = new GameObject("currentPlaceObj");
            self.currentPlaceObj.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            self.currentPlaceObj.transform.up = Vector3.up;

            string unitCfgId = monsterCallUnitCfgId;
            string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);
            go.transform.SetParent(self.currentPlaceObj.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * resScale;
            go.transform.localEulerAngles = Vector3.zero;

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectTower(this DlgBattleDragItem self, string towerCfgId)
        {
            self.currentPlaceObj = new GameObject("currentPlaceObj");
            self.currentPlaceObj.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            self.currentPlaceObj.transform.up = Vector3.up;

            Unit towerUnit = null;
            if (self.moveTowerUnitId != 0)
            {
                towerUnit = UnitHelper.GetUnit(self.DomainScene(), self.moveTowerUnitId);
            }

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
            bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
            if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
            {
                string unitCfgId = towerToUnitCfgId.CfgId;
                float3 releativePos = float3.zero;
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);

                float3 forward = new float3(0, 0, 1);

                string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
                float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

                GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);

                GameObject goTmp = go;
                goTmp.transform.SetParent(self.currentPlaceObj.transform);
                goTmp.transform.localPosition = releativePos;
                goTmp.transform.localScale = Vector3.one * resScale;
                goTmp.transform.localEulerAngles = Vector3.zero;
                goTmp.transform.forward = forward;

                if (isAttackTower || isTrap || isCollider)
                {
                    ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerShow");
                    string resEffectName = resEffectCfg.ResName;
                    GameObject resEffectGoTmp = GameObjectPoolHelper.GetObjectFromPool(resEffectName, true, 1);

                    resEffectGoTmp.transform.SetParent(self.currentPlaceObj.transform);
                    resEffectGoTmp.transform.localPosition = releativePos;
                    resEffectGoTmp.transform.localScale = Vector3.one * resScale;
                    resEffectGoTmp.transform.localEulerAngles = Vector3.zero;

                    Transform attackAreaTran = resEffectGoTmp.transform.Find("AttackArea");
                    Transform defaultShowTran = resEffectGoTmp.transform.Find("DefaultShow");
                    for (int i1 = 0; i1 < resEffectGoTmp.transform.childCount; i1++)
                    {
                        Transform child = resEffectGoTmp.transform.GetChild(i1);
                        if (child == attackAreaTran || child == defaultShowTran)
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }

                    float skillDis;
                    if (towerUnit != null)
                    {
                        skillDis = UnitHelper.GetMaxSkillDis(towerUnit);
                    }
                    else
                    {
                        skillDis = ET.Ability.UnitHelper.GetMaxSkillDis(self.DomainScene(), unitCfg, ET.AbilityConfig.SkillSlotType.NormalAttack);
                    }
                    attackAreaTran.localScale = Vector3.one * skillDis * 2 / resScale;

                    long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                    GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
                    float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
                    Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
                    ParticleSystem[] psList = attackAreaTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }
                    psList = defaultShowTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }

                    UnityEngine.Rendering.Universal.DecalProjector[] projectorList =
                            attackAreaTran.gameObject.GetComponentsInChildren<UnityEngine.Rendering.Universal.DecalProjector>(true);
                    foreach (UnityEngine.Rendering.Universal.DecalProjector projector in projectorList)
                    {
                        Material projectorMaterial = new Material(projector.material);
                        float alpha = projectorMaterial.GetColor("_Color").a;
                        color.a = alpha;
                        projectorMaterial.SetColor("_Color", color);
                        projector.material = projectorMaterial;
                    }
                    projectorList = defaultShowTran.gameObject.GetComponentsInChildren<UnityEngine.Rendering.Universal.DecalProjector>(true);
                    foreach (UnityEngine.Rendering.Universal.DecalProjector projector in projectorList)
                    {
                        Material projectorMaterial = new Material(projector.material);
                        float alpha = projectorMaterial.GetColor("_Color").a;
                        color.a = alpha;
                        projectorMaterial.SetColor("_Color", color);
                        projector.material = projectorMaterial;
                    }
                }
            }
            else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
            {
                for (int i = 0; i < towerToMonsterCfgId.CfgId.Count; i++)
                {
                    string monsterCfgId = towerToMonsterCfgId.CfgId[i];
                    if (TowerDefense_MonsterCfgCategory.Instance.Contain(monsterCfgId) == false)
                    {
                        Log.Error($"CreateTower towerCfgId[{towerCfgId}] isCallMonster, but TowerDefense_MonsterCfgCategory.Instance.Contain({monsterCfgId}) == false");
                        continue;
                    }
                    TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                    string unitCfgId = monsterCfg.UnitId;
                    float3 releativePos = float3.zero;
                    if (towerToMonsterCfgId.RelativePosition.Count > i)
                    {
                        releativePos = new float3(towerToMonsterCfgId.RelativePosition[i].X, towerToMonsterCfgId.RelativePosition[i].Y, towerToMonsterCfgId.RelativePosition[i].Z);
                    }
                    UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);

                    int unitNum = 1;
                    if (towerToMonsterCfgId.Num.Count > i)
                    {
                        unitNum = towerToMonsterCfgId.Num[i];
                    }
                    int unitLevel = 1;
                    if (towerToMonsterCfgId.Level.Count > i)
                    {
                        unitLevel = towerToMonsterCfgId.Level[i];
                    }
                    float3 forward = new float3(0, 0, 1);

                    string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
                    float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

                    for (int j = 0; j < unitNum; j++)
                    {
                        GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);

                        GameObject goTmp = go;
                        goTmp.transform.SetParent(self.currentPlaceObj.transform);
                        goTmp.transform.localPosition = releativePos;
                        goTmp.transform.localScale = Vector3.one * resScale;
                        goTmp.transform.localEulerAngles = Vector3.zero;
                        goTmp.transform.forward = forward;
                    }
                }
            }

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectMonster(this DlgBattleDragItem self, string monsterCfgId)
        {
            if (ET.ItemHelper.ChkIsTower(monsterCfgId))
            {
                self.OnSelectTower(monsterCfgId);
                return;
            }
            self.currentPlaceObj = new GameObject("currentPlaceObj");
            self.currentPlaceObj.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            self.currentPlaceObj.transform.up = Vector3.up;

            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(monsterCfg.UnitId);

            string unitCfgId = unitCfg.Id;

            string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);
            go.transform.SetParent(self.currentPlaceObj.transform);
            go.transform.localPosition = Vector3.zero;

            go.transform.localScale = Vector3.one * resScale;
            go.transform.localEulerAngles = Vector3.zero;

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectPlayer(this DlgBattleDragItem self, string monsterCfgId)
        {
            self.currentPlaceObj = new GameObject("currentPlaceObj");
            self.currentPlaceObj.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            self.currentPlaceObj.transform.up = Vector3.up;

            string unitCfgId = "Unit_PlayerPK";

            string resName = ET.Ability.UnitHelper.GetResName(unitCfgId);
            float resScale = ET.Ability.UnitHelper.GetGameResScale(self.DomainScene(), unitCfgId);

            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName, true, 1);
            go.transform.SetParent(self.currentPlaceObj.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * resScale;
            go.transform.localEulerAngles = Vector3.zero;
            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectMoveTower(this DlgBattleDragItem self, string moveTowerUnitId)
        {
        }

        public static void ChgCurrentPlaceObj(this DlgBattleDragItem self, bool canPut)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }

            Color colorNew;
            if (canPut)
            {
                colorNew = Color.white;
            }
            else
            {
                colorNew = Color.red;
            }

            MeshRenderer[] rendererList = self.currentPlaceObj.gameObject.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in rendererList)
            {
                foreach (var material in renderer.materials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    if (material.HasColor("_Color"))
                    {
                        Color color = material.color;
                        material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                    }
                }
            }
            SkinnedMeshRenderer[] rendererList2 = self.currentPlaceObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (SkinnedMeshRenderer renderer in rendererList2)
            {
                foreach (var material in renderer.materials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    if (material.HasColor("_Color"))
                    {
                        Color color = material.color;
                        material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                    }
                }
            }
        }

        public static bool IsNeedChkCanPutRepeat(this DlgBattleDragItem self, float3 rayHitPos)
        {
            if (self.recordLastRayPos.Equals(float3.zero))
            {
                self.recordLastRayPos = rayHitPos;
                self.recordLastChkPutRepeatTime = 0;
                return false;
            }

            float disNear = 0.1f;
            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            disNear *= clientResScale;
            if (math.abs(self.recordLastRayPos.x - rayHitPos.x) < disNear
                && math.abs(self.recordLastRayPos.y - rayHitPos.y) < disNear
                && math.abs(self.recordLastRayPos.z - rayHitPos.z) < disNear)
            {
                self.recordLastRayPos = rayHitPos;
                if (self.recordLastChkPutRepeatTime == 0)
                {
                    self.recordLastChkPutRepeatTime = TimeHelper.ClientNow() + 200;
                    return false;
                }
                if (self.recordLastChkPutRepeatTime < TimeHelper.ClientNow())
                {
                    return true;
                }
                return false;
            }
            self.recordLastRayPos = rayHitPos;
            self.recordLastChkPutRepeatTime = 0;
            return false;
        }

        public static bool ChkCanPutWhenRepeat(this DlgBattleDragItem self, Vector3 position, bool isForce)
        {
            if (isForce)
            {

            }
            else
            {
                if (self.lastRayPos.Equals(float3.zero) == false)
                {
                    float dis = 0.1f;
                    float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                    dis *= clientResScale;
                    if (math.abs(self.lastRayPos.x - position.x) < dis
                        && math.abs(self.lastRayPos.y - position.y) < dis
                        && math.abs(self.lastRayPos.z - position.z) < dis)
                    {
                        if (self.lastDragRectifyPos.Equals(float3.zero))
                        {
                            return false;
                        }
                        self.currentPlaceObj.transform.position = self.lastDragRectifyPos;
                        self.LookAtMainCamera();
                        return true;
                    }
                }
            }

            self.lastRayPos = position;
            int tryNum = self.tryNum;
            float tryDis = self.tryDis;
            bool bRet = false;
            for (int i = 0; i < tryNum; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Vector3 newPos;
                    Vector3 positionNew1_x = position + new Vector3(i * tryDis, 0, j * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew1_x);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }

                    Vector3 positionNew1_z = position + new Vector3(j * tryDis, 0, i * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew1_z);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }

                    Vector3 positionNew2_x = position + new Vector3(-i * tryDis, 0, j * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew2_x);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }
                    Vector3 positionNew2_z = position + new Vector3(-j * tryDis, 0, i * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew2_z);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }

                    Vector3 positionNew3_x = position + new Vector3(-i * tryDis, 0, -j * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew3_x);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }
                    Vector3 positionNew3_z = position + new Vector3(-j * tryDis, 0, -i * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew3_z);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }

                    Vector3 positionNew4_x = position + new Vector3(i * tryDis, 0, -j * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew4_x);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }
                    Vector3 positionNew4_z = position + new Vector3(j * tryDis, 0, -i * tryDis);
                    (bRet, newPos) = self.ChkCanPutWhenRepeatOnce(positionNew4_z);
                    if (bRet)
                    {
                        self.currentPlaceObj.transform.position = newPos;
                        self.LookAtMainCamera();
                        self.lastDragRectifyPos = newPos;
                        return true;
                    }
                }
            }

            self.lastDragRectifyPos = float3.zero;
            return false;
        }

        public static bool CheckCliffy(this DlgBattleDragItem self, Vector3 point)
        {
            foreach (var pointDiff in self.PointDiffs)
            {
                (var ret, var hit) = self.GetHitPointInfo(point + (Vector3)pointDiff);
                if (ret)
                {
                    if (math.abs(hit.point.y - point.y) > self._CliffyYOffset)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static (bool, Vector3) ChkCanPutWhenRepeatOnce(this DlgBattleDragItem self, Vector3 position)
        {
            Vector3 point = Vector3.zero;
            self.isRaycast = false;
            self.isCliffy = false;

            bool bRet = false;
            RaycastHit raycastHit;
            (bRet, raycastHit) = self.GetHitPointInfo(position);
            if (bRet)
            {
                self.isRaycast = true;
                point = raycastHit.point;
                self.isCliffy = self.CheckCliffy(point);
            }

            if (self.isRaycast)
            {
                Vector3 tryPos = point + new Vector3(0, self._YOffset, 0);
                bool isLimitRule;
                (bRet, isLimitRule, _) = self.ChkCanPut(tryPos);
                return (bRet, tryPos);
            }
            return (false, Vector3.zero);
        }

        public static (bool bRet, bool isLimitRule, bool isObstacle) ChkCanPut(this DlgBattleDragItem self, Vector3 position)
        {
            if (self.isClickUGUI)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnUI");
                self.ShowPutTipMsg(tipMsg);
                return (false, true, false);
            }

            if (self.isRaycast == false)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                self.ShowPutTipMsg(tipMsg);
                return (false, false, false);
            }

            if (self.isCliffy)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                self.ShowPutTipMsg(tipMsg);
                return (false, false, false);
            }

            if (self.battleDragItemType is BattleDragItemType.Tower)
            {
                string towerCfgId = self.battleDragItemParam;
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
                float radius = 0;
                if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
                {
                    radius = ET.Ability.UnitHelper.GetBodyRadius(self.DomainScene(), towerToUnitCfgId.CfgId);
                }
                else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
                {
                    radius = towerToMonsterCfgId.Radius;
                    float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                    radius *= clientResScale;
                }

                bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
                bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
                bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
                bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
                string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(towerCfgId);
                bool isNavObstacle = ET.Ability.UnitHelper.ChkIsNavObstacle(unitCfgId);

                if (self.battleDragItemType is BattleDragItemType.Tower)
                {
                    long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                    if (gamePlayTowerDefenseComponent != null)
                    {
                        (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.battleDragItemParam);
                        if (bRet == false)
                        {
                            string tipMsg = msg;
                            self.ShowPutTipMsg(tipMsg);
                            return (false, true, false);
                        }
                    }
                }

                if (isCollider || isNavObstacle)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }
                    return (true, false, true);
                }
                if (isAttackTower || isTrap)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }

                    if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }
                }
                if (isCallMonster)
                {

                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                    if (gamePlayTowerDefenseComponent != null)
                    {
                        long playerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                        float3 homePosition = gamePlayTowerDefenseComponent.GetHomePosition(playerId);

                        (ET.Ability.TeamFlagType teamFlagType, Unit nearHostileHomeUnit) = gamePlayTowerDefenseComponent.GetNearHostileHomeByPlayerId(playerId, position);

                        if (nearHostileHomeUnit != null)
                        {
                            if (math.distancesq(homePosition, position) > math.distancesq(nearHostileHomeUnit.Position, position))
                            {
                                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutTowerCallMonster_TooNearHostileHome");
                                self.ShowPutTipMsg(tipMsg);
                                return (false, true, false);
                            }
                        }
                        else
                        {
                            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutTowerCallMonster_CanotFindNearHostileHome");
                            self.ShowPutTipMsg(tipMsg);
                            return (false, true, false);
                        }
                    }
                }
            }
            else if (self.battleDragItemType is BattleDragItemType.MoveTower)
            {
                string towerCfgId = self.battleDragItemParam;
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
                float radius = 0;
                if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
                {
                    radius = ET.Ability.UnitHelper.GetBodyRadius(self.DomainScene(), towerToUnitCfgId.CfgId);
                }
                else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
                {
                    radius = towerToMonsterCfgId.Radius;
                    float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                    radius *= clientResScale;
                }

                bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
                bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
                bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
                bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
                string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(towerCfgId);
                bool isNavObstacle = ET.Ability.UnitHelper.ChkIsNavObstacle(unitCfgId);

                if (isCollider || isNavObstacle)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }
                    return (true, false, true);
                }
                if (isAttackTower || isTrap)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }

                    if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                        self.ShowPutTipMsg(tipMsg);
                        return (false, false, false);
                    }
                }
                if (isCallMonster)
                {
                }
            }
            else if (self.battleDragItemType is BattleDragItemType.PKTower or BattleDragItemType.PKMoveTower)
            {

                // string towerCfgId = self.battleDragItemParam;
                // TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
                // float radius = 0;
                // UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                // if (towerCfg.Radius <= 0)
                // {
                //     radius = unitCfg.BodyRadius * unitCfg.ResScale;
                // }
                // else
                // {
                //     radius = towerCfg.Radius;
                //     float clientResScale = ET.Ability.UnitHelper.GetClientResScale();
                //     radius *= clientResScale;
                // }
                //
                // bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
                // bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
                // bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
                // bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
                //
                // if (isCollider)
                // {
                //     if (self.ChkIsNearTower(self.battleDragItemParam, position))
                //     {
                //         string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                //         self.ShowPutTipMsg(tipMsg);
                //         return (false, false, false);
                //     }
                //     return (true, false, true);
                // }
                // if (isAttackTower || isTrap)
                // {
                //     if (self.ChkIsNearTower(self.battleDragItemParam, position))
                //     {
                //         string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                //         self.ShowPutTipMsg(tipMsg);
                //         return (false, false, false);
                //     }
                // }
            }
            return (true, false, false);
        }

        public static bool ChkIsNearTower(this DlgBattleDragItem self, string towerCfgId, float3 pos)
        {
            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            float3 targetPos = pos;
            float radius = 0;
            if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
            {
                radius = ET.Ability.UnitHelper.GetBodyRadius(self.DomainScene(), towerToUnitCfgId.CfgId);
            }
            else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
            {
                radius = towerToMonsterCfgId.Radius;
                radius *= clientResScale;
            }

            bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
            bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
            bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return true;
            }

            if (isAttackTower)
            {
                radius = radius + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenAttackTower * clientResScale;
            }
            else if (isTrap)
            {
                radius = radius + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenTrapTower * clientResScale;
            }
            else if (isCollider)
            {
                radius = radius + GlobalSettingCfgCategory.Instance.TowerDefenseNearDisWhenColliderTower * clientResScale;
            }

            if (isAttackTower || isTrap || isCollider)
            {
                if (self.battleDragItemType == BattleDragItemType.MoveTower)
                {
                    return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius, (long)PlayerId.PlayerNone, self.moveTowerUnitId);
                }
                else
                {
                    return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius, (long)PlayerId.PlayerNone);
                }
            }
            if (isCallMonster)
            {
                long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
                return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius + 0.3f * clientResScale, myPlayerId);
            }
            return false;
        }

        public static bool ChkIsNearHeadQuarter(this DlgBattleDragItem self, float dis)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return false;
            }

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            float nearDis = dis * clientResScale;
            float length = gamePlayTowerDefenseComponent.GetPathLength(false);
            if (length <= nearDis)
            {
                return true;
            }

            return false;
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattleDragItem self)
        {
            return ET.UGUIHelper.CheckUserInput(false);
        }

        public static void ShowPutTipMsg(this DlgBattleDragItem self, string tipMsg)
        {
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattleDragItem self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static async ETTask<bool> DoPutHome(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            var hasNavMesh = await self.DrawReachableNavmesh(true);
            if (!hasNavMesh)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutHome_RecahableAreaSmall");
                self.ShowPutTipMsg(tipMsg);
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            await ET.Client.GamePlayTowerDefenseHelper.SendPutHome(self.ClientScene(), self.battleDragItemParam, position, forward);

            return true;
        }

        public static async ETTask<bool> DoPutMonsterCall(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            self.isConfirming = true;
            var pathfindingSuccess = await self.DrawMonsterCall2HeadQuarter(true);

            if (pathfindingSuccess == false)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPut_IsNotReachHome");
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.isConfirming = false;
                self.Close();
                return false;
            }

            self.View.E_TipNodeImage.SetVisible(false);
            self.ChgCurrentPlaceObj(true);

            if (self.ChkIsNearHeadQuarter(GlobalSettingCfgCategory.Instance.MinDisWhenMonsterCall2HeadQuarter))
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMonsterCall_IsNearHeadQuarter");
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);

                self.isConfirming = false;
                self.Close();
                return false;
            }

            self.View.EG_ConfirmRootRectTransform.SetVisible(true);
            self.View.E_ConfirmButtonButton.AddListenerAsync(async () =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
                (bool bRet, string msg) =
                        await ET.Client.GamePlayTowerDefenseHelper.SendPutMonsterCall(self.ClientScene(), self.battleDragItemParam, position);
                if (bRet)
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent =
                            ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                    float length = gamePlayTowerDefenseComponent.GetPathLength();
                    EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "PortalPlaced",
                        properties = new()
                        {
                            { "pathLength", length },
                        }
                    });
                }

                self.isConfirming = false;
                self.Close();
            });
            self.View.E_CancelButtonButton.AddListener(() =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

                self.isConfirming = false;
                self.Close();
            });

            return true;
        }

        public static async ETTask<bool> DoPutPKTower(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            ET.Client.GamePlayPKHelper.SendCallTower(self.ClientScene(), self.battleDragItemParam, position, forward, self.createActionIds).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMonster(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            int count = self.countOnce;
            if (count > 50)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeUITip()
                {
                    tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Tip_TooMuch"),
                });
                return false;
            }
            ET.Client.GamePlayPKHelper.SendCallMonster(self.ClientScene(), self.battleDragItemParam, position, forward, count, self.createActionIds)
                   .Coroutine();
            return true;
        }

        public static async ETTask DoPutOwnTower(this DlgBattleDragItem self, float3 position, float3 forward, bool clearPath)
        {
            if (!await self._PutOwnTower(position, forward) && clearPath)
            {
                PathLineRendererComponent.Instance.Clear();
            }
        }

        private static async ETTask<bool> _PutOwnTower(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            string towerCfgId = self.battleDragItemParam;
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, towerCfgId);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.Close();
                return false;
            }

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.battleDragItemParam);

            bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
            bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
            bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
            string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(towerCfgId);
            bool isNavObstacle = ET.Ability.UnitHelper.ChkIsNavObstacle(unitCfgId);

            float radius = 0;
            if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
            {
                radius = ET.Ability.UnitHelper.GetBodyRadius(self.DomainScene(), towerToUnitCfgId.CfgId);
            }
            else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
            {
                radius = towerToMonsterCfgId.Radius;
                float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                radius *= clientResScale;
            }

            if (isCollider || isNavObstacle)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }

                var pathfindingSuccess = await self.TryMoveUnitAndDrawAllMonsterCall2HeadQuarter(true);
                if (!pathfindingSuccess)
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPut_IsNotReachHome");
                    self.ShowPutTipMsg(tipMsg);
                    UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    PathLineRendererComponent.Instance.Clear();
                    return false;
                }
            }
            else if (isAttackTower || isTrap)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }
            }

            if (isCallMonster)
            {
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            (bool isFirstPutOwnTower, float3 firstPos) = await self.ChkIsFirstPutOwnTower();
            if (isFirstPutOwnTower)
            {
                position = firstPos;
            }
            ET.Client.GamePlayTowerDefenseHelper.SendCallOwnTower(self.ClientScene(), self.battleDragItemParam, position, forward).Coroutine();
            return true;
        }

        public static async ETTask<(bool, float3)> ChkIsFirstPutOwnTower(this DlgBattleDragItem self)
        {
            return (false, float3.zero);
            // if (ET.Client.UIGuideHelper.ChkIsUIGuideing(self.DomainScene(), true) == false)
            // {
            //     return (false, float3.zero);
            // }
            //
            // GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            // if (gamePlayTowerDefenseComponent == null)
            // {
            //     return (false, float3.zero);
            // }
            //
            // float3 midPos = gamePlayTowerDefenseComponent.GetPathMidPos();
            // return (true, midPos);
        }

        public static async ETTask DoPutMoveTower(this DlgBattleDragItem self, float3 position, float3 forward, bool clearPath)
        {
            if (!await self._PutMoveTower(position, forward) && clearPath)
            {
                PathLineRendererComponent.Instance.Clear();
            }
        }

        private static async ETTask<bool> _PutMoveTower(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkMovePlayerTower(myPlayerId, self.moveTowerUnitId, position);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.Close();
                return false;
            }

            string towerCfgId = self.battleDragItemParam;
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);

            bool isAttackTower = ET.ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ET.ItemHelper.ChkIsTrap(towerCfgId);
            bool isCollider = ET.ItemHelper.ChkIsCollider(towerCfgId);
            bool isCallMonster = ET.ItemHelper.ChkIsCallMonster(towerCfgId);
            string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(towerCfgId);
            bool isNavObstacle = ET.Ability.UnitHelper.ChkIsNavObstacle(unitCfgId);

            float radius = 0;
            if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
            {
                radius = ET.Ability.UnitHelper.GetBodyRadius(self.DomainScene(), towerToUnitCfgId.CfgId);
            }
            else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
            {
                radius = towerToMonsterCfgId.Radius;
                float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                radius *= clientResScale;
            }

            if (isCollider || isNavObstacle)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }

                var pathfindingSuccess = await self.TryMoveUnitAndDrawAllMonsterCall2HeadQuarter(true);
                if (!pathfindingSuccess)
                {
                    PathLineRendererComponent.Instance.Clear();
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPut_IsNotReachHome");
                    self.ShowPutTipMsg(tipMsg);
                    return false;
                }
            }
            else if (isAttackTower || isTrap)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.Close();
                    return false;
                }
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayTowerDefenseHelper.SendMovePlayerTower(self.ClientScene(), self.moveTowerUnitId, position, forward).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMoveTower(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayPKHelper.SendMovePKTower(self.ClientScene(), self.moveTowerUnitId, position, forward).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMovePlayer(this DlgBattleDragItem self, float3 position, float3 forward)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayPKHelper.SendMovePKPlayer(self.ClientScene(), self.moveTowerUnitId, position, forward).Coroutine();
            return true;
        }

        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattleDragItem self)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }
            (bool bRet, Vector3 screenPosition, Vector3 touchForward) = ET.UGUIHelper.GetUserInputPress();
            if (bRet == false)
            {
                return;
            }
            screenPosition += new Vector3(-100, 20, 0);

            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            Vector3 point = Vector3.zero;
            self.isRaycast = false;
            self.isCliffy = false;
            if (Physics.Raycast(ray, out hitInfo, 10000, self._groundLayerMask))
            {
                self.isRaycast = true;
                point = hitInfo.point;
                self.isCliffy = self.CheckCliffy(point);
            }

            if (self.isRaycast)
            {
                if (self.currentPlaceObj.gameObject.activeSelf == false)
                {
                    self.currentPlaceObj.gameObject.SetActive(true);
                }
                self.currentPlaceObj.transform.position = point + new Vector3(0, self._YOffset, 0);
                self.LookAtMainCamera();
                self.rayHitPos = self.currentPlaceObj.transform.position;

            }
            else
            {
                bool isHit = false;
                for (int i = 1; i < self.tryNum; i++)
                {
                    float radius = self.tryDis * i;
                    if (Physics.SphereCast(ray, radius, out hitInfo, 10000, self._groundLayerMask))
                    {
                        point = hitInfo.point;
                        self.currentPlaceObj.transform.position = point + new Vector3(0, self._YOffset, 0);
                        self.LookAtMainCamera();
                        self.rayHitPos = self.currentPlaceObj.transform.position;
                        isHit = true;
                        break;
                    }
                }

                if (self.currentPlaceObj.gameObject.activeSelf == false)
                {
                    self.currentPlaceObj.gameObject.SetActive(true);
                }
            }
        }

        public static (bool, RaycastHit) GetHitPointInfo(this DlgBattleDragItem self, Vector3 startPos)
        {
            RaycastHit hitInfo;
            bool bRet = Physics.Raycast(startPos + new Vector3(0, 3, 0), Vector3.down, out hitInfo, 6, self._groundLayerMask);
            return (bRet, hitInfo);
        }

        public static bool CheckAsyncCheckTiming(this DlgBattleDragItem self, bool forceShow)
        {
            if (!forceShow && self.newShowLineRendererTime > TimeHelper.ClientNow())
            {
                return false;
            }
            self.newShowLineRendererTime = TimeHelper.ClientNow() + 100;

            if (!forceShow)
            {
                float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                float dis = math.distancesq(self.asyncCheckPos, self.currentPlaceObj.transform.position);
                if (dis > 0.1f * clientResScale)
                {
                    self.asyncCheckPos = self.currentPlaceObj.transform.position;
                    self.asyncChecked = false;
                    return false;
                }
                if (self.isAsyncChecking)
                {
                    return false;
                }
                if (dis < 0.001f * clientResScale && self.asyncChecked)
                {
                    return false;
                }
                self.asyncChecked = true;
            }

            self.asyncCheckPos = self.currentPlaceObj.transform.position;
            return true;
        }

        public static async ETTask<bool> DrawReachableNavmesh(this DlgBattleDragItem self, bool forceShow = false)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return false;
            }
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.PutHome)
            {
                return false;
            }
            if (!self.CheckAsyncCheckTiming(forceShow))
            {
                return false;
            }
            self.isAsyncChecking = true;
            self.hasNavMeshFromHeadQuarter = await NavMeshRendererComponent.Instance.ShowNavMeshFromPos(self.asyncCheckPos);
            self.isAsyncChecking = false;
            return self.hasNavMeshFromHeadQuarter;
        }

        public static async ETTask<bool> TryMoveUnitAndDrawAllMonsterCall2HeadQuarter(this DlgBattleDragItem self, bool forceShow)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return false;
            }
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.RestTime &&
                gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.InTheBattle)
            {
                return false;
            }

            string towerCfgId = self.battleDragItemParam;
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            string unitCfgId = ET.ItemHelper.GetTowerItem2UnitCfgId(towerCfgId);
            bool isNavObstacle = ET.Ability.UnitHelper.ChkIsNavObstacle(unitCfgId);
            if (isNavObstacle == false)
            {
                return false;
            }

            if (!self.CheckAsyncCheckTiming(forceShow))
            {
                return false;
            }

            self.isAsyncChecking = true;
            self.redrawPathWhenClose = true;
            bool canArrive =
                    await gamePlayTowerDefenseComponent.TryMoveUnitAndDrawAllMonsterCall2HeadQuarterPaths(self.moveTowerUnitId, unitCfgId,
                        self.asyncCheckPos);
            if (canArrive)
            {
                self.pathfindingSuccess = true;
            }
            else
            {
                self.pathfindingSuccess = false;
            }
            self.isAsyncChecking = false;
            return canArrive;
        }

        public static async ETTask<bool> DrawMonsterCall2HeadQuarter(this DlgBattleDragItem self, bool forceShow)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return false;
            }
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                return false;
            }

            if (self.currentPlaceObj.gameObject.activeSelf == false)
            {
                await self._HideMonsterCall2HeadQuarter();
                return false;
            }

            if (!self.CheckAsyncCheckTiming(forceShow))
            {
                return false;
            }

            self.isAsyncChecking = true;
            bool canArrive = await self._DrawMonsterCall2HeadQuarter(self.asyncCheckPos);
            self.isAsyncChecking = false;
            if (canArrive)
            {
                self.pathfindingSuccess = true;
            }
            else
            {
                self.pathfindingSuccess = false;
            }
            return canArrive;
        }

        public static async ETTask<bool> _DrawMonsterCall2HeadQuarter(this DlgBattleDragItem self, float3 pos)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return await gamePlayTowerDefenseComponent.DoDrawMyMonsterCall2HeadQuarter(pos);
        }

        public static async ETTask _HideMonsterCall2HeadQuarter(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            await gamePlayTowerDefenseComponent.DoHideMyMonsterCall2HeadQuarter();
        }

        public static void _DrawAllMonsterCall2HeadQuarter(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            gamePlayTowerDefenseComponent.DoDrawAllMonsterCall2HeadQuarter(true);
        }

        public static void Clear(this DlgBattleDragItem self)
        {
            if (self.IsDisposed)
            {
                return;
            }

            ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), ARSessionComponent.ArMeshVisibility.TranslucentOcclusion);
            NavMeshRendererComponent.Instance?.ShowMesh(false);

            self.isConfirming = false;
            self.isDragging = false;
            if (self.currentPlaceObj != null)
            {
                if (self.battleDragItemType == BattleDragItemType.MonsterCall)
                {
                    self._HideMonsterCall2HeadQuarter().Coroutine();
                }
                if (self.redrawPathWhenClose)
                {
                    PathLineRendererComponent.Instance.Clear();
                }
                // Restore the color before recycle to objects pool.
                self.ChgCurrentPlaceObj(true);
                foreach (var poolObject in self.currentPlaceObj.GetComponentsInChildren<PoolObject>())
                {
                    GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                }
                GameObject.Destroy(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }

        }

        public static void Close(this DlgBattleDragItem self)
        {
            if (self.isConfirming)
            {
                return;
            }

            try
            {
                self.callBack?.Invoke(self.sceneIn);
                self.callBack = null;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            self.sceneIn = null;

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDragItem>();
        }

        public static void HideWindow(this DlgBattleDragItem self)
        {
            self.Clear();

            TimerComponent.Instance?.Remove(ref self.Timer);
        }

    }
}
