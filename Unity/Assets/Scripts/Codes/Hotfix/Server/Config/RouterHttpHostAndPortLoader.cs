using System;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;

namespace ET.Server
{
    [Invoke]
    public class GetRouterHttpHostAndPort: AInvokeHandler<ConfigComponent.GetRouterHttpHostAndPort, (string, int)>
    {
        public override (string, int) Handle(ConfigComponent.GetRouterHttpHostAndPort args)
        {
            string RouterHttpHost = StartMachineConfigCategory.Instance.DataList[0].OuterIP;
            int RouterHttpPort = StartSceneConfigCategory.Instance.RouterManager.OuterPort;
            return (RouterHttpHost, RouterHttpPort);
        }
    }
}