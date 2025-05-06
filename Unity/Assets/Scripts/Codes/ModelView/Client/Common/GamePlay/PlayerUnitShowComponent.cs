using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class PlayerUnitShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        private EntityRef<PlayerUnitComponent> _playerUnitComponent;
        public PlayerUnitComponent playerUnitComponent
        {
            get
            {
                return this._playerUnitComponent;
            }
            set
            {
                this._playerUnitComponent = value;
            }
        }

        public Transform transRoot { get; set; }
        public Transform transCollider { get; set; }
        public Transform transDefaultShow { get; set; }
        public Transform transSelectShow { get; set; }
        public Transform transCanUpgradeShow { get; set; }
        public Transform transAttackArea { get; set; }
    }
}