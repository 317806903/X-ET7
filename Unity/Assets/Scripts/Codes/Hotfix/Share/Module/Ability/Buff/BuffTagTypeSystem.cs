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
        /// <summary>
        /// 检测能否添加buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 判断是否在添加buff的同时移除现有buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
        public static (bool bRet, HashSet<BuffObj> buffObjList) ChkWillRemoveExist(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }

            HashSetComponent<BuffObj> removeExistBuffObjs = HashSetComponent<BuffObj>.Create();

            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            if (buffCfg.RemoveTags != null)
            {
                foreach (BuffTagType removeBuffTagType in buffCfg.RemoveTags)
                {
                    if (self.buffTagTypeList.TryGetValue(removeBuffTagType, out var list))
                    {
                        if (list != null)
                        {
                            foreach (BuffObj buffObj in list)
                            {
                                if (removeExistBuffObjs.Contains(buffObj) == false)
                                {
                                    removeExistBuffObjs.Add(buffObj);
                                }
                            }
                        }
                    }
                }
            }

            if (buffCfg.RemoveTagGroups != null)
            {
                foreach (BuffTagGroupType removeBuffTagGroupType in buffCfg.RemoveTagGroups)
                {
                    if (self.buffTagGroupTypeList.TryGetValue(removeBuffTagGroupType, out var list))
                    {
                        if (list != null)
                        {
                            foreach (BuffObj buffObj in list)
                            {
                                if (removeExistBuffObjs.Contains(buffObj) == false)
                                {
                                    removeExistBuffObjs.Add(buffObj);
                                }
                            }
                        }
                    }
                }
            }

            if (removeExistBuffObjs.Count == 0)
            {
                return (false, null);
            }
            return (true, removeExistBuffObjs);
        }

        /// <summary>
        /// 如果有同组TagGroup的，对优先级低的进行SetEnabled(false)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
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
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
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
            }

            return true;
        }

        /// <summary>
        /// 激活同组TagGroup的优先级最大的下一个buffObj,SetEnabled(true)
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
            if (curBuffObj.isEnabled == false)
            {
                return;
            }

            BuffTagGroupType tagGroup = curBuffObj.model.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
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
                        if (buffObj.duration <= 0)
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
        }

        /// <summary>
        /// 检测是否被当前挂载的某个buff所免疫
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
        public static (bool bRet, string msg) ChkCanAddByTag(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            foreach (var tag in buffCfg.Tags)
            {
                if (self.buffImmuneTagTypeList.ContainsKey(tag))
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
            }
            return (true, "");
        }

        /// <summary>
        /// 检测是否被当前挂载的某个buff所免疫
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
        public static (bool bRet, string msg) ChkCanAddByTagGroup(this BuffComponent self, AddBuffInfo addBuffInfo)
        {
            BuffCfg buffCfg = BuffCfgCategory.Instance.Get(addBuffInfo.BuffId);
            if (buffCfg.TagGroup == null)
            {
                return (true, "");
            }

            BuffTagGroupType tagGroup = buffCfg.TagGroup.Value;
            if (self.buffImmuneTagGroupTypeList.ContainsKey(tagGroup))
            {
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

        public static List<BuffObj> GetBuffListByTagType(this BuffComponent self, BuffTagType buffTagType)
        {
            if (self.buffTagTypeList.TryGetValue(buffTagType, out var buffObjList))
            {
                return buffObjList;
            }
            return null;
        }

        public static List<BuffObj> GetBuffListByTagGroupType(this BuffComponent self, BuffTagGroupType buffTagGroupType)
        {
            if (self.buffTagGroupTypeList.TryGetValue(buffTagGroupType, out var buffObjList))
            {
                return buffObjList;
            }
            return null;
        }

        public static bool ChkBuffByTagType(this BuffComponent self, BuffTagType buffTagType)
        {
            List<BuffObj> list = self.GetBuffListByTagType(buffTagType);
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

        public static bool ChkBuffByTagGroupType(this BuffComponent self, BuffTagGroupType buffTagGroupType)
        {
            List<BuffObj> list = self.GetBuffListByTagGroupType(buffTagGroupType);
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