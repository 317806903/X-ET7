using System.Collections.Generic;
using System.Linq;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class RoomManagerComponent : Entity, IAwake, IDestroy
	{
		public HashSet<long> IdleRoomList;
		public HashSet<long> EnterBattleRoomList;
		public HashSet<long> InTheBattleRoomList;
		public Dictionary<long, long> player2Room;
		public Dictionary<long, string> _ARMeshDownLoadUrlDic;
	}
}