using UnityEngine;

namespace ET.Client
{
    [ObjectSystem]
    public class GlobalComponentAwakeSystem: AwakeSystem<GlobalComponent>
    {
        protected override void Awake(GlobalComponent self)
        {
            GlobalComponent.Instance = self;
            
            self.Global = GameObject.Find("/Global").transform;
            self.Unit = self.Global.Find("Unit").transform;
            self.MainCamera = self.Global.Find("MainCamera").GetComponent<Camera>();
            self.UICamera = self.Global.Find("UICamera").GetComponent<Camera>();
            
            self.NormalRoot = self.Global.Find("UIRoot/NormalRoot").transform;
            self.PopUpRoot = self.Global.Find("UIRoot/PopUpRoot").transform;
            self.FixedRoot = self.Global.Find("UIRoot/FixedRoot").transform;
            self.OtherRoot = self.Global.Find("UIRoot/OtherRoot").transform;
            self.PoolRoot =  self.Global.Find("PoolRoot").transform;
        }
    }
}