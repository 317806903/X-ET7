using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf()]
    public class PlayerLocationChkComponent : Entity, IAwake, IDestroy, IFixedUpdate
    {
        [BsonIgnore]
        public int waitFrameChk = 300;
        [BsonIgnore]
        public int curFrameChk = 0;
        [BsonIgnore]
        public Dictionary<long, long> playerWaitQuitTimeTmp = new();
        public HashSet<long> existPlayerListTmp = new();

        public List<long> playerListIn = new();
        public HashSet<long> notExistPlayerList = new();
        public bool isChking;
    }
}