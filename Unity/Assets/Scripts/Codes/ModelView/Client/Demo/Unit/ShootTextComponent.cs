using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class ShootTextComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static ShootTextComponent Instance;

        public Transform shootTextRoot_Normal;
        public Transform shootTextRoot_High;
        public Transform shootTextRoot_Crt;
        public Transform shootTextRoot_CrtAndHigh;
        public Transform shootTextRoot_Cure;
        public ShootTextProManager shootTextProManager_Normal;
        public ShootTextProManager shootTextProManager_High;
        public ShootTextProManager shootTextProManager_Crt;
        public ShootTextProManager shootTextProManager_CrtAndHigh;
        public ShootTextProManager shootTextProManager_Cure;

        public Dictionary<Unit, Queue<(int value, bool isCrt)>> unit2DamageShowList;

        public int waitFrame = 300;
        public int curFrame = 0;
    }
}