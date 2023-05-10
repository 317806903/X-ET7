namespace ET
{
	
	[ComponentOf(typeof(Scene))]
	public class UnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
	{
		public HashSetComponent<long> playerList;
		public HashSetComponent<long> monsterList;
		public HashSetComponent<long> npcList;
		public HashSetComponent<long> sceneObjList;
		public HashSetComponent<long> bulletList;
	}
}