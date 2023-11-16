using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class RoomManagerComponent : Entity, IAwake, IDestroy
	{
		public HashSet<long> IdleRoomList;
		public HashSet<long> EnterBattleRoomList;
		public HashSet<long> InTheBattleRoomList;
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, long> player2Room;
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public Dictionary<long, string> _ARMeshDownLoadUrlDic;
	}
}