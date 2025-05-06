namespace ET.Client
{
	public enum ARHallType
	{
		/// <summary>
		/// 加入特定roomId房间
		/// </summary>
		JoinTheRoom,
		/// <summary>
		/// 保持当前roomId，进入重新选择大小或再次重扫界面
		/// </summary>
		KeepRoomAndRescan,
		/// <summary>
		/// 拥有arSceneId,创建新roomId房间
		/// </summary>
		CreateRoomWithARSceneId,
		/// <summary>
		///没有arSceneId,创建新roomId房间
		/// </summary>
		CreateRoomWithOutARSceneId,
		/// <summary>
		/// 进入扫描界面
		/// </summary>
		ScanQRCode,
	}

	public enum BattleDragItemType
	{
		PKTower,
		PKMonster,
		PKMoveTower,
		PKMovePlayer,
		HeadQuarter,
		MonsterCall,
		Tower,
		MoveTower,
	}

	public enum TutorialMenuType
	{
		None = 0,
		FAQ = 1,
		GameTips = 2,
		FAQAndGameTips = 3,
	}
}
