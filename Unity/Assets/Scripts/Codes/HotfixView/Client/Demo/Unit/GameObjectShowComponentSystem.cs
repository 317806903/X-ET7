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
                if (self.gameObject != null)
                {
                    //UnityEngine.Object.Destroy(self.gameObject);
                    GameObjectPoolHelper.ReturnTransformToPool(self.gameObject.transform);
                    self.gameObject = null;
                }
                if (self.cubeGameObject != null)
                {
                    UnityEngine.Object.Destroy(self.cubeGameObject);
                    self.cubeGameObject = null;
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

        public static void _SetGo(this GameObjectShowComponent self, GameObject go)
        {
            self.gameObject = go;

            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            int gameObjectShowType = ET.Ability.BuffHelper.GetGameObjectShowType(self.GetUnit(), myPlayerId);

#if ENABLE_VIEW && UNITY_EDITOR
            ReferenceCollector referenceCollector = self.gameObject.GetComponent<ReferenceCollector>();
            if (referenceCollector == null)
            {
                referenceCollector = self.gameObject.AddComponent<ReferenceCollector>();
            }
            referenceCollector.Clear();
            referenceCollector.Add("EntityViewGO", self.GetUnit().viewGO);

            self.GetUnit().viewPrefabGO = self.GetGo();
#endif
        }

        public static GameObject GetGo(this GameObjectShowComponent self)
        {
            return self.gameObject;
        }

        public static void SetRefGameObject(this GameObjectShowComponent self, GameObjectComponent gameObjectComponent)
        {
            self.refGameObjectComponent = gameObjectComponent;
            self.resName = gameObjectComponent.resName;
            self.resScale = gameObjectComponent.resScale;
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
                if (self.gameObject != null)
                {
                    //UnityEngine.Object.Destroy(self.gameObject);
                    GameObjectPoolHelper.ReturnTransformToPool(self.gameObject.transform);
                    self.gameObject = null;
                }
            }

            self.SetRefGameObject(gameObjectComponent);
            await self.InitPrefab();
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

            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);
            go.transform.SetParent(GlobalComponent.Instance.Unit);
            go.transform.position = unit.Position;
            go.transform.forward = unit.Forward;
            go.transform.localScale = Vector3.one * resScale;

            ET.Client.GameObjectPoolHelper.TrigFromPool(go);

            self._SetGo(go);

#if UNITY_EDITOR
            // self.cubeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // self.cubeGameObject.transform.SetParent(GlobalComponent.Instance.Unit);
#endif

            self.ChkBattleNotice();
        }

        public static void ChkBattleNotice(this GameObjectShowComponent self)
        {
            Unit unit = self.GetUnit();
            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent != null)
            {
                if (string.IsNullOrEmpty(towerComponent.model.TutorialCfgId) == false)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeShowBattleNotice()
                    {
                        tutorialCfgId = towerComponent.model.TutorialCfgId,
                    });
                }
            }

            MonsterComponent monsterComponent = unit.GetComponent<MonsterComponent>();
            if (monsterComponent != null)
            {
                if (string.IsNullOrEmpty(monsterComponent.model.TutorialCfgId) == false)
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeShowBattleNotice()
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
            // TransparentSetter[] tss = self.gameObject.GetComponentsInChildren<TransparentSetter>();
            // if (isMoving)
            // {
            //     foreach (var ts in tss)
            //     {
            //         ts.SetTransparent(true, 0.6f);
            //     }
            // }
            // else
            // {
            //     foreach (var ts in tss)
            //     {
            //         ts.SetTransparent(false, 1f);
            //     }
            // }

            if (isMoving)
            {
                self.AddComponent<GameObjectTransparentComponent>();
            }
            else
            {
                self.RemoveComponent<GameObjectTransparentComponent>();
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
            float3 targetPos = unit.Position;

            if (self.cubeGameObject != null)
            {
                self.cubeGameObject.transform.position = targetPos;
            }

            if (math.abs(unit.Position.x - transform.position.x) < 0.05f
                && math.abs(unit.Position.z - transform.position.z) < 0.05f
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

                        if (PingComponent.Instance.Ping > 200)
                        {
                            donePercentageMove *= 100f / (int)(PingComponent.Instance.Ping);
                        }
                        else if (PingComponent.Instance.Ping > 100)
                        {
                            donePercentageMove *= 100f / (int)(PingComponent.Instance.Ping);
                        }

                        if (Math.Pow((1f * moveSpeed * Time.deltaTime/donePercentageMove), 2) > math.lengthsq(targetPos - curGameObjectPos))
                        {
                            donePercentageMove = Mathf.Min(1f, 1f * moveSpeed * Time.deltaTime/math.length(targetPos - curGameObjectPos));
                        }
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

            bool isForceFace = true;
            if (math.abs(targetPos.x - transform.position.x) < 0.1f
                && math.abs(targetPos.z - transform.position.z) < 0.1f
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

        public static async ETTask DealPrefabEffect(this GameObjectShowComponent self)
        {
            await self.DealPrefabEffect_Hide();
            await self.DealPrefabEffect_Flicker();
            await self.DealPrefabEffect_Transparent();
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
                if (self.GetComponent<GameObjectFlickerComponent>() == null)
                {
                    self.AddComponent<GameObjectFlickerComponent>();
                }
                self.GetComponent<GameObjectFlickerComponent>().ResetTime(gameObjectComponent.flickerEndTime);
            }
        }

        public static async ETTask DealPrefabEffect_Transparent(this GameObjectShowComponent self)
        {
            GameObjectComponent gameObjectComponent = self.refGameObjectComponent;
            if (gameObjectComponent == null)
            {
                return;
            }
            if (gameObjectComponent.isTransparent)
            {
                if (self.GetComponent<GameObjectTransparentComponent>() == null)
                {
                    self.AddComponent<GameObjectTransparentComponent>();
                }
            }
            else
            {
                if (self.GetComponent<GameObjectTransparentComponent>() != null)
                {
                    self.RemoveComponent<GameObjectTransparentComponent>();
                }
            }
        }

        public static async ETTask FlickerWhenBeHit(this GameObjectShowComponent self)
        {
            long flickerEndTime = TimeHelper.ServerNow() + 200;

            if (self.GetComponent<GameObjectFlickerComponent>() == null)
            {
                self.AddComponent<GameObjectFlickerComponent>();
            }
            self.GetComponent<GameObjectFlickerComponent>().ResetTime(flickerEndTime);
        }

    }
}