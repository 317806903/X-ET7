namespace ET.Client
{
	public static class SceneHelper
	{
		public static async ETTask EnterLogin(Scene scene, bool isFromInit = false)
		{
			Scene currentScene = scene.CurrentScene();
			if (currentScene != null)
			{
				currentScene.Dispose();
			}
			await EventSystem.Instance.PublishAsync(scene, new EventType.EnterLoginSceneStart()
			{
				isFromInit = isFromInit,
			});
		}

		public static async ETTask EnterHall(Scene scene, bool isFromLogin = false, bool isRelogin = false)
		{
			Scene currentScene = scene.CurrentScene();
			if (currentScene != null)
			{
				currentScene.Dispose();
			}
			await EventSystem.Instance.PublishAsync(scene, new EventType.EnterHallSceneStart()
			{
				isFromLogin = isFromLogin,
				isRelogin = isRelogin,
			});
		}

		public static async ETTask EnterBattle(Scene scene)
		{
			await EventSystem.Instance.PublishAsync(scene, new EventType.BattleSceneEnterStart());
		}

	}
}
