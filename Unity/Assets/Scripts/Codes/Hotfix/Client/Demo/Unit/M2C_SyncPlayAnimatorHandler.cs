using System.Collections.Generic;
using ET.Ability;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncPlayAnimatorHandler : AMHandler<M2C_SyncPlayAnimator>
	{
		protected override async ETTask Run(Session session, M2C_SyncPlayAnimator message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			if (currentScene == null)
			{
				return;
			}

			long unitId = message.UnitId;

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

			Entity entity = MongoHelper.Deserialize<Entity>(message.PlayAnimatorComponent);
			unit.RemoveComponent<AnimatorComponent>();
			unit.AddComponent(entity);

			await ETTask.CompletedTask;
		}
	}
}
