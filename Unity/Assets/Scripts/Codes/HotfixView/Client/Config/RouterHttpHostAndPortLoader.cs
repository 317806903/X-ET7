using System;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using UnityEngine;
using YooAsset;

namespace ET.Client
{
    [Invoke]
    public class GetRouterHttpHostAndPort: AInvokeHandler<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>
    {
        public override (string, int) Handle(ConfigComponent.GetRouterHttpHostAndPort args)
        {
            string RouterHttpHost = ResConfig.Instance.RouterHttpHost;
            //string RouterHttpHost = "192.168.0.186";
            int RouterHttpPort = ResConfig.Instance.RouterHttpPort;
            return (RouterHttpHost, RouterHttpPort);
        }
    }

    [Invoke]
    public class GetRouterHttpHostAndPortWhenEditor: AInvokeHandler<ConfigComponent.GetRouterHttpHostAndPortWhenEditor, (string, int)>
    {
        public override (string, int) Handle(ConfigComponent.GetRouterHttpHostAndPortWhenEditor args)
        {
            string RouterHttpHost = StartMachineConfigCategory.Instance.DataList[0].OuterIP;
            int RouterHttpPort = StartSceneConfigCategory.Instance.RouterManager.OuterPort;
            return (RouterHttpHost, RouterHttpPort);
        }
    }
}