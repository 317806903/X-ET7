using System;
using System.Collections.Generic;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Interfaces;
using AppleAuth.Native;
#endif

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LoginAppleSDKComponent : Entity, IAwake, IDestroy, IUpdate
    {
        public LoginType loginType;
        public Action finishCallBack;
        public Action failCallBack;
        public string Token;
        public string Email;
#if UNITY_IOS
        public IAppleAuthManager m_AppleAuthManager;
        public string User;
        public string Error;
#endif
    }
}
