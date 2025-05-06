using System.Collections.Generic;
using System;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class UIImageLocalizeComponent : Entity,IAwake,IDestroy
    {
        public static UIImageLocalizeComponent Instance { get; set; }

        public HashSet<UIImageLocalizeMonoView> _UIImageLocalizeMonoViewList = new ();

    }
}