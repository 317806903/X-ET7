using System;
using System.Collections.Generic;
using UnityEngine.Events;
using AppsFlyerSDK;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AppsflyerSDKComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AppsflyerSDKComponent Instance;
    }
}