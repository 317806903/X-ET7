using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class MailManagerComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, long> playerId2MailList = new();
	}
}