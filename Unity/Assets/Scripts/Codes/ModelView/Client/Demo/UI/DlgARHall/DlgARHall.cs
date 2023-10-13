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
        public ARRoomType _ARRoomTypeIn;
    }

    public class DlgARHall_ShowWindowData : ShowWindowData
    {
        public PlayerStatus playerStatus;
        public ARRoomType _ARRoomType;
        public long arRoomId;
    }
}