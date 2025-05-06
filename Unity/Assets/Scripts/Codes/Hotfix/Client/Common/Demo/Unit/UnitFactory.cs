using ET.Ability;
using Unity.Mathematics;

namespace ET.Client
{
	[FriendOf(typeof(Unit))]
    public static class UnitFactory
    {
        public static Unit Create(UnitComponent unitComponent, UnitInfo unitInfo)
        {
	        Unit unit = ET.Ability.UnitHelper_Create.CreateWhenClient(unitComponent, unitInfo);

	        float3 serverPos = unitInfo.Position;
	        quaternion serverRotation = unitInfo.Rotation;
	        serverPos = ET.Ability.UnitHelper.TranServerPos2ClientPos(unitComponent.DomainScene(), serverPos);
	        serverRotation = ET.Ability.UnitHelper.TranServerQuaternion2ClientQuaternion(serverRotation);

	        unit.SetPositionWhenClient(serverPos);
	        unit.SetRotationWhenClient(serverRotation);

	        if (unitInfo.Components != null)
	        {
		        foreach (byte[] bytes in unitInfo.Components)
		        {
			        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			        if (entity != null)
			        {
				        unit.AddComponent(entity);
			        }
		        }
	        }

	        EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
	        if (effectComponent != null)
	        {
		        unit.RemoveComponent<EffectComponent>();
	        }
	        effectComponent = unit.AddComponent<EffectComponent>();
	        if (unitInfo.EffectComponents != null)
	        {
		        EffectShowChgComponent effectShowChgComponent = unit.GetComponent<EffectShowChgComponent>();
		        if (effectShowChgComponent == null)
		        {
			        effectShowChgComponent = unit.AddComponent<EffectShowChgComponent>();
		        }

		        foreach (byte[] bytes in unitInfo.EffectComponents)
		        {
			        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			        if (entity != null)
			        {
				        effectComponent.AddChild(entity);
				        effectShowChgComponent.chgEffectList.Add(entity.Id);
			        }
		        }
	        }

	        if (unit.Type == UnitType.Bullet)
	        {
	        }
	        else
	        {
		        unit.AddComponent<MoveByPathComponent>();
		        unit.AddComponent<ObjectWait>();

		        //unit.AddComponent<XunLuoPathComponent>();
	        }

	        EventSystem.Instance.Publish(unit.DomainScene(), new ClientEventType.AfterUnitCreate() {Unit = unit});
            return unit;
        }

        public static void ReplaceComponent(Unit unit, UnitInfo unitInfo)
        {
	        UnitType unitType = (UnitType)unitInfo.Type;
	        unit.level = unitInfo.Level;
	        unit.Type = unitType;

	        float3 serverPos = unitInfo.Position;
	        quaternion serverRotation = unitInfo.Rotation;
	        serverPos = ET.Ability.UnitHelper.TranServerPos2ClientPos(unit.DomainScene(), serverPos);
	        serverRotation = ET.Ability.UnitHelper.TranServerQuaternion2ClientQuaternion(serverRotation);

	        unit.SetPositionWhenClient(serverPos);
	        unit.SetRotationWhenClient(serverRotation);

	        foreach (byte[] bytes in unitInfo.Components)
	        {
		        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
		        System.Type type = entity.GetType();
		        if (unit.GetComponent(type) != null)
		        {
			        unit.RemoveComponent(type);
		        }
		        unit.AddComponent(entity);
	        }

	        EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
	        if (effectComponent != null)
	        {
		        unit.RemoveComponent<EffectComponent>();
	        }
	        effectComponent = unit.AddComponent<EffectComponent>();
	        if (unitInfo.EffectComponents != null)
	        {
		        EffectShowChgComponent effectShowChgComponent = unit.GetComponent<EffectShowChgComponent>();
		        if (effectShowChgComponent == null)
		        {
			        effectShowChgComponent = unit.AddComponent<EffectShowChgComponent>();
		        }

		        foreach (byte[] bytes in unitInfo.EffectComponents)
		        {
			        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			        effectComponent.AddChild(entity);
			        effectShowChgComponent.chgEffectList.Add(entity.Id);
		        }
	        }

	        if (unit.Type == UnitType.Bullet)
	        {
	        }
	        else
	        {
		        MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
		        if (moveByPathComponent != null)
		        {
			        unit.RemoveComponent<MoveByPathComponent>();
		        }
		        moveByPathComponent = unit.AddComponent<MoveByPathComponent>();

		        ObjectWait objectWait = unit.GetComponent<ObjectWait>();
		        if (objectWait != null)
		        {
			        unit.RemoveComponent<ObjectWait>();
		        }
		        unit.AddComponent<ObjectWait>();
	        }
	        EventSystem.Instance.Publish(unit.DomainScene(), new ClientEventType.AfterUnitCreate() {Unit = unit});
        }
    }
}
