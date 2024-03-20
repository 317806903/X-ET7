using UnityEngine;

namespace ET.Client
{
    public class DebugWhenEditorComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static DebugWhenEditorComponent Instance;

        public Transform Root;

        public bool IsShowShootDamageNum { get; set; }
        public bool IsStopActorMove { get; set; }
    }
}