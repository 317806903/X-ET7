using Unity.Services.Core;

namespace Unity.Services.PlayerAccounts
{
    /// <summary>
    ///
    /// </summary>
    public static class PlayerAccountService
    {
        static IPlayerAccountService s_Instance;

        /// <summary>
        ///
        /// </summary>
        /// <exception cref="ServicesInitializationException"></exception>
        public static IPlayerAccountService Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    throw new ServicesInitializationException("Singleton is not initialized. " +
                        "Please call UnityServices.InitializeAsync() to initialize.");
                }

                return s_Instance;
            }
            internal set => s_Instance = value;
        }
    }
}
