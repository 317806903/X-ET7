namespace ET.Server
{

    public enum DBType
    {
        NoDB,
        MongoDB,
        LocalDB,
    }
    public class DBManagerComponent: Entity, IAwake, IDestroy
    {
        [StaticField]
        public static DBManagerComponent Instance;

        public DBType dbType;

        public DBComponent[] DBComponents = new DBComponent[IdGenerater.MaxZone];
    }
}