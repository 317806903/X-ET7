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
            List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext);
            if (list == null)
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                DoCoinAdd(list[i], actionCfgCoinAdd);
            }
        }

        public static void DoCoinAdd(Unit unit, ActionCfg_CoinAdd actionCfgCoinAdd)
        {
            long playerId = GamePlayHelper.GetPlayerIdByUnitId(unit);
            if (playerId == -1)
            {
                return;
            }
            if (GamePlayHelper.ChkCanDoCoinAdd(unit.DomainScene()) == false)
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