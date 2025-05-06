using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class MonsterCallShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        private EntityRef<MonsterCallComponent> _monsterCall;
        public MonsterCallComponent monsterCallComponent
        {
            get
            {
                return this._monsterCall;
            }
            set
            {
                this._monsterCall = value;
            }
        }

        public Transform transRoot { get; set; }
        public Transform transCollider { get; set; }
        public Transform transDefaultShow { get; set; }
        public Transform transSelectShow { get; set; }
    }
}