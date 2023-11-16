using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	public enum RankType: byte
	{
		PVE,
		EndlessChallenge,
	}

	[ComponentOf(typeof(Scene))]
	public class RankManagerComponent : Entity, IAwake, IDestroy, ISerializeToEntity
	{
	}
}