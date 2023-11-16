using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PlayerStatusComponent))]
    public static class PlayerStatusComponentSystem
    {
        public static async ETTask NoticeClient(this PlayerStatusComponent self)
        {
            Player player = self.GetParent<Player>();
            if (player != null)
            {
                G2C_PlayerStatusChgNotice _G2C_PlayerStatusChgNotice = new()
                {
                    PlayerStatusComponentBytes = self.ToBson(),
                };
                player?.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_PlayerStatusChgNotice);
            }
            await ETTask.CompletedTask;
        }
    }
}