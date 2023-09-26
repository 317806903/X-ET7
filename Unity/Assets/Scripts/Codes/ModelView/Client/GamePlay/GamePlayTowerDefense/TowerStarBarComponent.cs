using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class TowerStarBarComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transRoot { get; set; }
        public Transform transStar1 { get; set; }
        public Transform transStar2 { get; set; }
        public Transform transStar3 { get; set; }
        public Transform transStarGrey1 { get; set; }
        public Transform transStarGrey2 { get; set; }
        public Transform transStarGrey3 { get; set; }
    }
}