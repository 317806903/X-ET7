using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"DOTween.dll",
		"IngameDebugConsole.Runtime.dll",
		"MirrorVerse.UI.MirrorSceneClassyUI.dll",
		"MirrorVerse.dll",
		"MongoDB.Bson.dll",
		"System.Core.dll",
		"System.dll",
		"Unity.Core.dll",
		"Unity.Loader.dll",
		"Unity.ThirdParty.dll",
		"UnityEngine.CoreModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnEnter>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnExist>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BulletOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BulletOnHitMesh>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitChgSaveSelectObj>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnHitMesh>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>
	// ET.AEvent<object,ET.Client.NetClientComponentOnRead>
	// ET.AEvent<object,ET.EventType.AfterCreateClientScene>
	// ET.AEvent<object,ET.EventType.AfterCreateCurrentScene>
	// ET.AEvent<object,ET.EventType.AfterUnitCreate>
	// ET.AEvent<object,ET.EventType.AppStartInitFinish>
	// ET.AEvent<object,ET.EventType.BattleCfgIdChoose>
	// ET.AEvent<object,ET.EventType.BattleSceneEnterFinish>
	// ET.AEvent<object,ET.EventType.BattleSceneEnterStart>
	// ET.AEvent<object,ET.EventType.BeKickedRoom>
	// ET.AEvent<object,ET.EventType.ChangePosition>
	// ET.AEvent<object,ET.EventType.ChangeRotation>
	// ET.AEvent<object,ET.EventType.EnterMapFinish>
	// ET.AEvent<object,ET.EventType.EntryEvent1>
	// ET.AEvent<object,ET.EventType.EntryEvent3>
	// ET.AEvent<object,ET.EventType.GamePlayChg>
	// ET.AEvent<object,ET.EventType.GamePlayCoinChg>
	// ET.AEvent<object,ET.EventType.HallSceneEnterStart>
	// ET.AEvent<object,ET.EventType.LoginFinish>
	// ET.AEvent<object,ET.EventType.LoginOutFinish>
	// ET.AEvent<object,ET.EventType.LoginSceneEnterStart>
	// ET.AEvent<object,ET.EventType.MoveByPathStart>
	// ET.AEvent<object,ET.EventType.MoveByPathStop>
	// ET.AEvent<object,ET.EventType.MovePointList>
	// ET.AEvent<object,ET.EventType.NoticeEventLogging>
	// ET.AEvent<object,ET.EventType.NoticeGameEnd2Server>
	// ET.AEvent<object,ET.EventType.NoticeGameEndToRoom>
	// ET.AEvent<object,ET.EventType.NoticeGamePlayPlayerListToClient>
	// ET.AEvent<object,ET.EventType.NoticeUIReconnect>
	// ET.AEvent<object,ET.EventType.NoticeUITip>
	// ET.AEvent<object,ET.EventType.NoticeUnitBuffStatusChg>
	// ET.AEvent<object,ET.EventType.NumbericChange>
	// ET.AEvent<object,ET.EventType.OnPatchDownloadProgress>
	// ET.AEvent<object,ET.EventType.OnPatchDownlodFailed>
	// ET.AEvent<object,ET.EventType.RoomInfoChg>
	// ET.AEvent<object,ET.EventType.StopMove>
	// ET.AEvent<object,ET.EventType.SwitchLanguage>
	// ET.AEvent<object,ET.EventType.SyncNumericUnits>
	// ET.AEvent<object,ET.EventType.SyncPlayAnimator>
	// ET.AEvent<object,ET.EventType.SyncPlayAudio>
	// ET.AEvent<object,ET.EventType.SyncPosUnits>
	// ET.AEvent<object,ET.EventType.SyncUnitEffects>
	// ET.AEvent<object,ET.EventType.UnitEnterSightRange>
	// ET.AEvent<object,ET.EventType.UnitLeaveSightRange>
	// ET.AEvent<object,ET.EventType.WaitNoticeGamePlayModeToClient>
	// ET.AEvent<object,ET.EventType.WaitNoticeGamePlayPlayerListToClient>
	// ET.AEvent<object,ET.EventType.WaitNoticeGamePlayToClient>
	// ET.AInvokeHandler<ET.ConfigComponent.GetAllConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetCodeMode,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetOneConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetRes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetRouterHttpHostAndPort,System.ValueTuple<object,int>>
	// ET.AInvokeHandler<ET.ConfigComponent.GetRouterHttpHostAndPortWhenEditor,System.ValueTuple<object,int>>
	// ET.AInvokeHandler<ET.NavmeshManagerComponent.RecastFileLoader,object>
	// ET.AInvokeHandler<ET.TimerCallback>
	// ET.ATimer<object>
	// ET.AwakeSystem<object,System.Net.Sockets.AddressFamily>
	// ET.AwakeSystem<object,float>
	// ET.AwakeSystem<object,int,Unity.Mathematics.float3>
	// ET.AwakeSystem<object,int>
	// ET.AwakeSystem<object,object,int>
	// ET.AwakeSystem<object,object>
	// ET.AwakeSystem<object>
	// ET.ConfigSingleton<object>
	// ET.DestroySystem<object>
	// ET.ETAsyncTaskMethodBuilder<!!0>
	// ET.ETAsyncTaskMethodBuilder<!0>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<Unity.Mathematics.float3,object>>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>
	// ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>
	// ET.ETAsyncTaskMethodBuilder<byte>
	// ET.ETAsyncTaskMethodBuilder<int>
	// ET.ETAsyncTaskMethodBuilder<object>
	// ET.ETAsyncTaskMethodBuilder<uint>
	// ET.ETTask<!!0>
	// ET.ETTask<!0>
	// ET.ETTask<ET.Client.Wait_CreateMyUnit>
	// ET.ETTask<ET.Client.Wait_SceneChangeFinish>
	// ET.ETTask<ET.Client.Wait_UnitStop>
	// ET.ETTask<System.ValueTuple<Unity.Mathematics.float3,object>>
	// ET.ETTask<System.ValueTuple<byte,object>>
	// ET.ETTask<System.ValueTuple<uint,object>>
	// ET.ETTask<UnityEngine.SceneManagement.Scene>
	// ET.ETTask<byte>
	// ET.ETTask<int>
	// ET.ETTask<object>
	// ET.ETTask<uint>
	// ET.EntityRef<object>
	// ET.EventTriggerListener.UIEvent<object>
	// ET.EventTriggerListener.UIEventHandle<object>
	// ET.FixedUpdateSystem<object>
	// ET.HashSetComponent<object>
	// ET.IAwake<System.Net.Sockets.AddressFamily>
	// ET.IAwake<float>
	// ET.IAwake<int,Unity.Mathematics.float3>
	// ET.IAwake<int>
	// ET.IAwake<object,int>
	// ET.IAwake<object>
	// ET.IAwakeSystem<System.Net.Sockets.AddressFamily>
	// ET.IAwakeSystem<float>
	// ET.IAwakeSystem<int,Unity.Mathematics.float3>
	// ET.IAwakeSystem<int>
	// ET.IAwakeSystem<object,int>
	// ET.IAwakeSystem<object>
	// ET.LateUpdateSystem<object>
	// ET.ListComponent<DotRecast.Detour.StraightPathItem>
	// ET.ListComponent<ET.Ability.TeamFlagType>
	// ET.ListComponent<Unity.Mathematics.float3>
	// ET.ListComponent<long>
	// ET.ListComponent<object>
	// ET.LoadSystem<object>
	// ET.MultiDictionary<ET.Ability.TeamFlagType,object,float>
	// ET.MultiDictionary<int,int,object>
	// ET.MultiDictionary<long,object,float>
	// ET.MultiDictionary<long,object,int>
	// ET.MultiDictionary<long,object,object>
	// ET.MultiMap<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// ET.MultiMap<ET.Ability.SkillSlotType,long>
	// ET.MultiMap<ET.AbilityConfig.BuffTagGroupType,object>
	// ET.MultiMap<ET.AbilityConfig.BuffTagType,object>
	// ET.MultiMap<ET.AbilityConfig.BuffType,object>
	// ET.MultiMap<float,object>
	// ET.MultiMap<int,object>
	// ET.MultiMap<long,byte>
	// ET.MultiMap<long,long>
	// ET.MultiMap<long,object>
	// ET.MultiMap<object,ET.EntityRef<object>>
	// ET.MultiMap<object,long>
	// ET.MultiMapSet<long,long>
	// ET.Singleton<object>
	// ET.UpdateSystem<object>
	// MirrorVerse.StatusOr<MirrorVerse.SceneInfo>
	// MirrorVerse.StatusOr<object>
	// MirrorVerse.UI.MirrorSceneClassyUI.SubMenu<object>
	// MongoDB.Bson.Serialization.IBsonSerializer<object>
	// System.Action<!!0,!!1>
	// System.Action<!!0>
	// System.Action<!0,!1>
	// System.Action<!0>
	// System.Action<DotRecast.Detour.StraightPathItem>
	// System.Action<ET.Ability.TeamFlagType>
	// System.Action<ET.AbilityConfig.BuffTagGroupType>
	// System.Action<ET.AbilityConfig.BuffTagType>
	// System.Action<ET.Client.WindowID>
	// System.Action<ET.EntityRef<object>>
	// System.Action<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Action<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Action<System.Numerics.Vector3>
	// System.Action<Unity.Mathematics.float3>
	// System.Action<UnityEngine.EventSystems.RaycastResult>
	// System.Action<UnityEngine.RaycastHit>
	// System.Action<byte>
	// System.Action<float>
	// System.Action<int>
	// System.Action<long,int>
	// System.Action<long,long,object>
	// System.Action<long>
	// System.Action<object,int>
	// System.Action<object,object>
	// System.Action<object>
	// System.Collections.Generic.ArraySortHelper<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ArraySortHelper<ET.Ability.TeamFlagType>
	// System.Collections.Generic.ArraySortHelper<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.ArraySortHelper<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.ArraySortHelper<ET.Client.WindowID>
	// System.Collections.Generic.ArraySortHelper<ET.EntityRef<object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ArraySortHelper<System.Numerics.Vector3>
	// System.Collections.Generic.ArraySortHelper<Unity.Mathematics.float3>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ArraySortHelper<byte>
	// System.Collections.Generic.ArraySortHelper<float>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<long>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<DotRecast.Core.RcVec3f>
	// System.Collections.Generic.Comparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.Comparer<ET.Ability.AbilityBuffMonitorTriggerEvent>
	// System.Collections.Generic.Comparer<ET.Ability.SkillSlotType>
	// System.Collections.Generic.Comparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.Comparer<ET.AbilityConfig.AnimatorMotionName>
	// System.Collections.Generic.Comparer<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.Comparer<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.Comparer<ET.AbilityConfig.BuffType>
	// System.Collections.Generic.Comparer<ET.AbilityConfig.NumericType>
	// System.Collections.Generic.Comparer<ET.Client.WindowID>
	// System.Collections.Generic.Comparer<ET.EntityRef<object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Comparer<System.Nullable<UnityEngine.RaycastHit>>
	// System.Collections.Generic.Comparer<System.Numerics.Vector3>
	// System.Collections.Generic.Comparer<Unity.Mathematics.float3>
	// System.Collections.Generic.Comparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.Comparer<UnityEngine.Vector2>
	// System.Collections.Generic.Comparer<byte>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<long>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Comparer<uint>
	// System.Collections.Generic.ComparisonComparer<DotRecast.Core.RcVec3f>
	// System.Collections.Generic.ComparisonComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ComparisonComparer<ET.Ability.AbilityBuffMonitorTriggerEvent>
	// System.Collections.Generic.ComparisonComparer<ET.Ability.SkillSlotType>
	// System.Collections.Generic.ComparisonComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.ComparisonComparer<ET.AbilityConfig.AnimatorMotionName>
	// System.Collections.Generic.ComparisonComparer<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.ComparisonComparer<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.ComparisonComparer<ET.AbilityConfig.BuffType>
	// System.Collections.Generic.ComparisonComparer<ET.AbilityConfig.NumericType>
	// System.Collections.Generic.ComparisonComparer<ET.Client.WindowID>
	// System.Collections.Generic.ComparisonComparer<ET.EntityRef<object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ComparisonComparer<System.Nullable<UnityEngine.RaycastHit>>
	// System.Collections.Generic.ComparisonComparer<System.Numerics.Vector3>
	// System.Collections.Generic.ComparisonComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ComparisonComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ComparisonComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ComparisonComparer<byte>
	// System.Collections.Generic.ComparisonComparer<float>
	// System.Collections.Generic.ComparisonComparer<int>
	// System.Collections.Generic.ComparisonComparer<long>
	// System.Collections.Generic.ComparisonComparer<object>
	// System.Collections.Generic.ComparisonComparer<uint>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary.Enumerator<ET.MailType,long>
	// System.Collections.Generic.Dictionary.Enumerator<ET.RankType,long>
	// System.Collections.Generic.Dictionary.Enumerator<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary.Enumerator<float,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,!!1>
	// System.Collections.Generic.Dictionary.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary.Enumerator<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary.Enumerator<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary.Enumerator<long,byte>
	// System.Collections.Generic.Dictionary.Enumerator<long,int>
	// System.Collections.Generic.Dictionary.Enumerator<long,long>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.MailType,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.RankType,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<float,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,!!1>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.MailType,long>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.RankType,long>
	// System.Collections.Generic.Dictionary.KeyCollection<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary.KeyCollection<float,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,!!1>
	// System.Collections.Generic.Dictionary.KeyCollection<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection<int,float>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary.KeyCollection<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary.KeyCollection<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary.KeyCollection<long,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<long,int>
	// System.Collections.Generic.Dictionary.KeyCollection<long,long>
	// System.Collections.Generic.Dictionary.KeyCollection<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary.KeyCollection<object,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<object,float>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,long>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ushort,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.MailType,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.RankType,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<float,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,!!1>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,float>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.MailType,long>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.RankType,long>
	// System.Collections.Generic.Dictionary.ValueCollection<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary.ValueCollection<float,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,!!1>
	// System.Collections.Generic.Dictionary.ValueCollection<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection<int,float>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary.ValueCollection<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary.ValueCollection<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary.ValueCollection<long,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<long,int>
	// System.Collections.Generic.Dictionary.ValueCollection<long,long>
	// System.Collections.Generic.Dictionary.ValueCollection<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary.ValueCollection<object,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<object,float>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,long>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ushort,object>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary<ET.MailType,long>
	// System.Collections.Generic.Dictionary<ET.RankType,long>
	// System.Collections.Generic.Dictionary<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary<float,object>
	// System.Collections.Generic.Dictionary<int,!!1>
	// System.Collections.Generic.Dictionary<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary<int,float>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.Dictionary<long,ET.EntityRef<object>>
	// System.Collections.Generic.Dictionary<long,Unity.Mathematics.float3>
	// System.Collections.Generic.Dictionary<long,byte>
	// System.Collections.Generic.Dictionary<long,int>
	// System.Collections.Generic.Dictionary<long,long>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.Dictionary<object,byte>
	// System.Collections.Generic.Dictionary<object,float>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,long>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<ushort,object>
	// System.Collections.Generic.EqualityComparer<!!1>
	// System.Collections.Generic.EqualityComparer<DotRecast.Core.RcVec3f>
	// System.Collections.Generic.EqualityComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.EqualityComparer<ET.AbilityConfig.AnimatorMotionName>
	// System.Collections.Generic.EqualityComparer<ET.AbilityConfig.CoinType>
	// System.Collections.Generic.EqualityComparer<ET.AbilityConfig.NumericType>
	// System.Collections.Generic.EqualityComparer<ET.AbilityConfig.RecordKeyInt>
	// System.Collections.Generic.EqualityComparer<ET.AbilityConfig.RecordKeyString>
	// System.Collections.Generic.EqualityComparer<ET.Client.WindowID>
	// System.Collections.Generic.EqualityComparer<ET.EntityRef<object>>
	// System.Collections.Generic.EqualityComparer<ET.MailType>
	// System.Collections.Generic.EqualityComparer<ET.RankType>
	// System.Collections.Generic.EqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.EqualityComparer<System.Nullable<UnityEngine.RaycastHit>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<object,int>>
	// System.Collections.Generic.EqualityComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.EqualityComparer<UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<float>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<uint>
	// System.Collections.Generic.EqualityComparer<ushort>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<long>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet.Enumerator<ushort>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<long>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSet<ushort>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<long>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.HashSetEqualityComparer<ushort>
	// System.Collections.Generic.ICollection<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ICollection<ET.Ability.TeamFlagType>
	// System.Collections.Generic.ICollection<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.ICollection<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.ICollection<ET.Client.WindowID>
	// System.Collections.Generic.ICollection<ET.EntityRef<object>>
	// System.Collections.Generic.ICollection<ET.RpcInfo>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.MailType,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.RankType,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,int>,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,!!1>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,ET.Ability.TeamFlagType>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,ET.EntityRef<object>>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,Unity.Mathematics.float3>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,UnityEngine.AnimatorControllerParameterType>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ushort,object>>
	// System.Collections.Generic.ICollection<System.Numerics.Vector3>
	// System.Collections.Generic.ICollection<Unity.Mathematics.float3>
	// System.Collections.Generic.ICollection<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ICollection<byte>
	// System.Collections.Generic.ICollection<float>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<long>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.ICollection<ushort>
	// System.Collections.Generic.IComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IComparer<ET.Ability.AbilityBuffMonitorTriggerEvent>
	// System.Collections.Generic.IComparer<ET.Ability.SkillSlotType>
	// System.Collections.Generic.IComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.IComparer<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.IComparer<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.IComparer<ET.AbilityConfig.BuffType>
	// System.Collections.Generic.IComparer<ET.Client.WindowID>
	// System.Collections.Generic.IComparer<ET.EntityRef<object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IComparer<System.Numerics.Vector3>
	// System.Collections.Generic.IComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.IComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IComparer<byte>
	// System.Collections.Generic.IComparer<float>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<long>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IEnumerable<ET.Ability.TeamFlagType>
	// System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.IEnumerable<ET.Client.WindowID>
	// System.Collections.Generic.IEnumerable<ET.EntityRef<object>>
	// System.Collections.Generic.IEnumerable<ET.RpcInfo>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.MailType,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.RankType,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,int>,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,!!1>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,ET.Ability.TeamFlagType>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,ET.EntityRef<object>>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,Unity.Mathematics.float3>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,UnityEngine.AnimatorControllerParameterType>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ushort,object>>
	// System.Collections.Generic.IEnumerable<System.Numerics.Vector3>
	// System.Collections.Generic.IEnumerable<Unity.Mathematics.float3>
	// System.Collections.Generic.IEnumerable<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IEnumerable<byte>
	// System.Collections.Generic.IEnumerable<float>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<long>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerable<ushort>
	// System.Collections.Generic.IEnumerator<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IEnumerator<ET.Ability.TeamFlagType>
	// System.Collections.Generic.IEnumerator<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.IEnumerator<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.IEnumerator<ET.Client.WindowID>
	// System.Collections.Generic.IEnumerator<ET.EntityRef<object>>
	// System.Collections.Generic.IEnumerator<ET.RpcInfo>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.MailType,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.RankType,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,int>,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,!!1>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,float>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,ET.Ability.TeamFlagType>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,ET.EntityRef<object>>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,Unity.Mathematics.float3>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,UnityEngine.AnimatorControllerParameterType>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,float>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ushort,object>>
	// System.Collections.Generic.IEnumerator<System.Numerics.Vector3>
	// System.Collections.Generic.IEnumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.IEnumerator<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IEnumerator<byte>
	// System.Collections.Generic.IEnumerator<float>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<long>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEnumerator<ushort>
	// System.Collections.Generic.IEqualityComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.IEqualityComparer<ET.AbilityConfig.CoinType>
	// System.Collections.Generic.IEqualityComparer<ET.AbilityConfig.RecordKeyInt>
	// System.Collections.Generic.IEqualityComparer<ET.AbilityConfig.RecordKeyString>
	// System.Collections.Generic.IEqualityComparer<ET.Client.WindowID>
	// System.Collections.Generic.IEqualityComparer<ET.MailType>
	// System.Collections.Generic.IEqualityComparer<ET.RankType>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEqualityComparer<System.ValueTuple<object,int>>
	// System.Collections.Generic.IEqualityComparer<float>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<long>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IEqualityComparer<ushort>
	// System.Collections.Generic.IList<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IList<ET.Ability.TeamFlagType>
	// System.Collections.Generic.IList<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.IList<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.IList<ET.Client.WindowID>
	// System.Collections.Generic.IList<ET.EntityRef<object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IList<System.Numerics.Vector3>
	// System.Collections.Generic.IList<Unity.Mathematics.float3>
	// System.Collections.Generic.IList<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.IList<byte>
	// System.Collections.Generic.IList<float>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<long>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,byte>
	// System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,long>
	// System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>
	// System.Collections.Generic.KeyValuePair<ET.MailType,long>
	// System.Collections.Generic.KeyValuePair<ET.RankType,long>
	// System.Collections.Generic.KeyValuePair<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.KeyValuePair<float,object>
	// System.Collections.Generic.KeyValuePair<int,!!1>
	// System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>
	// System.Collections.Generic.KeyValuePair<int,float>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,ET.Ability.TeamFlagType>
	// System.Collections.Generic.KeyValuePair<long,ET.EntityRef<object>>
	// System.Collections.Generic.KeyValuePair<long,Unity.Mathematics.float3>
	// System.Collections.Generic.KeyValuePair<long,byte>
	// System.Collections.Generic.KeyValuePair<long,int>
	// System.Collections.Generic.KeyValuePair<long,long>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<object,UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.KeyValuePair<object,byte>
	// System.Collections.Generic.KeyValuePair<object,float>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,long>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<ushort,object>
	// System.Collections.Generic.List.Enumerator<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.List.Enumerator<ET.Ability.TeamFlagType>
	// System.Collections.Generic.List.Enumerator<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.List.Enumerator<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.List.Enumerator<ET.Client.WindowID>
	// System.Collections.Generic.List.Enumerator<ET.EntityRef<object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List.Enumerator<System.Numerics.Vector3>
	// System.Collections.Generic.List.Enumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.List.Enumerator<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.List.Enumerator<byte>
	// System.Collections.Generic.List.Enumerator<float>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.List<ET.Ability.TeamFlagType>
	// System.Collections.Generic.List<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.List<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.List<ET.Client.WindowID>
	// System.Collections.Generic.List<ET.EntityRef<object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.List<System.Numerics.Vector3>
	// System.Collections.Generic.List<Unity.Mathematics.float3>
	// System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<DotRecast.Core.RcVec3f>
	// System.Collections.Generic.ObjectComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ObjectComparer<ET.Ability.AbilityBuffMonitorTriggerEvent>
	// System.Collections.Generic.ObjectComparer<ET.Ability.SkillSlotType>
	// System.Collections.Generic.ObjectComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.ObjectComparer<ET.AbilityConfig.AnimatorMotionName>
	// System.Collections.Generic.ObjectComparer<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.ObjectComparer<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.ObjectComparer<ET.AbilityConfig.BuffType>
	// System.Collections.Generic.ObjectComparer<ET.AbilityConfig.NumericType>
	// System.Collections.Generic.ObjectComparer<ET.Client.WindowID>
	// System.Collections.Generic.ObjectComparer<ET.EntityRef<object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectComparer<System.Nullable<UnityEngine.RaycastHit>>
	// System.Collections.Generic.ObjectComparer<System.Numerics.Vector3>
	// System.Collections.Generic.ObjectComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ObjectComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ObjectComparer<byte>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<long>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectComparer<uint>
	// System.Collections.Generic.ObjectEqualityComparer<!!1>
	// System.Collections.Generic.ObjectEqualityComparer<DotRecast.Core.RcVec3f>
	// System.Collections.Generic.ObjectEqualityComparer<ET.Ability.TeamFlagType>
	// System.Collections.Generic.ObjectEqualityComparer<ET.AbilityConfig.AnimatorMotionName>
	// System.Collections.Generic.ObjectEqualityComparer<ET.AbilityConfig.CoinType>
	// System.Collections.Generic.ObjectEqualityComparer<ET.AbilityConfig.NumericType>
	// System.Collections.Generic.ObjectEqualityComparer<ET.AbilityConfig.RecordKeyInt>
	// System.Collections.Generic.ObjectEqualityComparer<ET.AbilityConfig.RecordKeyString>
	// System.Collections.Generic.ObjectEqualityComparer<ET.Client.WindowID>
	// System.Collections.Generic.ObjectEqualityComparer<ET.EntityRef<object>>
	// System.Collections.Generic.ObjectEqualityComparer<ET.MailType>
	// System.Collections.Generic.ObjectEqualityComparer<ET.RankType>
	// System.Collections.Generic.ObjectEqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Nullable<UnityEngine.RaycastHit>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<object,int>>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.AnimatorControllerParameterType>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Vector2>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<float>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<uint>
	// System.Collections.Generic.ObjectEqualityComparer<ushort>
	// System.Collections.Generic.Queue.Enumerator<ET.Client.WindowID>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<ET.Client.WindowID>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<float,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<int,long>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<int,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<object,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<float,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<int,long>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<int,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<object,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<int,long>
	// System.Collections.Generic.SortedDictionary.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<int,long>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<int,long>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<int,long>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<int,long>
	// System.Collections.Generic.SortedDictionary.KeyCollection<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<object,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<float,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<int,long>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<int,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<long,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<int,long>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<int,long>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,long>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,long>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<object,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary<float,object>
	// System.Collections.Generic.SortedDictionary<int,long>
	// System.Collections.Generic.SortedDictionary<int,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedDictionary<object,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<DotRecast.Detour.StraightPathItem>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.Ability.TeamFlagType>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.AbilityConfig.BuffTagType>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.Client.WindowID>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.EntityRef<object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Numerics.Vector3>
	// System.Collections.ObjectModel.ReadOnlyCollection<Unity.Mathematics.float3>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.ObjectModel.ReadOnlyCollection<byte>
	// System.Collections.ObjectModel.ReadOnlyCollection<float>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<long>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<DotRecast.Core.RcVec3f>
	// System.Comparison<DotRecast.Detour.StraightPathItem>
	// System.Comparison<ET.Ability.AbilityBuffMonitorTriggerEvent>
	// System.Comparison<ET.Ability.SkillSlotType>
	// System.Comparison<ET.Ability.TeamFlagType>
	// System.Comparison<ET.AbilityConfig.AnimatorMotionName>
	// System.Comparison<ET.AbilityConfig.BuffTagGroupType>
	// System.Comparison<ET.AbilityConfig.BuffTagType>
	// System.Comparison<ET.AbilityConfig.BuffType>
	// System.Comparison<ET.AbilityConfig.NumericType>
	// System.Comparison<ET.Client.WindowID>
	// System.Comparison<ET.EntityRef<object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Comparison<System.Nullable<UnityEngine.RaycastHit>>
	// System.Comparison<System.Numerics.Vector3>
	// System.Comparison<Unity.Mathematics.float3>
	// System.Comparison<UnityEngine.EventSystems.RaycastResult>
	// System.Comparison<UnityEngine.Vector2>
	// System.Comparison<byte>
	// System.Comparison<float>
	// System.Comparison<int>
	// System.Comparison<long>
	// System.Comparison<object>
	// System.Comparison<uint>
	// System.Func<System.Threading.Tasks.VoidTaskResult>
	// System.Func<System.ValueTuple<byte,object>>
	// System.Func<System.ValueTuple<uint,uint>>
	// System.Func<UnityEngine.Vector3>
	// System.Func<int,object>
	// System.Func<object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,System.ValueTuple<uint,uint>>
	// System.Func<object,Unity.Mathematics.float3,System.ValueTuple<byte,Unity.Mathematics.float3>>
	// System.Func<object,object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,object,System.ValueTuple<uint,uint>>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Linq.Buffer<ET.RpcInfo>
	// System.Linq.Buffer<object>
	// System.Nullable<ET.AbilityConfig.BuffTagGroupType>
	// System.Nullable<UnityEngine.RaycastHit>
	// System.Predicate<DotRecast.Detour.StraightPathItem>
	// System.Predicate<ET.Ability.TeamFlagType>
	// System.Predicate<ET.AbilityConfig.BuffTagGroupType>
	// System.Predicate<ET.AbilityConfig.BuffTagType>
	// System.Predicate<ET.Client.WindowID>
	// System.Predicate<ET.EntityRef<object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Predicate<System.Numerics.Vector3>
	// System.Predicate<Unity.Mathematics.float3>
	// System.Predicate<UnityEngine.EventSystems.RaycastResult>
	// System.Predicate<byte>
	// System.Predicate<float>
	// System.Predicate<int>
	// System.Predicate<long>
	// System.Predicate<object>
	// System.Predicate<ushort>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConditionalWeakTable.<>c<object,object>
	// System.Runtime.CompilerServices.ConditionalWeakTable.CreateValueCallback<object,object>
	// System.Runtime.CompilerServices.ConditionalWeakTable.Enumerator<object,object>
	// System.Runtime.CompilerServices.ConditionalWeakTable<object,object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.ValueTuple<uint,uint>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.ValueTuple<uint,uint>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.Task<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskFactory.<>c<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.TaskFactory.<>c<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass32_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass32_0<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass32_0<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.TaskFactory<object>
	// System.ValueTuple<ET.Ability.TeamFlagType,object>
	// System.ValueTuple<ET.AbilityConfig.NumericType,float>
	// System.ValueTuple<Unity.Mathematics.float3,Unity.Mathematics.float3>
	// System.ValueTuple<Unity.Mathematics.float3,object>
	// System.ValueTuple<UnityEngine.Vector2,float,float>
	// System.ValueTuple<UnityEngine.Vector2,float>
	// System.ValueTuple<byte,DotRecast.Core.RcVec3f>
	// System.ValueTuple<byte,ET.AbilityConfig.AnimatorMotionName>
	// System.ValueTuple<byte,Unity.Mathematics.float3>
	// System.ValueTuple<byte,byte,Unity.Mathematics.float3>
	// System.ValueTuple<byte,byte,object>
	// System.ValueTuple<byte,byte>
	// System.ValueTuple<byte,float>
	// System.ValueTuple<byte,object,object>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<float,object>
	// System.ValueTuple<int,object>
	// System.ValueTuple<object,System.Nullable<UnityEngine.RaycastHit>>
	// System.ValueTuple<object,int>
	// System.ValueTuple<uint,object>
	// System.ValueTuple<uint,uint>
	// System.WeakReference<object>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.InvokableCall<int>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Events.UnityEvent<int>
	// UnityEngine.Events.UnityEvent<object>
	// }}

	public void RefMethods()
	{
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagGroupType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.CoinType,int>(System.Collections.Generic.IDictionary<ET.AbilityConfig.CoinType,int>)
		// string Bright.Common.StringUtil.CollectionToString<System.Numerics.Vector3>(System.Collections.Generic.IEnumerable<System.Numerics.Vector3>)
		// string Bright.Common.StringUtil.CollectionToString<float>(System.Collections.Generic.IEnumerable<float>)
		// string Bright.Common.StringUtil.CollectionToString<int>(System.Collections.Generic.IEnumerable<int>)
		// string Bright.Common.StringUtil.CollectionToString<object,int>(System.Collections.Generic.IDictionary<object,int>)
		// string Bright.Common.StringUtil.CollectionToString<object>(System.Collections.Generic.IEnumerable<object>)
		// object DG.Tweening.TweenSettingsExtensions.OnComplete<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.SetEase<object>(object,DG.Tweening.Ease)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.AFsmNodeHandler.<OnEnter>d__4>(ET.ETTaskCompleted&,ET.AFsmNodeHandler.<OnEnter>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_AttackArea.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffAdd.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffRemove.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffResetDuration.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffStackCountChg.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffStackCountChg.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_CallActor.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_CallActor.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_CallAoe.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_CallAoe.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_CoinAdd.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_CoinAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_DamageUnit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_DeathShow.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_EffectCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_EffectCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_EffectRemove.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_EffectRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_FaceTo.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_FireBullet.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_ForceMove.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_PlayAnimator.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_PlayAudio.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_TimelineJumpTime.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_TimelineJumpTime.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_TimelinePlay.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_TimelinePlay.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_TimelineReplace.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_TimelineReplace.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Aoe.EventHandler_AoeOnEnter.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Aoe.EventHandler_AoeOnEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Aoe.EventHandler_AoeOnExist.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Aoe.EventHandler_AoeOnExist.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Aoe.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Aoe.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Aoe.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Aoe.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_BulletOnHitMesh.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_BulletOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnKill.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_SkillOnCast.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_SkillOnCast.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_UnitChgSaveSelectObj.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_UnitChgSaveSelectObj.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_UnitOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_UnitOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_UnitOnHitMesh.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_UnitOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Buff.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Buff.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHitMesh.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Bullet.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Bullet.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandler_Bullet.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandler_Bullet.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_Direction>d__6>(ET.ETTaskCompleted&,ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_Direction>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_TargetPosition>d__7>(ET.ETTaskCompleted&,ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_TargetPosition>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.NoticeUnitBuffStatusChg_AnimatorPlay.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.NoticeUnitBuffStatusChg_AnimatorPlay.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.BattleCfgIdChoose_RefreshARRoomUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.BattleCfgIdChoose_RefreshARRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.BattleCfgIdChoose_RefreshLobbyUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.BattleCfgIdChoose_RefreshLobbyUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.BattleCfgIdChoose_RefreshRoomUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.BattleCfgIdChoose_RefreshRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgARRoomPVPSystem.<ShowBattleCfgChoose>d__3>(ET.ETTaskCompleted&,ET.Client.DlgARRoomPVPSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgARRoomPVPSystem.<ShowQrCode>d__13>(ET.ETTaskCompleted&,ET.Client.DlgARRoomPVPSystem.<ShowQrCode>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgARRoomSystem.<ShowBattleCfgChoose>d__3>(ET.ETTaskCompleted&,ET.Client.DlgARRoomSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgARRoomSystem.<ShowQrCode>d__12>(ET.ETTaskCompleted&,ET.Client.DlgARRoomSystem.<ShowQrCode>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgBattleCfgChooseSystem.<RefreshUI>d__9>(ET.ETTaskCompleted&,ET.Client.DlgBattleCfgChooseSystem.<RefreshUI>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgBattleSystem.<RegisterSkill>d__2>(ET.ETTaskCompleted&,ET.Client.DlgBattleSystem.<RegisterSkill>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4>(ET.ETTaskCompleted&,ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgLoginSystem.<ChkIsShowDebugUI>d__7>(ET.ETTaskCompleted&,ET.Client.DlgLoginSystem.<ChkIsShowDebugUI>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgLoginSystem.<InitDebugMode>d__10>(ET.ETTaskCompleted&,ET.Client.DlgLoginSystem.<InitDebugMode>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgLoginSystem.<_ShowWindow>d__2>(ET.ETTaskCompleted&,ET.Client.DlgLoginSystem.<_ShowWindow>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgRoomSystem.<ShowBattleCfgChoose>d__3>(ET.ETTaskCompleted&,ET.Client.DlgRoomSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.DlgRoomSystem.<ShowQrCode>d__12>(ET.ETTaskCompleted&,ET.Client.DlgRoomSystem.<ShowQrCode>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.EnterMapFinish_UI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.EventLoggingSDKComponentSystem.<Awake>d__2>(ET.ETTaskCompleted&,ET.Client.EventLoggingSDKComponentSystem.<Awake>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.EventLoggingSDKComponentSystem.<Destroy>d__3>(ET.ETTaskCompleted&,ET.Client.EventLoggingSDKComponentSystem.<Destroy>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.GamePlayChg_ChkAllMyTowerUpgrade.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.GamePlayChg_ChkAllMyTowerUpgrade.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.GamePlayChg_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.GamePlayChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.GamePlayCoinChg_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.GamePlayCoinChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.LoginSDKComponentSystem.<Destroy>d__14>(ET.ETTaskCompleted&,ET.Client.LoginSDKComponentSystem.<Destroy>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_CreateMyUnitHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_CreateMyUnitHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_CreateUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_CreateUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_RemoveUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_RemoveUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_StopHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_StopHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncDataListHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncDataListHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncPlayAnimatorHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncPlayAnimatorHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncPlayAudioHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncPlayAudioHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.MainQualitySettingComponentSystem.<Awake>d__3>(ET.ETTaskCompleted&,ET.Client.MainQualitySettingComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.NetClientComponentOnReadEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.NetClientComponentOnReadEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.NoticeEventLogging_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.NoticeEventLogging_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.OnPatchDownloadProgressEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.OnPatchDownloadProgressEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.OnPatchDownlodFailedEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.OnPatchDownlodFailedEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7>(ET.ETTaskCompleted&,ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.RoomInfoChg_RefreshARRoomPVPUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.RoomInfoChg_RefreshARRoomPVPUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.RoomInfoChg_RefreshARRoomUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.RoomInfoChg_RefreshARRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.RoomInfoChg_RefreshRoomUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.RoomInfoChg_RefreshRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.SwitchLanguageEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.SwitchLanguageEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIAudioManagerComponentSystem.<PlayUIAudio>d__4>(ET.ETTaskCompleted&,ET.Client.UIAudioManagerComponentSystem.<PlayUIAudio>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIAudioManagerComponentSystem.<PlayUIGuideAudio>d__5>(ET.ETTaskCompleted&,ET.Client.UIAudioManagerComponentSystem.<PlayUIGuideAudio>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIAudioManagerComponentSystem.<_PlayMusicOne>d__8>(ET.ETTaskCompleted&,ET.Client.UIAudioManagerComponentSystem.<_PlayMusicOne>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6>(ET.ETTaskCompleted&,ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5>(ET.ETTaskCompleted&,ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideComponentSystem.<StopUIGuide>d__7>(ET.ETTaskCompleted&,ET.Client.UIGuideComponentSystem.<StopUIGuide>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8>(ET.ETTaskCompleted&,ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideHelper_StaticMethod.<HideTowerInfo>d__9>(ET.ETTaskCompleted&,ET.Client.UIGuideHelper_StaticMethod.<HideTowerInfo>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7>(ET.ETTaskCompleted&,ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetAudio>d__15>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetAudio>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetParentUIGuide>d__24>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetParentUIGuide>d__24&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenText>d__18>(ET.ETTaskCompleted&,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenText>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIManagerComponentSystem.<Init>d__3>(ET.ETTaskCompleted&,ET.Client.UIManagerComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.EntryEvent1_InitShare.<Run>d__0>(ET.ETTaskCompleted&,ET.EntryEvent1_InitShare.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.EventHandler_UnitBeKill.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.ETTaskCompleted&,ET.EventHandler_UnitBeKill.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayComponentSystem.<DownloadMapRecast>d__5>(ET.ETTaskCompleted&,ET.GamePlayComponentSystem.<DownloadMapRecast>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayComponentSystem.<InitWhenGlobal>d__3>(ET.ETTaskCompleted&,ET.GamePlayComponentSystem.<InitWhenGlobal>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayComponentSystem.<InitWhenRoom>d__2>(ET.ETTaskCompleted&,ET.GamePlayComponentSystem.<InitWhenRoom>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7>(ET.ETTaskCompleted&,ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayTowerDefenseComponentSystem.<TransToBattle>d__18>(ET.ETTaskCompleted&,ET.GamePlayTowerDefenseComponentSystem.<TransToBattle>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayTowerDefenseComponentSystem.<TransToGameEnd>d__23>(ET.ETTaskCompleted&,ET.GamePlayTowerDefenseComponentSystem.<TransToGameEnd>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayTowerDefenseComponentSystem.<TransToPutHome>d__16>(ET.ETTaskCompleted&,ET.GamePlayTowerDefenseComponentSystem.<TransToPutHome>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayTowerDefenseComponentSystem.<TransToPutMonsterPoint>d__17>(ET.ETTaskCompleted&,ET.GamePlayTowerDefenseComponentSystem.<TransToPutMonsterPoint>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MoveHelper.<FindPathMoveToAsync>d__0>(ET.ETTaskCompleted&,ET.MoveHelper.<FindPathMoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.ETTaskCompleted&,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.AOIEntitySystem.<WaitNextFrame>d__2>(System.Runtime.CompilerServices.TaskAwaiter&,ET.AOIEntitySystem.<WaitNextFrame>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.DlgCommonTipSystem.<_TipMove>d__4>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.DlgCommonTipSystem.<_TipMove>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.LoginSDKComponentSystem.<Awake>d__2>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.LoginSDKComponentSystem.<Awake>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.LoginSDKComponentSystem.<SDKLoginIn>d__16>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.LoginSDKComponentSystem.<SDKLoginIn>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.LoginSDKComponentSystem.<SDKLoginOut>d__17>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.LoginSDKComponentSystem.<SDKLoginOut>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.NavmeshComponentSystem.<CreateCrowd>d__3>(System.Runtime.CompilerServices.TaskAwaiter&,ET.NavmeshComponentSystem.<CreateCrowd>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>&,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.DlgBattleSystem.<ShowMesh>d__15>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.DlgBattleSystem.<ShowMesh>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.DlgBattleTowerARSystem.<ShowMesh>d__36>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.DlgBattleTowerARSystem.<ShowMesh>d__36&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.DlgBattleTowerSystem.<ShowMesh>d__36>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.DlgBattleTowerSystem.<ShowMesh>d__36&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.ConsoleComponentSystem.<Start>d__3>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.GamePlayComponentSystem.<DownloadMapRecast>d__5>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.GamePlayComponentSystem.<DownloadMapRecast>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AIComponentSystem.<FirstCheck>d__3>(object&,ET.AIComponentSystem.<FirstCheck>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AI_Attack.<Execute>d__3>(object&,ET.AI_Attack.<Execute>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AI_KaoJin.<Execute>d__1>(object&,ET.AI_KaoJin.<Execute>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AI_TowerDefense_AttackWhenBlock.<Execute>d__5>(object&,ET.AI_TowerDefense_AttackWhenBlock.<Execute>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AI_TowerDefense_Escape.<Execute>d__3>(object&,ET.AI_TowerDefense_Escape.<Execute>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AI_XunLuo.<Execute>d__1>(object&,ET.AI_XunLuo.<Execute>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AOIEntitySystem.<WaitNextFrame>d__2>(object&,ET.AOIEntitySystem.<WaitNextFrame>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.ActionHandlerComponentSystem.<Run>d__3>(object&,ET.Ability.ActionHandlerComponentSystem.<Run>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_AttackArea.<Run>d__0>(object&,ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffAdd.<Run>d__0>(object&,ET.Ability.Action_BuffAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffRemove.<Run>d__0>(object&,ET.Ability.Action_BuffRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffResetDuration.<Run>d__0>(object&,ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffStackCountChg.<Run>d__0>(object&,ET.Ability.Action_BuffStackCountChg.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_CallActor.<Run>d__0>(object&,ET.Ability.Action_CallActor.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_CallAoe.<Run>d__0>(object&,ET.Ability.Action_CallAoe.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_CoinAdd.<Run>d__0>(object&,ET.Ability.Action_CoinAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_DamageUnit.<Run>d__0>(object&,ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_DeathShow.<Run>d__0>(object&,ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_EffectCreate.<Run>d__0>(object&,ET.Ability.Action_EffectCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_EffectRemove.<Run>d__0>(object&,ET.Ability.Action_EffectRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_FaceTo.<Run>d__0>(object&,ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_FireBullet.<Run>d__0>(object&,ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_ForceMove.<Run>d__0>(object&,ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_PlayAnimator.<Run>d__0>(object&,ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_PlayAudio.<Run>d__0>(object&,ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_TimelineJumpTime.<Run>d__0>(object&,ET.Ability.Action_TimelineJumpTime.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_TimelinePlay.<Run>d__0>(object&,ET.Ability.Action_TimelinePlay.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_TimelineReplace.<Run>d__0>(object&,ET.Ability.Action_TimelineReplace.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Client.AudioPlayObjSystem.<PlayAudio>d__5>(object&,ET.Ability.Client.AudioPlayObjSystem.<PlayAudio>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_Direction>d__9>(object&,ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_Direction>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_TargetPosition>d__10>(object&,ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_TargetPosition>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleHelper.<DoMoveDirection>d__3>(object&,ET.Ability.MoveOrIdleHelper.<DoMoveDirection>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleHelper.<DoMoveTargetPosition>d__4>(object&,ET.Ability.MoveOrIdleHelper.<DoMoveTargetPosition>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.SkillComponentSystem.<ReplaceSkillTimeline>d__8>(object&,ET.Ability.SkillComponentSystem.<ReplaceSkillTimeline>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.SkillHelper.<ReplaceSkillTimeline>d__3>(object&,ET.Ability.SkillHelper.<ReplaceSkillTimeline>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ARSessionComponentSystem.<Init>d__3>(object&,ET.Client.ARSessionComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ARSessionComponentSystem.<InitCallBack>d__7>(object&,ET.Client.ARSessionComponentSystem.<InitCallBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ARSessionComponentSystem.<LoadARSession>d__4>(object&,ET.Client.ARSessionComponentSystem.<LoadARSession>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0>(object&,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0>(object&,ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1>(object&,ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(object&,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4>(object&,ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<CreateRoom>d__12>(object&,ET.Client.DlgARHallSystem.<CreateRoom>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<EnterARRoomUI>d__14>(object&,ET.Client.DlgARHallSystem.<EnterARRoomUI>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<HideMenu>d__4>(object&,ET.Client.DlgARHallSystem.<HideMenu>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<InitArSession>d__2>(object&,ET.Client.DlgARHallSystem.<InitArSession>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<JoinRoom>d__15>(object&,ET.Client.DlgARHallSystem.<JoinRoom>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<OnClose>d__6>(object&,ET.Client.DlgARHallSystem.<OnClose>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<OnCreateRoomCallBack>d__7>(object&,ET.Client.DlgARHallSystem.<OnCreateRoomCallBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<OnFinishedCallBack>d__11>(object&,ET.Client.DlgARHallSystem.<OnFinishedCallBack>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<OnJoinByQRCodeCallBack>d__10>(object&,ET.Client.DlgARHallSystem.<OnJoinByQRCodeCallBack>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<QuitRoom>d__13>(object&,ET.Client.DlgARHallSystem.<QuitRoom>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<ReStart>d__3>(object&,ET.Client.DlgARHallSystem.<ReStart>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<SetARRoomInfoAsync>d__16>(object&,ET.Client.DlgARHallSystem.<SetARRoomInfoAsync>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5>(object&,ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<ChgRoomMemberStatus>d__14>(object&,ET.Client.DlgARRoomPVPSystem.<ChgRoomMemberStatus>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<ChgRoomSeat>d__15>(object&,ET.Client.DlgARRoomPVPSystem.<ChgRoomSeat>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<GetRoomInfo>d__6>(object&,ET.Client.DlgARRoomPVPSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<KickOutRoom>d__10>(object&,ET.Client.DlgARRoomPVPSystem.<KickOutRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<OnChgTeam>d__17>(object&,ET.Client.DlgARRoomPVPSystem.<OnChgTeam>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<OnChooseBattleCfg>d__16>(object&,ET.Client.DlgARRoomPVPSystem.<OnChooseBattleCfg>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<RefreshBattleCfgIdChoose>d__4>(object&,ET.Client.DlgARRoomPVPSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomPVPSystem.<_QuitRoom>d__12>(object&,ET.Client.DlgARRoomPVPSystem.<_QuitRoom>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<ChgRoomMemberStatus>d__13>(object&,ET.Client.DlgARRoomSystem.<ChgRoomMemberStatus>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<ChgRoomSeat>d__14>(object&,ET.Client.DlgARRoomSystem.<ChgRoomSeat>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<GetRoomInfo>d__6>(object&,ET.Client.DlgARRoomSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<KickOutRoom>d__9>(object&,ET.Client.DlgARRoomSystem.<KickOutRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<OnChgTeam>d__16>(object&,ET.Client.DlgARRoomSystem.<OnChgTeam>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<OnChooseBattleCfg>d__15>(object&,ET.Client.DlgARRoomSystem.<OnChooseBattleCfg>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<RefreshBattleCfgIdChoose>d__4>(object&,ET.Client.DlgARRoomSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgARRoomSystem.<_QuitRoom>d__11>(object&,ET.Client.DlgARRoomSystem.<_QuitRoom>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleDragItemSystem.<DrawMonsterCall2HeadQuarter>d__25>(object&,ET.Client.DlgBattleDragItemSystem.<DrawMonsterCall2HeadQuarter>d__25&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleDragItemSystem.<_HideMonsterCall2HeadQuarter>d__27>(object&,ET.Client.DlgBattleDragItemSystem.<_HideMonsterCall2HeadQuarter>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleSystem.<RegisterClear>d__1>(object&,ET.Client.DlgBattleSystem.<RegisterClear>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleSystem.<RegisterSkill>d__2>(object&,ET.Client.DlgBattleSystem.<RegisterSkill>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleSystem.<_QuitBattle>d__7>(object&,ET.Client.DlgBattleSystem.<_QuitBattle>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerARSystem.<BuyTower>d__27>(object&,ET.Client.DlgBattleTowerARSystem.<BuyTower>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerARSystem.<ReadyWhenRestTime>d__29>(object&,ET.Client.DlgBattleTowerARSystem.<ReadyWhenRestTime>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerARSystem.<RefreshBuyTower>d__28>(object&,ET.Client.DlgBattleTowerARSystem.<RefreshBuyTower>d__28&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerARSystem.<ShowAvatar>d__38>(object&,ET.Client.DlgBattleTowerARSystem.<ShowAvatar>d__38&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerARSystem.<_QuitBattle>d__14>(object&,ET.Client.DlgBattleTowerARSystem.<_QuitBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__8>(object&,ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<Show>d__2>(object&,ET.Client.DlgBattleTowerEndSystem.<Show>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<ShowEffect>d__3>(object&,ET.Client.DlgBattleTowerEndSystem.<ShowEffect>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<ShowEffectEndlessChallenge>d__7>(object&,ET.Client.DlgBattleTowerEndSystem.<ShowEffectEndlessChallenge>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<ShowEffectNormal>d__4>(object&,ET.Client.DlgBattleTowerEndSystem.<ShowEffectNormal>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVE>d__6>(object&,ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVE>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVP>d__5>(object&,ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVP>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<BuyTower>d__27>(object&,ET.Client.DlgBattleTowerSystem.<BuyTower>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<ReadyWhenRestTime>d__29>(object&,ET.Client.DlgBattleTowerSystem.<ReadyWhenRestTime>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__28>(object&,ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__28&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<ShowAvatar>d__38>(object&,ET.Client.DlgBattleTowerSystem.<ShowAvatar>d__38&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<_QuitBattle>d__14>(object&,ET.Client.DlgBattleTowerSystem.<_QuitBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBeginnersGuideStorySystem.<DoNext>d__2>(object&,ET.Client.DlgBeginnersGuideStorySystem.<DoNext>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4>(object&,ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBeginnersGuideStorySystem.<ShowStory>d__3>(object&,ET.Client.DlgBeginnersGuideStorySystem.<ShowStory>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgCommonTipSystem.<_DoShowTip>d__3>(object&,ET.Client.DlgCommonTipSystem.<_DoShowTip>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<ClickAvatar>d__9>(object&,ET.Client.DlgGameModeARSystem.<ClickAvatar>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<ClickRank>d__12>(object&,ET.Client.DlgGameModeARSystem.<ClickRank>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<EnterAREndlessChallenge>d__4>(object&,ET.Client.DlgGameModeARSystem.<EnterAREndlessChallenge>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<EnterARPVE>d__5>(object&,ET.Client.DlgGameModeARSystem.<EnterARPVE>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<EnterARPVP>d__6>(object&,ET.Client.DlgGameModeARSystem.<EnterARPVP>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<EnterScanCode>d__7>(object&,ET.Client.DlgGameModeARSystem.<EnterScanCode>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<EnterTutorialFirst>d__8>(object&,ET.Client.DlgGameModeARSystem.<EnterTutorialFirst>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<ReturnLogin>d__11>(object&,ET.Client.DlgGameModeARSystem.<ReturnLogin>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeARSystem.<_ShowWindow>d__3>(object&,ET.Client.DlgGameModeARSystem.<_ShowWindow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterARRoomCreateMode>d__4>(object&,ET.Client.DlgGameModeSystem.<EnterARRoomCreateMode>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterARRoomJoinMode>d__5>(object&,ET.Client.DlgGameModeSystem.<EnterARRoomJoinMode>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3>(object&,ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2>(object&,ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<ReturnLogin>d__6>(object&,ET.Client.DlgGameModeSystem.<ReturnLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<CreateRoom>d__5>(object&,ET.Client.DlgHallSystem.<CreateRoom>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<GetRoomList>d__3>(object&,ET.Client.DlgHallSystem.<GetRoomList>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<JoinRoom>d__8>(object&,ET.Client.DlgHallSystem.<JoinRoom>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<RefreshRoomList>d__6>(object&,ET.Client.DlgHallSystem.<RefreshRoomList>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<ReturnBack>d__7>(object&,ET.Client.DlgHallSystem.<ReturnBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLobbySystem.<EnterMap>d__4>(object&,ET.Client.DlgLobbySystem.<EnterMap>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLobbySystem.<OnChooseBattleCfg>d__6>(object&,ET.Client.DlgLobbySystem.<OnChooseBattleCfg>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLobbySystem.<ReturnBack>d__5>(object&,ET.Client.DlgLobbySystem.<ReturnBack>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<InitAccount>d__8>(object&,ET.Client.DlgLoginSystem.<InitAccount>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<LoginWhenEditor>d__12>(object&,ET.Client.DlgLoginSystem.<LoginWhenEditor>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<LoginWhenGuest>d__13>(object&,ET.Client.DlgLoginSystem.<LoginWhenGuest>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<LoginWhenSDK>d__14>(object&,ET.Client.DlgLoginSystem.<LoginWhenSDK>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<LoginWhenSDK_LoginDone>d__15>(object&,ET.Client.DlgLoginSystem.<LoginWhenSDK_LoginDone>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<OnSwitchPlayerClickHandler>d__11>(object&,ET.Client.DlgLoginSystem.<OnSwitchPlayerClickHandler>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLoginSystem.<_ShowWindow>d__2>(object&,ET.Client.DlgLoginSystem.<_ShowWindow>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<AddAvatarItemRefreshListener>d__13>(object&,ET.Client.DlgPersonalInformationSystem.<AddAvatarItemRefreshListener>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<HidePersonalInfo>d__8>(object&,ET.Client.DlgPersonalInformationSystem.<HidePersonalInfo>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<Logout>d__5>(object&,ET.Client.DlgPersonalInformationSystem.<Logout>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<OnBGClick>d__7>(object&,ET.Client.DlgPersonalInformationSystem.<OnBGClick>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<OnSave>d__6>(object&,ET.Client.DlgPersonalInformationSystem.<OnSave>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgPersonalInformationSystem.<_ShowWindow>d__3>(object&,ET.Client.DlgPersonalInformationSystem.<_ShowWindow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRankEndlessChallengeSystem.<AddRankItemRefreshListener>d__6>(object&,ET.Client.DlgRankEndlessChallengeSystem.<AddRankItemRefreshListener>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRankEndlessChallengeSystem.<ShowPersonalInfo>d__7>(object&,ET.Client.DlgRankEndlessChallengeSystem.<ShowPersonalInfo>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRankEndlessChallengeSystem.<ShowRankScrollItem>d__5>(object&,ET.Client.DlgRankEndlessChallengeSystem.<ShowRankScrollItem>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRankEndlessChallengeSystem.<_ShowWindow>d__2>(object&,ET.Client.DlgRankEndlessChallengeSystem.<_ShowWindow>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__13>(object&,ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<ChgRoomSeat>d__14>(object&,ET.Client.DlgRoomSystem.<ChgRoomSeat>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<GetRoomInfo>d__6>(object&,ET.Client.DlgRoomSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<KickOutRoom>d__9>(object&,ET.Client.DlgRoomSystem.<KickOutRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<OnChgTeam>d__16>(object&,ET.Client.DlgRoomSystem.<OnChgTeam>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<OnChooseBattleCfg>d__15>(object&,ET.Client.DlgRoomSystem.<OnChooseBattleCfg>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<RefreshBattleCfgIdChoose>d__4>(object&,ET.Client.DlgRoomSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<_QuitRoom>d__11>(object&,ET.Client.DlgRoomSystem.<_QuitRoom>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d>(object&,ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d>(object&,ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EnterMapFinish_UI.<Run>d__0>(object&,ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EnterMapHelper.<EnterMapAsync>d__0>(object&,ET.Client.EnterMapHelper.<EnterMapAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<ChkHotUpdateAsync>d__1>(object&,ET.Client.EntryEvent3_InitClient.<ChkHotUpdateAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<EnterLogin>d__6>(object&,ET.Client.EntryEvent3_InitClient.<EnterLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<ReloadAll>d__5>(object&,ET.Client.EntryEvent3_InitClient.<ReloadAll>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<Run>d__0>(object&,ET.Client.EntryEvent3_InitClient.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0>(object&,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(object&,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendARCameraPos>d__3>(object&,ET.Client.GamePlayHelper.<SendARCameraPos>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayPKComponentSystem.<OnChkRay>d__11>(object&,ET.Client.GamePlayPKComponentSystem.<OnChkRay>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseComponentSystem.<DoHideMyMonsterCall2HeadQuarter>d__14>(object&,ET.Client.GamePlayTowerDefenseComponentSystem.<DoHideMyMonsterCall2HeadQuarter>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseComponentSystem.<OnChkRay>d__18>(object&,ET.Client.GamePlayTowerDefenseComponentSystem.<OnChkRay>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendBuyPlayerTower>d__3>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendBuyPlayerTower>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendCallMonster>d__12>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendCallMonster>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendCallOwnTower>d__5>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendCallOwnTower>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendCallTower>d__11>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendCallTower>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendClearAllMonster>d__14>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendClearAllMonster>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendClearMyTower>d__13>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendClearMyTower>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendMovePlayerTower>d__9>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendMovePlayerTower>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendPutHome>d__0>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendPutHome>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendReadyWhenRestTime>d__10>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendReadyWhenRestTime>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendReclaimPlayerTower>d__8>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendReclaimPlayerTower>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendRefreshBuyPlayerTower>d__4>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendRefreshBuyPlayerTower>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendScalePlayerTower>d__7>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendScalePlayerTower>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendUpgradePlayerTower>d__6>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendUpgradePlayerTower>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GlobalComponentSystem.<AddComponents>d__5>(object&,ET.Client.GlobalComponentSystem.<AddComponents>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GlobalComponentSystem.<Awake>d__3>(object&,ET.Client.GlobalComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.HallSceneEnterStart_UI.<DealWhenIsDebugMode>d__1>(object&,ET.Client.HallSceneEnterStart_UI.<DealWhenIsDebugMode>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.HallSceneEnterStart_UI.<DealWhenNotDebugMode>d__2>(object&,ET.Client.HallSceneEnterStart_UI.<DealWhenNotDebugMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.HallSceneEnterStart_UI.<Run>d__0>(object&,ET.Client.HallSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginFinish_UI.<FinishedCallBack>d__1>(object&,ET.Client.LoginFinish_UI.<FinishedCallBack>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginFinish_UI.<Run>d__0>(object&,ET.Client.LoginFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<LoginOut>d__1>(object&,ET.Client.LoginHelper.<LoginOut>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<ReLogin>d__2>(object&,ET.Client.LoginHelper.<ReLogin>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6>(object&,ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginSceneEnterStart_UI.<Run>d__0>(object&,ET.Client.LoginSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(object&,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0>(object&,ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0>(object&,ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_PathfindingResultHandler.<Run>d__0>(object&,ET.Client.M2C_PathfindingResultHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_StartSceneChangeHandler.<Run>d__0>(object&,ET.Client.M2C_StartSceneChangeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ModelClickManagerComponentSystem.<ChkPointDownNextFrame>d__6>(object&,ET.Client.ModelClickManagerComponentSystem.<ChkPointDownNextFrame>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.MoveHelper.<MoveToAsync>d__1>(object&,ET.Client.MoveHelper.<MoveToAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PathLineRendererComponentSystem.<ShowPath>d__4>(object&,ET.Client.PathLineRendererComponentSystem.<ShowPath>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PingComponentAwakeSystem.<PingAsync>d__1>(object&,ET.Client.PingComponentAwakeSystem.<PingAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetMyPlayerModelAll>d__2>(object&,ET.Client.PlayerCacheHelper.<GetMyPlayerModelAll>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7>(object&,ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(object&,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomBattleLevelCfgAsync>d__10>(object&,ET.Client.RoomHelper.<ChgRoomBattleLevelCfgAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomMemberSeatAsync>d__7>(object&,ET.Client.RoomHelper.<ChgRoomMemberSeatAsync>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__6>(object&,ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomMemberTeamAsync>d__8>(object&,ET.Client.RoomHelper.<ChgRoomMemberTeamAsync>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<GetRoomListAsync>d__1>(object&,ET.Client.RoomHelper.<GetRoomListAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<KickMemberOutRoomAsync>d__12>(object&,ET.Client.RoomHelper.<KickMemberOutRoomAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<MemberQuitBattleAsync>d__11>(object&,ET.Client.RoomHelper.<MemberQuitBattleAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__13>(object&,ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<QuitRoomAsync>d__5>(object&,ET.Client.RoomHelper.<QuitRoomAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ReturnBackBattle>d__14>(object&,ET.Client.RoomHelper.<ReturnBackBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2>(object&,ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<Init>d__1>(object&,ET.Client.RouterAddressComponentSystem.<Init>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3>(object&,ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(object&,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneChangeHelper.<SceneChangeTo>d__0>(object&,ET.Client.SceneChangeHelper.<SceneChangeTo>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneHelper.<EnterBattle>d__2>(object&,ET.Client.SceneHelper.<EnterBattle>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneHelper.<EnterHall>d__1>(object&,ET.Client.SceneHelper.<EnterHall>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneHelper.<EnterLogin>d__0>(object&,ET.Client.SceneHelper.<EnterLogin>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ShootTextComponentSystem.<Init>d__2>(object&,ET.Client.ShootTextComponentSystem.<Init>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SkillHelper.<CastSkill>d__1>(object&,ET.Client.SkillHelper.<CastSkill>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SkillHelper.<LearnSkill>d__0>(object&,ET.Client.SkillHelper.<LearnSkill>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIAudioManagerComponentSystem.<_PlayNextMusicOne>d__7>(object&,ET.Client.UIAudioManagerComponentSystem.<_PlayNextMusicOne>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__27>(object&,ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<ShowWindowAsync>d__10>(object&,ET.Client.UIComponentSystem.<ShowWindowAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<!0>>(object&,ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideComponentSystem.<Awake>d__3>(object&,ET.Client.UIGuideComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideComponentSystem.<DoGuideStep>d__8>(object&,ET.Client.UIGuideComponentSystem.<DoGuideStep>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6>(object&,ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5>(object&,ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper.<DoStaticMethodExecute>d__5>(object&,ET.Client.UIGuideHelper.<DoStaticMethodExecute>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper.<DoUIGuide>d__0>(object&,ET.Client.UIGuideHelper.<DoUIGuide>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper.<DoUIGuide>d__1>(object&,ET.Client.UIGuideHelper.<DoUIGuide>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper.<StopUIGuide>d__2>(object&,ET.Client.UIGuideHelper.<StopUIGuide>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper_StaticMethod.<EnterGuideBattle>d__6>(object&,ET.Client.UIGuideHelper_StaticMethod.<EnterGuideBattle>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8>(object&,ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7>(object&,ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper_StaticMethod.<ShowStory>d__5>(object&,ET.Client.UIGuideHelper_StaticMethod.<ShowStory>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<ChkNodeStatus>d__4>(object&,ET.Client.UIGuideStepComponentSystem.<ChkNodeStatus>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<ChkPosChg>d__5>(object&,ET.Client.UIGuideStepComponentSystem.<ChkPosChg>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<ChkSizeChg>d__6>(object&,ET.Client.UIGuideStepComponentSystem.<ChkSizeChg>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<DoGuideStepExecute>d__9>(object&,ET.Client.UIGuideStepComponentSystem.<DoGuideStepExecute>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<DoGuideStepOne>d__7>(object&,ET.Client.UIGuideStepComponentSystem.<DoGuideStepOne>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<FinishClick>d__29>(object&,ET.Client.UIGuideStepComponentSystem.<FinishClick>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23>(object&,ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<SetImageShow>d__21>(object&,ET.Client.UIGuideStepComponentSystem.<SetImageShow>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22>(object&,ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17>(object&,ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19>(object&,ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<ShowUIMaskWhenNoPoint>d__12>(object&,ET.Client.UIGuideStepComponentSystem.<ShowUIMaskWhenNoPoint>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<_DoGuideStep>d__10>(object&,ET.Client.UIGuideStepComponentSystem.<_DoGuideStep>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIManagerHelper.<EnterRoom>d__9>(object&,ET.Client.UIManagerHelper.<EnterRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIManagerHelper.<ExitRoom>d__10>(object&,ET.Client.UIManagerHelper.<ExitRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIManagerHelper.<SetImageByPath>d__7>(object&,ET.Client.UIManagerHelper.<SetImageByPath>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIManagerHelper.<SetMyIcon>d__6>(object&,ET.Client.UIManagerHelper.<SetMyIcon>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ConsoleComponentSystem.<Start>d__3>(object&,ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Entry.<StartAsync>d__2>(object&,ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.GamePlayComponentSystem.<InitWhenGlobal>d__3>(object&,ET.GamePlayComponentSystem.<InitWhenGlobal>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.GamePlayComponentSystem.<InitWhenRoom>d__2>(object&,ET.GamePlayComponentSystem.<InitWhenRoom>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7>(object&,ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.GamePlayTowerDefenseComponentSystem.<TransToRestTime>d__19>(object&,ET.GamePlayTowerDefenseComponentSystem.<TransToRestTime>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.GamePlayTowerDefenseComponentSystem.<TransToShowStartEffect>d__15>(object&,ET.GamePlayTowerDefenseComponentSystem.<TransToShowStartEffect>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6>(object&,ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<!0>>(object&,ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.PathfindingComponentSystem.<Init>d__3>(object&,ET.PathfindingComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.PutHomeComponentSystem.<ChkNextStep>d__9>(object&,ET.PutHomeComponentSystem.<ChkNextStep>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!0>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<!0>>(object&,ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!0>.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<Wait>d__4<!0>>(object&,ET.ObjectWaitSystem.<Wait>d__4<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!0>.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<Wait>d__5<!0>>(object&,ET.ObjectWaitSystem.<Wait>d__5<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<Unity.Mathematics.float3,object>>.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendGetMonsterCall2HeadQuarterPath>d__1>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendGetMonsterCall2HeadQuarterPath>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Ability.SkillComponentSystem.<CastSkill>d__7>(object&,ET.Ability.SkillComponentSystem.<CastSkill>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Ability.SkillHelper.<CastSkill>d__2>(object&,ET.Ability.SkillHelper.<CastSkill>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseHelper.<SendPutMonsterCall>d__2>(object&,ET.Client.GamePlayTowerDefenseHelper.<SendPutMonsterCall>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<Login>d__0>(object&,ET.Client.LoginHelper.<Login>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<SendGetPlayerModelAsync>d__11>(object&,ET.Client.PlayerCacheHelper.<SendGetPlayerModelAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,ET.Client.RankHelper.<SendGetRankShowAsync>d__2>(object&,ET.Client.RankHelper.<SendGetRankShowAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<GetRouterAddress>d__1>(object&,ET.Client.RouterHelper.<GetRouterAddress>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadSceneAsync>d__12>(object&,ET.Client.ResComponentSystem.<LoadSceneAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideHelper_StaticMethod.<ChkARMeshShow>d__3>(ET.ETTaskCompleted&,ET.Client.UIGuideHelper_StaticMethod.<ChkARMeshShow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.UIGuideHelper_StaticMethod.<ChkWaitTime>d__2>(ET.ETTaskCompleted&,ET.Client.UIGuideHelper_StaticMethod.<ChkWaitTime>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleComponentSystem.<_CreateIdleTimeLine>d__11>(object&,ET.Ability.MoveOrIdleComponentSystem.<_CreateIdleTimeLine>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Ability.MoveOrIdleComponentSystem.<_CreateMoveTimeLine>d__12>(object&,ET.Ability.MoveOrIdleComponentSystem.<_CreateMoveTimeLine>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleDragItemSystem.<_DrawMonsterCall2HeadQuarter>d__26>(object&,ET.Client.DlgBattleDragItemSystem.<_DrawMonsterCall2HeadQuarter>d__26&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__4>(object&,ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__3>(object&,ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMonsterCall2HeadQuarterByPos>d__15>(object&,ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMonsterCall2HeadQuarterByPos>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMyMonsterCall2HeadQuarter>d__13>(object&,ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMyMonsterCall2HeadQuarter>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<SendSavePlayerModelAsync>d__12>(object&,ET.Client.PlayerCacheHelper.<SendSavePlayerModelAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<CreateRoomAsync>d__3>(object&,ET.Client.RoomHelper.<CreateRoomAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<GetRoomInfoAsync>d__2>(object&,ET.Client.RoomHelper.<GetRoomInfoAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<JoinRoomAsync>d__4>(object&,ET.Client.RoomHelper.<JoinRoomAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<SetARRoomInfoAsync>d__9>(object&,ET.Client.RoomHelper.<SetARRoomInfoAsync>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideHelper.<DoStaticMethodChk>d__4>(object&,ET.Client.UIGuideHelper.<DoStaticMethodChk>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.Client.UIGuideStepComponentSystem.<ChkGuideStepCondition>d__8>(object&,ET.Client.UIGuideStepComponentSystem.<ChkGuideStepCondition>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.MoveByPathComponentSystem.<MoveToAsync>d__5>(object&,ET.MoveByPathComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.MoveHelper.<MoveToAsync>d__0>(object&,ET.Client.MoveHelper.<MoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6>(object&,ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4>(object&,ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3>(object&,ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.HttpClientHelper.<Get>d__0>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.HttpClientHelper.<Get>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.BuffComponentSystem.<AddBuff>d__3>(object&,ET.Ability.BuffComponentSystem.<AddBuff>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.SkillObjSystem.<CastSkill>d__6>(object&,ET.Ability.SkillObjSystem.<CastSkill>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineComponentSystem.<CreateTimeline>d__4>(object&,ET.Ability.TimelineComponentSystem.<CreateTimeline>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineComponentSystem.<PlayTimeline>d__6>(object&,ET.Ability.TimelineComponentSystem.<PlayTimeline>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineComponentSystem.<ReplaceTimeline>d__5>(object&,ET.Ability.TimelineComponentSystem.<ReplaceTimeline>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineHelper.<CreateTimeline>d__1>(object&,ET.Ability.TimelineHelper.<CreateTimeline>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineHelper.<PlayTimeline>d__3>(object&,ET.Ability.TimelineHelper.<PlayTimeline>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Ability.TimelineHelper.<ReplaceTimeline>d__2>(object&,ET.Ability.TimelineHelper.<ReplaceTimeline>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.IconHelper.<LoadIconSpriteAsync>d__1>(object&,ET.Client.IconHelper.<LoadIconSpriteAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetMyPlayerBackPack>d__5>(object&,ET.Client.PlayerCacheHelper.<GetMyPlayerBackPack>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetMyPlayerBaseInfo>d__4>(object&,ET.Client.PlayerCacheHelper.<GetMyPlayerBaseInfo>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetMyPlayerBattleCard>d__6>(object&,ET.Client.PlayerCacheHelper.<GetMyPlayerBattleCard>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetMyPlayerModel>d__3>(object&,ET.Client.PlayerCacheHelper.<GetMyPlayerModel>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetOtherPlayerBackPack>d__9>(object&,ET.Client.PlayerCacheHelper.<GetOtherPlayerBackPack>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetOtherPlayerBaseInfo>d__8>(object&,ET.Client.PlayerCacheHelper.<GetOtherPlayerBaseInfo>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetOtherPlayerBattleCard>d__10>(object&,ET.Client.PlayerCacheHelper.<GetOtherPlayerBattleCard>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.PlayerCacheHelper.<GetPlayerModel>d__1>(object&,ET.Client.PlayerCacheHelper.<GetPlayerModel>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.RankHelper.<GetRankShow>d__1>(object&,ET.Client.RankHelper.<GetRankShow>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadAssetAsync>d__11>(object&,ET.Client.ResComponentSystem.<LoadAssetAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13>(object&,ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14>(object&,ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<CreateRouterSession>d__0>(object&,ET.Client.RouterHelper.<CreateRouterSession>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.SceneFactory.<CreateClientScene>d__0>(object&,ET.Client.SceneFactory.<CreateClientScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17>(object&,ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.UIManagerHelper.<LoadSprite>d__5>(object&,ET.Client.UIManagerHelper.<LoadSprite>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.NavmeshManagerComponentSystem.<CreateCrowd>d__5>(object&,ET.NavmeshManagerComponentSystem.<CreateCrowd>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__3>(object&,ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__5>(object&,ET.SessionSystem.<Call>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<Connect>d__2>(object&,ET.Client.RouterHelper.<Connect>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AFsmNodeHandler.<OnEnter>d__4>(ET.AFsmNodeHandler.<OnEnter>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AIComponentSystem.<FirstCheck>d__3>(ET.AIComponentSystem.<FirstCheck>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AI_Attack.<Execute>d__3>(ET.AI_Attack.<Execute>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AI_KaoJin.<Execute>d__1>(ET.AI_KaoJin.<Execute>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AI_TowerDefense_AttackWhenBlock.<Execute>d__5>(ET.AI_TowerDefense_AttackWhenBlock.<Execute>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AI_TowerDefense_Escape.<Execute>d__3>(ET.AI_TowerDefense_Escape.<Execute>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AI_XunLuo.<Execute>d__1>(ET.AI_XunLuo.<Execute>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AOIEntitySystem.<WaitNextFrame>d__2>(ET.AOIEntitySystem.<WaitNextFrame>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.ActionHandlerComponentSystem.<Run>d__3>(ET.Ability.ActionHandlerComponentSystem.<Run>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_AttackArea.<Run>d__0>(ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffAdd.<Run>d__0>(ET.Ability.Action_BuffAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffRemove.<Run>d__0>(ET.Ability.Action_BuffRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffResetDuration.<Run>d__0>(ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffStackCountChg.<Run>d__0>(ET.Ability.Action_BuffStackCountChg.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_CallActor.<Run>d__0>(ET.Ability.Action_CallActor.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_CallAoe.<Run>d__0>(ET.Ability.Action_CallAoe.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_CoinAdd.<Run>d__0>(ET.Ability.Action_CoinAdd.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_DamageUnit.<Run>d__0>(ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_DeathShow.<Run>d__0>(ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_EffectCreate.<Run>d__0>(ET.Ability.Action_EffectCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_EffectRemove.<Run>d__0>(ET.Ability.Action_EffectRemove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_FaceTo.<Run>d__0>(ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_FireBullet.<Run>d__0>(ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_ForceMove.<Run>d__0>(ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_PlayAnimator.<Run>d__0>(ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_PlayAudio.<Run>d__0>(ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_TimelineJumpTime.<Run>d__0>(ET.Ability.Action_TimelineJumpTime.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_TimelinePlay.<Run>d__0>(ET.Ability.Action_TimelinePlay.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_TimelineReplace.<Run>d__0>(ET.Ability.Action_TimelineReplace.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Client.AudioPlayObjSystem.<PlayAudio>d__5>(ET.Ability.Client.AudioPlayObjSystem.<PlayAudio>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Client.AudioPlayObjSystem.<_Init>d__3>(ET.Ability.Client.AudioPlayObjSystem.<_Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Client.EffectShowObjSystem.<Init>d__2>(ET.Ability.Client.EffectShowObjSystem.<Init>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Aoe.EventHandler_AoeOnEnter.<Run>d__0>(ET.Ability.EventHandler_Aoe.EventHandler_AoeOnEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Aoe.EventHandler_AoeOnExist.<Run>d__0>(ET.Ability.EventHandler_Aoe.EventHandler_AoeOnExist.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Aoe.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandler_Aoe.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Aoe.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandler_Aoe.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_BulletOnHitMesh.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_BulletOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnHit.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnHit.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnKill.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_DamageBeforeOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_SkillOnCast.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_SkillOnCast.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_UnitChgSaveSelectObj.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_UnitChgSaveSelectObj.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_UnitOnHit.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_UnitOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_UnitOnHitMesh.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_UnitOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Buff.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandler_Buff.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHit.<Run>d__0>(ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHitMesh.<Run>d__0>(ET.Ability.EventHandler_Bullet.EventHandler_BulletOnHitMesh.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Bullet.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandler_Bullet.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandler_Bullet.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandler_Bullet.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_Direction>d__9>(ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_Direction>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_TargetPosition>d__10>(ET.Ability.MoveOrIdleComponentSystem.<DoMoveInput_TargetPosition>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_Direction>d__6>(ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_Direction>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_TargetPosition>d__7>(ET.Ability.MoveOrIdleComponentSystem.<_SetMoveInput_TargetPosition>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleHelper.<DoMoveDirection>d__3>(ET.Ability.MoveOrIdleHelper.<DoMoveDirection>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.MoveOrIdleHelper.<DoMoveTargetPosition>d__4>(ET.Ability.MoveOrIdleHelper.<DoMoveTargetPosition>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.NoticeUnitBuffStatusChg_AnimatorPlay.<Run>d__0>(ET.Ability.NoticeUnitBuffStatusChg_AnimatorPlay.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.SkillComponentSystem.<ReplaceSkillTimeline>d__8>(ET.Ability.SkillComponentSystem.<ReplaceSkillTimeline>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.SkillHelper.<ReplaceSkillTimeline>d__3>(ET.Ability.SkillHelper.<ReplaceSkillTimeline>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<HideMenu>d__9>(ET.Client.ARSessionComponentSystem.<HideMenu>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<Init>d__3>(ET.Client.ARSessionComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<InitCallBack>d__7>(ET.Client.ARSessionComponentSystem.<InitCallBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<LoadARSession>d__4>(ET.Client.ARSessionComponentSystem.<LoadARSession>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<ReStart>d__8>(ET.Client.ARSessionComponentSystem.<ReStart>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0>(ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0>(ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0>(ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0>(ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AudioPlay_Event.<Run>d__0>(ET.Client.AudioPlay_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleCfgIdChoose_RefreshARRoomUI.<Run>d__0>(ET.Client.BattleCfgIdChoose_RefreshARRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleCfgIdChoose_RefreshLobbyUI.<Run>d__0>(ET.Client.BattleCfgIdChoose_RefreshLobbyUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleCfgIdChoose_RefreshRoomUI.<Run>d__0>(ET.Client.BattleCfgIdChoose_RefreshRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0>(ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1>(ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4>(ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0>(ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0>(ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<CreateRoom>d__12>(ET.Client.DlgARHallSystem.<CreateRoom>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<EnterARRoomUI>d__14>(ET.Client.DlgARHallSystem.<EnterARRoomUI>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<HideMenu>d__4>(ET.Client.DlgARHallSystem.<HideMenu>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<InitArSession>d__2>(ET.Client.DlgARHallSystem.<InitArSession>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<JoinRoom>d__15>(ET.Client.DlgARHallSystem.<JoinRoom>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<OnClose>d__6>(ET.Client.DlgARHallSystem.<OnClose>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<OnCreateRoomCallBack>d__7>(ET.Client.DlgARHallSystem.<OnCreateRoomCallBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<OnFinishedCallBack>d__11>(ET.Client.DlgARHallSystem.<OnFinishedCallBack>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<OnJoinByQRCodeCallBack>d__10>(ET.Client.DlgARHallSystem.<OnJoinByQRCodeCallBack>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<OnQuitRoomCallBack>d__8>(ET.Client.DlgARHallSystem.<OnQuitRoomCallBack>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<QuitRoom>d__13>(ET.Client.DlgARHallSystem.<QuitRoom>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<ReStart>d__3>(ET.Client.DlgARHallSystem.<ReStart>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<SetARRoomInfoAsync>d__16>(ET.Client.DlgARHallSystem.<SetARRoomInfoAsync>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5>(ET.Client.DlgARHallSystem.<TriggerJoinScene>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<ChgRoomMemberStatus>d__14>(ET.Client.DlgARRoomPVPSystem.<ChgRoomMemberStatus>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<ChgRoomSeat>d__15>(ET.Client.DlgARRoomPVPSystem.<ChgRoomSeat>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<GetRoomInfo>d__6>(ET.Client.DlgARRoomPVPSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<KickOutRoom>d__10>(ET.Client.DlgARRoomPVPSystem.<KickOutRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<OnChgTeam>d__17>(ET.Client.DlgARRoomPVPSystem.<OnChgTeam>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<OnChooseBattleCfg>d__16>(ET.Client.DlgARRoomPVPSystem.<OnChooseBattleCfg>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<QuitRoom>d__11>(ET.Client.DlgARRoomPVPSystem.<QuitRoom>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<RefreshBattleCfgIdChoose>d__4>(ET.Client.DlgARRoomPVPSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<RefreshUI>d__2>(ET.Client.DlgARRoomPVPSystem.<RefreshUI>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<ShowBattleCfgChoose>d__3>(ET.Client.DlgARRoomPVPSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<ShowQrCode>d__13>(ET.Client.DlgARRoomPVPSystem.<ShowQrCode>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomPVPSystem.<_QuitRoom>d__12>(ET.Client.DlgARRoomPVPSystem.<_QuitRoom>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<ChgRoomMemberStatus>d__13>(ET.Client.DlgARRoomSystem.<ChgRoomMemberStatus>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<ChgRoomSeat>d__14>(ET.Client.DlgARRoomSystem.<ChgRoomSeat>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<GetRoomInfo>d__6>(ET.Client.DlgARRoomSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<KickOutRoom>d__9>(ET.Client.DlgARRoomSystem.<KickOutRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<OnChgTeam>d__16>(ET.Client.DlgARRoomSystem.<OnChgTeam>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<OnChooseBattleCfg>d__15>(ET.Client.DlgARRoomSystem.<OnChooseBattleCfg>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<QuitRoom>d__10>(ET.Client.DlgARRoomSystem.<QuitRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<RefreshBattleCfgIdChoose>d__4>(ET.Client.DlgARRoomSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<RefreshUI>d__2>(ET.Client.DlgARRoomSystem.<RefreshUI>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<ShowBattleCfgChoose>d__3>(ET.Client.DlgARRoomSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<ShowQrCode>d__12>(ET.Client.DlgARRoomSystem.<ShowQrCode>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgARRoomSystem.<_QuitRoom>d__11>(ET.Client.DlgARRoomSystem.<_QuitRoom>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleCfgChooseSystem.<OnBack>d__5>(ET.Client.DlgBattleCfgChooseSystem.<OnBack>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleCfgChooseSystem.<OnChoose>d__4>(ET.Client.DlgBattleCfgChooseSystem.<OnChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleCfgChooseSystem.<OnSure>d__6>(ET.Client.DlgBattleCfgChooseSystem.<OnSure>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleCfgChooseSystem.<RefreshUI>d__9>(ET.Client.DlgBattleCfgChooseSystem.<RefreshUI>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleDragItemSystem.<DrawMonsterCall2HeadQuarter>d__25>(ET.Client.DlgBattleDragItemSystem.<DrawMonsterCall2HeadQuarter>d__25&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleDragItemSystem.<_HideMonsterCall2HeadQuarter>d__27>(ET.Client.DlgBattleDragItemSystem.<_HideMonsterCall2HeadQuarter>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<QuitBattle>d__6>(ET.Client.DlgBattleSystem.<QuitBattle>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<RegisterClear>d__1>(ET.Client.DlgBattleSystem.<RegisterClear>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<RegisterSkill>d__2>(ET.Client.DlgBattleSystem.<RegisterSkill>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<ShowMesh>d__15>(ET.Client.DlgBattleSystem.<ShowMesh>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<_QuitBattle>d__7>(ET.Client.DlgBattleSystem.<_QuitBattle>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<BuyTower>d__27>(ET.Client.DlgBattleTowerARSystem.<BuyTower>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<CloseTowerBuyShow>d__20>(ET.Client.DlgBattleTowerARSystem.<CloseTowerBuyShow>d__20&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<NotTowerBuyShowWhenBattle>d__21>(ET.Client.DlgBattleTowerARSystem.<NotTowerBuyShowWhenBattle>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<QuitBattle>d__13>(ET.Client.DlgBattleTowerARSystem.<QuitBattle>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<ReadyWhenRestTime>d__29>(ET.Client.DlgBattleTowerARSystem.<ReadyWhenRestTime>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<RefreshBuyTower>d__28>(ET.Client.DlgBattleTowerARSystem.<RefreshBuyTower>d__28&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<RefreshUI>d__12>(ET.Client.DlgBattleTowerARSystem.<RefreshUI>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<ShowAvatar>d__38>(ET.Client.DlgBattleTowerARSystem.<ShowAvatar>d__38&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<ShowMesh>d__36>(ET.Client.DlgBattleTowerARSystem.<ShowMesh>d__36&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<TowerBuyShow>d__19>(ET.Client.DlgBattleTowerARSystem.<TowerBuyShow>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerARSystem.<_QuitBattle>d__14>(ET.Client.DlgBattleTowerARSystem.<_QuitBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__8>(ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<Show>d__2>(ET.Client.DlgBattleTowerEndSystem.<Show>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<ShowEffect>d__3>(ET.Client.DlgBattleTowerEndSystem.<ShowEffect>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<ShowEffectEndlessChallenge>d__7>(ET.Client.DlgBattleTowerEndSystem.<ShowEffectEndlessChallenge>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<ShowEffectNormal>d__4>(ET.Client.DlgBattleTowerEndSystem.<ShowEffectNormal>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVE>d__6>(ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVE>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVP>d__5>(ET.Client.DlgBattleTowerEndSystem.<ShowEffectPVP>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<BuyTower>d__27>(ET.Client.DlgBattleTowerSystem.<BuyTower>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<CloseTowerBuyShow>d__20>(ET.Client.DlgBattleTowerSystem.<CloseTowerBuyShow>d__20&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<NotTowerBuyShowWhenBattle>d__21>(ET.Client.DlgBattleTowerSystem.<NotTowerBuyShowWhenBattle>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<QuitBattle>d__13>(ET.Client.DlgBattleTowerSystem.<QuitBattle>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<ReadyWhenRestTime>d__29>(ET.Client.DlgBattleTowerSystem.<ReadyWhenRestTime>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__28>(ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__28&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<RefreshUI>d__12>(ET.Client.DlgBattleTowerSystem.<RefreshUI>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<ShowAvatar>d__38>(ET.Client.DlgBattleTowerSystem.<ShowAvatar>d__38&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<ShowMesh>d__36>(ET.Client.DlgBattleTowerSystem.<ShowMesh>d__36&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<TowerBuyShow>d__19>(ET.Client.DlgBattleTowerSystem.<TowerBuyShow>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<_QuitBattle>d__14>(ET.Client.DlgBattleTowerSystem.<_QuitBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBeginnersGuideStorySystem.<DoNext>d__2>(ET.Client.DlgBeginnersGuideStorySystem.<DoNext>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4>(ET.Client.DlgBeginnersGuideStorySystem.<DoShowStory>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBeginnersGuideStorySystem.<ShowStory>d__3>(ET.Client.DlgBeginnersGuideStorySystem.<ShowStory>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgCommonTipSystem.<_DoShowTip>d__3>(ET.Client.DlgCommonTipSystem.<_DoShowTip>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgCommonTipSystem.<_TipMove>d__4>(ET.Client.DlgCommonTipSystem.<_TipMove>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<ClickAvatar>d__9>(ET.Client.DlgGameModeARSystem.<ClickAvatar>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<ClickRank>d__12>(ET.Client.DlgGameModeARSystem.<ClickRank>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<ClickTutorial>d__10>(ET.Client.DlgGameModeARSystem.<ClickTutorial>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<EnterAREndlessChallenge>d__4>(ET.Client.DlgGameModeARSystem.<EnterAREndlessChallenge>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<EnterARPVE>d__5>(ET.Client.DlgGameModeARSystem.<EnterARPVE>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<EnterARPVP>d__6>(ET.Client.DlgGameModeARSystem.<EnterARPVP>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<EnterScanCode>d__7>(ET.Client.DlgGameModeARSystem.<EnterScanCode>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<EnterTutorialFirst>d__8>(ET.Client.DlgGameModeARSystem.<EnterTutorialFirst>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<ReturnLogin>d__11>(ET.Client.DlgGameModeARSystem.<ReturnLogin>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<ShowBg>d__2>(ET.Client.DlgGameModeARSystem.<ShowBg>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeARSystem.<_ShowWindow>d__3>(ET.Client.DlgGameModeARSystem.<_ShowWindow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterARRoomCreateMode>d__4>(ET.Client.DlgGameModeSystem.<EnterARRoomCreateMode>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterARRoomJoinMode>d__5>(ET.Client.DlgGameModeSystem.<EnterARRoomJoinMode>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3>(ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2>(ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<ReturnLogin>d__6>(ET.Client.DlgGameModeSystem.<ReturnLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<CreateRoom>d__5>(ET.Client.DlgHallSystem.<CreateRoom>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<GetRoomList>d__3>(ET.Client.DlgHallSystem.<GetRoomList>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<JoinRoom>d__8>(ET.Client.DlgHallSystem.<JoinRoom>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<RefreshRoomList>d__6>(ET.Client.DlgHallSystem.<RefreshRoomList>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<ReturnBack>d__7>(ET.Client.DlgHallSystem.<ReturnBack>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<EnterMap>d__4>(ET.Client.DlgLobbySystem.<EnterMap>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<OnChooseBattleCfg>d__6>(ET.Client.DlgLobbySystem.<OnChooseBattleCfg>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<RefreshBattleCfgIdChoose>d__3>(ET.Client.DlgLobbySystem.<RefreshBattleCfgIdChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<ReturnBack>d__5>(ET.Client.DlgLobbySystem.<ReturnBack>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<ChkIsShowDebugUI>d__7>(ET.Client.DlgLoginSystem.<ChkIsShowDebugUI>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<InitAccount>d__8>(ET.Client.DlgLoginSystem.<InitAccount>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<InitDebugMode>d__10>(ET.Client.DlgLoginSystem.<InitDebugMode>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<LoginWhenEditor>d__12>(ET.Client.DlgLoginSystem.<LoginWhenEditor>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<LoginWhenGuest>d__13>(ET.Client.DlgLoginSystem.<LoginWhenGuest>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<LoginWhenSDK>d__14>(ET.Client.DlgLoginSystem.<LoginWhenSDK>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<LoginWhenSDK_LoginDone>d__15>(ET.Client.DlgLoginSystem.<LoginWhenSDK_LoginDone>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<OnSwitchPlayerClickHandler>d__11>(ET.Client.DlgLoginSystem.<OnSwitchPlayerClickHandler>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLoginSystem.<_ShowWindow>d__2>(ET.Client.DlgLoginSystem.<_ShowWindow>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<AddAvatarItemRefreshListener>d__13>(ET.Client.DlgPersonalInformationSystem.<AddAvatarItemRefreshListener>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<CreateAvatarScrollItem>d__12>(ET.Client.DlgPersonalInformationSystem.<CreateAvatarScrollItem>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<HidePersonalInfo>d__8>(ET.Client.DlgPersonalInformationSystem.<HidePersonalInfo>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<IconSelected>d__14>(ET.Client.DlgPersonalInformationSystem.<IconSelected>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<Logout>d__5>(ET.Client.DlgPersonalInformationSystem.<Logout>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<OnBGClick>d__7>(ET.Client.DlgPersonalInformationSystem.<OnBGClick>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<OnSave>d__6>(ET.Client.DlgPersonalInformationSystem.<OnSave>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<ShowBg>d__2>(ET.Client.DlgPersonalInformationSystem.<ShowBg>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgPersonalInformationSystem.<_ShowWindow>d__3>(ET.Client.DlgPersonalInformationSystem.<_ShowWindow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRankEndlessChallengeSystem.<AddRankItemRefreshListener>d__6>(ET.Client.DlgRankEndlessChallengeSystem.<AddRankItemRefreshListener>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRankEndlessChallengeSystem.<ShowBg>d__3>(ET.Client.DlgRankEndlessChallengeSystem.<ShowBg>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRankEndlessChallengeSystem.<ShowPersonalInfo>d__7>(ET.Client.DlgRankEndlessChallengeSystem.<ShowPersonalInfo>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRankEndlessChallengeSystem.<ShowRankScrollItem>d__5>(ET.Client.DlgRankEndlessChallengeSystem.<ShowRankScrollItem>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRankEndlessChallengeSystem.<_ShowWindow>d__2>(ET.Client.DlgRankEndlessChallengeSystem.<_ShowWindow>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__13>(ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ChgRoomSeat>d__14>(ET.Client.DlgRoomSystem.<ChgRoomSeat>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<GetRoomInfo>d__6>(ET.Client.DlgRoomSystem.<GetRoomInfo>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<KickOutRoom>d__9>(ET.Client.DlgRoomSystem.<KickOutRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<OnChgTeam>d__16>(ET.Client.DlgRoomSystem.<OnChgTeam>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<OnChooseBattleCfg>d__15>(ET.Client.DlgRoomSystem.<OnChooseBattleCfg>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<QuitRoom>d__10>(ET.Client.DlgRoomSystem.<QuitRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<RefreshBattleCfgIdChoose>d__4>(ET.Client.DlgRoomSystem.<RefreshBattleCfgIdChoose>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<RefreshUI>d__2>(ET.Client.DlgRoomSystem.<RefreshUI>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ShowBattleCfgChoose>d__3>(ET.Client.DlgRoomSystem.<ShowBattleCfgChoose>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ShowQrCode>d__12>(ET.Client.DlgRoomSystem.<ShowQrCode>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<_QuitRoom>d__11>(ET.Client.DlgRoomSystem.<_QuitRoom>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d>(ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d>(ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EnterMapFinish_UI.<Run>d__0>(ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EnterMapHelper.<EnterMapAsync>d__0>(ET.Client.EnterMapHelper.<EnterMapAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<ChkHotUpdateAsync>d__1>(ET.Client.EntryEvent3_InitClient.<ChkHotUpdateAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<EnterLogin>d__6>(ET.Client.EntryEvent3_InitClient.<EnterLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<ReloadAll>d__5>(ET.Client.EntryEvent3_InitClient.<ReloadAll>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<Run>d__0>(ET.Client.EntryEvent3_InitClient.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EventLoggingSDKComponentSystem.<Awake>d__2>(ET.Client.EventLoggingSDKComponentSystem.<Awake>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EventLoggingSDKComponentSystem.<Destroy>d__3>(ET.Client.EventLoggingSDKComponentSystem.<Destroy>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EventLoggingSDKComponentSystem.<SDKLoginIn>d__4>(ET.Client.EventLoggingSDKComponentSystem.<SDKLoginIn>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EventLoggingSDKComponentSystem.<SDKLoginOut>d__5>(ET.Client.EventLoggingSDKComponentSystem.<SDKLoginOut>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0>(ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0>(ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayChg_ChkAllMyTowerUpgrade.<Run>d__0>(ET.Client.GamePlayChg_ChkAllMyTowerUpgrade.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayChg_RefreshUI.<Run>d__0>(ET.Client.GamePlayChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayCoinChg_RefreshUI.<Run>d__0>(ET.Client.GamePlayCoinChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendARCameraPos>d__3>(ET.Client.GamePlayHelper.<SendARCameraPos>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayPKComponentSystem.<OnChkRay>d__11>(ET.Client.GamePlayPKComponentSystem.<OnChkRay>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseComponentSystem.<DoHideMyMonsterCall2HeadQuarter>d__14>(ET.Client.GamePlayTowerDefenseComponentSystem.<DoHideMyMonsterCall2HeadQuarter>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseComponentSystem.<OnChkRay>d__18>(ET.Client.GamePlayTowerDefenseComponentSystem.<OnChkRay>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendBuyPlayerTower>d__3>(ET.Client.GamePlayTowerDefenseHelper.<SendBuyPlayerTower>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendCallMonster>d__12>(ET.Client.GamePlayTowerDefenseHelper.<SendCallMonster>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendCallOwnTower>d__5>(ET.Client.GamePlayTowerDefenseHelper.<SendCallOwnTower>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendCallTower>d__11>(ET.Client.GamePlayTowerDefenseHelper.<SendCallTower>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendClearAllMonster>d__14>(ET.Client.GamePlayTowerDefenseHelper.<SendClearAllMonster>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendClearMyTower>d__13>(ET.Client.GamePlayTowerDefenseHelper.<SendClearMyTower>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendMovePlayerTower>d__9>(ET.Client.GamePlayTowerDefenseHelper.<SendMovePlayerTower>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendPutHome>d__0>(ET.Client.GamePlayTowerDefenseHelper.<SendPutHome>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendReadyWhenRestTime>d__10>(ET.Client.GamePlayTowerDefenseHelper.<SendReadyWhenRestTime>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendReclaimPlayerTower>d__8>(ET.Client.GamePlayTowerDefenseHelper.<SendReclaimPlayerTower>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendRefreshBuyPlayerTower>d__4>(ET.Client.GamePlayTowerDefenseHelper.<SendRefreshBuyPlayerTower>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendScalePlayerTower>d__7>(ET.Client.GamePlayTowerDefenseHelper.<SendScalePlayerTower>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayTowerDefenseHelper.<SendUpgradePlayerTower>d__6>(ET.Client.GamePlayTowerDefenseHelper.<SendUpgradePlayerTower>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GlobalComponentSystem.<AddComponents>d__5>(ET.Client.GlobalComponentSystem.<AddComponents>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GlobalComponentSystem.<Awake>d__3>(ET.Client.GlobalComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GlobalComponentSystem.<CreateGlobalRoot>d__4>(ET.Client.GlobalComponentSystem.<CreateGlobalRoot>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.HallSceneEnterStart_UI.<DealWhenIsDebugMode>d__1>(ET.Client.HallSceneEnterStart_UI.<DealWhenIsDebugMode>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.HallSceneEnterStart_UI.<DealWhenNotDebugMode>d__2>(ET.Client.HallSceneEnterStart_UI.<DealWhenNotDebugMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.HallSceneEnterStart_UI.<Run>d__0>(ET.Client.HallSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginFinish_UI.<FinishedCallBack>d__1>(ET.Client.LoginFinish_UI.<FinishedCallBack>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginFinish_UI.<Run>d__0>(ET.Client.LoginFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginHelper.<LoginOut>d__1>(ET.Client.LoginHelper.<LoginOut>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginHelper.<ReLogin>d__2>(ET.Client.LoginHelper.<ReLogin>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginOutFinish_UI.<Run>d__0>(ET.Client.LoginOutFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSDKComponentSystem.<Awake>d__2>(ET.Client.LoginSDKComponentSystem.<Awake>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSDKComponentSystem.<Destroy>d__14>(ET.Client.LoginSDKComponentSystem.<Destroy>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSDKComponentSystem.<SDKLoginIn>d__16>(ET.Client.LoginSDKComponentSystem.<SDKLoginIn>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSDKComponentSystem.<SDKLoginOut>d__17>(ET.Client.LoginSDKComponentSystem.<SDKLoginOut>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6>(ET.Client.LoginSDKComponentSystem.<UpdateAuthentication>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginSceneEnterStart_UI.<Run>d__0>(ET.Client.LoginSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_CreateMyUnitHandler.<Run>d__0>(ET.Client.M2C_CreateMyUnitHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_CreateUnitsHandler.<Run>d__0>(ET.Client.M2C_CreateUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0>(ET.Client.M2C_GamePlayCoinChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0>(ET.Client.M2C_GamePlayModeChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_PathfindingResultHandler.<Run>d__0>(ET.Client.M2C_PathfindingResultHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_RemoveUnitsHandler.<Run>d__0>(ET.Client.M2C_RemoveUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_StartSceneChangeHandler.<Run>d__0>(ET.Client.M2C_StartSceneChangeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_StopHandler.<Run>d__0>(ET.Client.M2C_StopHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncDataListHandler.<Run>d__0>(ET.Client.M2C_SyncDataListHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0>(ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncPlayAnimatorHandler.<Run>d__0>(ET.Client.M2C_SyncPlayAnimatorHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncPlayAudioHandler.<Run>d__0>(ET.Client.M2C_SyncPlayAudioHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0>(ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0>(ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.MainQualitySettingComponentSystem.<Awake>d__3>(ET.Client.MainQualitySettingComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ModelClickManagerComponentSystem.<ChkPointDownNextFrame>d__6>(ET.Client.ModelClickManagerComponentSystem.<ChkPointDownNextFrame>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.MoveHelper.<MoveToAsync>d__1>(ET.Client.MoveHelper.<MoveToAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.NetClientComponentOnReadEvent.<Run>d__0>(ET.Client.NetClientComponentOnReadEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.NoticeEventLogging_Event.<Run>d__0>(ET.Client.NoticeEventLogging_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.NoticeUIReconnect_Event.<Run>d__0>(ET.Client.NoticeUIReconnect_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.NoticeUITip_Event.<Run>d__0>(ET.Client.NoticeUITip_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.OnPatchDownloadProgressEvent.<Run>d__0>(ET.Client.OnPatchDownloadProgressEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.OnPatchDownlodFailedEvent.<Run>d__0>(ET.Client.OnPatchDownlodFailedEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PathLineRendererComponentSystem.<ShowPath>d__4>(ET.Client.PathLineRendererComponentSystem.<ShowPath>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PingComponentAwakeSystem.<PingAsync>d__1>(ET.Client.PingComponentAwakeSystem.<PingAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PlayerCacheHelper.<GetMyPlayerModelAll>d__2>(ET.Client.PlayerCacheHelper.<GetMyPlayerModelAll>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7>(ET.Client.PlayerCacheHelper.<SaveMyPlayerModel>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomBattleLevelCfgAsync>d__10>(ET.Client.RoomHelper.<ChgRoomBattleLevelCfgAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomMemberSeatAsync>d__7>(ET.Client.RoomHelper.<ChgRoomMemberSeatAsync>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__6>(ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomMemberTeamAsync>d__8>(ET.Client.RoomHelper.<ChgRoomMemberTeamAsync>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<GetRoomListAsync>d__1>(ET.Client.RoomHelper.<GetRoomListAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<KickMemberOutRoomAsync>d__12>(ET.Client.RoomHelper.<KickMemberOutRoomAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<MemberQuitBattleAsync>d__11>(ET.Client.RoomHelper.<MemberQuitBattleAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__13>(ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<QuitRoomAsync>d__5>(ET.Client.RoomHelper.<QuitRoomAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ReturnBackBattle>d__14>(ET.Client.RoomHelper.<ReturnBackBattle>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomInfoChg_RefreshARRoomPVPUI.<Run>d__0>(ET.Client.RoomInfoChg_RefreshARRoomPVPUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomInfoChg_RefreshARRoomUI.<Run>d__0>(ET.Client.RoomInfoChg_RefreshARRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomInfoChg_RefreshRoomUI.<Run>d__0>(ET.Client.RoomInfoChg_RefreshRoomUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2>(ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<Init>d__1>(ET.Client.RouterAddressComponentSystem.<Init>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3>(ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneChangeHelper.<SceneChangeTo>d__0>(ET.Client.SceneChangeHelper.<SceneChangeTo>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneHelper.<EnterBattle>d__2>(ET.Client.SceneHelper.<EnterBattle>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneHelper.<EnterHall>d__1>(ET.Client.SceneHelper.<EnterHall>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneHelper.<EnterLogin>d__0>(ET.Client.SceneHelper.<EnterLogin>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ShootTextComponentSystem.<Init>d__2>(ET.Client.ShootTextComponentSystem.<Init>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SkillHelper.<CastSkill>d__1>(ET.Client.SkillHelper.<CastSkill>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SkillHelper.<LearnSkill>d__0>(ET.Client.SkillHelper.<LearnSkill>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SwitchLanguageEvent.<Run>d__0>(ET.Client.SwitchLanguageEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIAudioManagerComponentSystem.<PlayUIAudio>d__4>(ET.Client.UIAudioManagerComponentSystem.<PlayUIAudio>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIAudioManagerComponentSystem.<PlayUIGuideAudio>d__5>(ET.Client.UIAudioManagerComponentSystem.<PlayUIGuideAudio>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIAudioManagerComponentSystem.<_PlayMusicOne>d__8>(ET.Client.UIAudioManagerComponentSystem.<_PlayMusicOne>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIAudioManagerComponentSystem.<_PlayNextMusicOne>d__7>(ET.Client.UIAudioManagerComponentSystem.<_PlayNextMusicOne>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__27>(ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__27&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<ShowWindowAsync>d__10>(ET.Client.UIComponentSystem.<ShowWindowAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<!!0>>(ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<!!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<object>>(ET.Client.UIComponentSystem.<ShowWindowAsync>d__11<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<Awake>d__3>(ET.Client.UIGuideComponentSystem.<Awake>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<CreateUIGuidePrefab>d__4>(ET.Client.UIGuideComponentSystem.<CreateUIGuidePrefab>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<DoGuideStep>d__8>(ET.Client.UIGuideComponentSystem.<DoGuideStep>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6>(ET.Client.UIGuideComponentSystem.<DoUIGuide>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5>(ET.Client.UIGuideComponentSystem.<DoUIGuideByName>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideComponentSystem.<StopUIGuide>d__7>(ET.Client.UIGuideComponentSystem.<StopUIGuide>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper.<DoStaticMethodExecute>d__5>(ET.Client.UIGuideHelper.<DoStaticMethodExecute>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper.<DoUIGuide>d__0>(ET.Client.UIGuideHelper.<DoUIGuide>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper.<DoUIGuide>d__1>(ET.Client.UIGuideHelper.<DoUIGuide>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper.<StopUIGuide>d__2>(ET.Client.UIGuideHelper.<StopUIGuide>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper_StaticMethod.<EnterGuideBattle>d__6>(ET.Client.UIGuideHelper_StaticMethod.<EnterGuideBattle>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8>(ET.Client.UIGuideHelper_StaticMethod.<HidePointTower>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper_StaticMethod.<HideTowerInfo>d__9>(ET.Client.UIGuideHelper_StaticMethod.<HideTowerInfo>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7>(ET.Client.UIGuideHelper_StaticMethod.<ShowPointTower>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideHelper_StaticMethod.<ShowStory>d__5>(ET.Client.UIGuideHelper_StaticMethod.<ShowStory>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<ChkNodeStatus>d__4>(ET.Client.UIGuideStepComponentSystem.<ChkNodeStatus>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<ChkPosChg>d__5>(ET.Client.UIGuideStepComponentSystem.<ChkPosChg>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<ChkSizeChg>d__6>(ET.Client.UIGuideStepComponentSystem.<ChkSizeChg>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<DoGuideStepExecute>d__9>(ET.Client.UIGuideStepComponentSystem.<DoGuideStepExecute>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<DoGuideStepOne>d__7>(ET.Client.UIGuideStepComponentSystem.<DoGuideStepOne>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<FinishClick>d__29>(ET.Client.UIGuideStepComponentSystem.<FinishClick>d__29&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetAudio>d__15>(ET.Client.UIGuideStepComponentSystem.<SetAudio>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23>(ET.Client.UIGuideStepComponentSystem.<SetHighlightShow>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetImageShow>d__21>(ET.Client.UIGuideStepComponentSystem.<SetImageShow>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetParentUIGuide>d__24>(ET.Client.UIGuideStepComponentSystem.<SetParentUIGuide>d__24&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22>(ET.Client.UIGuideStepComponentSystem.<SetSpecImageShow>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17>(ET.Client.UIGuideStepComponentSystem.<SetTextShow>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19>(ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenImage>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenText>d__18>(ET.Client.UIGuideStepComponentSystem.<SetTextShowWhenText>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<ShowUIMaskDefault>d__13>(ET.Client.UIGuideStepComponentSystem.<ShowUIMaskDefault>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<ShowUIMaskWhenNoPoint>d__12>(ET.Client.UIGuideStepComponentSystem.<ShowUIMaskWhenNoPoint>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIGuideStepComponentSystem.<_DoGuideStep>d__10>(ET.Client.UIGuideStepComponentSystem.<_DoGuideStep>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIManagerComponentSystem.<Init>d__3>(ET.Client.UIManagerComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIManagerHelper.<EnterRoom>d__9>(ET.Client.UIManagerHelper.<EnterRoom>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIManagerHelper.<ExitRoom>d__10>(ET.Client.UIManagerHelper.<ExitRoom>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIManagerHelper.<SetImageByPath>d__7>(ET.Client.UIManagerHelper.<SetImageByPath>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIManagerHelper.<SetMyIcon>d__6>(ET.Client.UIManagerHelper.<SetMyIcon>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ConsoleComponentSystem.<Start>d__3>(ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Entry.<StartAsync>d__2>(ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EntryEvent1_InitShare.<Run>d__0>(ET.EntryEvent1_InitShare.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventHandler_UnitBeKill.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.EventHandler_UnitBeKill.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.AfterCreateClientScene>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.AfterCreateClientScene>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.BattleSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.BattleSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent1>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent1>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent2>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent2>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent3>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent3>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.HallSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.HallSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginFinish>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginOutFinish>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginOutFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayComponentSystem.<DownloadMapRecast>d__5>(ET.GamePlayComponentSystem.<DownloadMapRecast>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayComponentSystem.<InitWhenGlobal>d__3>(ET.GamePlayComponentSystem.<InitWhenGlobal>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayComponentSystem.<InitWhenRoom>d__2>(ET.GamePlayComponentSystem.<InitWhenRoom>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7>(ET.GamePlayTowerDefenseComponentSystem.<DoReadyForBattle>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToBattle>d__18>(ET.GamePlayTowerDefenseComponentSystem.<TransToBattle>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToGameEnd>d__23>(ET.GamePlayTowerDefenseComponentSystem.<TransToGameEnd>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToPutHome>d__16>(ET.GamePlayTowerDefenseComponentSystem.<TransToPutHome>d__16&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToPutMonsterPoint>d__17>(ET.GamePlayTowerDefenseComponentSystem.<TransToPutMonsterPoint>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToRestTime>d__19>(ET.GamePlayTowerDefenseComponentSystem.<TransToRestTime>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayTowerDefenseComponentSystem.<TransToShowStartEffect>d__15>(ET.GamePlayTowerDefenseComponentSystem.<TransToShowStartEffect>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6>(ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MoveHelper.<FindPathMoveToAsync>d__0>(ET.MoveHelper.<FindPathMoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.NavmeshComponentSystem.<CreateCrowd>d__3>(ET.NavmeshComponentSystem.<CreateCrowd>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<!0>>(ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.PathfindingComponentSystem.<Init>d__3>(ET.PathfindingComponentSystem.<Init>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.PutHomeComponentSystem.<ChkNextStep>d__9>(ET.PutHomeComponentSystem.<ChkNextStep>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!!0>.Start<ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<!!0>>(ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<!!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!!0>.Start<ET.ObjectWaitSystem.<Wait>d__4<!!0>>(ET.ObjectWaitSystem.<Wait>d__4<!!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!!0>.Start<ET.ObjectWaitSystem.<Wait>d__5<!!0>>(ET.ObjectWaitSystem.<Wait>d__5<!!0>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_CreateMyUnit>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_CreateMyUnit>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_SceneChangeFinish>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_SceneChangeFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_UnitStop>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_UnitStop>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<Unity.Mathematics.float3,object>>.Start<ET.Client.GamePlayTowerDefenseHelper.<SendGetMonsterCall2HeadQuarterPath>d__1>(ET.Client.GamePlayTowerDefenseHelper.<SendGetMonsterCall2HeadQuarterPath>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Ability.SkillComponentSystem.<CastSkill>d__7>(ET.Ability.SkillComponentSystem.<CastSkill>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Ability.SkillHelper.<CastSkill>d__2>(ET.Ability.SkillHelper.<CastSkill>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Client.GamePlayTowerDefenseHelper.<SendPutMonsterCall>d__2>(ET.Client.GamePlayTowerDefenseHelper.<SendPutMonsterCall>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Client.LoginHelper.<Login>d__0>(ET.Client.LoginHelper.<Login>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Client.PlayerCacheHelper.<SendGetPlayerModelAsync>d__11>(ET.Client.PlayerCacheHelper.<SendGetPlayerModelAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<ET.Client.RankHelper.<SendGetRankShowAsync>d__2>(ET.Client.RankHelper.<SendGetRankShowAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.Start<ET.Client.RouterHelper.<GetRouterAddress>d__1>(ET.Client.RouterHelper.<GetRouterAddress>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.Start<ET.Client.ResComponentSystem.<LoadSceneAsync>d__12>(ET.Client.ResComponentSystem.<LoadSceneAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Ability.MoveOrIdleComponentSystem.<_CreateIdleTimeLine>d__11>(ET.Ability.MoveOrIdleComponentSystem.<_CreateIdleTimeLine>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Ability.MoveOrIdleComponentSystem.<_CreateMoveTimeLine>d__12>(ET.Ability.MoveOrIdleComponentSystem.<_CreateMoveTimeLine>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutHome>d__17>(ET.Client.DlgBattleDragItemSystem.<DoPutHome>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutMonsterCall>d__18>(ET.Client.DlgBattleDragItemSystem.<DoPutMonsterCall>d__18&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutMoveTower>d__22>(ET.Client.DlgBattleDragItemSystem.<DoPutMoveTower>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutOwnTower>d__21>(ET.Client.DlgBattleDragItemSystem.<DoPutOwnTower>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutPKMonster>d__20>(ET.Client.DlgBattleDragItemSystem.<DoPutPKMonster>d__20&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<DoPutPKTower>d__19>(ET.Client.DlgBattleDragItemSystem.<DoPutPKTower>d__19&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.DlgBattleDragItemSystem.<_DrawMonsterCall2HeadQuarter>d__26>(ET.Client.DlgBattleDragItemSystem.<_DrawMonsterCall2HeadQuarter>d__26&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__4>(ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__3>(ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMonsterCall2HeadQuarterByPos>d__15>(ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMonsterCall2HeadQuarterByPos>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMyMonsterCall2HeadQuarter>d__13>(ET.Client.GamePlayTowerDefenseComponentSystem.<DoDrawMyMonsterCall2HeadQuarter>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.LoginSDKComponentSystem.<ChkSDKLoginDone>d__15>(ET.Client.LoginSDKComponentSystem.<ChkSDKLoginDone>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.PlayerCacheHelper.<SendSavePlayerModelAsync>d__12>(ET.Client.PlayerCacheHelper.<SendSavePlayerModelAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.RoomHelper.<CreateRoomAsync>d__3>(ET.Client.RoomHelper.<CreateRoomAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.RoomHelper.<GetRoomInfoAsync>d__2>(ET.Client.RoomHelper.<GetRoomInfoAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.RoomHelper.<JoinRoomAsync>d__4>(ET.Client.RoomHelper.<JoinRoomAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.RoomHelper.<SetARRoomInfoAsync>d__9>(ET.Client.RoomHelper.<SetARRoomInfoAsync>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideHelper.<DoStaticMethodChk>d__4>(ET.Client.UIGuideHelper.<DoStaticMethodChk>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideHelper_StaticMethod.<ChkARMeshShow>d__3>(ET.Client.UIGuideHelper_StaticMethod.<ChkARMeshShow>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideHelper_StaticMethod.<ChkIsNotShowStory>d__4>(ET.Client.UIGuideHelper_StaticMethod.<ChkIsNotShowStory>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideHelper_StaticMethod.<ChkTowerPut>d__1>(ET.Client.UIGuideHelper_StaticMethod.<ChkTowerPut>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideHelper_StaticMethod.<ChkWaitTime>d__2>(ET.Client.UIGuideHelper_StaticMethod.<ChkWaitTime>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.Client.UIGuideStepComponentSystem.<ChkGuideStepCondition>d__8>(ET.Client.UIGuideStepComponentSystem.<ChkGuideStepCondition>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.MoveByPathComponentSystem.<MoveToAsync>d__5>(ET.MoveByPathComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.MoveHelper.<MoveToAsync>d__0>(ET.Client.MoveHelper.<MoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6>(ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4>(ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3>(ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.BuffComponentSystem.<AddBuff>d__3>(ET.Ability.BuffComponentSystem.<AddBuff>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.SkillObjSystem.<CastSkill>d__6>(ET.Ability.SkillObjSystem.<CastSkill>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineComponentSystem.<CreateTimeline>d__4>(ET.Ability.TimelineComponentSystem.<CreateTimeline>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineComponentSystem.<PlayTimeline>d__6>(ET.Ability.TimelineComponentSystem.<PlayTimeline>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineComponentSystem.<ReplaceTimeline>d__5>(ET.Ability.TimelineComponentSystem.<ReplaceTimeline>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineHelper.<CreateTimeline>d__1>(ET.Ability.TimelineHelper.<CreateTimeline>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineHelper.<PlayTimeline>d__3>(ET.Ability.TimelineHelper.<PlayTimeline>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Ability.TimelineHelper.<ReplaceTimeline>d__2>(ET.Ability.TimelineHelper.<ReplaceTimeline>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.HttpClientHelper.<Get>d__0>(ET.Client.HttpClientHelper.<Get>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.IconHelper.<LoadIconSpriteAsync>d__1>(ET.Client.IconHelper.<LoadIconSpriteAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetMyPlayerBackPack>d__5>(ET.Client.PlayerCacheHelper.<GetMyPlayerBackPack>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetMyPlayerBaseInfo>d__4>(ET.Client.PlayerCacheHelper.<GetMyPlayerBaseInfo>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetMyPlayerBattleCard>d__6>(ET.Client.PlayerCacheHelper.<GetMyPlayerBattleCard>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetMyPlayerModel>d__3>(ET.Client.PlayerCacheHelper.<GetMyPlayerModel>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetOtherPlayerBackPack>d__9>(ET.Client.PlayerCacheHelper.<GetOtherPlayerBackPack>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetOtherPlayerBaseInfo>d__8>(ET.Client.PlayerCacheHelper.<GetOtherPlayerBaseInfo>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetOtherPlayerBattleCard>d__10>(ET.Client.PlayerCacheHelper.<GetOtherPlayerBattleCard>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.PlayerCacheHelper.<GetPlayerModel>d__1>(ET.Client.PlayerCacheHelper.<GetPlayerModel>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.RankHelper.<GetRankShow>d__1>(ET.Client.RankHelper.<GetRankShow>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<object>>(ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadAssetAsync>d__11>(ET.Client.ResComponentSystem.<LoadAssetAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13>(ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14>(ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.RouterHelper.<CreateRouterSession>d__0>(ET.Client.RouterHelper.<CreateRouterSession>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.SceneFactory.<CreateClientScene>d__0>(ET.Client.SceneFactory.<CreateClientScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17>(ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.UIManagerHelper.<LoadSprite>d__5>(ET.Client.UIManagerHelper.<LoadSprite>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.NavmeshManagerComponentSystem.<CreateCrowd>d__5>(ET.NavmeshManagerComponentSystem.<CreateCrowd>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__3>(ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__5>(ET.SessionSystem.<Call>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.Start<ET.Client.RouterHelper.<Connect>d__2>(ET.Client.RouterHelper.<Connect>d__2&)
		// !!1 ET.Entity.AddChild<!!1>(bool)
		// object ET.Entity.AddChild<object,object>(object,bool)
		// object ET.Entity.AddChild<object>(bool)
		// object ET.Entity.AddChildWithId<object,int>(long,int,bool)
		// object ET.Entity.AddChildWithId<object,object>(long,object,bool)
		// object ET.Entity.AddChildWithId<object>(long,bool)
		// object ET.Entity.AddComponent<object,System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily,bool)
		// object ET.Entity.AddComponent<object,float>(float,bool)
		// object ET.Entity.AddComponent<object,int,Unity.Mathematics.float3>(int,Unity.Mathematics.float3,bool)
		// object ET.Entity.AddComponent<object,object,int>(object,int,bool)
		// object ET.Entity.AddComponent<object,object>(object,bool)
		// object ET.Entity.AddComponent<object>(bool)
		// object ET.Entity.AddComponentWithId<object,System.Net.Sockets.AddressFamily>(long,System.Net.Sockets.AddressFamily,bool)
		// object ET.Entity.AddComponentWithId<object,float>(long,float,bool)
		// object ET.Entity.AddComponentWithId<object,int,Unity.Mathematics.float3>(long,int,Unity.Mathematics.float3,bool)
		// object ET.Entity.AddComponentWithId<object,object,int>(long,object,int,bool)
		// object ET.Entity.AddComponentWithId<object,object>(long,object,bool)
		// object ET.Entity.AddComponentWithId<object>(long,bool)
		// object ET.Entity.GetChild<object>(long)
		// !!0 ET.Entity.GetComponent<!!0>()
		// object ET.Entity.GetComponent<object>()
		// object ET.Entity.GetParent<object>()
		// System.Void ET.Entity.RemoveComponent<object>()
		// ET.Ability.AbilityBuffMonitorTriggerEvent ET.EnumHelper.FromString<ET.Ability.AbilityBuffMonitorTriggerEvent>(string)
		// ET.Ability.TeamFlagType ET.EnumHelper.FromString<ET.Ability.TeamFlagType>(string)
		// ET.SceneType ET.EnumHelper.FromString<ET.SceneType>(string)
		// System.Void ET.EventSystem.Awake<System.Net.Sockets.AddressFamily>(ET.Entity,System.Net.Sockets.AddressFamily)
		// System.Void ET.EventSystem.Awake<float>(ET.Entity,float)
		// System.Void ET.EventSystem.Awake<int,Unity.Mathematics.float3>(ET.Entity,int,Unity.Mathematics.float3)
		// System.Void ET.EventSystem.Awake<int>(ET.Entity,int)
		// System.Void ET.EventSystem.Awake<object,int>(ET.Entity,object,int)
		// System.Void ET.EventSystem.Awake<object>(ET.Entity,object)
		// System.ValueTuple<object,int> ET.EventSystem.Invoke<ET.ConfigComponent.GetRouterHttpHostAndPort,System.ValueTuple<object,int>>(ET.ConfigComponent.GetRouterHttpHostAndPort)
		// System.ValueTuple<object,int> ET.EventSystem.Invoke<ET.ConfigComponent.GetRouterHttpHostAndPort,System.ValueTuple<object,int>>(int,ET.ConfigComponent.GetRouterHttpHostAndPort)
		// object ET.EventSystem.Invoke<ET.NavmeshManagerComponent.RecastFileLoader,object>(ET.NavmeshManagerComponent.RecastFileLoader)
		// object ET.EventSystem.Invoke<ET.NavmeshManagerComponent.RecastFileLoader,object>(int,ET.NavmeshManagerComponent.RecastFileLoader)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BulletOnHit>(object,ET.Ability.AbilityTriggerEventType.BulletOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BulletOnHitMesh>(object,ET.Ability.AbilityTriggerEventType.BulletOnHitMesh)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>(object,ET.Ability.AbilityTriggerEventType.SkillOnCast)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitChgSaveSelectObj>(object,ET.Ability.AbilityTriggerEventType.UnitChgSaveSelectObj)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>(object,ET.Ability.AbilityTriggerEventType.UnitOnCreate)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>(object,ET.Ability.AbilityTriggerEventType.UnitOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>(object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved)
		// System.Void ET.EventSystem.Publish<object,ET.Client.NetClientComponentOnRead>(object,ET.Client.NetClientComponentOnRead)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterCreateCurrentScene>(object,ET.EventType.AfterCreateCurrentScene)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterUnitCreate>(object,ET.EventType.AfterUnitCreate)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.BattleCfgIdChoose>(object,ET.EventType.BattleCfgIdChoose)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.BattleSceneEnterFinish>(object,ET.EventType.BattleSceneEnterFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangePosition>(object,ET.EventType.ChangePosition)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangeRotation>(object,ET.EventType.ChangeRotation)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.EnterMapFinish>(object,ET.EventType.EnterMapFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.GamePlayChg>(object,ET.EventType.GamePlayChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.GamePlayCoinChg>(object,ET.EventType.GamePlayCoinChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStart>(object,ET.EventType.MoveByPathStart)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStop>(object,ET.EventType.MoveByPathStop)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MovePointList>(object,ET.EventType.MovePointList)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeEventLogging>(object,ET.EventType.NoticeEventLogging)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeGameEnd2Server>(object,ET.EventType.NoticeGameEnd2Server)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeGameEndToRoom>(object,ET.EventType.NoticeGameEndToRoom)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeGamePlayPlayerListToClient>(object,ET.EventType.NoticeGamePlayPlayerListToClient)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeUIReconnect>(object,ET.EventType.NoticeUIReconnect)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeUITip>(object,ET.EventType.NoticeUITip)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeUnitBuffStatusChg>(object,ET.EventType.NoticeUnitBuffStatusChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NumbericChange>(object,ET.EventType.NumbericChange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownloadProgress>(object,ET.EventType.OnPatchDownloadProgress)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownlodFailed>(object,ET.EventType.OnPatchDownlodFailed)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.RoomInfoChg>(object,ET.EventType.RoomInfoChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.StopMove>(object,ET.EventType.StopMove)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SwitchLanguage>(object,ET.EventType.SwitchLanguage)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncNumericUnits>(object,ET.EventType.SyncNumericUnits)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncPlayAnimator>(object,ET.EventType.SyncPlayAnimator)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncPlayAudio>(object,ET.EventType.SyncPlayAudio)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncPosUnits>(object,ET.EventType.SyncPosUnits)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncUnitEffects>(object,ET.EventType.SyncUnitEffects)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitEnterSightRange>(object,ET.EventType.UnitEnterSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitLeaveSightRange>(object,ET.EventType.UnitLeaveSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.WaitNoticeGamePlayModeToClient>(object,ET.EventType.WaitNoticeGamePlayModeToClient)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.WaitNoticeGamePlayPlayerListToClient>(object,ET.EventType.WaitNoticeGamePlayPlayerListToClient)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.WaitNoticeGamePlayToClient>(object,ET.EventType.WaitNoticeGamePlayToClient)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.AfterCreateClientScene>(object,ET.EventType.AfterCreateClientScene)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.BattleSceneEnterStart>(object,ET.EventType.BattleSceneEnterStart)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent1>(object,ET.EventType.EntryEvent1)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent2>(object,ET.EventType.EntryEvent2)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent3>(object,ET.EventType.EntryEvent3)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.HallSceneEnterStart>(object,ET.EventType.HallSceneEnterStart)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginFinish>(object,ET.EventType.LoginFinish)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginOutFinish>(object,ET.EventType.LoginOutFinish)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginSceneEnterStart>(object,ET.EventType.LoginSceneEnterStart)
		// object ET.Game.AddSingleton<object>()
		// bool ET.Game.ChkIsExistSingleton<object>()
		// object ET.Game.GetExistSingleton<object>()
		// object ET.JsonHelper.FromJson<object>(string)
		// object ET.MongoHelper.Deserialize<object>(byte[])
		// object ET.MongoHelper.FromJson<object>(string)
		// System.Void ET.ObjectHelper.Swap<object>(object&,object&)
		// System.Void ET.RandomGenerator.BreakRank<object>(System.Collections.Generic.List<object>)
		// object ET.RandomGenerator.GetRandomIndexLinear<object>(System.Collections.Generic.Dictionary<object,int>)
		// System.Numerics.Vector3 ET.RandomGenerator.RandomArray<System.Numerics.Vector3>(System.Collections.Generic.List<System.Numerics.Vector3>)
		// object ET.RandomGenerator.RandomArray<object>(System.Collections.Generic.List<object>)
		// System.Void IngameDebugConsole.DebugLogConsole.AddCommand<object>(string,string,System.Action<object>)
		// object MongoDB.Bson.Serialization.BsonSerializer.Deserialize<object>(MongoDB.Bson.IO.IBsonReader,System.Action<MongoDB.Bson.Serialization.BsonDeserializationContext.Builder>)
		// object MongoDB.Bson.Serialization.BsonSerializer.Deserialize<object>(string,System.Action<MongoDB.Bson.Serialization.BsonDeserializationContext.Builder>)
		// MongoDB.Bson.Serialization.IBsonSerializer<object> MongoDB.Bson.Serialization.BsonSerializer.LookupSerializer<object>()
		// object MongoDB.Bson.Serialization.IBsonSerializerExtensions.Deserialize<object>(MongoDB.Bson.Serialization.IBsonSerializer<object>,MongoDB.Bson.Serialization.BsonDeserializationContext)
		// !!0 ReferenceCollector.Get<!!0>(string)
		// object ReferenceCollector.Get<object>(string)
		// !0 System.Activator.CreateInstance<!0>()
		// object System.Activator.CreateInstance<object>()
		// bool System.Enum.TryParse<ET.AreaType>(string,ET.AreaType&)
		// bool System.Enum.TryParse<ET.AreaType>(string,bool,ET.AreaType&)
		// bool System.Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(string,ET.Client.GuideConditionStaticMethodType&)
		// bool System.Enum.TryParse<ET.Client.GuideConditionStaticMethodType>(string,bool,ET.Client.GuideConditionStaticMethodType&)
		// bool System.Enum.TryParse<ET.Client.GuideExecuteStaticMethodType>(string,ET.Client.GuideExecuteStaticMethodType&)
		// bool System.Enum.TryParse<ET.Client.GuideExecuteStaticMethodType>(string,bool,ET.Client.GuideExecuteStaticMethodType&)
		// ET.RpcInfo[] System.Linq.Enumerable.ToArray<ET.RpcInfo>(System.Collections.Generic.IEnumerable<ET.RpcInfo>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.List<long> System.Linq.Enumerable.ToList<long>(System.Collections.Generic.IEnumerable<long>)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.NavmeshComponentSystem.<_InitDtCrowd>d__6>(ET.ETTaskCompleted&,ET.NavmeshComponentSystem.<_InitDtCrowd>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.NavmeshComponentSystem.<_InitDtCrowd>d__6>(ET.ETTaskCompleted&,ET.NavmeshComponentSystem.<_InitDtCrowd>d__6&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<ET.NavmeshComponentSystem.<_InitDtCrowd>d__6>(ET.NavmeshComponentSystem.<_InitDtCrowd>d__6&)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.Task.FromResult<object>(object)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.TaskFactory.StartNew<object>(System.Func<object>,System.Threading.CancellationToken)
		// object UnityEngine.Component.GetComponent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>(bool)
		// object UnityEngine.GameObject.AddComponent<object>()
		// !!0 UnityEngine.GameObject.GetComponent<!!0>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// object UnityEngine.Object.Instantiate<object>(object)
		// !0 YooAsset.AssetOperationHandle.GetAssetObject<!0>()
		// YooAsset.AssetOperationHandle YooAsset.ResourcePackage.LoadAssetAsync<!0>(string)
		// YooAsset.AssetOperationHandle YooAsset.ResourcePackage.LoadAssetSync<!!0>(string)
		// YooAsset.AssetOperationHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetAsync<!0>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetSync<!!0>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
		// string string.Join<ET.AbilityConfig.BuffTagGroupType>(string,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string string.Join<ET.AbilityConfig.BuffTagType>(string,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string string.Join<System.Numerics.Vector3>(string,System.Collections.Generic.IEnumerable<System.Numerics.Vector3>)
		// string string.Join<float>(string,System.Collections.Generic.IEnumerable<float>)
		// string string.Join<int>(string,System.Collections.Generic.IEnumerable<int>)
		// string string.Join<object>(string,System.Collections.Generic.IEnumerable<object>)
		// string string.JoinCore<ET.AbilityConfig.BuffTagGroupType>(System.Char*,int,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string string.JoinCore<ET.AbilityConfig.BuffTagType>(System.Char*,int,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string string.JoinCore<System.Numerics.Vector3>(System.Char*,int,System.Collections.Generic.IEnumerable<System.Numerics.Vector3>)
		// string string.JoinCore<float>(System.Char*,int,System.Collections.Generic.IEnumerable<float>)
		// string string.JoinCore<int>(System.Char*,int,System.Collections.Generic.IEnumerable<int>)
		// string string.JoinCore<object>(System.Char*,int,System.Collections.Generic.IEnumerable<object>)
	}
}