using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class RoomManagerComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public HashSet<long> IdleRoomList;
		public HashSet<long> EnterBattleRoomList;
		public HashSet<long> InTheBattleRoomList;
		[BsonIgnore]
		public Dictionary<long, long> player2Room;
		[BsonIgnore]
		public Dictionary<long, (ARMeshType, string, string, string, byte[])> _ARMeshInfoDic;

		[BsonIgnore]
		public int waitFrameChk = 100;
		[BsonIgnore]
		public int curFrameChk = 0;
	}
}