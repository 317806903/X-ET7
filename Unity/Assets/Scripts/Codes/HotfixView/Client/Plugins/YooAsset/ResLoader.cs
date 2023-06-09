using System;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using UnityEngine;
using YooAsset;

namespace ET.Client
{
    [Invoke]
    public class ResLoader: AInvokeHandler<ConfigComponent.GetRes, GameObject>
    {
        public override GameObject Handle(ConfigComponent.GetRes args)
        {
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(args.ResName);
            return go;
        }
    }
}