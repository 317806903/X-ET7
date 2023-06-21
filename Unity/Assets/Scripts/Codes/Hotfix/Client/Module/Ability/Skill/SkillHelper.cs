using System;


namespace ET.Client
{
    public static class SkillHelper
    {
        public static async ETTask LearnSkill(Scene clientScene, string skillId)
        {
            try
            {
                C2M_LearnSkill _C2MLearnSkill = new C2M_LearnSkill();
                _C2MLearnSkill.UnitId = clientScene.GetComponent<PlayerComponent>().MyId;
                _C2MLearnSkill.SkillId = skillId;
                M2C_LearnSkill _M2C_LearnSkill = await clientScene.GetComponent<SessionComponent>().Session.Call(_C2MLearnSkill) as M2C_LearnSkill;

            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        
        public static async ETTask CastSkill(Scene clientScene, string skillId)
        {
            try
            {
                C2M_CastSkill _C2M_CastSkill = new ();
                _C2M_CastSkill.UnitId = clientScene.GetComponent<PlayerComponent>().MyId;
                _C2M_CastSkill.SkillId = skillId;
                M2C_CastSkill _M2C_CastSkill = await clientScene.GetComponent<SessionComponent>().Session.Call(_C2M_CastSkill) as M2C_CastSkill;

            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}