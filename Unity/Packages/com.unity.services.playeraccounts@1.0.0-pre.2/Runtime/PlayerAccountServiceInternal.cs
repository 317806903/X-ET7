using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Unity.Services.Core;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    class PlayerAccountServiceInternal : IPlayerAccountService
    {
        public event Action SignedIn;
        public event Action SignedOut;
        public event Action<RequestFailedException> SignInFailed;

        public bool IsSignedIn => SignInState == PlayerAccountState.Authorized || SignInState == PlayerAccountState.Refreshing;
        public string AccessToken { get; internal set; }
        public string IdToken { get; internal set; }
        public string RefreshToken { get; internal set; }
        public IdToken IdTokenClaims { get; internal set; }

        readonly IBrowserUtils m_BrowserUtils;
        readonly IDateTimeWrapper m_DateTime;
        readonly IJwtDecoder m_JwtDecoder;

        string m_RedirectUri;
        string m_CodeVerifier;
        string m_AccessToken;

        internal PlayerAccountState SignInState { get; set; }
        internal INetworkHandler NetworkingClient { get; set; }

        static HttpListener s_HttpListener;

        internal PlayerAccountServiceInternal(

            IJwtDecoder jwtDecoder,
            INetworkHandler networkingClient,
            IDateTimeWrapper dateTime
        )
        {
            m_BrowserUtils = BrowserUtils.CreateBrowserUtils(OnAuthCodeReceived);
            m_RedirectUri = RedirectUri.GetRedirectUri();
            m_JwtDecoder = jwtDecoder;
            m_DateTime = dateTime;
            NetworkingClient = networkingClient;
            SignInState = PlayerAccountState.SignedOut;

            Application.deepLinkActivated += OnDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
        }

        public async Task StartSignInAsync()
        {
            if (SignInState == PlayerAccountState.Authorized || SignInState == PlayerAccountState.Refreshing)
            {
                throw PlayerAccountsException.Create(PlayerAccountsErrorCodes.InvalidState, "Player is already signed in.");
            }

            if (string.IsNullOrEmpty(PlayerAccountSettings.ClientId))
            {
                throw PlayerAccountsException.Create(PlayerAccountsErrorCodes.MissingClientId, "The Client Id is not configured.");
            }

            SignInState = PlayerAccountState.SigningIn;

            try
            {
                // Open system browser to the OAuth 2.0 authorization endpoint.
                await m_BrowserUtils.LaunchUrl(AuthorizationRequest());
            }
            catch (PlayerAccountsException exception)
            {
                SendSignInFailedEvent(exception, true);
                throw;
            }
            catch (RequestFailedException exception)
            {
                SendSignInFailedEvent(new RequestFailedException(exception.ErrorCode,
                    "Error opening system browser for OAuth 2.0 authorization request."
                    ), true);
            }
        }

        string AuthorizationRequest()
        {
            // Generate OAuth 2.0 request parameters
            var challengeGenerator = new CodeChallengeGenerator();
            m_CodeVerifier = challengeGenerator.GenerateCode();
            var state = challengeGenerator.GenerateStateString();
            var codeChallenge = (CodeChallengeGenerator.S256EncodeChallenge(m_CodeVerifier));

            // Create OAuth 2.0 authorization request.
            var authorizationRequest =
                $"{PlayerAccountSettings.AuthUrl}?response_type=code&" +
                $"redirect_uri={Uri.EscapeDataString(m_RedirectUri)}&" +
                "response_mode=query&" +
                $"client_id={PlayerAccountSettings.ClientId}&" +
                $"state={state}&" +
                $"code_challenge={codeChallenge}&" +
                $"code_challenge_method={PlayerAccountSettings.CodeChallengeMethod}";

            if (!string.IsNullOrEmpty(PlayerAccountSettings.Scope))
            {
                authorizationRequest += $"&scope={PlayerAccountSettings.Scope}";
            }

            Logger.Log($"AuthorizationRequest URL: {authorizationRequest}");

            return authorizationRequest;
        }

        public Task RefreshTokenAsync()
        {
            if (!IsSignedIn)
            {
                throw new PlayerAccountsException(PlayerAccountsErrorCodes.InvalidState, "Player is not signed in.");
            }

            var refreshToken = RefreshToken;

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new PlayerAccountsException(PlayerAccountsErrorCodes.MissingRefreshToken, "Refresh token is null or empty.");
            }

            SignInState = PlayerAccountState.Refreshing;
            var refreshRequest =
                $"client_id={PlayerAccountSettings.ClientId}&" +
                $"refresh_token={refreshToken}&" +
                $"grant_type=refresh_token";

            if (!string.IsNullOrEmpty(PlayerAccountSettings.Scope))
            {
                refreshRequest += $"&scope={PlayerAccountSettings.Scope}";
            }

            return HandleSignInRequestAsync(() => NetworkingClient.PostAsync<SignInResponse>(PlayerAccountSettings.TokenUrl, refreshRequest));
        }

        public void SignOut()
        {
            AccessToken = null;
            var oldState = SignInState;
            SignInState = PlayerAccountState.SignedOut;

            if (oldState != PlayerAccountState.SigningIn)
                SignedOut?.Invoke();
        }

        void OnDeepLinkActivated(string url)
        {
            var uri = new Uri(url);
            
            var code = "";
            if (Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Query).Keys.Contains("code")) {
                code = Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Query)["code"];
            }
            var error = "";
            if (Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Query).Keys.Contains("error")) {
                error = Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Query)["error"];
            }

            if (string.IsNullOrEmpty(code))
            {
                var fragment = Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Fragment);
                code = fragment["code"];
                error ??= fragment["error"];
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw PlayerAccountsExceptionHandler.HandleError(error);
            }

#if UNITY_IOS
            m_BrowserUtils.Dismiss();
#endif
            OnAuthCodeReceived(code);
        }

        void OnAuthCodeReceived(string code)
        {
            SignInRequest(code, m_CodeVerifier, m_RedirectUri);
        }

        Task SignInRequest(string code, string codeVerifier, string redirectUri)
        {
            var signInRequestBody =
                $"code={code}&" +
                $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                $"client_id={PlayerAccountSettings.ClientId}&" +
                $"code_verifier={codeVerifier}&" +
                $"grant_type=authorization_code";

            return HandleSignInRequestAsync(() => NetworkingClient.PostAsync<SignInResponse>(PlayerAccountSettings.TokenUrl, signInRequestBody));
        }

        async Task HandleSignInRequestAsync(Func<Task<SignInResponse>> signInRequest)
        {
            try
            {
                SignInState = PlayerAccountState.SigningIn;
                var response = await signInRequest();
                CompleteSignIn(response);
            }
            catch (RequestFailedException exception)
            {
                SendSignInFailedEvent(exception, true);
                throw;
            }
            catch (WebRequestException exception)
            {
                var errorResponse = JsonConvert.DeserializeObject<PlayerAccountsErrorResponse>(exception.Message);
                var playerAccountsException = PlayerAccountsExceptionHandler.HandleError(errorResponse?.Error, errorResponse?.Description, exception);

                Logger.LogException(playerAccountsException);
                throw playerAccountsException;
            }
        }

        void SendSignInFailedEvent(RequestFailedException exception, bool forceSignOut)
        {
            SignInFailed?.Invoke(exception);
            if (forceSignOut)
            {
                SignOut();
            }
        }

        internal void CompleteSignIn(SignInResponse signInResponse)
        {
            AccessToken = signInResponse?.AccessToken;
            IdToken = signInResponse?.IdToken;
            if (IdToken != null)
            {
                IdTokenClaims = m_JwtDecoder.Decode<IdToken>(IdToken);
            }

            RefreshToken = signInResponse?.RefreshToken;
            SignInState = PlayerAccountState.Authorized;
            SignedIn?.Invoke();
        }
    }
}
