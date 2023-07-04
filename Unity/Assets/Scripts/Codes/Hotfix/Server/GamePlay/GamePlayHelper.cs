namespace ET.Server
{
    [FriendOf(typeof(GamePlayComponent))]
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlayer(Scene scene)
		{
			return scene.GetComponent<GamePlayComponent>();
		}
	}
}