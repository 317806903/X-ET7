namespace ET.Client
{
	public static class SceneHelper
	{
		public static async ETTask EnterHall(Scene scene)
		{
			await EventSystem.Instance.PublishAsync(scene, new EventType.HallSceneEnterStart());
			
		}
		
		public static async ETTask EnterBattle(Scene scene)
		{
			await EventSystem.Instance.PublishAsync(scene, new EventType.BattleSceneEnterStart());
			
		}
	}
}
