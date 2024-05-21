using System.Collections.Generic;

namespace ET
{
    public class SyncData_UnitPlayAudio : Entity, IAwake, IDestroy
    {
        public List<long> unitId { get; set; }
        public List<string> playAudioActionId;
        public List<bool> isOnlySelfShow;
    }
}