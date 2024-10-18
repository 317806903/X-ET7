using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_BuySkillEnergyHandler : AMActorLocationRpcHandler<Unit, C2M_BuySkillEnergy, M2C_BuySkillEnergy>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_BuySkillEnergy request, M2C_BuySkillEnergy response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			Scene scene = observerUnit.DomainScene();
			long unitId = request.unitId;
			string skillCfgId = request.SkillCfgId;
			Unit unit = Ability.UnitHelper.GetUnit(scene, unitId);

			long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(unit);
			if (playerId == -1)
			{
				Log.Error($"playerId == -1");
				return;
			}
			if (playerId != observerUnit.Id)
			{
				Log.Error($"playerId[{playerId}] != observerUnit.Id[{observerUnit.Id}]");
				return;
			}

			SkillConsumeCfg skillConsumeCfg = SkillConsumeCfgCategory.Instance.Get(skillCfgId);
			bool isCostGold = false;
			bool isCostDiamond = true;
			int costGold = 0;
			int costDiamond = skillConsumeCfg.ResetFullEnergyByCostDiamond;
			if (isCostGold)
			{
				float curGold = ET.GamePlayHelper.GetPlayerCoin(scene, playerId, CoinTypeInGame.Gold);
				if (curGold < costGold)
				{
					response.Error = ET.ErrorCode.ERR_LogicError;
					response.Message = $"Gold不足[{curGold}] < {costGold}";
					return;
				}
			}
			if (isCostDiamond)
			{
				int curDiamond = await PlayerCacheHelper.GetTokenDiamondByPlayerId(scene, playerId);
				if (curDiamond < costDiamond)
				{
					response.Error = ET.ErrorCode.ERR_LogicError;
					response.Message = $"Diamond不足[{curDiamond}] < {costDiamond}";
					return;
				}
			}

			(bool ret, string msg) = await SkillHelper.RestoreSkillEnergy(unit, skillCfgId);
			if (ret == false)
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			if (isCostGold)
			{
				ET.GamePlayHelper.ChgPlayerCoin(scene, playerId, CoinTypeInGame.Gold, -costGold);
			}
			if (isCostDiamond)
			{
				await PlayerCacheHelper.ReduceTokenDiamond(scene, playerId, costDiamond);
			}

			await ETTask.CompletedTask;
		}
	}
}