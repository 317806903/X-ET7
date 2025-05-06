using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class EventLoggingSDKComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static EventLoggingSDKComponent Instance;

        public bool IsOpenEventLogging;

        public bool enableLog;
        public bool enableAutoTrack;
        public string serverURL;
        public string appID;
    }
}