using Unity.Mathematics;

namespace ET.Client
{
    public static class UnitHelper
    {
        public static Unit GetMyUnit(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = currentScene.Parent.GetParent<Scene>();
            }

            PlayerComponent playerComponent = clientScene.GetComponent<PlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static async ETTask SendCallTower(Scene scene, string towerUnitCfgId, float3 position)
        {
            C2M_CallTower _C2M_CallTower = new ()
            {
                TowerUnitCfgId = towerUnitCfgId,
                Position = position,
            };
            M2C_CallTower _M2C_CallTower = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(_C2M_CallTower) as M2C_CallTower;
        }
        
        public static async ETTask SendCallTank(Scene scene, string tankUnitCfgId, float3 position)
        {
            C2M_CallTank _C2M_CallTank = new ()
            {
                TankUnitCfgId = tankUnitCfgId,
                Position = position,
            };
            M2C_CallTank _M2C_CallTank = await scene.ClientScene().GetComponent<SessionComponent>().Session.Call(_C2M_CallTank) as M2C_CallTank;
        }
        
        
    }
}