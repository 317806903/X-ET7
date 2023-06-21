using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PlayerStatusComponent))]
    public static class PlayerStatusComponentSystem
    {
        public static async ETTask NoticeClient(this PlayerStatusComponent self)
        {
            G2C_PlayerStatusChgNotice _G2C_PlayerStatusChgNotice = new()
            {
                PlayerStatus = self.PlayerStatus.ToString(),
                RoomId = self.RoomId,
            };
            Player player = self.GetParent<Player>();
            player?.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_PlayerStatusChgNotice);
            await ETTask.CompletedTask;
        }
    }
}