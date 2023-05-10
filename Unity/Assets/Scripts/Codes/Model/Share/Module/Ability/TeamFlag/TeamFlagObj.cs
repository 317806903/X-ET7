using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
    public class TeamFlagObj: Entity, IAwake, IDestroy
    {
        ///<summary>
        /// 自身阵营
        ///</summary>
        public TeamFlagType selfTeamFlagType;
    }
}