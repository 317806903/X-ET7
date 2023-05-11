using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (MoveComponent))]
    [FriendOf(typeof (MoveObj))]
    public static class MoveComponentSystem
    {
        [ObjectSystem]
        public class MoveComponentAwakeSystem: AwakeSystem<MoveComponent>
        {
            protected override void Awake(MoveComponent self)
            {
                self.moveDirectionInput = float3.zero;
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class MoveComponentDestroySystem: DestroySystem<MoveComponent>
        {
            protected override void Destroy(MoveComponent self)
            {
                self.removeList.Clear();
            }
        }

        public static MoveObj AddMove(this MoveComponent self, int moveCfgId)
        {
            MoveObj moveObj = self.AddChild<MoveObj>();
            moveObj.Init(moveCfgId);
            
            return moveObj;
        }

        public static void SetMoveInput(this MoveComponent self, float3 moveDirectionInput)
        {
            self.moveDirectionInput = moveDirectionInput;
        }

        public static void RunMove(this MoveComponent self, float fixedDeltaTime)
        {
            foreach (var moveObjs in self.Children)
            {
                MoveObj moveObj = moveObjs.Value as MoveObj;
                self.moveDirectionInput += moveObj.GetVeloInTime();
            }

            Unit unit = self.GetParent<Unit>();
            float3 targetPos = unit.Position + self.moveDirectionInput * fixedDeltaTime;
            
            List<float3> list = new List<float3>();
            unit.GetComponent<PathfindingComponent>().Find(unit.Position, targetPos, list);
            if (list.Count < 2)
            {
                //unit.SendStop(3);
                return;
            }
            ET.MoveByPathComponent moveByPathComponent = unit.GetComponent<ET.MoveByPathComponent>();
            float speed = 2f;
            moveByPathComponent.MoveToAsync(list, speed).Coroutine();
        }

        public static void FixedUpdate(this MoveComponent self, float fixedDeltaTime)
        {
            self.RunMove(fixedDeltaTime);

            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();
            foreach (var moveObjs in self.Children)
            {
                MoveObj moveObj = moveObjs.Value as MoveObj;
                moveObj.FixedUpdate(fixedDeltaTime);

                if (moveObj.ChkNeedRemove())
                {
                    self.removeList.Add(moveObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.removeList[i].Dispose();
            }

            self.removeList.Clear();
        }
    }
}