using UnityEngine;
namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AppsflyerSDKComponent : Entity,IAwake,IDestroy
    {
        public MonoBehaviour AppsflyerSDKMono;
    }
}