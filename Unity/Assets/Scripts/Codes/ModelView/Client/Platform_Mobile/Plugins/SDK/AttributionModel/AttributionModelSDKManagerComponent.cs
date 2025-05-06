namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AttributionModelSDKManagerComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AttributionModelSDKManagerComponent Instance;

        public bool isAvailable;
    }
}