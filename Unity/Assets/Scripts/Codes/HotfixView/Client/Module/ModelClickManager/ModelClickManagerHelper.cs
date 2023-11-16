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

            ModelClickManagerComponent _ModelClickManagerComponent = clientScene.GetComponent<ModelClickManagerComponent>();
            if (_ModelClickManagerComponent == null)
            {
                _ModelClickManagerComponent = clientScene.AddComponent<ModelClickManagerComponent>();
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

    }
}