using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    /// Unity Player Accounts Settings
    /// </summary>
    public class PlayerAccountSettings : ScriptableObject
    {
        static PlayerAccountSettings s_Instance;

        /// <summary>
        /// Unity Player Accounts Client ID.
        /// </summary>
        [SerializeField]
        [Tooltip("Unity Player Account Client ID.")]
        public string clientId;

        /// <summary>
        /// Scope mask, defaults to all scopes selected
        /// </summary>
        [HideInInspector]
        [SerializeField]
        internal int scopeMask = CalculateAllScopesValue();

        /// <summary>
        /// useCustomDeepLinkUri to override deep link uri'
        /// </summary>
        [HideInInspector]
        [SerializeField]
        [Tooltip("Override the default redirect uri")]
        public bool useCustomDeepLinkUri;

        /// <summary>
        /// custom scheme.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        [Tooltip("Custom Deep Link URI Scheme")]
        public string customScheme;

        /// <summary>
        /// Custom host.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        [Tooltip("Custom Deep Link URI Host Prefix")]
        public string customHost;

        /// <summary>
        /// Supported scopes dictionary mapping enums to strings
        /// </summary>
        static readonly Dictionary<SupportedScopesEnum, string> k_SupportedScopesDictionary = new Dictionary<SupportedScopesEnum, string>
        {
            { SupportedScopesEnum.OpenId, "openid" },
            { SupportedScopesEnum.Email, "email" },
            { SupportedScopesEnum.OfflineAccess, "offline_access" }
        };

        /// <summary>
        /// Supported scope enums. 'All' and 'Empty' options grant all available scopes.
        /// </summary>
        [Flags]
        public enum SupportedScopesEnum
        {
            /// <summary>
            /// Represents an empty set of scopes. This is used when no scope is specified.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// The OpenID scope. It provides authentication-related scopes, typically used for single sign-on.
            /// </summary>
            OpenId = 1 << 0,

            /// <summary>
            /// The Email scope. This scope is used when the application needs to access the user's email.
            /// </summary>
            Email = 1 << 1,

            /// <summary>
            /// The OfflineAccess scope. This scope is used to get a refresh token that can be used to maintain access to resources when the user is not logged in.
            /// </summary>
            OfflineAccess = 1 << 2,

            /// <summary>
            /// Represents all available scopes. It is used when the application needs to access all resources.
            /// </summary>
            All = ~0
        }

        /// <summary>
        /// Scope Flags
        /// </summary>
        public SupportedScopesEnum ScopeFlags
        {
            get => (SupportedScopesEnum)scopeMask;
            set => scopeMask = (int)value;
        }

        string m_CloudProjectId;
        const string k_AuthUrl = "https://player-login.unity.com/v1/oauth2/auth";
        const string k_AccountPortalUrl = "https://player-account.unity.com";
        const string k_TokenUrl = "https://player-login.unity.com/v1/oauth2/token";
        const string k_DeepLinkUriScheme = "unitydl";
        const string k_DeepLinkUriHostPrefix = "com.unityplayeraccounts.";
        const string k_CodeChallengeMethod = "S256";

        /// <summary>
        /// Unity Player Accounts Client ID.
        /// </summary>
        public static string ClientId
        {
            get
            {
                var trimmedClientId = Instance.clientId.Trim();
                return string.IsNullOrEmpty(trimmedClientId) ? null : trimmedClientId;
            }
            set => Instance.clientId = value.Trim();
        }

        /// <summary>
        /// The scope of access that your player account requires. Example: 'openid;email'
        /// </summary>
        public static string Scope
        {
            get
            {
                if (Instance.ScopeFlags == SupportedScopesEnum.Empty)
                {
                    return "";
                }

                var scope = "";
                var scopeFlags = Instance.ScopeFlags;

                foreach (var kvp in k_SupportedScopesDictionary)
                {
                    if (scopeFlags.HasFlag(kvp.Key))
                    {
                        scope += kvp.Value + ";";
                    }
                }

                return scope.TrimEnd(';');
            }
        }

        /// <summary>
        /// The Cloud project Id associated with the current project
        /// </summary>
        public static string CloudProjectId
        {
            get => Instance.m_CloudProjectId;
            set => Instance.m_CloudProjectId = value;
        }

        /// <summary>
        /// Returns true if using a custom uri
        /// </summary>
        public static bool UseCustomUri => Instance.useCustomDeepLinkUri;

        /// <summary>
        /// Scheme for the deep link Uri for Android and iOS platforms.
        /// </summary>
        public static string DeepLinkUriScheme => Instance.useCustomDeepLinkUri ? Instance.customScheme : k_DeepLinkUriScheme;

        /// <summary>
        /// Prefix value for the deep link Uri Host name for Android and iOS platforms.
        /// </summary>
        public static string DeepLinkUriHostPrefix => Instance.useCustomDeepLinkUri ? Instance.customHost : k_DeepLinkUriHostPrefix;

        /// <summary>
        /// The authorization endpoint URL.
        /// </summary>
        public static string AuthUrl => k_AuthUrl;

        /// <summary>
        /// The account portal URL for managing player's account and privacy features, including delete account.
        /// </summary>
        public static string AccountPortalUrl => k_AccountPortalUrl;

        /// <summary>
        /// The token endpoint URL.
        /// </summary>
        public static string TokenUrl => k_TokenUrl;

        /// <summary>
        /// The code challenge method.
        /// </summary>
        public static string CodeChallengeMethod => k_CodeChallengeMethod;

        /// <summary>
        /// The instance of the PlayerAccountSettings class.
        /// </summary>
        public static PlayerAccountSettings Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = Resources.Load<PlayerAccountSettings>(nameof(PlayerAccountSettings));
                }

                return s_Instance;
            }
        }

        /// <summary>
        /// Calculates the default value for the scopeMask by combining all the individual scope values.
        /// </summary>
        /// <returns>The default value for the scopeMask.</returns>
        static int CalculateAllScopesValue()
        {
            var allScopesValue = 0;
            foreach (SupportedScopesEnum scopeEnum in Enum.GetValues(typeof(SupportedScopesEnum)))
            {
                if (scopeEnum != SupportedScopesEnum.Empty && scopeEnum != SupportedScopesEnum.All)
                {
                    allScopesValue |= (int)scopeEnum;
                }
            }

            return allScopesValue;
        }
    }
}
