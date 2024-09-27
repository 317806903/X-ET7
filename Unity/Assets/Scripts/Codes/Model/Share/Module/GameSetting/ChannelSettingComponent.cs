using System.Collections.Generic;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class ChannelSettingComponent : Entity, IAwake
	{
		[StaticField]
		public static ChannelSettingComponent Instance;

		public string channelId;
	}
}