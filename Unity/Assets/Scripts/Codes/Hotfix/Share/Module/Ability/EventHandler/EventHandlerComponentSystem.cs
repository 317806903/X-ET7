﻿using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (EventHandlerComponent))]
    public static class EventHandlerComponentSystem
    {
        [ObjectSystem]
        public class EventHandlerComponentAwakeSystem: AwakeSystem<EventHandlerComponent>
        {
            protected override void Awake(EventHandlerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class EventHandlerComponentDestroySystem: DestroySystem<EventHandlerComponent>
        {
            protected override void Destroy(EventHandlerComponent self)
            {
            }
        }
    }
}