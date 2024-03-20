using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (MoveOrIdleComponent))]
    public static class MoveOrIdleComponentSystem
    {
        [ObjectSystem]
        public class MoveOrIdleComponentAwakeSystem: AwakeSystem<MoveOrIdleComponent>
        {
            protected override void Awake(MoveOrIdleComponent self)
            {
                self.isIdleCreating = false;
            }
        }

        [ObjectSystem]
        public class MoveOrIdleComponentDestroySystem: DestroySystem<MoveOrIdleComponent>
        {
            protected override void Destroy(MoveOrIdleComponent self)
            {
                self.CurIdleTimelineObj = null;
                self.CurMoveTimelineObj = null;
            }
        }

        [ObjectSystem]
        public class MoveOrIdleComponentFixedUpdateSystem: FixedUpdateSystem<MoveOrIdleComponent>
        {
            protected override void FixedUpdate(MoveOrIdleComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this MoveOrIdleComponent self, float fixedDeltaTime)
        {
            if (self.CurMoveTimelineObj != null)
            {
                if (self.isIdleCreating)
                {
                    return;
                }
                bool isMoveFinished = ET.MoveHelper.ChkIsMoveFinished(self.GetUnit());
                if (isMoveFinished)
                {
                    self.DoIdle().Coroutine();
                }
            }
        }

        public static Unit GetUnit(this MoveOrIdleComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void StopMove(this MoveOrIdleComponent self)
        {
            self.StopMoveAndIdle(false).Coroutine();
        }

        public static async ETTask StopMoveAndIdle(this MoveOrIdleComponent self, bool needEnterIdle)
        {
            self.moveInputType = MoveInputType.Stop;

            Unit unit = self.GetUnit();
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            pathfindingComponent?.StopMoveTarget();

            if (self.CurMoveTimelineObj != null)
            {
                self.CurMoveTimelineObj.Dispose();
            }

            if (needEnterIdle)
            {
                if (self.CurIdleTimelineObj == null)
                {
                    if (self.isIdleCreating == false)
                    {
                        self.isIdleCreating = true;
                        await self._CreateIdleTimeLine();
                        self.isIdleCreating = false;
                    }
                }
            }
            else
            {
                if (self.CurIdleTimelineObj != null)
                {
                    self.CurIdleTimelineObj.Dispose();
                }
            }
        }

        public static async ETTask _SetMoveInput_Direction(this MoveOrIdleComponent self, float3 directionInput)
        {
            self.moveInputType = MoveInputType.Direction;
            self.directionInput = directionInput;
            await ETTask.CompletedTask;
        }

        public static float3 GetMoveInput_Direction(this MoveOrIdleComponent self)
        {
            bool bRet = BuffHelper.ChkCanMoveInput(self.GetUnit());
            if (bRet)
            {
                return self.directionInput;
            }
            else
            {
                return float3.zero;
            }
        }

        public static async ETTask _SetMoveInput_TargetPosition(this MoveOrIdleComponent self, float3 targetPositionInput)
        {
            self.moveInputType = MoveInputType.TargetPosition;
            if (self.targetPositionInput.Equals(targetPositionInput) == false)
            {
                self.targetPositionInput = ET.RecastHelper.GetNearNavmeshPos(self.GetUnit(), targetPositionInput);
            }
            await ETTask.CompletedTask;
        }

        public static float3 GetMoveInput_TargetPosition(this MoveOrIdleComponent self)
        {
            Unit unit = self.GetUnit();
#if UNITY_EDITOR
            UnitComponent unitComponent = UnitHelper.GetUnitComponent(unit);
            if (unitComponent.IsStopActorMove && UnitHelper.ChkIsActor(unit))
            {
                return unit.Position;
            }
#endif

            bool bRet = BuffHelper.ChkCanMoveInput(unit);
            if (bRet)
            {
                return self.targetPositionInput;
            }
            else
            {
                return unit.Position;
            }
        }

        public static async ETTask DoIdle(this MoveOrIdleComponent self)
        {
            await self.StopMoveAndIdle(true);
        }

        public static async ETTask DoMoveInput_Direction(this MoveOrIdleComponent self, float3 directionInput)
        {
            bool bRet = await self._CreateMoveTimeLine();
            if (bRet == false)
            {
                return;
            }
            await self._SetMoveInput_Direction(directionInput);
        }

        public static async ETTask DoMoveInput_TargetPosition(this MoveOrIdleComponent self, float3 targetPositionInput)
        {
            bool bRet = await self._CreateMoveTimeLine();
            if (bRet == false)
            {
                return;
            }
            await self._SetMoveInput_TargetPosition(targetPositionInput);
        }

        public static async ETTask<bool> _CreateIdleTimeLine(this MoveOrIdleComponent self)
        {
            if (self.CurIdleTimelineObj != null)
            {
                return true;
            }
            if (self.CurMoveTimelineObj != null)
            {
                self.CurMoveTimelineObj.Dispose();
            }

            Unit unit = self.GetUnit();
            string idleTimelineId = UnitHelper.GetIdleTimeLineId(unit);
            if (string.IsNullOrEmpty(idleTimelineId))
            {
                return false;
            }

            ActionContext _ActionContext = new()
            {
                unitId = self.GetUnit().Id,
            };
            TimelineObj timelineObj = await ET.Ability.TimelineHelper.PlayTimeline(unit, unit.Id, idleTimelineId, _ActionContext);
            self.CurIdleTimelineObj = timelineObj;
            return true;
        }

        public static async ETTask<bool> _CreateMoveTimeLine(this MoveOrIdleComponent self)
        {
            if (self.CurMoveTimelineObj != null)
            {
                return true;
            }
            if (self.CurIdleTimelineObj != null)
            {
                self.CurIdleTimelineObj.Dispose();
            }

            Unit unit = self.GetUnit();
            string moveTimelineId = unit.model.MoveTimelineId;
            if (string.IsNullOrEmpty(moveTimelineId))
            {
                return false;
            }

            ActionContext _ActionContext = new()
            {
                unitId = self.GetUnit().Id,
            };
            TimelineObj timelineObj = await ET.Ability.TimelineHelper.PlayTimeline(unit, unit.Id, moveTimelineId, _ActionContext);
            self.CurMoveTimelineObj = timelineObj;
            return true;
        }
    }
}