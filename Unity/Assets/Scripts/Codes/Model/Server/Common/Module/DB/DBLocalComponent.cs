
namespace ET.Server
{
    /// <summary>
    /// 用来缓存数据
    /// </summary>
    [ChildOf(typeof(DBManagerComponent))]
    public class DBLocalComponent: Entity, IAwake, IDestroy
    {
        public string savePath;
    }
}