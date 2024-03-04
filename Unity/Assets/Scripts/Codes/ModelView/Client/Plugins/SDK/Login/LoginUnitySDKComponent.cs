using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(LoginSDKManagerComponent))]
    public class LoginUnitySDKComponent : Entity, IAwake, IDestroy
    {
        public LoginType loginType;
        public Action finishCallBack;
    }
}