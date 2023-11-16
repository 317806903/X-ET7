using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    ///
    /// </summary>
    public class BaseJwt
    {
        /// <summary>
        ///
        /// </summary>
        [Preserve]
        public BaseJwt() {}
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("exp")]
        public int ExpirationTimeUnix;
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("iat")]
        public int IssuedAtTimeUnix;
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("nbf")]
        public int NotBeforeTimeUnix;
        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public DateTime ExpirationTime => ConvertTimestamp(ExpirationTimeUnix);
        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public DateTime IssuedAtTime => ConvertTimestamp(IssuedAtTimeUnix);
        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public DateTime NotBeforeTime => ConvertTimestamp(NotBeforeTimeUnix);

        internal DateTime ConvertTimestamp(int timestamp)
        {
            if (timestamp != 0)
            {
                var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                return dateTimeOffset.DateTime;
            }

            throw new Exception("Token does not contain a value for this timestamp.");
        }
    }
}
