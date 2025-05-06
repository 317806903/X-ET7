
namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_DrawAllMonsterCall2HeadQuarterPathHandler : AMHandler<M2C_DrawAllMonsterCall2HeadQuarterPath>
    {
        protected override async ETTask Run(Session session, M2C_DrawAllMonsterCall2HeadQuarterPath message)
        {
            
            ClientEventType.NoticeRedrawAllPaths _noticeRedrawAllPaths = new()
            {
                pathToDraw = message
            };
            EventSystem.Instance.Publish(session.DomainScene().CurrentScene(), _noticeRedrawAllPaths);
            await ETTask.CompletedTask;
        }
    }
}
