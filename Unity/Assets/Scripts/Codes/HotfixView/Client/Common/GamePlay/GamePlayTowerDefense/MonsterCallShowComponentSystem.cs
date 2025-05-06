using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    public static class MonsterCallShowComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<MonsterCallShowComponent>
        {
            protected override void Awake(MonsterCallShowComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<MonsterCallShowComponent>
        {
            protected override void Destroy(MonsterCallShowComponent self)
            {
                self.HideBattleWaveInfo();
                if (self.transRoot != null)
                {
                    GameObjectPoolHelper.ReturnTransformToPool(self.transRoot);
                    self.transRoot = null;
                }

                self.monsterCallComponent = null;
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<MonsterCallShowComponent>
        {
            protected override void Update(MonsterCallShowComponent self)
            {
                // Transform transform = self.go.transform;
                // Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
                // if (mainCamera == null)
                // {
                //     return;
                // }
                // Vector3 direction = mainCamera.transform.forward;
                // transform.forward = -direction;
            }
        }

        public static Unit GetUnit(this MonsterCallShowComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void Init(this MonsterCallShowComponent self, MonsterCallComponent monsterCallComponent)
        {
            self.monsterCallComponent = monsterCallComponent;
            self.CreateShow().Coroutine();
            self.ShowBattleWaveInfo().Coroutine();
        }

        public static async ETTask ShowBattleWaveInfo(this MonsterCallShowComponent self)
        {
            MonsterCallComponent monsterCallComponent = self.monsterCallComponent;
            long myPlayerId = PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            if (gamePlayTowerDefenseComponent.isTowerDefenseTeamOne)
            {
            }
            else
            {
                if (monsterCallComponent.playerId != myPlayerId)
                {
                    return;
                }
            }

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowBattleMonsterCallHUD()
            {
                monsterCallUnitId = self.GetUnit().Id,
            });

        }

        public static void HideBattleWaveInfo(this MonsterCallShowComponent self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.HideBattleMonsterCallHUD());
        }

        public static async ETTask CreateShow(this MonsterCallShowComponent self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            while (gamePlayComponent == null)
            {
                if (self.IsDisposed)
                {
                    return;
                }
                await TimerComponent.Instance.WaitFrameAsync();
                gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            }

            bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, self.GetUnit());
            if (bRet == false)
            {
                return;
            }
            GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_MonsterCallShow");
            GameObject MonsterCallShowGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
            self.transRoot = MonsterCallShowGo.transform;
            MonsterCallShowGo.transform.SetParent(gameObjectShowComponent.GetEffectResNoRotateRoot().transform);
            MonsterCallShowGo.transform.localPosition = Vector3.zero;

            Unit unit = self.GetUnit();
            float parentScale = self.transRoot.parent.localScale.x;
            float radius = Ability.UnitHelper.GetBodyRadius(unit) / parentScale;
            float height = Ability.UnitHelper.GetBodyHeight(unit) / parentScale;
            MonsterCallShowGo.transform.localScale = new Vector3(radius * 2, height, radius * 2);

            Transform  transCollider = self.transRoot.Find("ColliderRoot");
            transCollider.gameObject.SetActive(true);
            self.transCollider = self.transRoot.Find("ColliderRoot/Collider");
            ET.Client.ModelClickManagerHelper.SetMonsterCallInfoToClickInfo(self.DomainScene(), self.transCollider, self, null, null);

            float chgY = self.transRoot.localScale.x / self.transRoot.localScale.y;
            self.transDefaultShow = self.transRoot.Find("DefaultShow");
            self.transDefaultShow.localScale = new Vector3(1f, chgY, 1f);
            self.transSelectShow = self.transRoot.Find("SelectShow");
            self.transSelectShow.localScale = new Vector3(1f, chgY, 1f);

            // self.transDefaultShow.gameObject.SetActive(true);
            // self.transSelectShow.gameObject.SetActive(false);
            self.transDefaultShow.gameObject.SetActive(false);
            self.transSelectShow.gameObject.SetActive(false);

            self.ChgColor(self.transDefaultShow);
            self.ChgColor(self.transSelectShow);
        }

        public static void ChgColor(this MonsterCallShowComponent self, Transform trans)
        {
        }

        public static async ETTask DoSelect(this MonsterCallShowComponent self)
        {
            Log.Debug($"ET.Client.MonsterCallShowComponentSystem.DoSelect unitId[{self.GetUnit().Id}] monsterCallCfgId[{self.monsterCallComponent.monsterCallCfgId}]");

            return;
            // self.transDefaultShow.gameObject.SetActive(false);
            // self.transSelectShow.gameObject.SetActive(true);

            // EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.ShowBattleHomeHUD()
            // {
            //     homeUnitId = self.GetUnit().Id,
            //     monsterCallCfgId = self.monsterCallComponent.monsterCallCfgId,
            // });
        }

        public static bool CancelSelect(this MonsterCallShowComponent self)
        {
            bool bRet = false;
            if (self.transSelectShow.gameObject.activeSelf)
            {
                self.transDefaultShow.gameObject.SetActive(true);
                self.transSelectShow.gameObject.SetActive(false);
                bRet = true;

                Log.Debug($"ET.Client.MonsterCallShowComponentSystem.CancelSelect unitId[{self.GetUnit().Id}] monsterCallCfgId[{self.monsterCallComponent.monsterCallCfgId}]");

            }
            return bRet;
        }
    }
}