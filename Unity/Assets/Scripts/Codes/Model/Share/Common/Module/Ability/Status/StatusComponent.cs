using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof(StatusObj))]
    [ComponentOf(typeof(Unit))]
    public class StatusComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        // ///<summary>
        // //角色最终的可操作性状态
        // ///</summary>
        // public StatusObj _controlState;
        //
        // ///<summary>
        // ///GameTimeline专享的ChaControlState
        // ///</summary>
        // public StatusObj timelineControlState;

        public List<StatusObj> removeList;
    }
}