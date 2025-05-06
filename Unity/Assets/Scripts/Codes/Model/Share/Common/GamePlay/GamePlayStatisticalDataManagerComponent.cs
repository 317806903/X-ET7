using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayComponent))]
	public class GamePlayStatisticalDataManagerComponent : Entity, IAwake, IDestroy
	{
	}
}