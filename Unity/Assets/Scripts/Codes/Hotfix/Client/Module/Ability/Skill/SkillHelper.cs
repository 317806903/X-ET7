using System;


namespace ET.Client
{
    public static class SkillHelper
    {
        public static async ETTask LearnSkill(Scene clientScene, string skillId)
        {
            try
            {
                C2M_LearnSkill _C2MLearnSkill = new ();
                _C2MLearnSkill.SkillId = skillId;
                M2C_LearnSkill _M2C_LearnSkill = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2MLearnSkill) as M2C_LearnSkill;
                if (_M2C_LearnSkill.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _M2C_LearnSkill.Message,
                    });
                }
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
                _C2M_CastSkill.SkillId = skillId;
                M2C_CastSkill _M2C_CastSkill = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2M_CastSkill) as M2C_CastSkill;
                if (_M2C_CastSkill.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _M2C_CastSkill.Message,
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}