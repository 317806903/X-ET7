using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum MailType: byte
    {
    }

    [ChildOf(typeof(PlayerMailComponent))]
    public class MailComponent : Entity, IAwake, IDestroy, ISerializeToEntity
    {
        public MailType mailType;
        public string mailTitle;
        public string mailContent;
        public List<long> itemList = new();
    }
}