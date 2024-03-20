using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DebugWhenEditorComponent))]
    public static class DebugWhenEditorComponentSystem
    {
        [ObjectSystem]
        public class DebugWhenEditorComponentAwakeSystem: AwakeSystem<DebugWhenEditorComponent>
        {
            protected override void Awake(DebugWhenEditorComponent self)
            {
                DebugWhenEditorComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DebugWhenEditorComponentDestroySystem: DestroySystem<DebugWhenEditorComponent>
        {
            protected override void Destroy(DebugWhenEditorComponent self)
            {
                DebugWhenEditorComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class DebugWhenEditorComponentUpdateSystem: UpdateSystem<DebugWhenEditorComponent>
        {
            protected override void Update(DebugWhenEditorComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this DebugWhenEditorComponent self, Transform DebugRoot)
        {
            self.Root = DebugRoot;
            self.IsShowShootDamageNum = false;
            self.IsStopActorMove = false;

            await ETTask.CompletedTask;
        }

        public static void Update(this DebugWhenEditorComponent self)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                self.IsStopActorMove = !self.IsStopActorMove;
                self.SetStopActorMove();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                self.IsShowShootDamageNum = !self.IsShowShootDamageNum;
            }
#endif
        }

        public static void SetStopActorMove(this DebugWhenEditorComponent self)
        {
            Scene clientScene = null;
            var childs = ClientSceneManagerComponent.Instance.Children;
            foreach (var child in childs.Values)
            {
                Scene scene = (Scene)child;
                if (scene.SceneType == SceneType.Client)
                {
                    clientScene = scene;
                    break;
                }
            }

            if (clientScene == null)
            {
                return;
            }
            ET.Client.GamePlayHelper.SendSetStopActorMove(clientScene, self.IsStopActorMove).Coroutine();
        }

    }
}