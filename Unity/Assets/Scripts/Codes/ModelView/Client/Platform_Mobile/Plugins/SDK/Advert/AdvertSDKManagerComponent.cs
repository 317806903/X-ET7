namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AdvertSDKManagerComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AdvertSDKManagerComponent Instance;

        public bool isAvailable;
    }
}