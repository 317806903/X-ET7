using UnityEngine;
namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AppsflyerSDKComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AppsflyerSDKComponent Instance;
        public MonoBehaviour AppsflyerSDKMono;
    }
}