using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class TeamFlagComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public long playerId;
        public TeamFlagType teamFlagType;
    }
}