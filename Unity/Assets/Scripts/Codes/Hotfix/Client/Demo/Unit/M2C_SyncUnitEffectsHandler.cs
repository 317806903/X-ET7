using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncUnitEffectsHandler : AMHandler<M2C_SyncUnitEffects>
	{
		protected override async ETTask Run(Session session, M2C_SyncUnitEffects message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			if (currentScene == null)
			{
				return;
			}

			long unitId = message.UnitId;
			int addOrRemove = message.AddOrRemove;
			long effectObjId = message.EffectObjId;
			EffectComponent effectComponent;
			if (unitId == 0)
			{
				effectComponent = currentScene.GetComponent<EffectComponent>();
			}
			else
			{
				UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
				if (unitComponent == null)
				{
					return;
				}
				Unit unit = unitComponent.Get(unitId);
				int retryNum = 30;
				while (unit == null)
				{
					await TimerComponent.Instance.WaitFrameAsync();
					unit = unitComponent.Get(unitId);
					if (retryNum-- < 0)
					{
						return;
					}
				}

				effectComponent = unit.GetComponent<EffectComponent>();
			}
			if (addOrRemove == 0)
			{
				Entity entity = MongoHelper.Deserialize<Entity>(message.EffectComponent);
				if (entity != null && effectComponent.GetChild<Entity>(entity.Id) != null)
				{
					entity.Dispose();
				}
				else
				{
					effectComponent.AddChild(entity);
				}
			}
			else
			{
				effectComponent.RemoveChild(effectObjId);
			}
			await ETTask.CompletedTask;
		}
	}
}
