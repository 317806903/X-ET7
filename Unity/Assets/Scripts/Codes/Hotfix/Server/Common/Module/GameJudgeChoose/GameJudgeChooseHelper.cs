using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class GameJudgeChooseHelper
    {
	    public static GameJudgeChooseManagerComponent GetGameJudgeChooseManager(Scene scene)
	    {
		    GameJudgeChooseManagerComponent gameJudgeChooseManagerComponent = scene.GetComponent<GameJudgeChooseManagerComponent>();
		    if (gameJudgeChooseManagerComponent == null)
		    {
			    gameJudgeChooseManagerComponent = scene.AddComponent<GameJudgeChooseManagerComponent>();
		    }
		    return gameJudgeChooseManagerComponent;
	    }

        public static async ETTask RecordGameJudgeChoose(Scene scene, long playerId, GameJudgeChooseType gameJudgeChooseType, string complainMsg)
        {
	        GameJudgeChooseManagerComponent gameJudgeChooseManagerComponent = GetGameJudgeChooseManager(scene);
	        await gameJudgeChooseManagerComponent.RecordGameJudgeChoose(playerId, gameJudgeChooseType, complainMsg);
        }

        public static async ETTask<GameJudgeChooseComponent> GetGameJudgeChooseComponent(Scene scene, long playerId)
        {
	        GameJudgeChooseManagerComponent gameJudgeChooseManagerComponent = GetGameJudgeChooseManager(scene);
	        GameJudgeChooseComponent GameJudgeChooseComponent = await gameJudgeChooseManagerComponent.GetGameJudgeChooseComponent(playerId);
	        return GameJudgeChooseComponent;
        }

        public static async ETTask<bool> ChkNeedShow(Scene scene, long playerId)
        {
	        GameJudgeChooseComponent gameJudgeChooseComponent = await GetGameJudgeChooseComponent(scene, playerId);
	        return gameJudgeChooseComponent.ChkNeedShow();
        }
    }
}