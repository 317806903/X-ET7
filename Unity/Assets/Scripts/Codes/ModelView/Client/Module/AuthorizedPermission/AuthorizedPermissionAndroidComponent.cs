using System;
using UnityEngine;

namespace ET.Client
{
    public class AuthorizedPermissionAndroidComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public GameObject root;
        public Action<bool, bool> callBack;
        public UnityEngine.Android.PermissionCallbacks permissionCallbacks;
        public bool WebCam_permission;
        public bool VIBRATE_permission;
    }
}