namespace ET.Server
{
    [FriendOf(typeof(GateSessionKeyComponent))]
    public static class GateSessionKeyComponentSystem
    {
        public static long Add(this GateSessionKeyComponent self, string account)
        {
            long key = self.GetRandomKey(account);
            self.sessionKey.Add(key, account);
            self.TimeoutRemoveKey(key).Coroutine();
            return key;
        }

        public static long GetRandomKey(this GateSessionKeyComponent self, string account)
        {
            long key = RandomGenerator.RandInt64();
            while (self.sessionKey.ContainsKey(key))
            {
                key = RandomGenerator.RandInt64();
            }
            return key;
        }

        public static string GetAccountId(this GateSessionKeyComponent self, long key)
        {
            string account = null;
            self.sessionKey.TryGetValue(key, out account);
            return account;
        }

        public static void Remove(this GateSessionKeyComponent self, long key)
        {
            self.sessionKey.Remove(key);
        }

        private static async ETTask TimeoutRemoveKey(this GateSessionKeyComponent self, long key)
        {
            await TimerComponent.Instance.WaitAsync(20000);
            self.sessionKey.Remove(key);
        }
    }
}