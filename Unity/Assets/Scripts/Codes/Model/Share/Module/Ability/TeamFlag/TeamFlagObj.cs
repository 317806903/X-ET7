using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
    public class TeamFlagObj: Entity, IAwake<TeamFlagType>, IDestroy, ITransfer
    {
        ///<summary>
        /// 自身阵营
        ///</summary>
        public TeamFlagType teamFlagType;
    }
}