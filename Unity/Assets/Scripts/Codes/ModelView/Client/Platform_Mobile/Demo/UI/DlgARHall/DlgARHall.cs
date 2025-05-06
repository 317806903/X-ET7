namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    public class DlgARHall: Entity, IAwake, IUILogic, IUIDlg
    {
        public UnityEngine.Transform GetUITransform { get => View.uiTransform; }
        public DlgARHallViewComponent View
        {
            get => this.GetComponent<DlgARHallViewComponent>();
        }

        public bool isHost;
        public bool isCreateRooming;
        public ARHallType _ARHallType;
        public long roomId = 0;
        public string arSceneId;
        public string arSceneMeshId;
        public RoomTypeInfo roomTypeInfo;
    }

    public class DlgARHall_ShowWindowData : ShowWindowData
    {
        public ARHallType ARHallType;
        public long roomId;
        public string arSceneId;
        public string arSceneMeshId;
        public RoomTypeInfo roomTypeInfo;
    }
}