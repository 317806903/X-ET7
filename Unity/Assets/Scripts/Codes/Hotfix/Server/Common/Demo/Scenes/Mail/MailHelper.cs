using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    public static class MailHelper
    {
        /// <summary>
        /// 获取MailManagerComponent组件
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static MailManagerComponent GetMailManager(Scene scene)
	    {
		    MailManagerComponent mailManagerComponent = scene.GetComponent<MailManagerComponent>();
		    if (mailManagerComponent == null)
		    {
		    }
		    return mailManagerComponent;
	    }

        public static MailHistoryManagerComponent GetMailHistoryManager(Scene scene)
	    {
		    MailHistoryManagerComponent mailHistoryManagerComponent = scene.GetComponent<MailHistoryManagerComponent>();
		    if (mailHistoryManagerComponent == null)
		    {
		    }
		    return mailHistoryManagerComponent;
	    }

        /// <summary>
        /// 获取邮箱数据MailInfoComponent
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static async ETTask<(bool, List<byte[]>)> GetPlayerMailFromCenter(Scene scene, long playerId)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Mail, playerId))
	        {
		        return await SendGetPlayerMailFromCenterAsync(scene, playerId);
	        }
        }

        public static async ETTask<(bool, bool)> ChkIsNewPlayerMailFromCenter(Scene scene, long playerId)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Mail, playerId))
	        {
		        return await SendChkIsNewPlayerMailFromCenterAsync(scene, playerId);
	        }
        }

        /// <summary>
        /// 初始化mailToPlayersComponent组件并插入到邮箱中心
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <param name="mailType"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailContent"></param>
        /// <param name="itemCfgList"></param>
        /// <param name="receiveTime"></param>
        /// <param name="limitTime"></param>
        /// <param name="mailToPlayerType"></param>
        /// <param name="waitSendPlayerList"></param>
        /// <returns></returns>
        public static async ETTask InsertMailToCenter(Scene scene, long playerId, string mailType, string mailTitle, string mailContent, Dictionary<string, int> itemCfgList, long receiveTime, long limitTime, MailToPlayerType mailToPlayerType, List<long> waitSendPlayerList, Dictionary<long, string> playerParam)
        {
            MailToPlayersComponent mailToPlayersComponent = scene.AddChild<MailToPlayersComponent>();
	        mailToPlayersComponent.InitMailInfo(mailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime);
	        mailToPlayersComponent.SetMailToPlayerType(mailToPlayerType, waitSendPlayerList, playerParam);
	        byte[] bytes = mailToPlayersComponent.ToBson();
	        mailToPlayersComponent.Dispose();
	        await InsertMailToCenter(scene, playerId, bytes);

	        await ETTask.CompletedTask;
        }

		/// <summary>
		/// 无初始化MailToPlayersComponent直接写入到邮箱进程
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <param name="mailToPlayersComponent"></param>
		/// <returns></returns>
        public static async ETTask InsertMailToCenter(Scene scene, long playerId, byte[] bytes)
        {
	        using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Mail, playerId))
	        {
		        await SendInsertMailToCenterAsync(scene, bytes);
	        }

	        await ETTask.CompletedTask;
        }

        /// <summary>
        /// 向邮箱进程请求邮箱信息MailInfoComponent
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="playerId"></param>
        /// <returns>是否成功，MailInfoComponent字节数据</returns>
        public static async ETTask<(bool, List<byte[]>)> SendGetPlayerMailFromCenterAsync(Scene scene, long playerId)
        {
	        StartSceneConfig mailSceneConfig = StartSceneConfigCategory.Instance.GetMailManager(scene.DomainZone());

	        M2G_GetMailFromCenter _M2G_GetMailFromCenter = (M2G_GetMailFromCenter) await ActorMessageSenderComponent.Instance.Call(mailSceneConfig.InstanceId, new G2M_GetMailFromCenter()
	        {
		        PlayerId = playerId,
	        });

	        if (_M2G_GetMailFromCenter.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendGetPlayerMailFromCenterAsync Error==1 msg={_M2G_GetMailFromCenter.Message}");
		        return (false, null);
	        }
	        else
	        {
		        List<byte[]> componentBytes = _M2G_GetMailFromCenter.ComponentBytes;
		        if (componentBytes == null || componentBytes.Count == 0)
		        {
			        return (false, null);
		        }
		        return (true, componentBytes);
	        }
        }

        public static async ETTask<(bool, bool)> SendChkIsNewPlayerMailFromCenterAsync(Scene scene, long playerId)
        {
	        StartSceneConfig mailSceneConfig = StartSceneConfigCategory.Instance.GetMailManager(scene.DomainZone());

	        M2G_ChkIsNewMailFromCenter _M2G_ChkIsNewMailFromCenter = (M2G_ChkIsNewMailFromCenter) await ActorMessageSenderComponent.Instance.Call(mailSceneConfig.InstanceId, new G2M_ChkIsNewMailFromCenter()
	        {
		        PlayerId = playerId,
	        });

	        if (_M2G_ChkIsNewMailFromCenter.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendChkIsNewPlayerMailFromCenterAsync Error==1 msg={_M2G_ChkIsNewMailFromCenter.Message}");
		        return (false, false);
	        }
	        else
	        {
		        bool isNew = _M2G_ChkIsNewMailFromCenter.IsNew == 1? true : false;
		        return (true, isNew);
	        }
        }

        /// <summary>
        /// 发送mailToPlayersComponent字节数据给Mail进程
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="bytes">mailToPlayersComponent字节数据</param>
        /// <returns></returns>
        public static async ETTask<bool> SendInsertMailToCenterAsync(Scene scene, byte[] bytes)
        {
	        StartSceneConfig mailSceneConfig = StartSceneConfigCategory.Instance.GetMailManager(scene.DomainZone());

	        M2G_InsertMailToCenter _M2G_InsertMailToCenter = (M2G_InsertMailToCenter) await ActorMessageSenderComponent.Instance.Call(mailSceneConfig.InstanceId, new G2M_InsertMailToCenter()
	        {
		        ComponentBytes = bytes,
	        });

	        if (_M2G_InsertMailToCenter.Error != ET.ErrorCode.ERR_Success)
	        {
		        Log.Error($"SendInsertMailToCenterAsync Error==1 msg={_M2G_InsertMailToCenter.Message}");
		        return false;
	        }
	        else
	        {
		        return true;
	        }
        }

    }
}