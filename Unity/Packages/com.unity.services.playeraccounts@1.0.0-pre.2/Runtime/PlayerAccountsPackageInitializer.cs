using System.Threading.Tasks;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    class PlayerAccountsPackageInitializer : IInitializablePackage
    {
        public Task Initialize(CoreRegistry registry)
        {
            var network = new NetworkHandler();
            var dateTime = new DateTimeWrapper();
            var jwtDecoder = new JwtDecoder(dateTime);
            var projectId = registry.GetServiceComponent<ICloudProjectId>();
            PlayerAccountSettings.CloudProjectId = projectId.GetCloudProjectId();

            var unityPlayer = new PlayerAccountServiceInternal(jwtDecoder, network, dateTime);
            PlayerAccountService.Instance = unityPlayer;


            return Task.CompletedTask;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            CoreRegistry.Instance.RegisterPackage(new PlayerAccountsPackageInitializer()).DependsOn<ICloudProjectId>();
        }
    }
}
