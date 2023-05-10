using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class RotateHelper
    {
        public static void AddRotate(Unit unit, int rotateCfgId)
        {
            unit.GetComponent<RotateComponent>().AddRotate(rotateCfgId);
        }
        
        public static void SetRotateInput(Unit unit, float rotateDirectionInput)
        {
            unit.GetComponent<RotateComponent>().SetRotateInput(rotateDirectionInput);
        }
    }
}