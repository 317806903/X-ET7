using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (SelectHandleObj))]
    public static class SelectHandleObjSystem
    {
        [ObjectSystem]
        public class SelectHandleObjAwakeSystem: AwakeSystem<SelectHandleObj>
        {
            protected override void Awake(SelectHandleObj self)
            {
            }
        }

        [ObjectSystem]
        public class SelectHandleObjDestroySystem: DestroySystem<SelectHandleObj>
        {
            protected override void Destroy(SelectHandleObj self)
            {
                self.selectHandle?.Dispose();
                self.selectHandle = null;
            }
        }

        public static void SaveSelectHandle(this SelectHandleObj self, SelectHandle selectHandle)
        {
            self.selectHandle = selectHandle;
        }
        
        public static SelectHandle GetSaveSelectHandle(this SelectHandleObj self)
        {
            return self.selectHandle;
        }
    }
}