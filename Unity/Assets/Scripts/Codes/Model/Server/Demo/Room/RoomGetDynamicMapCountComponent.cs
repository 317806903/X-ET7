using System.Collections.Generic;

namespace ET.Server
{
	[ComponentOf(typeof(Scene))]
	public class RoomGetDynamicMapCountComponent : Entity, IAwake, IDestroy
	{
		public int CheckInteral = 120000;
		public long RepeatedTimer;
		public Dictionary<StartSceneConfig, int> DynamicMapId2Count;
	}
}