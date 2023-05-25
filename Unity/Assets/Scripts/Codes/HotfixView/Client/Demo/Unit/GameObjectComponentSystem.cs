using System;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectComponent>
        {
            protected override void Destroy(GameObjectComponent self)
            {
                UnityEngine.Object.Destroy(self.GameObject);
            }
        }
        
        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<GameObjectComponent>
        {
            protected override void Update(GameObjectComponent self)
            {
                Transform transform = self.GameObject.transform;
                // transform.position = Vector3.Lerp(transform.position, self.GetUnit().Position, Time.deltaTime);
                // transform.rotation = Quaternion.Slerp(transform.rotation, self.GetUnit().Rotation, Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, self.GetUnit().Position, 1);
                transform.rotation = Quaternion.Slerp(transform.rotation, self.GetUnit().Rotation, Time.deltaTime);
            }
        }

        public static Unit GetUnit(this GameObjectComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}