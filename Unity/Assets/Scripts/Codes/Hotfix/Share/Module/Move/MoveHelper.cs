using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public static class MoveHelper
    {
        public static void Stop(this Unit unit, int error)
        {
            MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            if (moveByPathComponent != null)
            {
                if (moveByPathComponent.IsArrived() == false)
                {
                    moveByPathComponent.Stop(error == WaitTypeError.Success);
                    unit.SendStop(error);
                }
            }
        }

        // error: 0表示协程走完正常停止
        public static void SendStop(this Unit unit, int error)
        {
            EventSystem.Instance.Publish(unit.DomainScene(), new EventType.StopMove()
            {
                unit = unit,
                error = error,
            });
        }
    }
}