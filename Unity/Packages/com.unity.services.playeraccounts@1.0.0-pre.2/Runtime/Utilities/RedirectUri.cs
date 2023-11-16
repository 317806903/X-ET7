using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    internal static class RedirectUri
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        static int? s_BoundPort = null;

        /// <summary>
        /// Binds to a free port and returns the port number.
        /// </summary>
        static int BindToFreePort()
        {
            HttpUtilities.TryBindListenerOnFreePort(out var httpListener, out var port);
            StandaloneBrowserUtils.httpListener = httpListener;
            s_BoundPort = port;
            return port;
        }

#endif

        /// <summary>
        /// Gets the redirect URI to use for Player Account authentication.
        /// </summary>
        internal static string GetRedirectUri()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            var port = s_BoundPort ?? BindToFreePort();
            return $"http://localhost:{port}/callback";
#elif UNITY_ANDROID || UNITY_IOS
            return $"{PlayerAccountSettings.DeepLinkUriScheme}://{(PlayerAccountSettings.UseCustomUri ? PlayerAccountSettings.DeepLinkUriHostPrefix : PlayerAccountSettings.DeepLinkUriHostPrefix + PlayerAccountSettings.CloudProjectId)}";
#else
            Debug.LogError("Unsupported platform type");
            return "";
#endif
        }
    }
}
