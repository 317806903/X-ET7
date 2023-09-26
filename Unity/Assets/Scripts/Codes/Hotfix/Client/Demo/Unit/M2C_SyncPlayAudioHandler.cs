using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncPlayAudioHandler : AMHandler<M2C_SyncPlayAudio>
	{
		protected override async ETTask Run(Session session, M2C_SyncPlayAudio message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			if (currentScene == null)
			{
				return;
			}

			long unitId = message.UnitId;
			string playAudioActionId = message.PlayAudioActionId;
			if (string.IsNullOrEmpty(playAudioActionId))
			{
				return;
			}
			
			UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
			if (unitComponent == null)
			{
				return;
			}
			Unit unit = unitComponent.Get(unitId);
			if (unit == null)
			{
				return;
			}
			EventType.SyncPlayAudio _SyncPlayAudio = new ()
			{
				unit = unit,
				playAudioActionId = playAudioActionId,
			};
			EventSystem.Instance.Publish(unit.DomainScene(), _SyncPlayAudio);
			
			await ETTask.CompletedTask;
		}
	}
}
