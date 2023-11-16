using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    /// Represents the Player Account Service.
    /// </summary>
    public interface IPlayerAccountService
    {
        /// <summary>
        /// Invoked when a sign-in attempt has completed successfully.
        /// </summary>
        event Action SignedIn;

        /// <summary>
        /// Invoked when a sign-out attempt has completed successfully.
        /// </summary>
        event Action SignedOut;

        /// <summary>
        /// Invoked when a sign-in attempt has failed. The reason for failure is passed as the parameter
        /// <see cref="RequestFailedException"/>
        /// </summary>
        event Action<RequestFailedException> SignInFailed;

        /// <summary>
        /// Gets the access token that was obtained during sign-in.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Gets the ID token that was obtained during sign-in.
        /// </summary>
        string IdToken { get; }

        /// <summary>
        /// Gets the claims from the ID token that was obtained during sign-in.
        /// </summary>
        IdToken IdTokenClaims { get; }

        /// <summary>
        /// Checks whether the player is signed in or not.
        /// A player can remain signed in but have an expired session.
        /// </summary>
        /// <returns>Returns true if player is signed in, else false.</returns>
        bool IsSignedIn { get; }

        /// <summary>
        /// Starts the sign in flow by launching the system browser to sign in the current player.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// To be notified of the sign-in outcome, including the retrieval of tokens, the developer must subscribe to the <see cref="SignedIn"/> event.
        /// </remarks>
        Task StartSignInAsync();

        /// <summary>
        /// Refreshes the current access token using the refresh token.
        /// </summary>
        /// <returns></returns>
        Task RefreshTokenAsync();

        /// <summary>
        /// Signs out the current player and revokes the access token.
        /// </summary>
        void SignOut();
    }
}
