using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (MoveComponent))]
    public static class MoveComponentSystem
    {
        [ObjectSystem]
        public class MoveComponentAwakeSystem: AwakeSystem<MoveComponent>
        {
            protected override void Awake(MoveComponent self)
            {
            }
        }

        [ObjectSystem]
        public class MoveComponentDestroySystem: DestroySystem<MoveComponent>
        {
            protected override void Destroy(MoveComponent self)
            {
            }
        }

        [ObjectSystem]
        public class MoveComponentFixedUpdateSystem: FixedUpdateSystem<MoveComponent>
        {
            protected override void FixedUpdate(MoveComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static Unit GetUnit(this MoveComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this MoveComponent self, float fixedDeltaTime)
        {
            Unit unit = self.GetUnit();
            if (UnitHelper.ChkCanMove(unit) == false)
            {
                return;
            }
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return;
            }
            if (pathfindingComponent.navMeshAgent == null)
            {
                return;
            }

            float3 speedVector = BuffHelper.GetMotionSpeedVector(unit);


            if (speedVector.Equals(float3.zero))
            {
                if (pathfindingComponent != null)
                {
                    pathfindingComponent.StopMoveVelocity();
                }
                MoveOrIdleComponent moveOrIdleComponent = unit.GetComponent<MoveOrIdleComponent>();
                if (moveOrIdleComponent != null)
                {
                    float speed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
                    if (moveOrIdleComponent.moveInputType == MoveInputType.Stop)
                    {
                    }
                    else if (moveOrIdleComponent.moveInputType == MoveInputType.Direction)
                    {
                        float3 directionInput = moveOrIdleComponent.GetMoveInput_Direction();
                        pathfindingComponent.SetMoveVelocity(directionInput * speed);
                    }
                    else if (moveOrIdleComponent.moveInputType == MoveInputType.TargetPosition)
                    {
                        float3 targetPositionInput = moveOrIdleComponent.GetMoveInput_TargetPosition();
                        float3 unitPos = unit.Position;
                        if (math.abs(targetPositionInput.x - unitPos.x) < 0.01f && math.abs(targetPositionInput.z - unitPos.z) < 0.01f)
                        {
                            ET.Ability.UnitHelper.ResetPos(unit, targetPositionInput);
                            return;
                        }
                        unit.FindPathMoveToAsync(targetPositionInput, null).Coroutine();
                    }
                }
                return;
            }
            else
            {
                MoveOrIdleComponent moveOrIdleComponent = unit.GetComponent<MoveOrIdleComponent>();
                if (moveOrIdleComponent != null)
                {
                    float speed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
                    if (moveOrIdleComponent.moveInputType == MoveInputType.Stop)
                    {
                    }
                    else if (moveOrIdleComponent.moveInputType == MoveInputType.Direction)
                    {
                        float3 directionInput = moveOrIdleComponent.GetMoveInput_Direction();
                        speedVector += directionInput * speed;
                    }
                    else if (moveOrIdleComponent.moveInputType == MoveInputType.TargetPosition)
                    {
                        float3 targetPositionInput = moveOrIdleComponent.GetMoveInput_TargetPosition();
                        float3 unitPos = unit.Position;
                        // if (math.abs(targetPositionInput.x - unitPos.x) < 0.1f && math.abs(targetPositionInput.z - unitPos.z) < 0.1f)
                        // {
                        //     ET.Ability.UnitHelper.ResetPos(unit, targetPositionInput);
                        //     return;
                        // }
                        speedVector += math.normalize(targetPositionInput - unitPos) * speed;

                        ET.Ability.MoveOrIdleHelper.StopMove(unit);
                    }
                }
            }

            if (speedVector.Equals(float3.zero))
            {
                if (pathfindingComponent != null)
                {
                    pathfindingComponent.StopMoveVelocity();
                }
                return;
            }

            var isnan = math.isnan(speedVector);
            if (isnan.Equals(true))
            {
                return;
            }

            speedVector.y = 0;

            if (pathfindingComponent != null)
            {
                pathfindingComponent.SetMoveVelocity(speedVector);
            }
            else
            {
                float3 targetPos = unit.Position + speedVector * fixedDeltaTime;
                unit.Position = targetPos;
            }
        }
    }
}