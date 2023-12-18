using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (BuffObj))]
    public static class BuffTypeSystem
    {
        public static void AddBuffTypeList(this BuffComponent self, BuffObj buffObj)
        {
            self.buffTypeList.Add(buffObj.model.BuffType, buffObj);
        }

        public static void RemoveBuffTypeList(this BuffComponent self, BuffObj buffObj)
        {
            self.buffTypeList.Remove(buffObj.model.BuffType, buffObj);
        }

        public static List<BuffObj> GetBuffListByBuffType(this BuffComponent self, BuffType buffType)
        {
            return self.buffTypeList[buffType];
        }

        public static bool ChkBuffByBuffType(this BuffComponent self, BuffType buffType)
        {
            List<BuffObj> list = self.GetBuffListByBuffType(buffType);
            if (list == null)
            {
                return false;
            }
            foreach (BuffObj buffObj in list)
            {
                if (buffObj.isEnabled)
                {
                    return true;
                }
            }
            return false;
        }
    }
}