using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using AudioPlayHelper = ET.Ability.Client.AudioPlayHelper;
namespace ET.Client
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    public static class GamePlayTowerDefenseComponentSystem
    {
        [ObjectSystem]
        public class GamePlayTowerDefenseComponentUpdateSystem: UpdateSystem<GamePlayTowerDefenseComponent>
        {
            protected override void Update(GamePlayTowerDefenseComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Current)
                {
                    return;
                }

                self.InitClient();
                self.DoUpdate();
            }
        }

        public static void InitClient(this GamePlayTowerDefenseComponent self)
        {
            if (self.isInitClient)
            {
                return;
            }
            self.isInitClient = true;

            ModelClickManagerHelper.SetModelClickCallBack(self.DomainScene(), (rayHit) =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.DoClickModel(rayHit);
            });
            ModelClickManagerHelper.SetModelPressCallBack(self.DomainScene(), (rayHit) =>
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.DoPressModel(rayHit);
            });
        }

        public static void DoUpdate(this GamePlayTowerDefenseComponent self)
        {
            self.DoDrawAllMonsterCall2HeadQuarter(false);
            //self.ChkMouseRightClick();
            self.SendARCameraPos();
            self.SendNeedReNoticeUnitIds();
            self.SendNeedReNoticeTowerDefense();
            if (self.previousGamePlayTowerDefenseStatus != self.gamePlayTowerDefenseStatus)
            {
                if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutMonsterPoint)
                {
                    self.OnPutMonsterPointStart().Coroutine();
                }
            }
            self.previousGamePlayTowerDefenseStatus = self.gamePlayTowerDefenseStatus;
        }

        public static async ETTask OnPutMonsterPointStart(this GamePlayTowerDefenseComponent self)
        {
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            var navMeshData = await GamePlayTowerDefenseHelper.GetReachableAreaFromHeadQuarter(self.ClientScene(), homeTeamFlagType);
            if (self.IsDisposed)
            {
                return;
            }
            NavMeshRendererComponent.Instance.SetNavMesh(self.DomainScene(), navMeshData);
        }

        public static bool ChkIsHitMap(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            GameObject hitGo = hit.collider.gameObject;
            bool isHitMap = false;
            if (PathLineRendererComponent.Instance.ChkIsHitPath(hitGo))
            {
                isHitMap = true;
            }
            else if (hitGo.layer == LayerMask.NameToLayer("Map"))
            {
                isHitMap = true;
            }

            return isHitMap;
        }

        public static void DoClickModel(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            self.DoCancelHitLast();
            bool isHitMap = self.ChkIsHitMap(hit);
            if (isHitMap)
            {
                self.OnHitMap(hit);
                // bool bRet1 = self.DoCancelSelectMyTower();
                // if (bRet1 == false)
                // {
                // 	self.OnHitMap(hit);
                // }
            }
            else
            {
                bool isHitTower = ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
                if (isHitTower)
                {
                    self.DoHitTower(hit);
                }
            }

        }

        public static void DoPressModel(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            bool isHitMap = self.ChkIsHitMap(hit);
            if (isHitMap)
            {
                // bool bRet1 = self.DoCancelSelectMyTower();
                // if (bRet1 == false)
                // {
                // }
            }
            else
            {
                bool isHitTower = ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
                if (isHitTower)
                {
                    self.DoPressHitTower(hit);
                }
            }
        }

        public static void OnHitMap(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.OnPlayerMoveTarget(hit.point);
        }

        public static void OnPlayerMoveTarget(this GamePlayTowerDefenseComponent self, float3 targetPos)
        {
            C2M_PathfindingResult c2MPathfindingResult = new();
            c2MPathfindingResult.Position = targetPos - new float3(0, 0f, 0);
            SessionHelper.GetSession(self.DomainScene()).Send(c2MPathfindingResult);
        }

        public static void DoCancelHitLast(this GamePlayTowerDefenseComponent self)
        {
            TowerShowComponent curTowerShowComponent = ModelClickManagerHelper.GetLastClickTowerInfo(self.DomainScene());
            curTowerShowComponent?.CancelSelect();
        }

        public static void DoHitTower(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }
            GameObject hitGo = hit.collider.gameObject;

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            foreach (List<long> unitIds in playerOwnerTowersComponent.playerId2unitTowerId.Values)
            {
                foreach (long unitId in unitIds)
                {
                    Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
                    if (Ability.UnitHelper.ChkUnitAlive(unit) == false)
                    {
                        continue;
                    }

                    TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
                    if (towerShowComponent != null)
                    {
                        towerShowComponent.CancelSelect();
                    }
                }
            }

            TowerShowComponent curTowerShowComponent = ModelClickManagerHelper.GetTowerInfoFromClickInfo(self.DomainScene(), hit);
            if (curTowerShowComponent != null)
            {
                curTowerShowComponent.DoSelect().Coroutine();
            }
        }

        public static void DoPressHitTower(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }
            GameObject hitGo = hit.collider.gameObject;

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            foreach (List<long> unitIds in playerOwnerTowersComponent.playerId2unitTowerId.Values)
            {
                foreach (long unitId in unitIds)
                {
                    Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
                    if (Ability.UnitHelper.ChkUnitAlive(unit) == false)
                    {
                        continue;
                    }

                    TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
                    if (towerShowComponent != null)
                    {
                        towerShowComponent.CancelSelect();
                    }
                }
            }
            TowerShowComponent curTowerShowComponent = ModelClickManagerHelper.GetTowerInfoFromClickInfo(self.DomainScene(), hit);
            if (myPlayerId != curTowerShowComponent.towerComponent.playerId)
            {
                return;
            }
            self.DoMoveTower(curTowerShowComponent.towerComponent.towerCfgId, curTowerShowComponent.GetUnit().Id).Coroutine();
        }

        public static async ETTask DoMoveTower(this GamePlayTowerDefenseComponent self, string towerCfgId, long towerUnitId)
        {
            AudioPlayHelper.PlayVibrate();

            Unit unitTower = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
            GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
            if (gameObjectShowComponent != null)
            {
                gameObjectShowComponent.ChgColor(true);
            }
            DlgBattleDragItem_ShowWindowData showWindowData = new()
            {
                battleDragItemType = BattleDragItemType.MoveTower,
                battleDragItemParam = towerCfgId,
                moveTowerUnitId = towerUnitId,
                sceneIn = self.DomainScene(),
                callBack = (scene) =>
                {
                },
            };
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData);

            while (UIManagerHelper.GetUIComponent(self.DomainScene()).GetDlgLogic<DlgBattleDragItem>(true) != null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    break;
                }
            }

            if (gameObjectShowComponent != null)
            {
                gameObjectShowComponent.ChgColor(false);
            }
        }

        public static async ETTask ChkAllMyTowerUpgrade(this GamePlayTowerDefenseComponent self)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            if (playerOwnerTowersComponent.playerId2unitTowerId.TryGetValue(myPlayerId, out List<long> unitIds) == false)
            {
                return;
            }

            foreach (long unitId in unitIds)
            {
                bool bUnitExist = await UnitHelper.ChkUnitExist(self, unitId);
                if (bUnitExist == false)
                {
                    continue;
                }
                Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
                if (unit == null)
                {
                    continue;
                }

                bool bRet = await UnitViewHelper.ChkGameObjectShowReady(self, unit);
                if (bRet == false)
                {
                    continue;
                }
            }

            foreach (long unitId in unitIds)
            {
                bool bUnitExist = await UnitHelper.ChkUnitExist(self, unitId);
                if (bUnitExist == false)
                {
                    continue;
                }
                Unit unit = Ability.UnitHelper.GetUnit(self.DomainScene(), unitId);
                if (unit == null)
                {
                    continue;
                }

                bool bRet = await UnitViewHelper.ChkGameObjectShowReady(self, unit);
                if (bRet == false)
                {
                    continue;
                }
                TowerShowComponent towerShowComponent = unit.GetComponent<TowerShowComponent>();
                if (towerShowComponent != null)
                {
                    towerShowComponent.ChkUpgradePlayerTower();
                }
            }
        }

        public static async ETTask<bool> DoDrawMyMonsterCall2HeadQuarter(this GamePlayTowerDefenseComponent self, float3 pos)
        {
            if (self.IsDisposed)
            {
                return false;
            }
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            return await self.DoDrawMonsterCall2HeadQuarterByPos(homeTeamFlagType, myPlayerId, pos);
        }

        public static async ETTask DoHideMyMonsterCall2HeadQuarter(this GamePlayTowerDefenseComponent self)
        {
            if (self.IsDisposed)
            {
                return;
            }
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            await PathLineRendererComponent.Instance.ShowPath(homeTeamFlagType, myPlayerId, false, null);
        }

        public static async ETTask<bool> DoDrawMonsterCall2HeadQuarterByPos(this GamePlayTowerDefenseComponent self, TeamFlagType homeTeamFlagType,
        long monsterCallUnitId, float3 pos)
        {
            (float3 homePos, List<float3> points) =
                    await GamePlayTowerDefenseHelper.SendGetMonsterCall2HeadQuarterPath(self.ClientScene(), homeTeamFlagType, pos);
            if (self.IsDisposed)
            {
                return false;
            }
            return await PathLineRendererComponent.Instance.ShowPathIfCanArrive(homeTeamFlagType, monsterCallUnitId, homePos, points);
        }

        public static async ETTask<bool> TryMoveUnitAndDrawAllMonsterCall2HeadQuarterPaths(this GamePlayTowerDefenseComponent self, long unitId,
        string unitCfgId, float3 pos)
        {
            M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath result =
                    await GamePlayTowerDefenseHelper.SendTryMoveUnitAndGetAllMonsterCall2HeadQuarterPath(self.ClientScene(), unitId, unitCfgId, pos);
            if (self.IsDisposed)
            {
                return false;
            }
            if (result == null)
            {
                return false;
            }
            return await self.DrawPaths(result.Path);
        }

        public static async ETTask<bool> DrawPaths(this GamePlayTowerDefenseComponent self, List<NavPath> paths)
        {
            if (paths == null || paths.Count == 0)
            {
                return false;
            }
            bool success = true;

            foreach (var path in paths)
            {
                long playerId = path.PlayerId;
                if (playerId == 0)
                {
                    success = false;
                    continue;
                }
                TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(playerId);
                long monsterCallUnitId = self.GetCallMonsterUnitId(playerId);

                if (monsterCallUnitId == 0)
                {
                    success = false;
                    continue;
                }
                if (!await PathLineRendererComponent.Instance.ShowPathIfCanArrive(homeTeamFlagType, monsterCallUnitId, path.TargetPosition,
                        path.Points))
                {
                    success = false;
                }
            }
            return success;
        }

        public static void DoDrawAllMonsterCall2HeadQuarter(this GamePlayTowerDefenseComponent self, bool forceDraw)
        {
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutHome)
            {
                PathLineRendererComponent.Instance.Clear();
                return;
            }
            if (self.ChkIsGameEnd()
                || self.ChkIsGameRecover()
                || self.ChkIsGameRecovering())
            {
                return;
            }

            PutMonsterCallComponent putMonsterCallComponent = self.GetComponent<PutMonsterCallComponent>();
            if (putMonsterCallComponent != null && putMonsterCallComponent.MonsterCallUnitId != null)
            {
                foreach (var monsterCallUnitIds in putMonsterCallComponent.MonsterCallUnitId)
                {
                    long playerId = monsterCallUnitIds.Key;
                    long monsterCallUnitId = monsterCallUnitIds.Value;
                    Unit monsterCallUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), monsterCallUnitId);
                    if (monsterCallUnit == null)
                    {
                        continue;
                    }
                    float3 pos = monsterCallUnit.Position;
                    TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(playerId);
                    if (!forceDraw && PathLineRendererComponent.Instance.ChkIsShowPath(homeTeamFlagType, monsterCallUnitId, pos))
                    {
                        continue;
                    }
                    self.DoDrawMonsterCall2HeadQuarterByPos(homeTeamFlagType, monsterCallUnitId, pos).Coroutine();
                }
            }
        }

        public static float GetPathLength(this GamePlayTowerDefenseComponent self, bool isARScale = true)
        {
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            float length = PathLineRendererComponent.Instance.GetPathLength(homeTeamFlagType, myPlayerId);
            if (isARScale)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
                length /= gamePlayComponent.GetARScale();
            }
            return length;
        }

        public static float3 GetPathMidPos(this GamePlayTowerDefenseComponent self)
        {
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            long callMonsterUnitId = self.GetCallMonsterUnitId(myPlayerId);
            float3 midPos = PathLineRendererComponent.Instance.GetPathMidPos(homeTeamFlagType, callMonsterUnitId);
            return midPos;
        }

        public static void ChkMouseRightClick(this GamePlayTowerDefenseComponent self)
        {
            if (Application.isEditor == false)
            {
                return;
            }

            if (Input.GetMouseButtonUp(1))
            {
                try
                {
                    Ray ray = CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(Input.mousePosition);
                    Vector3 startPos = ray.origin;
                    Vector3 endPos = ray.GetPoint(10000);
                    self.OnChkRay(startPos, endPos).Coroutine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async ETTask OnChkRay(this GamePlayTowerDefenseComponent self, float3 startPos, float3 endPos)
        {
            self.ShowRay(startPos, endPos);
            C2M_ChkRay _C2M_ChkRay = new();
            _C2M_ChkRay.StartPosition = startPos;
            _C2M_ChkRay.EndPosition = endPos;
            M2C_ChkRay _M2C_ChkRay = await SessionHelper.GetSession(self.DomainScene()).Call(_C2M_ChkRay) as M2C_ChkRay;
            if (_M2C_ChkRay.HitRet == 1)
            {
                GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj1.transform.position = _M2C_ChkRay.HitPosition;
            }
            else
            {
                Log.Debug($"_M2C_ChkRay.HitRet != 1");
            }
        }

        public static void ShowRay(this GamePlayTowerDefenseComponent self, float3 startPos, float3 endPos)
        {
            GameObject showRay = GameObject.Find("ShowRay");
            if (showRay != null)
            {
                GameObject.Destroy(showRay);
            }

            showRay = new GameObject("ShowRay");
            GameObject objStart = GameObject.CreatePrimitive(PrimitiveType.Cube);
            objStart.transform.SetParent(showRay.transform);
            objStart.transform.position = startPos;
            GameObject objEnd = GameObject.CreatePrimitive(PrimitiveType.Cube);
            objEnd.transform.SetParent(showRay.transform);
            objEnd.transform.position = endPos;

            GameObject objLineRenderer = new GameObject("LineRenderer");
            objLineRenderer.transform.SetParent(showRay.transform);
            LineRenderer lineRenderer = objLineRenderer.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }

        public static void SendARCameraPos(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            if (gamePlayComponent.IsAR() == false)
            {
                return;
            }

            if (gamePlayComponent.gamePlayStatus != GamePlayStatus.Gaming)
            {
                return;
            }

            long leftTime = self.lastSendTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return;
            }
            self.lastSendTime = TimeHelper.ClientNow() + 2000;

            Camera camera = CameraHelper.GetMainCamera(self.DomainScene());
            float3 cameraPos = camera.transform.position;
            float3 cameraHitPos = float3.zero;
            RaycastHit hitInfo;
            LayerMask _groundLayerMask = LayerMask.GetMask("Map");
            if (Physics.Raycast(cameraPos, camera.transform.forward, out hitInfo, 1000, _groundLayerMask))
            {
                cameraHitPos = hitInfo.point;
                GamePlayHelper.SendARCameraPos(self.DomainScene(), cameraPos, cameraHitPos).Coroutine();
            }
        }

        public static void SendNeedReNoticeUnitIds(this GamePlayTowerDefenseComponent self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());

            if (gamePlayComponent.gamePlayStatus != GamePlayStatus.Gaming)
            {
                return;
            }

            long leftTime = self.lastChkUnitExistTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return;
            }
            self.lastChkUnitExistTime = TimeHelper.ClientNow() + 5000;

            List<long> needReNoticeUnitIds = ListComponent<long>.Create();

            PutHomeComponent putHomeComponent = self.GetComponent<PutHomeComponent>();
            if (putHomeComponent == null)
            {
                return;
            }
            Dictionary<TeamFlagType, long> homeUnitList = putHomeComponent.GetHomeUnitList();
            foreach (var homeUnits in homeUnitList)
            {
                long homeUnitId = homeUnits.Value;
                Unit homeUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), homeUnitId);
                if (homeUnit == null)
                {
                    needReNoticeUnitIds.Add(homeUnitId);
                }
            }

            PutMonsterCallComponent putMonsterCallComponent = self.GetComponent<PutMonsterCallComponent>();
            if (putMonsterCallComponent != null)
            {
                foreach (var monsterCallUnits in putMonsterCallComponent.MonsterCallUnitId)
                {
                    long monsterCallUnitId = monsterCallUnits.Value;
                    Unit monsterCallUnit = Ability.UnitHelper.GetUnit(self.DomainScene(), monsterCallUnitId);
                    if (monsterCallUnit == null)
                    {
                        needReNoticeUnitIds.Add(monsterCallUnitId);
                    }
                }

            }

            GamePlayHelper.SendNeedReNoticeUnitIds(self.DomainScene(), needReNoticeUnitIds).Coroutine();
        }

        public static void SendNeedReNoticeTowerDefense(this GamePlayTowerDefenseComponent self)
        {
            if (self.isNeedReNoticeTowerDefense == false)
            {
                return;
            }
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());

            if (gamePlayComponent.gamePlayStatus != GamePlayStatus.Gaming)
            {
                return;
            }

            long leftTime = self.lastSendTimeTowerDefense - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return;
            }
            self.lastSendTimeTowerDefense = TimeHelper.ClientNow() + 1000;

            GamePlayHelper.SendNeedReNoticeTowerDefense(self.DomainScene()).Coroutine();
        }
    }
}
