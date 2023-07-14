namespace ET.Client
{
    [FriendOf(typeof(GamePlayTowerDefenseComponent))]
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			Scene currentScene = scene.ClientScene().CurrentScene();
			return ET.GamePlayHelper.GetGamePlay(currentScene);
		}
		
		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayTowerDefenseComponent>();
		}
	}
}