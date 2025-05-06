using System;
using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class GlobalConditionHelper
    {
        public static bool ChkConditionOne_Immediately(Scene scene, TriggerImmediatelyBase globalCondition,
        ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (globalCondition.TriggerType != abilityGameMonitorTriggerEvent)
            {
                return false;
            }
            if (globalCondition is TriggerImmediatelyNoParam TriggerImmediatelyNoParam)
            {
                return true;
            }
            else if (globalCondition is TriggerImmediatelyPutTower TriggerImmediatelyPutTower)
            {
                if (TriggerImmediatelyPutTower.TowerType != TowerType.None &&
                    TriggerImmediatelyPutTower.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyPutTower.TowerCfgId) == false &&
                    TriggerImmediatelyPutTower.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }
                return false;
            }
            else if (globalCondition is TriggerImmediatelyScaleTower TriggerImmediatelyScaleTower)
            {
                if (TriggerImmediatelyScaleTower.TowerType != TowerType.None &&
                    TriggerImmediatelyScaleTower.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyScaleTower.TowerCfgId) == false &&
                    TriggerImmediatelyScaleTower.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }
                return false;
            }
            else if (globalCondition is TriggerImmediatelyReclaimTower TriggerImmediatelyReclaimTower)
            {
                if (TriggerImmediatelyReclaimTower.TowerType != TowerType.None &&
                    TriggerImmediatelyReclaimTower.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyReclaimTower.TowerCfgId) == false &&
                    TriggerImmediatelyReclaimTower.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }
                return false;
            }
            else if (globalCondition is TriggerImmediatelyUpgradeTower TriggerImmediatelyUpgradeTower)
            {
                if (TriggerImmediatelyUpgradeTower.TowerType != TowerType.None &&
                    TriggerImmediatelyUpgradeTower.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyUpgradeTower.TowerCfgId) == false &&
                    TriggerImmediatelyUpgradeTower.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }
                return false;
            }
            else if (globalCondition is TriggerImmediatelyTowerKillMonster TriggerImmediatelyTowerKillMonster)
            {
                if (TriggerImmediatelyTowerKillMonster.TowerType != TowerType.None &&
                    TriggerImmediatelyTowerKillMonster.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyTowerKillMonster.TowerCfgId) == false &&
                    TriggerImmediatelyTowerKillMonster.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }

                long monsterUnitId = actionGameContext.defenderUnitId;
                Unit monsterUnit = UnitHelper.GetUnit(scene, monsterUnitId);
                if (monsterUnit == null)
                {
                    return false;
                }
                MonsterComponent monsterComponent = monsterUnit.GetComponent<MonsterComponent>();
                if (monsterComponent == null)
                {
                    return false;
                }

                if (TriggerImmediatelyTowerKillMonster.MonsterType != MonsterType.None &&
                    TriggerImmediatelyTowerKillMonster.MonsterType != monsterComponent.monsterType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(TriggerImmediatelyTowerKillMonster.MonsterCfgId) == false &&
                    TriggerImmediatelyTowerKillMonster.MonsterCfgId != monsterComponent.monsterCfgId)
                {
                    return false;
                }
                return false;
            }

            return false;
        }

        public static bool ChkConditionOne_Condition(Scene scene, TriggerChkConditionBase globalCondition, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (globalCondition is TriggerChkConditionImmediately TriggerChkConditionImmediately)
            {
                return ChkConditionOne_Immediately(scene, TriggerChkConditionImmediately.TriggerImmediatelyType, abilityGameMonitorTriggerEvent, ref actionGameContext);
            }
            else if (globalCondition is TriggerChkConditionStatus TriggerChkConditionStatus)
            {
                bool bChkTrig = false;
                foreach (TriggerImmediatelyBase triggerImmediatelyType in TriggerChkConditionStatus.TriggerImmediatelyTypes)
                {
                    bool bRet = ChkConditionOne_Immediately(scene, triggerImmediatelyType, abilityGameMonitorTriggerEvent, ref actionGameContext);
                    if (bRet)
                    {
                        bChkTrig = true;
                        break;
                    }
                }
                if (bChkTrig == false)
                {
                    return false;
                }

                return ChkConditionOne_ConditionStatus(scene, TriggerChkConditionStatus.ConditionStatusChk, ref actionGameContext);
            }

            return false;
        }

        public static bool ChkConditionOne_ConditionStatus(Scene scene, ConditionStatusChkBase conditionStatus, ref ActionGameContext actionGameContext)
        {
            if (conditionStatus is ConditionStatusChkTowerNum ConditionStatusChkTowerNum)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);

                if (ConditionStatusChkTowerNum.TowerType != TowerType.None &&
                    ConditionStatusChkTowerNum.TowerType != actionGameContext.towerType)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(ConditionStatusChkTowerNum.TowerCfgId) == false &&
                    ConditionStatusChkTowerNum.TowerCfgId != actionGameContext.towerCfgId)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool ChkCompare(string value, ConditionCompare conditionCompare, string conditionValue)
        {
            if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(float value, ConditionCompare conditionCompare, float conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(int value, ConditionCompare conditionCompare, int conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

        public static bool ChkCompare(int value, ConditionCompare conditionCompare, long conditionValue)
        {
            if (conditionCompare == ConditionCompare.gt)
            {
                return value > conditionValue;
            }
            else if (conditionCompare == ConditionCompare.eq)
            {
                return value == conditionValue;
            }
            else if (conditionCompare == ConditionCompare.lt)
            {
                return value < conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ge)
            {
                return value >= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.le)
            {
                return value <= conditionValue;
            }
            else if (conditionCompare == ConditionCompare.ne)
            {
                return value != conditionValue;
            }

            return false;
        }

    }
}