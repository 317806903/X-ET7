using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectTransparentComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Dictionary<Renderer, Material[]> MaterialRecordDic = new();
    }
}