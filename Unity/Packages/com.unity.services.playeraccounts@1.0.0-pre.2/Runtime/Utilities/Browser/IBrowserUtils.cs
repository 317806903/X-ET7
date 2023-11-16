using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    /// Provides a set of utility functions for interacting with a web browser.
    /// </summary>
    internal interface IBrowserUtils
    {
        /// <summary>
        /// Launches the specified URL in a web browser.
        /// </summary>
        /// <param name="url">The URL to launch.</param>
        Task LaunchUrl(string url);

        public void Dismiss();
    }
}
