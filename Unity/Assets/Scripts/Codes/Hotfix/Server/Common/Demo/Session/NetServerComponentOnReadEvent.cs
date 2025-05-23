﻿namespace ET.Server
{
    [Event(SceneType.Process)]
    public class NetServerComponentOnReadEvent: AEvent<Scene, NetServerComponentOnRead>
    {
        protected override async ETTask Run(Scene scene, NetServerComponentOnRead args)
        {
            Session session = args.Session;
            object message = args.Message;

            if (session.IsDisposed)
            {
                return;
            }
            if (message is IResponse response)
            {
                session.OnResponse(response);
                return;
            }

            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理,比如需要转发给Chat Scene，可以做一个IChatMessage接口
            switch (message)
            {
                case IActorLocationMessage actorLocationMessage:
                {
                    SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
                    if (sessionPlayerComponent == null || sessionPlayerComponent.IsDisposed)
                    {
                        return;
                    }
                    Player player = sessionPlayerComponent.Player;
                    if (player == null || player.IsDisposed)
                    {
                        return;
                    }
                    long unitId = player.Id;
                    ActorLocationSenderComponent.Instance.Get(LocationType.Unit).Send(unitId, actorLocationMessage, scene.InstanceId);
                    break;
                }
                case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                {
                    SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
                    if (sessionPlayerComponent == null || sessionPlayerComponent.IsDisposed)
                    {
                        return;
                    }
                    Player player = sessionPlayerComponent.Player;
                    if (player == null || player.IsDisposed)
                    {
                        return;
                    }
                    long unitId = player.Id;
                    int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    IResponse iResponse = await ActorLocationSenderComponent.Instance.Get(LocationType.Unit).Call(unitId, actorLocationRequest, scene.InstanceId);
                    iResponse.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(iResponse);
                    }
                    break;
                }
                case IActorRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                {
                    break;
                }
                case IActorMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                {
                    break;
                }

                default:
                {
                    // 非Actor消息
                    MessageDispatcherComponent.Instance.Handle(session, message);
                    break;
                }
            }
        }
    }
}