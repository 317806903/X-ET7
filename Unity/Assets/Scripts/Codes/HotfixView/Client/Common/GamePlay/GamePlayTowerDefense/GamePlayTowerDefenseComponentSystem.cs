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
            self.isNavmeshFromHomeInitialized = false;

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
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                self.InitializeNavmeshFromHome().Coroutine();
            }
        }

        public static async ETTask InitializeNavmeshFromHome(this GamePlayTowerDefenseComponent self)
        {
            if (self.isNavmeshFromHomeInitialized)
            {
                return;
            }
            // Wait until HomeUnit is initialized.
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            TeamFlagType homeTeamFlagType = self.GetHomeTeamFlagTypeByPlayer(myPlayerId);
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            Unit homeUnit = putHomeComponent.GetHomeUnit(homeTeamFlagType);
            if (homeUnit == null)
            {
                Log.Warning($"HomeUnit is null");
                return;
            }
            self.isNavmeshFromHomeInitialized = true;
            await NavMeshRendererComponent.Instance.ShowNavMeshFromPos(homeUnit.Position);
        }

        public static bool ChkIsHitMap(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            bool isHitTower = ModelClickManagerHelper.ChkIsHitTowerClickInfo(self.DomainScene(), hit);
            if (isHitTower)
            {
                return false;
            }

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
            Log.Debug($"==========DoClickModel {hit}");
            self.DoCancelHitLast(hit);
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

                bool isHitHome = ET.Client.ModelClickManagerHelper.ChkIsHitHomeClickInfo(self.DomainScene(), hit);
                if (isHitHome)
                {
                    self.DoHitHome(hit);
                }

                bool isHitMonsterCall = ET.Client.ModelClickManagerHelper.ChkIsHitMonsterCallClickInfo(self.DomainScene(), hit);
                if (isHitMonsterCall)
                {
                    self.DoHitMonsterCall(hit);
                }

            }

        }

        public static void DoPressModel(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            Log.Debug($"==========DoPressModel {hit}");
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

            GamePlayHelper.SendPlayerMoveTarget(self.DomainScene(), hit.point);
        }

        public static void DoCancelHitLast(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            ModelClickManagerHelper.DoCancelHitLast(self.DomainScene(), hit);
        }

        public static void DoHitTower(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            PlayerOwnerTowersComponent playerOwnerTowersComponent = self.GetComponent<PlayerOwnerTowersComponent>();
            if (playerOwnerTowersComponent == null)
            {
                return;
            }
            GameObject hitGo = hit.collider.gameObject;

            TowerShowComponent curTowerShowComponent = ModelClickManagerHelper.GetTowerInfoFromClickInfo(self.DomainScene(), hit);
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
                    if (towerShowComponent != null && towerShowComponent != curTowerShowComponent)
                    {
                        towerShowComponent.CancelSelect();
                    }
                }
            }

            if (curTowerShowComponent != null)
            {
                curTowerShowComponent.DoSelect().Coroutine();
            }
        }

        public static void DoHitHome(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            Log.Debug($" hit.collider.name[{hit.collider.name}]");
            HomeShowComponent curHomeShowComponent = ET.Client.ModelClickManagerHelper.GetHomeInfoFromClickInfo(self.DomainScene(), hit);
            if (curHomeShowComponent != null)
            {
                curHomeShowComponent.DoSelect().Coroutine();
            }
        }

        public static void DoHitMonsterCall(this GamePlayTowerDefenseComponent self, RaycastHit hit)
        {
            Log.Debug($" hit.collider.name[{hit.collider.name}]");
            MonsterCallShowComponent curMonsterCallShowComponent = ET.Client.ModelClickManagerHelper.GetMonsterCallInfoFromClickInfo(self.DomainScene(), hit);
            if (curMonsterCallShowComponent != null)
            {
                curMonsterCallShowComponent.DoSelect().Coroutine();
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
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.BlockSkill(){});
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
            if (self.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_CannotMoveTowerWhenInBattle");
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return;
            }
            self.DoMoveTower(curTowerShowComponent.towerComponent.towerCfgId, curTowerShowComponent.GetUnit().Id).Coroutine();
        }

        public static async ETTask DoMoveTower(this GamePlayTowerDefenseComponent self, string towerCfgId, long towerUnitId)
        {
            AudioPlayHelper.PlayVibrate(MoreMountains.NiceVibrations.HapticTypes.Warning);

            Unit unitTower = Ability.UnitHelper.GetUnit(self.DomainScene(), towerUnitId);
            GameObjectShowComponent gameObjectShowComponent = unitTower.GetComponent<GameObjectShowComponent>();
            if (gameObjectShowComponent != null)
            {
                gameObjectShowComponent.ChgColor(true);
            }
            TowerShowComponent towerShowComponent = unitTower.GetComponent<TowerShowComponent>();
            if (towerShowComponent != null)
            {
                towerShowComponent.ShowOrHide(false);
            }
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowBattleDragItem()
            {
                battleDragItemType = BattleDragItemType.MoveTower,
                battleDragItemParam = towerCfgId,
                moveTowerUnitId = towerUnitId,
                sceneIn = self.DomainScene(),
                callBack = (scene) =>
                {
                    EventSystem.Instance.Publish(scene, new ClientEventType.HideBattleDragItem());
                },
            });

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
            if (towerShowComponent != null)
            {
                towerShowComponent.ShowOrHide(true);
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
                if (playerId == (long)ET.PlayerId.PlayerNone)
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

                bool bRet = await PathLineRendererComponent.Instance.ShowPathIfCanArrive(homeTeamFlagType, monsterCallUnitId, path.TargetPosition, path.Points);
                if (!bRet)
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
                || self.ChkIsGameRecoverSuccess()
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
                length /= gamePlayComponent.GetGameMapScale();
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

            var mouse = UnityEngine.InputSystem.Mouse.current;
            if(mouse.rightButton.wasReleasedThisFrame)
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

            (bool bRet, float3 hitPos) = await GamePlayHelper.SendChkRay(self.DomainScene(), startPos, endPos);

            if (bRet)
            {
                GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj1.transform.position = hitPos;
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

        public static void PlayBattleMusic(this GamePlayTowerDefenseComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();

            bool bRet = monsterWaveCallComponent.GetRealWaveInfo(out int waveIndex, out int circleWaveIndex, out int circleNum, out int circleIndex, out int stageWaveIndex,
                out float monsterWaveNumScalePercent, out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
            if (bRet == false)
            {
            }

            if (waveIndex == 0)
            {
                waveIndex = 1;
            }

            ET.AbilityConfig.TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                    ET.AbilityConfig.TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(monsterWaveCallComponent.monsterWaveRuleCfgId, waveIndex);

            Dictionary<string, float> audioList = monsterWaveCallCfg.BattleMusicList;
            UIAudioManagerComponent _UIAudioManagerComponent = UIAudioManagerHelper.GetUIAudioManagerComponent(self.DomainScene());
            _UIAudioManagerComponent.PlayMusic(audioList);
        }

        public static void PlayRestTimeMusic(this GamePlayTowerDefenseComponent self)
        {
            MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();

            bool bRet = monsterWaveCallComponent.GetRealWaveInfo(out int waveIndex, out int circleWaveIndex, out int circleNum, out int circleIndex, out int stageWaveIndex,
                out float monsterWaveNumScalePercent, out float monsterWaveLevelScalePercent, out float waveRewardGoldScalePercent);
            if (bRet == false)
            {
            }

            if (waveIndex == 0)
            {
                waveIndex = 1;
            }

            ET.AbilityConfig.TowerDefense_MonsterWaveCallRuleCfg monsterWaveCallCfg =
                    ET.AbilityConfig.TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.Get(monsterWaveCallComponent.monsterWaveRuleCfgId, waveIndex);

            Dictionary<string, float> audioList = monsterWaveCallCfg.RestMusicList;
            UIAudioManagerComponent _UIAudioManagerComponent = UIAudioManagerHelper.GetUIAudioManagerComponent(self.DomainScene());
            _UIAudioManagerComponent.PlayMusic(audioList);
        }
    }
}
