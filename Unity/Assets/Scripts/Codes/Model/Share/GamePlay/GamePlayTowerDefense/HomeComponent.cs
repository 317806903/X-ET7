using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class HomeComponent: Entity, IAwake, IDestroy, ITransferClient
    {
    }
}