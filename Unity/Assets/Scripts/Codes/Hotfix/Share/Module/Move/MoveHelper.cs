using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public static class MoveHelper
    {
        // 可以多次调用，多次调用的话会取消上一次的协程
        public static async ETTask FindPathMoveToAsync(this Unit unit, float3 target, ETCancellationToken cancellationToken)
        {
            float speed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
            if (speed < 0.01)
            {
                cancellationToken?.Cancel();
                unit.SendStop(WaitTypeError.Cancel);
                return;
            }

            unit.GetComponent<PathfindingComponent>().SetMoveTarget(target);
            await ETTask.CompletedTask;

            // List<float3> list = ListComponent<float3>.Create();
            // unit.GetComponent<PathfindingComponent>().Find(unit.Position, target, list);
            //
            // if (list.Count < 2)
            // {
            //     cancellationToken?.Cancel();
            //     unit.SendStop(WaitTypeError.Timeout);
            //     return;
            // }
            //
            // unit.SendMovePointList(list);
            //
            // MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            //
            // bool ret = await moveByPathComponent.MoveToAsync(list, speed);
            // if (ret) // 如果返回false，说明被其它移动取消了，这时候不需要通知客户端stop
            // {
            //     //cancellationToken?.Cancel();
            //     unit.SendStop(WaitTypeError.Success);
            // }
        }

        public static void Stop(this Unit unit, int error)
        {
            ET.Ability.MoveOrIdleHelper.StopMove(unit);

            //
            // MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            // if (moveByPathComponent != null)
            // {
            //     if (moveByPathComponent.IsArrived() == false)
            //     {
            //         moveByPathComponent.Stop(error == WaitTypeError.Success);
            //         unit.SendStop(error);
            //     }
            // }
        }

        public static bool ChkIsMoveFinished(this Unit unit)
        {
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            return pathfindingComponent.ChkIsArrived();
            // MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            // if (moveByPathComponent != null)
            // {
            //     if (moveByPathComponent.IsArrived() == false)
            //     {
            //         return false;
            //     }
            // }
            // return true;
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

        public static void SendMovePointList(this Unit unit, List<float3> pointList)
        {
            EventSystem.Instance.Publish(unit.DomainScene(), new EventType.MovePointList()
            {
                unit = unit,
                pointList = pointList,
            });
        }
    }
}