using System;
using System.Collections.Generic;
#if UNITY_ANDROID
using Google;
#endif

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LoginGoogleSDKComponent : Entity, IAwake, IDestroy
    {
        public LoginType loginType;
        public Action finishCallBack;
        public Action failCallBack;
        public string Token;
        public string Email;
#if UNITY_ANDROID        
        public GoogleSignInConfiguration configuration;
#endif
    }
}
