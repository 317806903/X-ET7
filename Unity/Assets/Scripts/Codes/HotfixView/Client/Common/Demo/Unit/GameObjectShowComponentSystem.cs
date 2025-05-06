using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectShowComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectShowComponent>
        {
            protected override void Awake(GameObjectShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectShowComponent>
        {
            protected override void Destroy(GameObjectShowComponent self)
            {
                if (self.unitResGameObject != null)
                {
                    foreach (var poolObject in self.unitResRoot.GetComponentsInChildren<PoolObject>())
                    {
                        if (self.unitResGameObject.transform == poolObject.transform)
                        {
                            continue;
                        }
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }
                    GameObjectPoolHelper.ReturnTransformToPool(self.unitResGameObject.transform);
                    self.unitResGameObject = null;
                }
                if (self.unitResRoot != null)
                {
                    foreach (var poolObject in self.unitResRoot.GetComponentsInChildren<PoolObject>())
                    {
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }

                    self.unitResRoot.transform.DeleteAllChildren();
                    self.unitResRoot = null;
                }
                if (self.effectResRoot != null)
                {
                    foreach (var poolObject in self.effectResRoot.GetComponentsInChildren<PoolObject>())
                    {
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }

                    self.effectResRoot.transform.DeleteAllChildren();
                    self.effectResRoot = null;
                }
                if (self.effectResScaleRoot != null)
                {
                    foreach (var poolObject in self.effectResScaleRoot.GetComponentsInChildren<PoolObject>())
                    {
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }

                    self.effectResScaleRoot.transform.DeleteAllChildren();
                    self.effectResScaleRoot = null;
                }
                if (self.effectResNoRotateRoot != null)
                {
                    foreach (var poolObject in self.effectResNoRotateRoot.GetComponentsInChildren<PoolObject>())
                    {
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }

                    self.effectResNoRotateRoot.transform.DeleteAllChildren();
                    self.effectResNoRotateRoot = null;
                }
                if (self.effectResScaleNoRotateRoot != null)
                {
                    foreach (var poolObject in self.effectResScaleNoRotateRoot.GetComponentsInChildren<PoolObject>())
                    {
                        GameObjectPoolHelper.ReturnTransformToPool(poolObject.transform);
                    }

                    self.effectResScaleNoRotateRoot.transform.DeleteAllChildren();
                    self.effectResScaleNoRotateRoot = null;
                }
                if (self.gameObject != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.gameObject.transform);
                    self.gameObject = null;
                }
                if (self.cubeGameObject != null)
                {
                    UnityEngine.Object.Destroy(self.cubeGameObject);
                    self.cubeGameObject = null;
                }
                if (self.sphereGameObject != null)
                {
                    UnityEngine.Object.Destroy(self.sphereGameObject);
                    self.sphereGameObject = null;
                }
                if (self.cylinderGameObject != null)
                {
                    UnityEngine.Object.Destroy(self.cylinderGameObject);
                    self.cylinderGameObject = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<GameObjectShowComponent>
        {
            protected override void Update(GameObjectShowComponent self)
            {
                if (self.IsDisposed)
                {
                    return;
                }
                self.Update();
            }
        }

        public static void _SetDebugInfo(this GameObjectShowComponent self)
        {
            // long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            // int gameObjectShowType = ET.Ability.BuffHelper.GetGameObjectShowType(self.GetUnit(), myPlayerId);

#if ENABLE_VIEW && UNITY_EDITOR
            ReferenceCollector referenceCollector = self.gameObject.GetComponent<ReferenceCollector>();
            if (referenceCollector == null)
            {
                referenceCollector = self.gameObject.AddComponent<ReferenceCollector>();
            }
            referenceCollector.Clear();
            referenceCollector.Add("EntityViewGO", self.GetUnit().viewGO);

            Unit unit = self.GetUnit();
            unit.viewPrefabGO = self.gameObject;
            string cfgId = "";
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                cfgId = unit.GetComponent<BulletObj>().CfgId;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                cfgId = unit.GetComponent<AoeObj>().CfgId;
            }
            else
            {
                cfgId = unit.CfgId;
            }
            self.gameObject.name = $"{cfgId} ({self.GetUnit().Id})";
#endif
        }

        public static GameObject GetGo(this GameObjectShowComponent self)
        {
            return self.gameObject;
        }

        public static GameObject GetUnitResGo(this GameObjectShowComponent self)
        {
            return self.unitResGameObject;
        }

        public static Vector3 GetGoPosition(this GameObjectShowComponent self)
        {
            return self.gameObject.transform.position;
        }

        public static GameObject GetUnitResRoot(this GameObjectShowComponent self)
        {
            return self.unitResRoot;
        }

        public static GameObject GetEffectResRoot(this GameObjectShowComponent self)
        {
            return self.effectResRoot;
        }

        public static GameObject GetEffectResScaleRoot(this GameObjectShowComponent self)
        {
            return self.effectResScaleRoot;
        }

        public static GameObject GetEffectResNoRotateRoot(this GameObjectShowComponent self)
        {
            return self.effectResNoRotateRoot;
        }

        public static GameObject GetEffectResScaleNoRotateRoot(this GameObjectShowComponent self)
        {
            return self.effectResScaleNoRotateRoot;
        }

        public static GameObject GetTiltedGo(this GameObjectShowComponent self)
        {
            return self.keepTilted?.gameObject;
        }

        public static GameObject GetForwardGo(this GameObjectShowComponent self)
        {
            return self.keepForward?.gameObject;
        }

        public static void SetRefGameObject(this GameObjectShowComponent self, GameObjectComponent gameObjectComponent)
        {
            self.refGameObjectComponent = gameObjectComponent;
            self.resName = gameObjectComponent.resName;
            self.resScale = gameObjectComponent.resScale;
            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            self.resScale *= clientResScale;
        }

        public static async ETTask Init(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
            while (gameObjectComponent == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
                Unit unit = self.GetUnit();
                if (unit == null)
                {
                    return;
                }
                gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            }

            if (gameObjectComponent.resName != self.resName
                || gameObjectComponent.resScale != self.resScale)
            {
                if (self.unitResGameObject != null)
                {
                    self.RemoveComponent<GameObjectFlickerComponent>();
                    self.RemoveComponent<GameObjectTransparentComponent>();
                    self.RemoveComponent<GameObjectHideComponent>();
                    GameObjectPoolHelper.ReturnTransformToPool(self.unitResGameObject.transform);
                    self.unitResGameObject = null;
                }
            }

            self.SetRefGameObject(gameObjectComponent);
            await self.InitPrefab();
            await self.InitUnitResPrefab();
            await self.DealPrefabEffect();
        }

        public static async ETTask InitPrefab(this GameObjectShowComponent self)
        {
            if (self.gameObject != null)
            {
                return;
            }
            Unit unit = self.GetUnit();

            string resName = self.resName;
            float resScale = self.resScale;

            string resNameRoot = "ResEffect_UnitGameObjectShow";
            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resNameRoot);
            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,10);
            go.transform.SetParent(GlobalComponent.Instance.Unit);
            go.transform.position = unit.Position;
            go.transform.forward = unit.Forward;
            go.transform.localScale = Vector3.one;

            self.gameObject = go;
            self.unitResRoot = self.gameObject.transform.Find("UnitRes").gameObject;
            self.effectResRoot = self.gameObject.transform.Find("EffectRes").gameObject;
            self.effectResScaleRoot = self.gameObject.transform.Find("EffectResScale").gameObject;
            self.effectResNoRotateRoot = self.gameObject.transform.Find("EffectResNoRotate").gameObject;
            self.effectResScaleNoRotateRoot = self.gameObject.transform.Find("EffectResScaleNoRotate").gameObject;

            self.unitResRoot.transform.localPosition = Vector3.zero;
            self.unitResRoot.transform.localEulerAngles = Vector3.zero;
            self.unitResRoot.transform.localScale = Vector3.one * resScale;

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            self.effectResRoot.transform.localPosition = Vector3.zero;
            self.effectResRoot.transform.localEulerAngles = Vector3.zero;
            self.effectResRoot.transform.localScale = Vector3.one * clientResScale;

            self.effectResScaleRoot.transform.localPosition = Vector3.zero;
            self.effectResScaleRoot.transform.localEulerAngles = Vector3.zero;
            self.effectResScaleRoot.transform.localScale = Vector3.one * resScale;

            self.effectResNoRotateRoot.transform.localPosition = Vector3.zero;
            self.effectResNoRotateRoot.transform.localEulerAngles = Vector3.zero;
            self.effectResNoRotateRoot.transform.localScale = Vector3.one * clientResScale;

            self.effectResScaleNoRotateRoot.transform.localPosition = Vector3.zero;
            self.effectResScaleNoRotateRoot.transform.localEulerAngles = Vector3.zero;
            self.effectResScaleNoRotateRoot.transform.localScale = Vector3.one * resScale;

            self.firstRotation = unit.Rotation;

            self._SetDebugInfo();

#if UNITY_EDITOR
            if (Ability.UnitHelper.ChkIsPlayer(unit))
            {
            //     self.cubeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //     self.cubeGameObject.transform.SetParent(GlobalComponent.Instance.Unit);

            //     self.sphereGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //     self.sphereGameObject.transform.SetParent(GlobalComponent.Instance.Unit);

            //     self.cylinderGameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            //     self.cylinderGameObject.transform.SetParent(GlobalComponent.Instance.Unit);
            }

#endif

            self.ChkBattleNotice();
        }

        public static async ETTask InitUnitResPrefab(this GameObjectShowComponent self)
        {
            if (self.unitResGameObject != null)
            {
                return;
            }

            string resName = self.resName;
            float resScale = self.resScale;

            // Unit View层
            if (string.IsNullOrEmpty(resName))
            {
                return;
            }

            while (TimeHelper.ClientNow() > TimeHelper.ClientFrameTime() + 200)
            {
                //await TimerComponent.Instance.WaitFrameAsync();
                await TimerComponent.Instance.WaitAsync(200);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            GameObject unitResGo = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);
            if (unitResGo == null)
            {
                unitResGo = new GameObject();
            }
            unitResGo.transform.SetParent(self.unitResRoot.transform);
            unitResGo.transform.localPosition = Vector3.zero;
            unitResGo.transform.localEulerAngles = Vector3.zero;
            unitResGo.transform.localScale = Vector3.one;
            ET.Client.GameObjectPoolHelper.TrigFromPool(unitResGo);

            self.unitResGameObject = unitResGo;

            self.keepTilted = unitResGo.GetComponentInChildren<KeepTilted>();
            self.keepForward = unitResGo.GetComponentInChildren<KeepForward>();
            if (self.keepForward != null)
            {
                self.keepForward.transform.rotation = self.firstRotation;
            }
        }

        public static void ChkBattleNotice(this GameObjectShowComponent self)
        {
            Unit unit = self.GetUnit();
            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent != null)
            {
                if (string.IsNullOrEmpty(towerComponent.model.TutorialCfgId) == false
                    && towerComponent.model.IsShowTutorialInBattle)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeShowBattleNotice()
                    {
                        tutorialCfgId = towerComponent.model.TutorialCfgId,
                    });
                }
            }

            MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
            if (monsterComponent != null)
            {
                if (string.IsNullOrEmpty(monsterComponent.monsterCfgId) == false
                    && monsterComponent.model.IsShowTutorialInBattle
                    && string.IsNullOrEmpty(monsterComponent.model.TutorialCfgId) == false)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeShowBattleNotice()
                    {
                        tutorialCfgId = monsterComponent.model.TutorialCfgId,
                    });
                }
            }

        }

        public static Unit GetUnit(this GameObjectShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ChgColor(this GameObjectShowComponent self, bool isMoving)
        {
            if (isMoving)
            {
                self.DealPrefabEffect_Transparent(true);
            }
            else
            {
                self.DealPrefabEffect_Transparent(false);
            }
        }

        public static void Update(this GameObjectShowComponent self)
        {
            if (self.gameObject == null)
            {
                return;
            }

            self.ChkGameObjectComponentChg();

            float3 targetPos = self.UpdatePos();
            self.UpdateRotation(targetPos);

            self.UpdateKeepForward();
            self.UpdateTiltedRotation();
            self.UpdateForwardRotation();
        }

        public static void ChkGameObjectComponentChg(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.refGameObjectComponent;
            if (gameObjectComponent != null)
            {
                return;
            }

            self.Init().Coroutine();
        }

        public static float3 UpdatePos(this GameObjectShowComponent self)
        {
            Transform transform = self.gameObject.transform;
            Unit unit = self.GetUnit();

            UnitClientPosComponent unitClientPosComponent = unit.GetComponent<UnitClientPosComponent>();

            float3 curGameObjectPos = transform.position;
            float3 curGameObjectForward = transform.forward;
            float3 targetPos = unit.Position;

            // if (curGameObjectPos.Equals(targetPos) == false)
            // {
            //     Log.Error($"--zpb [{math.normalize(targetPos - curGameObjectPos)}] curGameObjectPos={curGameObjectPos} targetPos={targetPos}");
            // }
            if (self.cubeGameObject != null)
            {
                self.cubeGameObject.transform.position = targetPos;
            }

            if (ET.Ability.UnitHelper.ChkIsFly(unit))
            {
                float gameResScale = ET.Ability.UnitHelper.GetClientGameResScale(unit.DomainScene());
                targetPos += new float3(0, GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale, 0);
            }

            float nearDis = 0.05f;

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            nearDis *= clientResScale;

            if (math.abs(unit.Position.x - curGameObjectPos.x) < nearDis
                && math.abs(unit.Position.z - curGameObjectPos.z) < nearDis
                )
            {
                transform.position = targetPos;
            }
            else if (false && Ability.UnitHelper.ChkIsBullet(unit))
            {
                transform.position = Vector3.Lerp(transform.position, unit.Position, 0.8f);
            }
            else
            {
                float moveSpeed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
                if (moveSpeed == 0)
                {
                    transform.position = targetPos;
                }
                else
                {
                    bool isNeedPreMove = false;
                    if (isNeedPreMove)
                    {
                        targetPos = targetPos + unit.Forward * moveSpeed * Time.deltaTime * 5;
                    }

                    Vector3 dis = curGameObjectPos - targetPos;
                    float tmp1 = dis.sqrMagnitude / (moveSpeed * moveSpeed);
                    if (tmp1 <= Time.deltaTime * Time.deltaTime)
                    {
                        //Log.Error($"--zpb 1 ->0");
                        transform.position = targetPos;
                    }
                    else if (true)
                    {
                        float donePercentageMove = 0.1f;

                        if (PingComponent.Instance != null && PingComponent.Instance.Ping > 200)
                        {
                            donePercentageMove *= 100f / (int)(PingComponent.Instance.Ping);
                        }
                        else if (PingComponent.Instance != null && PingComponent.Instance.Ping > 100)
                        {
                            donePercentageMove *= 100f / (int)(PingComponent.Instance.Ping);
                        }

                        if (Math.Pow((1f * moveSpeed * Time.deltaTime/donePercentageMove), 2) > math.lengthsq(targetPos - curGameObjectPos))
                        {
                            donePercentageMove = Mathf.Min(1f, 1f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        }

                        donePercentageMove = Mathf.Clamp(donePercentageMove, 0, 1);
                        //Log.Error($"---zpb donePercentageMove={donePercentageMove}");
                        //Log.Error($"--zpb donePercentageMove[{donePercentageMove}]");
                        var position1 = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = position1;
                    }
                    else if (tmp1 <= Time.deltaTime * Time.deltaTime * 9)
                    {
                        Log.Error($"--zpb 3 ->1.2");
                        float donePercentageMove = 1;
                        donePercentageMove = Mathf.Min(1f, 1.2f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else if (tmp1 <= Time.deltaTime * Time.deltaTime * 16)
                    {
                        Log.Error($"--zpb 4 ->1.5");
                        float donePercentageMove = 1;
                        donePercentageMove = Mathf.Min(1f, 1.2f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else if (tmp1 <= Time.deltaTime * Time.deltaTime * 25)
                    {
                        Log.Error($"--zpb 5 ->2");
                        float donePercentageMove = 1;
                        donePercentageMove = Mathf.Min(1f, 2f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else if (tmp1 <= Time.deltaTime * Time.deltaTime * 49)
                    {
                        Log.Error($"--zpb 7 ->3");
                        float donePercentageMove = 1;
                        donePercentageMove = Mathf.Min(1f, 3f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else if (tmp1 <= Time.deltaTime * Time.deltaTime * 100)
                    {
                        Log.Error($"--zpb 10 ->5");
                        float donePercentageMove = 1;
                        donePercentageMove = Mathf.Min(1f, 5f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else if (tmp1 <= 1500)
                    {
                        Log.Error($"--zpb xx ->0.3");
                        float donePercentageMove = 1;
                        //donePercentageMove = Mathf.Min(1f, 1f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        donePercentageMove = 0.3f;
                        targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                        transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                    }
                    else
                    {
                        if (unitClientPosComponent != null)
                        {
                            float timeToCompleteMove = unitClientPosComponent.targetPosClientNeedTime;

                            if (PingComponent.Instance.Ping > 200)
                            {
                                timeToCompleteMove += 0.002f * PingComponent.Instance.Ping;
                            }
                            else if (PingComponent.Instance.Ping > 100)
                            {
                                timeToCompleteMove += 0.0015f * PingComponent.Instance.Ping;
                            }

                            float donePercentageMove = 1;
                            //Log.Error($"--zpb timeToCompleteMove==0 [{timeToCompleteMove ==0}]");
                            if (timeToCompleteMove == 0)
                            {
                                donePercentageMove = Mathf.Min(1f, 1f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                                targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                            }
                            else
                            {
                                float tmp22 = timeToCompleteMove - (unitClientPosComponent.targetPosClientTime - TimeHelper.ClientNow()) * 0.001f;
                                donePercentageMove = Mathf.Min(1f, tmp22/timeToCompleteMove);
                                //Log.Error($"--zpb tmp22[{tmp22}] timeToCompleteMove[{timeToCompleteMove}] donePercentageMove[{donePercentageMove}]");
                                targetPos = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                            }

                            transform.position = Vector3.Lerp(curGameObjectPos, targetPos, 0.3f);
                        }
                        else
                        {
                            int speedScale = 1;
                            if (tmp1 >= 1)
                            {
                                speedScale = 3;
                            }
                            else if (tmp1 >= 0.5f)
                            {
                                speedScale = 2;
                            }
                            if (speedScale >= 1)
                            {
                                float timeToCompleteMove = dis.magnitude / moveSpeed;
                                float donePercentageMove = Mathf.Min(1f, speedScale * Time.deltaTime / timeToCompleteMove);
                                transform.position = Vector3.Lerp(curGameObjectPos, targetPos, donePercentageMove);
                            }
                            else
                            {
                                transform.position = targetPos;
                            }
                        }
                    }

                }
            }

            if (unitClientPosComponent != null)
            {
                unitClientPosComponent.clientPosition = transform.position;
                if (ET.Ability.UnitHelper.ChkIsFly(unit))
                {
                    float gameResScale = ET.Ability.UnitHelper.GetClientGameResScale(unit.DomainScene());
                    unitClientPosComponent.clientPosition -= new float3(0, GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale, 0);
                }
                if (math.abs(targetPos.x - transform.position.x) < 0.001f
                    && math.abs(targetPos.z - transform.position.z) < 0.001f
                   )
                {
                    unitClientPosComponent.serverTime = 0;
                }
            }

            return targetPos;
        }

        public static void UpdateRotation(this GameObjectShowComponent self, float3 targetPos)
        {
            Transform transform = self.gameObject.transform;
            Unit unit = self.GetUnit();

            float nearDis = 0.1f;

            float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
            nearDis *= clientResScale;

            bool isForceFace = true;
            if (math.abs(targetPos.x - transform.position.x) < nearDis
                && math.abs(targetPos.z - transform.position.z) < nearDis
               )
            {
                isForceFace = false;
            }

            if (isForceFace)
            {
                float3 forward = (Vector3)targetPos - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
            }
            else
            {
                Quaternion targetRotation = unit.Rotation;
                float rotationSpeed = ET.Ability.UnitHelper.GetRotationSpeed(unit);
                if (rotationSpeed <= 0)
                {
                    transform.rotation = targetRotation;
                }
                else
                {
                    float angle = Quaternion.Angle(transform.rotation, targetRotation);
                    if (angle <= rotationSpeed * Time.deltaTime)
                    {
                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        float timeToComplete = angle / rotationSpeed;
                        float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);

                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, donePercentage);
                    }
                }
            }
        }

        public static void UpdateKeepForward(this GameObjectShowComponent self)
        {
            if (self.keepForward == null)
            {
                return;
            }

            if (MathHelper.IsEqualQuaternion(self.firstRotation, self.GetUnit().Rotation))
            {
                return;
            }
            self.keepForward.transform.rotation = self.firstRotation;
        }

        public static void UpdateTiltedRotation(this GameObjectShowComponent self)
        {
            if (self.keepTilted == null)
            {
                return;
            }
            Unit unit = self.GetUnit();
            AttackTargetComponent attackTargetComponent = unit.GetComponent<AttackTargetComponent>();
            if (attackTargetComponent == null)
            {
                return;
            }

            Transform tiltedTransform = self.keepTilted.transform;

            Quaternion localTargetRotation;
            float rotationSpeed = 200;

            Unit attackTargetUnit = attackTargetComponent.GetAttackTargetUnit();
            if (ET.Ability.UnitHelper.ChkUnitAlive(attackTargetUnit) == false)
            {
                rotationSpeed = 10;
                localTargetRotation = Quaternion.identity;
            }
            else
            {
                rotationSpeed = 200;

                float tiltedAngle = attackTargetComponent.GetAttackTargetAngle();
                localTargetRotation = MathHelper.AngleToQuaternion(tiltedAngle, new float3(1, 0, 0));
            }

            float angle = Quaternion.Angle(tiltedTransform.localRotation, localTargetRotation);
            if (angle <= rotationSpeed * Time.deltaTime)
            {
                tiltedTransform.localRotation = localTargetRotation;
            }
            else
            {
                float timeToComplete = angle / rotationSpeed;
                float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);
                tiltedTransform.localRotation = Quaternion.Slerp(tiltedTransform.localRotation, localTargetRotation, donePercentage);
            }
        }

        public static void UpdateForwardRotation(this GameObjectShowComponent self)
        {
            if (self.chkChildCountTime == 0 || self.chkChildCountTime < Time.time)
            {
                self.chkChildCountTime = Time.time + 1;
                if (self.effectResNoRotateRoot != null)
                {
                    self.effectResNoRotateChildCount = self.effectResNoRotateRoot.transform.childCount;
                }
                if (self.effectResScaleNoRotateRoot != null)
                {
                    self.effectResScaleNoRotateChildCount = self.effectResScaleNoRotateRoot.transform.childCount;
                }
            }

            if (self.effectResNoRotateChildCount > 0)
            {
                Transform transform = self.gameObject.transform;
                if (MathHelper.IsEqualQuaternion(self.firstRotation, transform.rotation, 0.01f) == false)
                {
                    self.effectResNoRotateRoot.transform.rotation = self.firstRotation;
                }
            }
            if (self.effectResScaleNoRotateChildCount > 0)
            {
                Transform transform = self.gameObject.transform;
                if (MathHelper.IsEqualQuaternion(self.firstRotation, transform.rotation, 0.01f) == false)
                {
                    self.effectResScaleNoRotateRoot.transform.rotation = self.firstRotation;
                }
            }
        }

        public static async ETTask DealPrefabEffect(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.refGameObjectComponent;
            if (gameObjectComponent == null)
            {
                return;
            }
            await self.DealPrefabEffect_Hide();
            await self.DealPrefabEffect_Flicker();
            self.DealPrefabEffect_Transparent(gameObjectComponent.isTransparent);
        }

        public static async ETTask DealPrefabEffect_Hide(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.refGameObjectComponent;
            if (gameObjectComponent == null)
            {
                return;
            }
            if (gameObjectComponent.isHiding)
            {
                if (self.GetComponent<GameObjectHideComponent>() == null)
                {
                    self.AddComponent<GameObjectHideComponent>();
                }
                return;
            }
            else
            {
                if (self.GetComponent<GameObjectHideComponent>() != null)
                {
                    self.RemoveComponent<GameObjectHideComponent>();
                }
            }
        }

        public static async ETTask DealPrefabEffect_Flicker(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.refGameObjectComponent;
            if (gameObjectComponent == null)
            {
                return;
            }
            if (gameObjectComponent.flickerEndTime > TimeHelper.ServerNow())
            {
                Color startColor = new Color(gameObjectComponent.flickerStartColor.R, gameObjectComponent.flickerStartColor.G, gameObjectComponent.flickerStartColor.B, gameObjectComponent.flickerStartColor.A);
                Color endColor = new Color(gameObjectComponent.flickerEndColor.R, gameObjectComponent.flickerEndColor.G, gameObjectComponent.flickerEndColor.B, gameObjectComponent.flickerEndColor.A);
                if (self.GetComponent<GameObjectFlickerComponent>() == null)
                {
                    self.AddComponent<GameObjectFlickerComponent>();
                }
                self.GetComponent<GameObjectFlickerComponent>().ResetTime(gameObjectComponent.flickerEndTime, gameObjectComponent.flickerFrequency, startColor, endColor);
            }
        }

        public static void DealPrefabEffect_Transparent(this GameObjectShowComponent self, bool isTransparent)
        {
            GameObjectTransparentComponent gameObjectTransparentComponent = self.GetComponent<GameObjectTransparentComponent>();
            if (isTransparent)
            {
                if (gameObjectTransparentComponent == null)
                {
                    gameObjectTransparentComponent = self.AddComponent<GameObjectTransparentComponent>();
                }
            }
            else
            {
                if (gameObjectTransparentComponent != null)
                {
                    self.RemoveComponent<GameObjectTransparentComponent>();
                }
            }
        }

        public static async ETTask FlickerWhenBeHit(this GameObjectShowComponent self)
        {
            ActionCfg_GameObjectDeal actionCfgGameObjectDeal = ActionCfg_GameObjectDealCategory.Instance.Get("GameObjectDeal_FlickerWhenBeHit");
            if (actionCfgGameObjectDeal == null)
            {
                return;
            }

            foreach (GameObjectDeal gameObjectDeal in actionCfgGameObjectDeal.DealType)
            {
                if (gameObjectDeal is GameObjectFlicker gameObjectFlicker)
                {
                    if (gameObjectFlicker.FlickerDuration <= 0)
                    {
                        continue;
                    }
                    long flickerEndTime = TimeHelper.ServerNow() + (long)(gameObjectFlicker.FlickerDuration * 1000);
                    float flickerFrequency = gameObjectFlicker.FlickerFrequency;
                    Color startColor = new Color(gameObjectFlicker.StartColor.R, gameObjectFlicker.StartColor.G, gameObjectFlicker.StartColor.B, gameObjectFlicker.StartColor.A);
                    Color endColor = new Color(gameObjectFlicker.EndColor.R, gameObjectFlicker.EndColor.G, gameObjectFlicker.EndColor.B, gameObjectFlicker.EndColor.A);
                    if (self.GetComponent<GameObjectFlickerComponent>() == null)
                    {
                        self.AddComponent<GameObjectFlickerComponent>();
                    }
                    self.GetComponent<GameObjectFlickerComponent>().ResetTime(flickerEndTime, flickerFrequency, startColor, endColor);
                }
            }
        }

    }
}