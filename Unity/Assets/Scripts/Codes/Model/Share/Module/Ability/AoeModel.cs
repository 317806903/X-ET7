using System.Collections.Generic;

namespace ET.Ability
{
    ///<summary>
    /// AoE的模板数据
    ///</summary>
    public struct AoeModel
    {
        public string id;

        ///<summary>
        ///aoe的视觉特效，如果是空字符串，就不会添加视觉特效
        ///这里需要的是在Prefabs/下的路径，因为任何东西都可以是视觉特效
        ///</summary>
        public string prefab;

        ///<summary>
        ///aoe是否碰撞到阻挡就摧毁了（removed），如果不是，移动就是smooth的，如果移动的话……
        ///</summary>
        public bool removeOnObstacle;

        ///<summmary>
        ///aoe的tag
        ///</summary>
        public string[] tags;

        ///<summary>
        ///aoe每一跳的时间，单位：秒
        ///如果这个时间小于等于0，或者没有onTick，则不会执行aoe的onTick事件
        ///</summary>
        public float tickTime;
        
        
        public Dictionary<AbilityAoeMonitorTriggerEvent, string> monitorTriggers;
        //
        //
        // ///<summary>
        // ///aoe创建时的事件
        // ///</summary>
        // public AoeOnCreate onCreate;
        //
        // ///<summary>
        // ///aoe创建的参数
        // ///</summary>
        // public object[] onCreateParams;
        //
        // ///<summary>
        // ///aoe每一跳的事件，如果没有，就不会发生每一跳
        // ///</summary>
        // public AoeOnTick onTick;
        //
        // public object[] onTickParams;
        //
        // ///<summary>
        // ///aoe结束时的事件
        // ///</summary>
        // public AoeOnRemoved onRemoved;
        //
        // public object[] onRemovedParams;
        //
        // ///<summary>
        // ///有角色进入aoe时的事件，onCreate时候位于aoe范围内的人不会触发这个，但是在onCreate里面会已经存在
        // ///</summary>
        // public AoeOnCharacterEnter onChaEnter;
        //
        // public object[] onChaEnterParams;
        //
        // ///<summary>
        // ///有角色离开aoe结束时的事件
        // ///</summary>
        // public AoeOnCharacterLeave onChaLeave;
        //
        // public object[] onChaLeaveParams;
        //
        // ///<summary>
        // ///有子弹进入aoe时的事件，onCreate时候位于aoe范围内的子弹不会触发这个，但是在onCreate里面会已经存在
        // ///</summary>
        // public AoeOnBulletEnter onBulletEnter;
        //
        // public object[] onBulletEnterParams;
        //
        // ///<summary>
        // ///有子弹离开aoe时的事件
        // ///</summary>
        // public AoeOnBulletLeave onBulletLeave;
        //
        // public object[] onBulletLeaveParams;

        // public AoeModel(
        // string id, string prefab, string[] tags, float tickTime, bool removeOnObstacle,
        // string onCreate, object[] onCreateParam,
        // string onRemoved, object[] onRemovedParam,
        // string onTick, object[] onTickParam,
        // string onChaEnter, object[] onChaEnterParam,
        // string onChaLeave, object[] onChaLeaveParam,
        // string onBulletEnter, object[] onBulletEnterParam,
        // string onBulletLeave, object[] onBulletLeaveParam
        // )
        // {
        //     this.id = id;
        //     this.prefab = prefab;
        //     this.tags = tags;
        //     this.tickTime = tickTime;
        //     this.removeOnObstacle = removeOnObstacle;
        //     this.onCreate = onCreate == ""? null : DesignerScripts.AoE.onCreateFunc[onCreate]; //DesignerScripts.AoE.onCreateFunc[onCreate];
        //     this.onCreateParams = onCreateParam;
        //     this.onRemoved = onRemoved == ""? null : DesignerScripts.AoE.onRemovedFunc[onRemoved];
        //     this.onRemovedParams = onRemovedParam;
        //     this.onTick = onTick == ""? null : DesignerScripts.AoE.onTickFunc[onTick];
        //     this.onTickParams = onTickParam;
        //     this.onChaEnter = onChaEnter == ""? null : DesignerScripts.AoE.onChaEnterFunc[onChaEnter];
        //     this.onChaEnterParams = onChaEnterParam;
        //     this.onChaLeave = onChaLeave == ""? null : DesignerScripts.AoE.onChaLeaveFunc[onChaLeave];
        //     this.onChaLeaveParams = onChaLeaveParam;
        //     this.onBulletEnter = onBulletEnter == ""? null : DesignerScripts.AoE.onBulletEnterFunc[onBulletEnter];
        //     this.onBulletEnterParams = onBulletEnterParam;
        //     this.onBulletLeave = onBulletLeave == ""? null : DesignerScripts.AoE.onBulletLeaveFunc[onBulletLeave];
        //     this.onBulletLeaveParams = onBulletLeaveParam;
        // }
    }
}