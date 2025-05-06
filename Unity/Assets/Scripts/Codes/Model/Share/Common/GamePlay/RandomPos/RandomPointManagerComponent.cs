using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class RandomPointManagerComponent : Entity, IAwake, IDestroy
	{
	}
}