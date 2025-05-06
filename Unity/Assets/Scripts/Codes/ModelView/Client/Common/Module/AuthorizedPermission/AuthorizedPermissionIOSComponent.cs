using UnityEngine;

namespace ET.Client
{
    public class AuthorizedPermissionIOSComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public GameObject root;
        public bool WebCam_permission;
        public bool VIBRATE_permission;
    }
}