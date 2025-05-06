using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET
{
	[ComponentOf(typeof(SeasonComponent))]
	public class SeasonComponentStatusInSeason : Entity, IAwake, IDestroy, IFixedUpdate, ISerializeToEntity
	{
		public int waitFrameChk = 60;
		public int curFrameChk = 0;
	}
}