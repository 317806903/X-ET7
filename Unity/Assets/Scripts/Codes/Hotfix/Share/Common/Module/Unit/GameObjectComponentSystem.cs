using System;
using System.Collections.Generic;
using ET.Ability;

namespace ET
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectComponent>
        {
            protected override void Awake(GameObjectComponent self)
            {
                self.Init();
            }
        }

        public static void Init(this GameObjectComponent self)
        {
            Unit unit = self.GetUnit();

            string unitCfgId = unit.CfgId;

            self.orgUnitCfgId = unitCfgId;
            self.curUnitCfgId = unitCfgId;
            float resScale = ET.Ability.UnitHelper.GetUnitResScale(unit);
            string resName = ET.Ability.UnitHelper.GetResName(unit);

            self.resName = resName;
            self.resScale = resScale;
        }

        public static void Reset(this GameObjectComponent self, string newUnitCfgId)
        {
            self.curUnitCfgId = newUnitCfgId;
            float resScale = ET.Ability.UnitHelper.GetUnitResScale(newUnitCfgId);
            string resName = ET.Ability.UnitHelper.GetResName(newUnitCfgId);

            self.resName = resName;
            self.resScale = resScale;
        }

        public static Unit GetUnit(this GameObjectComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}