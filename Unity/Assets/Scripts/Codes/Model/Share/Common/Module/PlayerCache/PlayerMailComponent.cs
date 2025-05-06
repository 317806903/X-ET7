using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
    /// <summary>
    /// 邮件状态枚举
    /// </summary>
    public enum MailStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        UnRead,
        /// <summary>
        /// 已读没有领奖
        /// </summary>
        ReadAndNotGetItem,
        /// <summary>
        /// 已读并领奖
        /// </summary>
        ReadAndGetItem,
        /// <summary>
        /// 已读但没有奖
        /// </summary>
        ReadAndNoItem,
    }

    /// <summary>
    /// 邮件的处理方式枚举
    /// </summary>
    public enum DealMailType
    {
        ReadOnly,
        ReadAndGetItem,
    }

    /// <summary>
    /// 邮件的排序规则
    /// </summary>
    public enum MailSortRule
    {
        /// <summary>
        /// 按照接收邮件时间进行排序
        /// </summary>
        ReceivedTime,
        /// <summary>
        /// 按照邮件结束时间进行排序
        /// </summary>
        LimitTime,
    }

    [ComponentOf(typeof(PlayerDataComponent))]
    public class PlayerMailComponent : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 键是邮件ID，值保存着邮件的状态
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, MailStatus> mailStatus;
    }
}