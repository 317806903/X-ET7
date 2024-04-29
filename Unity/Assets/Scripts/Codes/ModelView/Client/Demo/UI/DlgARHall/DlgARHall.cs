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
        public bool isCreateRooming;
        public ARHallType _ARHallType;
        public long roomId = 0;
        public string arSceneId;
        public RoomType RoomTypeIn;
        public SubRoomType SubRoomTypeIn;
        public string battleCfgId;
    }

    public enum ARHallType
    {
        /// <summary>
        /// 加入特定roomId房间
        /// </summary>
        JoinTheRoom,
        /// <summary>
        /// 保持当前roomId，进入重扫界面
        /// </summary>
        KeepRoomAndRescan,
        /// <summary>
        /// 拥有arSceneId,创建新roomId房间
        /// </summary>
        CreateRoomWithARSceneId,
        /// <summary>
        ///没有arSceneId,创建新roomId房间
        /// </summary>
        CreateRoomWithOutARSceneId,
        /// <summary>
        /// 进入扫描界面
        /// </summary>
        ScanQRCode,
    }

    public class DlgARHall_ShowWindowData : ShowWindowData
    {
        public ARHallType ARHallType;
        public RoomType RoomType;
        public SubRoomType SubRoomType;
        public long roomId;
        public string battleCfgId;
        public string arSceneId;
    }
}