namespace ET.Client
{
    public static class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene clientScene, string sceneName, long sceneInstanceId)
        {
            //clientScene.RemoveComponent<AIComponent>();
            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 11");

            CurrentScenesComponent currentScenesComponent = clientScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, clientScene.Zone, sceneName, currentScenesComponent);
            ET.SceneHelper.InitWhenClient(currentScene);
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();

            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 22");
            // 可以订阅这个事件中创建Loading界面
            SceneHelper.EnterBattle(clientScene).Coroutine();

            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 33");
            // 等待CreateMyUnit的消息
            Wait_CreateMyUnit waitCreateMyUnit = await clientScene.GetComponent<ObjectWait>().Wait<Wait_CreateMyUnit>();
            M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;
            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 44");

            UnitInfo unitInfo = m2CCreateMyUnit.Unit;
            Unit unit = unitComponent.Get(unitInfo.UnitId);
            if (unit != null)
            {
                UnitFactory.ReplaceComponent(unit, unitInfo);
            }
            else
            {
                unit = UnitFactory.Create(unitComponent, unitInfo);
            }

            //clientScene.RemoveComponent<AIComponent>();

            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 55");
            EventSystem.Instance.Publish(currentScene, new EventType.BattleSceneEnterFinish());
            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 66");
            // 通知等待场景切换的协程
            clientScene.GetComponent<ObjectWait>().Notify(new Wait_SceneChangeFinish());
            Log.Debug("ET.Client.SceneChangeHelper.SceneChangeTo 77");
        }
    }
}