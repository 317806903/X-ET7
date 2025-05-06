namespace ET
{
    namespace EventType
    {
        public struct EntryEvent1
        {
        }

        public struct EntryEvent2
        {
        }

        public struct EntryEvent3
        {
        }
    }

    public static class Entry
    {
        public static void Init()
        {

        }

        public static void Start()
        {
            Log.Debug($"ET.Entry.Start In]");
            StartAsync().Coroutine();
        }

        private static async ETTask StartAsync()
        {
            Log.Debug($"ET.Entry.StartAsync 1]");
            WinPeriod.Init();

            Log.Debug($"ET.Entry.StartAsync 2]");
            MongoHelper.Init();
            Log.Debug($"ET.Entry.StartAsync 3]");
            ProtobufHelper.Init();

            Log.Debug($"ET.Entry.StartAsync 4]");
            Game.AddSingleton<NetServices>();
            Log.Debug($"ET.Entry.StartAsync 5]");
            Game.AddSingleton<Root>();

            Log.Debug($"ET.Entry.StartAsync 6]");
            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent1());
            Log.Debug($"ET.Entry.StartAsync 7]");
            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent3());
            Log.Debug($"ET.Entry.StartAsync 8]");
            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent2());
            Log.Debug($"ET.Entry.StartAsync 9]");
        }
    }

    public enum RoomType
    {
        Normal,
        AR,
    }

    public enum SubRoomType
    {
        None,
        NormalSingleMap,
        NormalRoom,
        NormalPVE,
        NormalPVP,
        NormalEndlessChallenge,
        NormalScanMesh,
        NormalARCreate,
        NormalARScanCode,
        ARPVE,
        ARPVP,
        AREndlessChallenge,
        ARScanCode,
        ARTutorialFirst,
        ArcadeScanMesh,
    }

    /// <summary>
    /// 玩法当前所在步骤
    /// </summary>
    public enum PlayerStatus
    {
        Hall,
        Room,
        Battle,
    }

    /// <summary>
    /// 玩法当前所在步骤
    /// </summary>
    public enum PlayerId
    {
        PlayerNone,
    }

    public struct GetGameMapOrResScale
    {
        public bool isGetMapScale;
        public Scene scene;
        public bool isClient;
    }

}