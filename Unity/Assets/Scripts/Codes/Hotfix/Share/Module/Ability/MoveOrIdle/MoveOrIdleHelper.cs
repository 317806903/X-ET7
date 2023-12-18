using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class MoveOrIdleHelper
    {
        public static MoveOrIdleComponent _GetOrAddMoveOrIdleComponent(Unit unit)
        {
            MoveOrIdleComponent moveOrIdleComponent = unit.GetComponent<MoveOrIdleComponent>();
            if (moveOrIdleComponent == null)
            {
                moveOrIdleComponent = unit.AddComponent<MoveOrIdleComponent>();
            }
            return moveOrIdleComponent;
        }

        public static void StopMove(Unit unit)
        {
            MoveOrIdleComponent moveOrIdleComponent = _GetOrAddMoveOrIdleComponent(unit);
            moveOrIdleComponent.StopMove();
        }

        public static async ETTask DoIdle(Unit unit)
        {
            MoveOrIdleComponent moveOrIdleComponent = _GetOrAddMoveOrIdleComponent(unit);
            await moveOrIdleComponent.DoIdle();
        }

        public static async ETTask DoMoveDirection(Unit unit, float3 directionInput)
        {
            MoveOrIdleComponent moveOrIdleComponent = _GetOrAddMoveOrIdleComponent(unit);
            await moveOrIdleComponent.DoMoveInput_Direction(directionInput);
        }

        public static async ETTask DoMoveTargetPosition(Unit unit, float3 targetPositionInput)
        {
            MoveOrIdleComponent moveOrIdleComponent = _GetOrAddMoveOrIdleComponent(unit);
            await moveOrIdleComponent.DoMoveInput_TargetPosition(targetPositionInput);
        }
    }
}