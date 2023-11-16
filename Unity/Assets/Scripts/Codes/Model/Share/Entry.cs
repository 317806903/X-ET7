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
            StartAsync().Coroutine();
        }

        private static async ETTask StartAsync()
        {
            WinPeriod.Init();

            MongoHelper.Init();
            ProtobufHelper.Init();

            Game.AddSingleton<NetServices>();
            Game.AddSingleton<Root>();

            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent1());
            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent3());
            await EventSystem.Instance.PublishAsync(Root.Instance.Scene, new EventType.EntryEvent2());
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
        NormalARCreate,
        NormalARScanCode,
        ARPVE,
        ARPVP,
        AREndlessChallenge,
        ARScanCode,
        ARTutorialFirst,
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
}