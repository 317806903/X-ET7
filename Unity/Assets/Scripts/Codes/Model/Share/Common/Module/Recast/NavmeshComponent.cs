using DotRecast.Detour.Crowd;
using MongoDB.Bson.Serialization.Attributes;
namespace ET
{
    [ComponentOf(typeof(NavmeshManagerComponent))]
    public class NavmeshComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        [BsonIgnore]
        public DtCrowd crowd;
        public float radius;
        public int waitFrameSyncPos = 0;
        public int curFrameSyncPos = 0;
    }
}