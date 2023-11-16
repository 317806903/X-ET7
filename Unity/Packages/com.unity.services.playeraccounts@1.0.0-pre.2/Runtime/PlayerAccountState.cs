namespace Unity.Services.PlayerAccounts
{
    enum PlayerAccountState
    {
        SignedOut,
        SigningIn,
        Authorized,
        Refreshing,
        Expired
    }
}
