using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (Unit))]
    public static class ModelClickManagerHelper
    {
        public static ModelClickManagerComponent GetModelClickManagerComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = scene.ClientScene();
            }

            ModelClickManagerComponent _ModelClickManagerComponent = currentScene.GetComponent<ModelClickManagerComponent>();
            if (_ModelClickManagerComponent == null)
            {
                _ModelClickManagerComponent = currentScene.AddComponent<ModelClickManagerComponent>();
            }
            return _ModelClickManagerComponent;
        }

        public static void SetModelClickCallBack(Scene scene, Action<RaycastHit> modelClickCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.ModelClick = modelClickCallBack;
        }

        public static void SetModelPressCallBack(Scene scene, Action<RaycastHit> modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.ModelPress = modelPressCallBack;
        }

        public static void SetTowerInfoToClickInfo(Scene scene, Transform colliderTrans, TowerShowComponent towerShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetTowerInfoToClickInfo(colliderTrans, towerShowComponent, modelClickCallBack, modelPressCallBack);
        }

        public static void ClearClickAndPressInfo(Scene scene, Transform colliderTrans)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.ClearClickAndPressInfo(colliderTrans);
        }

        public static bool ChkIsHitTowerClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitTowerClickInfo(raycastHit);
        }

        public static TowerShowComponent GetTowerInfoFromClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetTowerInfoFromClickInfo(raycastHit);
        }

        public static void SetHomeInfoToClickInfo(Scene scene, Transform colliderTrans, HomeShowComponent homeShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetHomeInfoToClickInfo(colliderTrans, homeShowComponent, modelClickCallBack, modelPressCallBack);
        }

        public static bool ChkIsHitHomeClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitHomeClickInfo(raycastHit);
        }

        public static HomeShowComponent GetHomeInfoFromClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetHomeInfoFromClickInfo(raycastHit);
        }

        public static void SetMonsterCallInfoToClickInfo(Scene scene, Transform colliderTrans, MonsterCallShowComponent monsterCallShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetMonsterCallInfoToClickInfo(colliderTrans, monsterCallShowComponent, modelClickCallBack, modelPressCallBack);
        }

        public static bool ChkIsHitMonsterCallClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitMonsterCallClickInfo(raycastHit);
        }

        public static MonsterCallShowComponent GetMonsterCallInfoFromClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetMonsterCallInfoFromClickInfo(raycastHit);
        }

        public static void SetPlayerUnitInfoToClickInfo(Scene scene, Transform colliderTrans, PlayerUnitShowComponent playerUnitShowComponent, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetPlayerUnitInfoToClickInfo(colliderTrans, playerUnitShowComponent, modelClickCallBack, modelPressCallBack);
        }

        public static bool ChkIsHitPlayerUnitClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitPlayerUnitClickInfo(raycastHit);
        }

        public static PlayerUnitShowComponent GetPlayerUnitInfoFromClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetPlayerUnitInfoFromClickInfo(raycastHit);
        }

        public static void DoCancelHitLast(Scene scene, RaycastHit hit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.DoCancelHitLast(hit);
        }

        public static void SetPutHomeInfoToClickInfo(Scene scene, Transform colliderTrans, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetPutHomeInfoToClickInfo(colliderTrans, modelClickCallBack, modelPressCallBack);
        }

        public static bool ChkIsHitPutHomeClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitPutHomeClickInfo(raycastHit);
        }

        public static void SetPutMonsterCallInfoToClickInfo(Scene scene, Transform colliderTrans, Action modelClickCallBack, Action modelPressCallBack)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetPutMonsterCallInfoToClickInfo(colliderTrans, modelClickCallBack, modelPressCallBack);
        }

        public static bool ChkIsHitPutMonsterCallClickInfo(Scene scene, RaycastHit raycastHit)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.ChkIsHitPutMonsterCallClickInfo(raycastHit);
        }
    }
}