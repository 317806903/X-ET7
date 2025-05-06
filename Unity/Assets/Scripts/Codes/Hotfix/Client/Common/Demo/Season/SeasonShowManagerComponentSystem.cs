using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(SeasonShowManagerComponent))]
    public static class SeasonShowManagerComponentSystem
    {
        [ObjectSystem]
        public class SeasonShowManagerComponentFixedUpdateSystem: FixedUpdateSystem<SeasonShowManagerComponent>
        {
            protected override void FixedUpdate(SeasonShowManagerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Client)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void FixedUpdate(this SeasonShowManagerComponent self, float fixedDeltaTime)
        {
            if (self.SeasonComponent == null)
            {
                self.GetSeasonInfo().Coroutine();
            }
            else
            {
                if (++self.curFrameChk >= self.waitFrameChk)
                {
                    self.curFrameChk = 0;

                    self.ChkSeasonRemainDes();
                }
            }
        }

        public static async ETTask GetSeasonInfo(this SeasonShowManagerComponent self)
        {
            if (self.isRequre)
            {
                return;
            }

            self.isRequre = true;
            await ET.Client.SeasonHelper.Init(self.DomainScene());

            int seasonIndex = ET.Client.SeasonHelper.GetSeasonIndex(self.DomainScene());
            if (seasonIndex != self.lastSeasonIndex)
            {
                self.lastSeasonIndex = seasonIndex;
                EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeUISeasonIndexChg());
                ET.Client.PlayerCacheHelper.SendChkSeasonIndexChg(self.DomainScene());
            }

            self.isRequre = false;
        }

        public static void ChkSeasonRemainDes(this SeasonShowManagerComponent self)
        {
            string seasonRemainDesc = ET.Client.SeasonHelper.GetSeasonLeftTime(self.DomainScene());
            if (seasonRemainDesc == self.lastRemainDes)
            {
                return;
            }

            self.lastRemainDes = seasonRemainDesc;
            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeUISeasonRemainChg());
        }

    }
}