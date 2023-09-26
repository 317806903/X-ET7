using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class R2M_CreateDynamicMapHandler : AMActorRpcHandler<Scene, R2M_CreateDynamicMap, M2R_CreateDynamicMap>
	{
		protected override async ETTask Run(Scene scene, R2M_CreateDynamicMap request, M2R_CreateDynamicMap response)
		{
			DynamicMapManagerComponent dynamicMapManagerComponent = scene.GetComponent<DynamicMapManagerComponent>();
			if (dynamicMapManagerComponent == null)
			{
				dynamicMapManagerComponent = scene.AddComponent<DynamicMapManagerComponent>();
			}

            RoomComponent roomComponent = MongoHelper.Deserialize<Entity>(request.RoomInfo) as RoomComponent;
            List<RoomMember> roomMemberList = new List<RoomMember>();
            if (request.RoomMemberInfos != null)
            {
	            for (int i = 0; i < request.RoomMemberInfos.Count; i++)
	            {
		            Entity roomMember = MongoHelper.Deserialize<Entity>(request.RoomMemberInfos[i]);
		            roomMemberList.Add(roomMember as RoomMember);
	            }
            }

            string _ARMeshDownLoadUrl = request.ARMeshDownLoadUrl;
			Scene dynamicMap = await dynamicMapManagerComponent.CreateDynamicMap(roomComponent, roomMemberList, _ARMeshDownLoadUrl);
			response.DynamicMapInstanceId = dynamicMap.InstanceId;

			await ETTask.CompletedTask;
		}
	}
}