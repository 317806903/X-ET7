namespace ET
{
	
	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		[StaticField]
		public static UnitComponent Instance;

		public HashSetComponent<long> playerList;
		public HashSetComponent<long> monsterList;
		public HashSetComponent<long> npcList;
		public HashSetComponent<long> sceneObjList;
		public HashSetComponent<long> bulletList;
	}
}