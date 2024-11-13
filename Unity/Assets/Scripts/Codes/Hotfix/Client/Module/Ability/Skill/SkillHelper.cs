using System;
using MongoDB.Bson;
using Unity.Mathematics;

namespace ET.Client
{
    public static class SkillHelper
    {
        public static async ETTask LearnSkill(Scene clientScene, string skillCfgId)
        {
            try
            {
                C2M_LearnSkill _C2MLearnSkill = new ();
                _C2MLearnSkill.SkillCfgId = skillCfgId;
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

        public static async ETTask<bool> CastSkill(Scene clientScene, string skillCfgId, long unitId, float3 cameraPosition, float3 cameraDirect, ET.Ability.SelectHandle selectHandle)
        {
            try
            {
                C2M_CastSkill _C2M_CastSkill = new ();
                _C2M_CastSkill.SkillCfgId = skillCfgId;
                _C2M_CastSkill.unitId = unitId;
                _C2M_CastSkill.CameraPosition = cameraPosition;
                _C2M_CastSkill.CameraDirect = cameraDirect;
                if (selectHandle == null)
                {
                    _C2M_CastSkill.SelectHandleBytes = null;
                }
                else
                {
                    byte[] selectHandleBytes = selectHandle.ToBson();
                    _C2M_CastSkill.SelectHandleBytes = selectHandleBytes;
                }
                M2C_CastSkill _M2C_CastSkill = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2M_CastSkill) as M2C_CastSkill;
                if (_M2C_CastSkill.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _M2C_CastSkill.Message,
                    });
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public static async ETTask BuySkillEnergy(Scene clientScene, long unitId, string skillCfgId)
        {
            try
            {
                C2M_BuySkillEnergy _C2M_BuySkillEnergy = new ();
                _C2M_BuySkillEnergy.SkillCfgId = skillCfgId;
                _C2M_BuySkillEnergy.unitId = unitId;
                M2C_BuySkillEnergy _M2C_BuySkillEnergy = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2M_BuySkillEnergy) as M2C_BuySkillEnergy;
                if (_M2C_BuySkillEnergy.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _M2C_BuySkillEnergy.Message,
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask RestoreSkillEnergy(Scene clientScene, long unitId, string skillCfgId)
        {
            try
            {
                C2M_RestoreSkillEnergy _C2M_RestoreSkillEnergy = new ();
                _C2M_RestoreSkillEnergy.unitId = unitId;
                _C2M_RestoreSkillEnergy.SkillCfgId = skillCfgId;
                M2C_RestoreSkillEnergy _M2C_RestoreSkillEnergy = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2M_RestoreSkillEnergy) as M2C_RestoreSkillEnergy;
                if (_M2C_RestoreSkillEnergy.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _M2C_RestoreSkillEnergy.Message,
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