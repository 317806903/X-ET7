#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    /// Provides a set of utility functions for interacting with a web browser on iOS.
    /// </summary>
    class IOSBrowserUtils : IBrowserUtils
    {
        [DllImport("__Internal")]
        static extern void launchUrl(string url);
        [DllImport("__Internal")]
        static extern void dismiss();

        public Task LaunchUrl(string url)
        {
            launchUrl(url);
            return Task.CompletedTask;
        }

        public void Dismiss()
        {
            dismiss();
        }
    }
}
#endif
