namespace ET.Client
{
	public static class SceneHelper
	{
		public static Scene GetCurrentScene(Scene scene)
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
				clientScene = scene.ClientScene();
			}

			return currentScene;
		}

		public static async ETTask EnterLogin(Scene scene, bool isFromInit = false)
		{
			Scene currentScene = scene.CurrentScene();
			if (currentScene != null)
			{
				currentScene.Dispose();
			}
			await EventSystem.Instance.PublishAsync(scene, new ClientEventType.EnterLoginSceneStart()
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
			await EventSystem.Instance.PublishAsync(scene, new ClientEventType.EnterHallSceneStart()
			{
				isFromLogin = isFromLogin,
				isRelogin = isRelogin,
			});
		}

		public static async ETTask EnterBattle(Scene scene)
		{
			await EventSystem.Instance.PublishAsync(scene, new ClientEventType.BattleSceneEnterStart());
		}

	}
}
