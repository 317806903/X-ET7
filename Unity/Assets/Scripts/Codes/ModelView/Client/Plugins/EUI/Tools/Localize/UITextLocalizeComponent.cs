﻿using System.Collections.Generic;
using System;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class UITextLocalizeComponent : Entity,IAwake,IDestroy
    {
        public static UITextLocalizeComponent Instance { get; set; }

        public HashSet<UITextLocalizeMonoView> _UITextLocalizeMonoViewList = new ();
    }
}