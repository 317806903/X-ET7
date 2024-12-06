using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class HybridCLRRefTypes
    {
        public void MyAOTRefs()
        {
            // var bytes = new byte[1];
            // using (MemoryStream memoryStream = new MemoryStream(bytes))
            // {
            //     var tmp1 = (Entity)BsonSerializer.Deserialize(memoryStream, typeof (Entity));
            //     object o = null;
            //     Unsafe.As<object>(bytes);
            //     System.SByte tmp2 = SByte.MinValue;
            //     Unsafe.As<System.SByte, object>(ref tmp2);
            // }
                        
    #if UNITY_IOS
                var tmp1 = Unity.Advertisement.IosSupport.ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED;
    #endif

        }
    }
}
