using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HomeShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        private EntityRef<HomeComponent> _homeComponent;
        public HomeComponent homeComponent
        {
            get
            {
                return this._homeComponent;
            }
            set
            {
                this._homeComponent = value;
            }
        }

        public Transform transRoot { get; set; }
        public Transform transCollider { get; set; }
        public Transform transDefaultShow { get; set; }
        public Transform transSelectShow { get; set; }
    }
}