using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LoginSDKManagerComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static LoginSDKManagerComponent Instance;

        public Action finishCallBack;
        public Action failCallBack;
    }
}