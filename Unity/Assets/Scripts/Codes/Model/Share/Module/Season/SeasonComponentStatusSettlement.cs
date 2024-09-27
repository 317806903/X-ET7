using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
	[ComponentOf(typeof(SeasonComponent))]
	public class SeasonComponentStatusSettlement : Entity, IAwake, IDestroy, IFixedUpdate, ISerializeToEntity
	{
	}
}