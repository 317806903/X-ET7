using System.Collections.Generic;

namespace ET
{
    public static class OpcodeHelper
    {
        [StaticField]
        private static readonly HashSet<ushort> ignoreDebugLogMessageSet = new HashSet<ushort>
        {
            OuterMessage.C2G_Ping,
            OuterMessage.G2C_Ping,
            OuterMessage.C2G_Benchmark,
            OuterMessage.G2C_Benchmark,
            OuterMessage.M2C_SyncPosUnits,
            OuterMessage.M2C_SyncNumericUnits,
            OuterMessage.M2C_SyncUnitEffects,
            OuterMessage.M2C_SyncPlayAudio,
            OuterMessage.M2C_SyncPlayAnimator,
            OuterMessage.M2C_Stop,
            OuterMessage.M2C_CreateUnits,
            OuterMessage.M2C_RemoveUnits,
            OuterMessage.M2C_PathfindingResult,
            OuterMessage.M2C_GamePlayCoinChgNotice,

            ushort.MaxValue, // ActorResponse
        };

        private static bool IsNeedLogMessage(ushort opcode)
        {
            if (ignoreDebugLogMessageSet.Contains(opcode))
            {
                return false;
            }

            return true;
        }

        public static bool IsOuterMessage(ushort opcode)
        {
            return opcode < OpcodeRangeDefine.OuterMaxOpcode;
        }

        public static bool IsInnerMessage(ushort opcode)
        {
            return opcode >= OpcodeRangeDefine.InnerMinOpcode;
        }

        public static void LogMsg(int zone, object message)
        {
            ushort opcode = NetServices.Instance.GetOpcode(message.GetType());
            if (!IsNeedLogMessage(opcode))
            {
                return;
            }

            Logger.Instance.Debug("zone: {0} {1}", zone, message);
        }

        public static void LogMsg(long actorId, object message)
        {
            ushort opcode = NetServices.Instance.GetOpcode(message.GetType());
            if (!IsNeedLogMessage(opcode))
            {
                return;
            }

            Logger.Instance.Debug("actorId: {0} {1}", actorId, message);
        }
    }
}