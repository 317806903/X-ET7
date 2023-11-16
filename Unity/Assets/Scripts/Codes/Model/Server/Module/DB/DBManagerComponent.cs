namespace ET.Server
{

    public class DBManagerComponent: Entity, IAwake, IDestroy
    {
        [StaticField]
        public static DBManagerComponent Instance;

        public bool NeedDB
        {
            get;
            set;
        }

        public DBComponent[] DBComponents = new DBComponent[IdGenerater.MaxZone];
    }
}