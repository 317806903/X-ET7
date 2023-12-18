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

        public static void SetTowerInfoToClickInfo(Scene scene, Transform colliderTrans, TowerShowComponent towerShowComponent)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetTowerInfoToClickInfo(colliderTrans, towerShowComponent);
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

        public static void SetPlayerUnitInfoToClickInfo(Scene scene, Transform colliderTrans, PlayerUnitShowComponent playerUnitShowComponent)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            modelClickManagerComponent.SetPlayerUnitInfoToClickInfo(colliderTrans, playerUnitShowComponent);
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

        public static TowerShowComponent GetLastClickTowerInfo(Scene scene)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetLastClickTowerInfo();
        }

        public static PlayerUnitShowComponent GetLastClickPlayerUnitInfo(Scene scene)
        {
            ModelClickManagerComponent modelClickManagerComponent = GetModelClickManagerComponent(scene);
            return modelClickManagerComponent.GetLastClickPlayerUnitInfo();
        }
    }
}