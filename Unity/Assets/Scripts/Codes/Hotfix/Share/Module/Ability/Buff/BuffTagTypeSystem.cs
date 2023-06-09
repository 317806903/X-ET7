using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (BuffObj))]
    public static class BuffTagTypeSystem
    {
        public static (bool bRet, string msg) ChkCanAdd(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            if (self.Children.Count <= 0)
            {
                return (true, "");
            }

            var result = self.ChkCanAddByTag(addBuffInfo);
            if (result.bRet == false)
            {
                return result;
            }
            var result2 = self.ChkCanAddByTagGroup(addBuffInfo);
            if (result2.bRet == false)
            {
                return result2;
            }
            return (true, "");
        }
        
        public static bool ChkIsEnabledByTagGroup(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            if (self.Children.Count <= 0)
            {
                return true;
            }

            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            if (buffCfg.TagGroup == null)
            {
                return true;
            }

            BuffTagGroupType tagGroup = buffCfg.TagGroup.Value;
            if (self.buffTagGroupTypeList[tagGroup].Count > 0)
            {
                foreach (BuffObj buffObj in self.buffTagGroupTypeList[tagGroup])
                {
                    if (buffObj.model.Priority > buffCfg.Priority)
                    {
                        string msg = $"buffId[{buffCfg.Id}] tagGroup[{tagGroup}] Priority[{buffCfg.Priority}] < buffId[{buffObj.CfgId}] Priority[{buffObj.model.Priority}]";
                        return false;
                    }
                    else
                    {
                        buffObj.SetEnabled(false);
                    }
                }
            }

            return true;
        }
        
        /// <summary>
        /// 激活同组的下一个buffObj
        /// </summary>
        /// <param name="self"></param>
        /// <param name="curBuffObj"></param>
        public static void DoEnabledTagGroup(this BuffComponent self, BuffObj curBuffObj)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            if (curBuffObj.model.TagGroup == null)
            {
                return;
            }

            BuffTagGroupType tagGroup = curBuffObj.model.TagGroup.Value;
            if (self.buffTagGroupTypeList[tagGroup].Count > 0)
            {
                BuffObj buffObjMaxPriority = null;
                int curPriority = -999;
                foreach (BuffObj buffObj in self.buffTagGroupTypeList[tagGroup])
                {
                    if (buffObj == curBuffObj)
                    {
                        continue;
                    }
                    if (buffObj.model.Priority > curPriority)
                    {
                        curPriority = buffObj.model.Priority;
                        buffObjMaxPriority = buffObj;
                    }
                }

                if (buffObjMaxPriority != null)
                {
                    buffObjMaxPriority.SetEnabled(true);
                }
            }
        }
        
        public static (bool bRet, string msg) ChkCanAddByTag(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            foreach (var tag in buffCfg.Tags)
            {
                if (self.buffImmuneTagTypeList[tag].Count > 0)
                {
                    foreach (BuffObj buffObjImmune in self.buffImmuneTagTypeList[tag])
                    {
                        if (buffObjImmune.isEnabled)
                        {
                            string msg = $"buffId[{buffCfg.Id}] tag[{tag}] is immune by buffId[{buffObjImmune.CfgId}]";
                            return (false, msg);
                        }
                    }
                }
            }
            return (true, "");
        }
        
        public static (bool bRet, string msg) ChkCanAddByTagGroup(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            if (buffCfg.TagGroup == null)
            {
                return (true, "");
            }

            BuffTagGroupType tagGroup = buffCfg.TagGroup.Value;
            if (self.buffImmuneTagGroupTypeList[tagGroup].Count > 0)
            {
                foreach (BuffObj buffObjImmune in self.buffImmuneTagGroupTypeList[tagGroup])
                {
                    if (buffObjImmune.isEnabled)
                    {
                        string msg = $"buffId[{buffCfg.Id}] tagGroup[{tagGroup}] is immune by buffId[{buffObjImmune.CfgId}]";
                        return (false, msg);
                    }
                }
            }
            
            return (true, "");
        }
        
        public static void AddBuffTagTypeList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var buffTagType in buffObj.model.Tags)
            {
                self.buffTagTypeList.Add(buffTagType, buffObj);
            }

            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Add(buffObj.model.TagGroup.Value, buffObj);
            }
        }
        
        public static void RemoveBuffTagTypeList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var buffTagType in buffObj.model.Tags)
            {
                self.buffTagTypeList.Remove(buffTagType, buffObj);
            }

            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Remove(buffObj.model.TagGroup.Value, buffObj);
            }
        }
        
        public static void AddBuffImmuneTagTypeList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var buffTagType in buffObj.model.ImmuneTags)
            {
                self.buffImmuneTagTypeList.Add(buffTagType, buffObj);
            }
            foreach (var buffTagGroupType in buffObj.model.ImmuneTagGroups)
            {
                self.buffImmuneTagGroupTypeList.Add(buffTagGroupType, buffObj);
            }
        }
        
        public static void RemoveBuffImmuneTagTypeList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var buffTagType in buffObj.model.ImmuneTags)
            {
                self.buffImmuneTagTypeList.Remove(buffTagType, buffObj);
            }
            foreach (var buffTagGroupType in buffObj.model.ImmuneTagGroups)
            {
                self.buffImmuneTagGroupTypeList.Remove(buffTagGroupType, buffObj);
            }
        }
        
        public static List<BuffObj> GetBuffListByTag(this BuffComponent self, BuffTagType buffTagType)
        {
            return self.buffTagTypeList[buffTagType];
        }
        
        public static List<BuffObj> GetBuffListByTagGroup(this BuffComponent self, BuffTagGroupType buffTagGroupType)
        {
            return self.buffTagGroupTypeList[buffTagGroupType];
        }
        
        public static bool ChkBuffTagType(this BuffComponent self, BuffTagType buffTagType)
        {
            List<BuffObj> list = self.GetBuffListByTag(buffTagType);
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