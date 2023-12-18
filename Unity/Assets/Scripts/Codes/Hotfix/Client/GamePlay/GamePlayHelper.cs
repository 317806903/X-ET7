using Unity.Mathematics;

namespace ET.Client
{
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

		public static GamePlayPKComponent GetGamePlayPK(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayPKComponent>();
		}

		public static async ETTask SendARCameraPos(Scene scene, float3 ARCameraPosition, float3 ARCameraHitPosition)
		{
			C2M_SendARCameraPos _C2M_SendARCameraPos = new ()
			{
				ARCameraPosition = ARCameraPosition,
				ARCameraHitPosition = ARCameraHitPosition,
			};
			M2C_SendARCameraPos _M2C_SendARCameraPos = await ET.Client.SessionHelper.GetSession(scene).Call(_C2M_SendARCameraPos) as M2C_SendARCameraPos;
			if (_M2C_SendARCameraPos.Error != ET.ErrorCode.ERR_Success)
			{
				EventSystem.Instance.Publish(scene, new EventType.NoticeUITip()
				{
					tipMsg = _M2C_SendARCameraPos.Message,
				});
			}
		}
	}
}