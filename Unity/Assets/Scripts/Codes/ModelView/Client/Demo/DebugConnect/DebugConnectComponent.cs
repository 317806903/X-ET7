using UnityEngine;

namespace ET.Client
{
    public class DebugConnectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static DebugConnectComponent Instance;

        public bool IsFirstShow = true;
        public bool IsDebugMode;
        public bool IsEditorLoginMode;
    }
}