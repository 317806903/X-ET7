namespace Unity.Services.PlayerAccounts
{
    interface INetworkConfiguration
    {
        int Retries { get; }
        int Timeout { get; }
    }
}
