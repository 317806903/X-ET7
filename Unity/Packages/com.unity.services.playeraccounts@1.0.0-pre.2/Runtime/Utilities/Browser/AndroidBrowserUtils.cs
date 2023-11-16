#if UNITY_ANDROID && !UNITY_EDITOR
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    /// Provides a set of utility functions for interacting with a web browser on Android.
    /// </summary>
    class AndroidBrowserUtils : IBrowserUtils
    {
        public Task LaunchUrl(string url)
        {
            Application.OpenURL(url);

            return Task.CompletedTask;
        }

        public void Dismiss()
        {
        }
    }
}
#endif
