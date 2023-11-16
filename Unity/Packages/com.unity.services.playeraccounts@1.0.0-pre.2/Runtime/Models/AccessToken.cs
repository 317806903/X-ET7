using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.PlayerAccounts
{
    class AccessToken : BaseJwt
    {
        [Preserve]
        public AccessToken() {}

        [JsonProperty("aud")]
        public string[] Audience;

        [JsonProperty("client_id")]
        public string ClientId;

        [JsonProperty("iss")]
        public string Issuer;

        [JsonProperty("jti")]
        public string JwtId;

        [JsonProperty("project_id")]
        public string ProjectId;

        [JsonProperty("scp")]
        public string[] Scope;

        [JsonProperty("sub")]
        public string Subject;

        [JsonProperty("up")]
        public string Up;
    }
}
