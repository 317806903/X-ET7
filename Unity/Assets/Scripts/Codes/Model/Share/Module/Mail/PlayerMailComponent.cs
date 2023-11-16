using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ChildOf(typeof(MailManagerComponent))]
	public class PlayerMailComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<MailType, long> mailList = new();
	}
}