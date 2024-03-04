using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class TowerShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        private EntityRef<TowerComponent> _towerComponent;
        public TowerComponent towerComponent
        {
            get
            {
                return this._towerComponent;
            }
            set
            {
                this._towerComponent = value;
            }
        }

        public Transform transRoot { get; set; }
        public Transform transCollider { get; set; }
        public Transform transDefaultShow { get; set; }
        public Transform transSelectShow { get; set; }
        public Transform transCanUpgradeShow { get; set; }
        public Transform transAttackArea { get; set; }
        public Transform transMyTowerShow { get; set; }
    }
}