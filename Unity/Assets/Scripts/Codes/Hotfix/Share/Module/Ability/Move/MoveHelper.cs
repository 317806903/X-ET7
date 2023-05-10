using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class MoveHelper
    {
        public static void AddMove(Unit unit, int moveCfgId)
        {
            unit.GetComponent<MoveComponent>().AddMove(moveCfgId);
        }
        
        public static void SetMoveInput(Unit unit, float3 moveDirectionInput)
        {
            unit.GetComponent<MoveComponent>().SetMoveInput(moveDirectionInput);
        }
    }
}