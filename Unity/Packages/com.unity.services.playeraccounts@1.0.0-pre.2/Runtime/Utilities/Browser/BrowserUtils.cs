using System;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    internal static class BrowserUtils
    {
        internal static IBrowserUtils CreateBrowserUtils(Action<string> onAuthCodeReceived)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            var standaloneBrowserUtils = new StandaloneBrowserUtils();
            standaloneBrowserUtils.AuthCodeReceivedEvent += onAuthCodeReceived;
            return standaloneBrowserUtils;
#elif UNITY_ANDROID
            return new AndroidBrowserUtils();
#elif UNITY_IOS
            return new IOSBrowserUtils();
#else
            Logger.LogError("Unsupported platform type");
            return null;
#endif
        }
    }
}
