namespace ET.Server
{
    [FriendOf(typeof(GamePlayTowerDefenseComponent))]
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			return ET.GamePlayHelper.GetGamePlay(scene);
		}
		
		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
		}
	}
}