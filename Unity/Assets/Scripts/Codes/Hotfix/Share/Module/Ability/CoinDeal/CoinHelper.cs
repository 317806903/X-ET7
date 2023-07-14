using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public static class CoinHelper
    {
        public static void DealCoinAdd(Unit unit, ActionCfg_CoinAdd actionCfgCoinAdd, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (selectHandle.selectHandleType != SelectHandleType.SelectUnits)
            {
                Log.Error($"ET.Ability.DamageHelper.DoDamage selectHandle.selectHandleType[{selectHandle.selectHandleType}] != SelectHandleType.SelectUnits");
                return;
            }

            for (int i = 0; i < selectHandle.unitIds.Count; i++)
            {
                Unit targetUnit = UnitHelper.GetUnit(unit.DomainScene(), selectHandle.unitIds[i]);
                if (UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    continue;
                }
                DoCoinAdd(targetUnit, actionCfgCoinAdd);
            }
        }
        
        public static void DoCoinAdd(Unit unit, ActionCfg_CoinAdd actionCfgCoinAdd)
        {
            long playerId = GamePlayHelper.GetPlayerIdByUnitId(unit);
            if (playerId == -1)
            {
                return;
            }
            foreach (var coinAdd in actionCfgCoinAdd.CoinAdd)
            {
                CoinType coinType = coinAdd.Key;
                int chgValue = coinAdd.Value;
                GamePlayHelper.ChgPlayerCoin(unit.DomainScene(), playerId, coinType, chgValue);
            }
        }
        
    }
}