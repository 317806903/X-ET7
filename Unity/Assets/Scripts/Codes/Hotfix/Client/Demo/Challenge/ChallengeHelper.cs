using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Client
{
    public static class ChallengeHelper
    {
	    public static int GetChallengeLevelCount()
	    {
		    return TowerDefense_ChallengeLevelCfgCategory.Instance.GetAll().Count;
	    }
    }
}