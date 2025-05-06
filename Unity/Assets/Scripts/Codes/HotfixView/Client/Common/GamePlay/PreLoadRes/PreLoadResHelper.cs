using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (Unit))]
    public static class PreLoadResHelper
    {
        public static async ETTask PreLoadRes_BattleCard(Scene scene)
        {
            PlayerBattleCardComponent playerBattleCardComponent = await PlayerCacheHelper.GetMyPlayerBattleCard(scene);
            if (playerBattleCardComponent.IsDisposed)
            {
                return;
            }

            using HashSetComponent<string> reloadUnitCfgIdHashSet = HashSetComponent<string>.Create();
            foreach (var towerCfgId in playerBattleCardComponent.itemCfgIdList)
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
                if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
                {
                    string unitCfgId = towerToUnitCfgId.CfgId;
                    reloadUnitCfgIdHashSet.Add(unitCfgId);
                }
                else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
                {
                    int count = towerToMonsterCfgId.CfgId.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string monsterCfgId = towerToMonsterCfgId.CfgId[i];
                        if (TowerDefense_MonsterCfgCategory.Instance.Contain(monsterCfgId) == false)
                        {
                            continue;
                        }
                        TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                        string unitCfgId = monsterCfg.UnitId;
                        reloadUnitCfgIdHashSet.Add(unitCfgId);
                    }
                }
            }

            int num = 0;
            foreach (string unitCfgId in reloadUnitCfgIdHashSet)
            {
                if (num++ > 5)
                {
                    num = 0;
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (scene.IsDisposed)
                    {
                        return;
                    }
                }
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                string resName = unitCfg.ResId_Ref.ResName;

                GameObject unitResGo = GameObjectPoolHelper.GetObjectFromPool(resName,false,1);
                GameObjectPoolHelper.ReturnObjectToPool(unitResGo);
            }
        }

        public static async ETTask PreLoadRes_TowerDefenseMonster(Scene scene, string towerDefenseCfgId)
        {
            using HashSetComponent<string> reloadUnitCfgIdHashSet = HashSetComponent<string>.Create();

            GamePlayTowerDefenseCfg gamePlayTowerDefenseCfg = GamePlayTowerDefenseCfgCategory.Instance.Get(towerDefenseCfgId);
            string monsterWaveCallRuleCfgId = gamePlayTowerDefenseCfg.MonsterWaveCallRule.MonsterWaveCallRuleCfgId;

            List<TowerDefense_MonsterWaveCallRuleCfg> list = TowerDefense_MonsterWaveCallRuleCfgCategory.Instance.DataList;

            for (int i = 0; i < list.Count; i++)
            {
                TowerDefense_MonsterWaveCallRuleCfg towerDefenseMonsterWaveCallRuleCfg = list[i];
                if (towerDefenseMonsterWaveCallRuleCfg.WaveRule == monsterWaveCallRuleCfgId)
                {
                    foreach (MonsterWaveCallNode monsterWaveCallNode in towerDefenseMonsterWaveCallRuleCfg.Nodes)
                    {
                        string unitCfgId = monsterWaveCallNode.MonsterCfgId_Ref.UnitId;
                        reloadUnitCfgIdHashSet.Add(unitCfgId);
                    }
                }
            }

            int num = 0;
            foreach (string unitCfgId in reloadUnitCfgIdHashSet)
            {
                if (num++ > 5)
                {
                    num = 0;
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (scene.IsDisposed)
                    {
                        return;
                    }
                }
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                string resName = unitCfg.ResId_Ref.ResName;

                GameObject unitResGo = GameObjectPoolHelper.GetObjectFromPool(resName, false, 1);
                GameObjectPoolHelper.ReturnObjectToPool(unitResGo);
            }
        }
    }
}