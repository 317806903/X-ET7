namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class DlgARHall: Entity, IAwake, IUILogic
    {
        public DlgARHallViewComponent View
        {
            get => this.GetComponent<DlgARHallViewComponent>();
        }

        public bool isHost;
        public long roomId = 0;
        public string arSceneId;
        public PlayerStatus playerStatusIn;
        public RoomType RoomTypeIn;
        public SubRoomType SubRoomTypeIn;
        public string battleCfgId;
    }

    public class DlgARHall_ShowWindowData : ShowWindowData
    {
        public PlayerStatus playerStatus;
        public RoomType RoomType;
        public SubRoomType SubRoomType;
        public long arRoomId;
        public string battleCfgId;
    }
}