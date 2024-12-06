using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitPlayAudio : Entity, IAwake, IDestroy
    {
        public List<long> unitId = new();
        public List<string> playAudioActionId = new();
        public List<bool> isOnlySelfShow = new();

        public Dictionary<string, HashSet<Unit>> playAudioActionId2Units = new();
        public List<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list = new();
    }
}