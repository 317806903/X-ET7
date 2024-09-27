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

            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletObj bulletObj = unit.GetComponent<BulletObj>();
                if (bulletObj == null)
                {
                    Log.Error($"bulletObj == null");
                    return;
                }
                if (bulletObj.model == null)
                {
                    Log.Error($"bulletObj.model[{bulletObj.CfgId}] == null");
                    return;
                }
                if (bulletObj.model.ResId_Ref == null)
                {
                    Log.Error($"bulletObj.model.ResId_Ref[{bulletObj.model.ResId}] == null");
                    return;
                }
                self.resName = bulletObj.model.ResId_Ref.ResName;
                self.resScale = bulletObj.model.ResScale;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeObj aoeObj = unit.GetComponent<AoeObj>();
                self.resName = aoeObj.model.ResId_Ref.ResName;
                self.resScale = aoeObj.model.ResScale;
            }
            else
            {
                self.resName = unit.model.ResId_Ref.ResName;
                self.resScale = unit.model.ResScale;
            }

        }

        public static Unit GetUnit(this GameObjectComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}