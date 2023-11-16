using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LoginSDKComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static LoginSDKComponent Instance;

        public Action finishCallBack;
    }
}