using System;
using MongoDB.Bson;
using Unity.Mathematics;

namespace ET.Client
{
    public static class SkillHelper
    {
        public static async ETTask ClientLearnSkill(Scene clientScene, string skillCfgId, long unitId)
        {
            try
            {
                C2M_LearnSkill _C2MLearnSkill = new ();
                _C2MLearnSkill.unitId = unitId;
                _C2MLearnSkill.SkillCfgId = skillCfgId;
                M2C_LearnSkill _M2C_LearnSkill = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2MLearnSkill) as M2C_LearnSkill;
                if (_M2C_LearnSkill.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _M2C_LearnSkill.Message,
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "SkillLearned",
                        properties = new()
                        {
                            { "unitId", unitId },
                            { "skill_id", skillCfgId },
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask<bool> CastSkill(Scene scene, bool isCameraSkill, string skillCfgId, long unitId, float3 cameraPosition, float3 cameraDirect, ET.Ability.SelectHandle selectHandle)
        {
            cameraPosition = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, cameraPosition);
            cameraDirect = ET.Ability.UnitHelper.TranClientForward2ServerForward(cameraDirect);
            if (selectHandle != null)
            {
                selectHandle.position = ET.Ability.UnitHelper.TranClientPos2ServerPos(scene, selectHandle.position);
                selectHandle.direction = ET.Ability.UnitHelper.TranClientForward2ServerForward(selectHandle.direction);
            }
            try
            {
                C2M_CastSkill _C2M_CastSkill = new ();
                _C2M_CastSkill.IsCameraSkill = isCameraSkill?1:0;
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
                M2C_CastSkill _M2C_CastSkill = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_CastSkill, false) as M2C_CastSkill;
                if (_M2C_CastSkill.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(scene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _M2C_CastSkill.Message,
                    });
                    return false;
                }
                else
                {
                    EventSystem.Instance.Publish(scene, new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "SkillUsed",
                        properties = new()
                        {
                            { "unitId", unitId },
                            { "skill_id", skillCfgId },
                        }
                    });
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
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _M2C_BuySkillEnergy.Message,
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "SkillRefiled",
                        properties = new()
                        {
                            { "unitId", unitId },
                            { "skill_id", skillCfgId },
                        }
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
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _M2C_RestoreSkillEnergy.Message,
                    });
                }
                else
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeEventLogging()
                    {
                        eventName = "SkillRestored",
                        properties = new()
                        {
                            { "unitId", unitId },
                            { "skill_id", skillCfgId },
                        }
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