using System;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(OperaComponent))]
    public static class OperaComponentSystem
    {
        [ObjectSystem]
        public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
        {
            protected override void Awake(OperaComponent self)
            {
                self.mapMask = LayerMask.GetMask("Map");
            }
        }

        [ObjectSystem]
        public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
        {
            protected override void Update(OperaComponent self)
            {

                if (Input.GetKeyDown(KeyCode.R))
                {
                    CodeLoader.Instance.LoadHotfix();
                    EventSystem.Instance.Load();
                    Log.Debug("hot reload success!");
                }
            
                if (Input.GetKeyDown(KeyCode.T))
                {
                    C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
                    self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
                }
                
                if (Input.GetKeyDown(KeyCode.H))
                {
                    ET.Client.SkillHelper.LearnSkill(self.ClientScene(), "Skill_MachineGunTower1");
                    ET.Client.SkillHelper.CastSkill(self.ClientScene(), "Skill_MachineGunTower1");
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    ET.Client.SkillHelper.LearnSkill(self.ClientScene(), "Skill_RocketTower_1");
                    ET.Client.SkillHelper.CastSkill(self.ClientScene(), "Skill_RocketTower_1");
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    ET.Client.SkillHelper.LearnSkill(self.ClientScene(), "Skill_LaserTower_0");
                    ET.Client.SkillHelper.CastSkill(self.ClientScene(), "Skill_LaserTower_0");
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    ET.Client.SkillHelper.LearnSkill(self.ClientScene(), "Skill_EnergyPylon_2");
                    ET.Client.SkillHelper.CastSkill(self.ClientScene(), "Skill_EnergyPylon_2");
                }
            }
        }
    }
}