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

	        unit.Position = unitInfo.Position;
	        unit.Forward = unitInfo.Forward;

	        foreach (byte[] bytes in unitInfo.Components)
	        {
		        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
		        unit.AddComponent(entity);
	        }

	        EffectComponent effectComponent = unit.AddComponent<EffectComponent>();
	        if (unitInfo.EffectComponents != null)
	        {
		        foreach (byte[] bytes in unitInfo.EffectComponents)
		        {
			        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			        effectComponent.AddChild(entity);
		        }
	        }

	        unit.AddComponent<MoveByPathComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.Points.Count > 0)
				{
					unitInfo.MoveInfo.Points[0] = unit.Position;
					unit.MoveToAsync(unitInfo.MoveInfo.Points).Coroutine();
				}
	        }

	        unit.AddComponent<ObjectWait>();

	        //unit.AddComponent<XunLuoPathComponent>();

	        EventSystem.Instance.Publish(unit.DomainScene(), new EventType.AfterUnitCreate() {Unit = unit});
            return unit;
        }

        public static void ReplaceComponent(Unit unit, UnitInfo unitInfo)
        {
	        UnitType unitType = (UnitType)unitInfo.Type;
	        unit.level = unitInfo.Level;
	        unit.Type = unitType;

	        unit.Position = unitInfo.Position;
	        unit.Forward = unitInfo.Forward;

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
		        foreach (byte[] bytes in unitInfo.EffectComponents)
		        {
			        Entity entity = MongoHelper.Deserialize<Entity>(bytes);
			        effectComponent.AddChild(entity);
		        }
	        }

	        MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
	        if (moveByPathComponent != null)
	        {
		        unit.RemoveComponent<MoveByPathComponent>();
	        }
	        moveByPathComponent = unit.AddComponent<MoveByPathComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.Points.Count > 0)
				{
					unitInfo.MoveInfo.Points[0] = unit.Position;
					unit.MoveToAsync(unitInfo.MoveInfo.Points).Coroutine();
				}
	        }

	        ObjectWait objectWait = unit.GetComponent<ObjectWait>();
	        if (objectWait != null)
	        {
		        unit.RemoveComponent<ObjectWait>();
	        }
	        unit.AddComponent<ObjectWait>();

	        EventSystem.Instance.Publish(unit.DomainScene(), new EventType.AfterUnitCreate() {Unit = unit});
        }
    }
}
