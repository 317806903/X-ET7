using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Location)]
    public class ChkObjectListExistRequestHandler: AMActorRpcHandler<Scene, ChkObjectListExistRequest, ChkObjectListExistResponse>
    {
        protected override async ETTask Run(Scene scene, ChkObjectListExistRequest request, ChkObjectListExistResponse response)
        {
            List<long> keyList = request.KeyList;
            List<long> notExistKeyList = ListComponent<long>.Create();
            foreach (var key in keyList)
            {
                long instanceId = await scene.GetComponent<LocationManagerComoponent>().Get(request.Type).Get(key);
                scene.GetComponent<LocationManagerComoponent>().Get(request.Type).RecordSceneByKey(key, request.SceneInstanceId);
                if (instanceId == 0)
                {
                    notExistKeyList.Add(key);
                }
            }

            response.NotExistKeyList = notExistKeyList;
        }
    }
}