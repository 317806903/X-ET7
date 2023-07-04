using System.Collections.Generic;
using System.Linq;

namespace ET
{
	public enum GamePlayStatus
	{
		ScanMap,
		DownloadMap,
		PutHome,
		PutMonsterPoint,
		RestTime,
		InTheBattle,
		GameSuccess,
		GameFailed,
	}
	
	[ComponentOf(typeof(Scene))]
	public class GamePlayComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public GamePlayStatus GamePlayStatus { get; set; }
		public long ownerPlayerId { get; set; }
	}
}