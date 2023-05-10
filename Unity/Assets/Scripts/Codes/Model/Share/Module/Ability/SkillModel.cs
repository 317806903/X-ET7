namespace ET.Ability
{
    ///<summary>
    ///策划填表的技能
    ///</summary>
    public struct SkillModel
    {
        ///<summary>
        ///技能的id
        ///</summary>
        public string id;

        // ///<summary>
        // ///技能使用的条件，这个游戏中只有资源需求，比如hp、ammo之类的
        // ///</summary>
        // public ChaResource condition;
        //
        // ///<summary>
        // ///技能的消耗，成功之后会扣除这些资源
        // ///</summary>
        // public ChaResource cost;

        ///<summary>
        ///技能的cd
        ///</summary>
        public float skillCD;

        ///<summary>
        ///技能的效果，必然是一个timeline
        ///</summary>
        public int timelineId;

        // ///<summary>
        // ///学会技能的时候，同时获得的buff
        // ///</summary>
        // public AddBuffInfo[] buff;

        // public SkillModel(string id, ChaResource cost, ChaResource condition, string effectTimeline, AddBuffInfo[] buff)
        // {
        //     this.id = id;
        //     this.cost = cost;
        //     this.condition = condition;
        //     this.effect = DesingerTables.Timeline.data[effectTimeline]; //SceneVariants.desingerTables.timeline.data[effectTimeline];
        //     this.buff = buff;
        // }
    }
}