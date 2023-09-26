using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"DOTween.dll",
		"MirrorVerse.UI.MirrorSceneDefaultUI.dll",
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
	// ET.MultiDictionary<int,int,object>
	// ET.MultiDictionary<long,object,int>
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
	// MirrorVerse.UI.MirrorSceneDefaultUI.SubMenu<object>
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
	// System.Action<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Action<System.Numerics.Vector3>
	// System.Action<Unity.Mathematics.float3>
	// System.Action<UnityEngine.EventSystems.RaycastResult>
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
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.Comparer<System.Numerics.Vector3>
	// System.Collections.Generic.Comparer<Unity.Mathematics.float3>
	// System.Collections.Generic.Comparer<UnityEngine.EventSystems.RaycastResult>
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
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ComparisonComparer<System.Numerics.Vector3>
	// System.Collections.Generic.ComparisonComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ComparisonComparer<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.ComparisonComparer<byte>
	// System.Collections.Generic.ComparisonComparer<float>
	// System.Collections.Generic.ComparisonComparer<int>
	// System.Collections.Generic.ComparisonComparer<long>
	// System.Collections.Generic.ComparisonComparer<object>
	// System.Collections.Generic.ComparisonComparer<uint>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.Enumerator<ET.Client.WindowID,object>
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
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ET.Client.WindowID,object>
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
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ET.Client.WindowID,object>
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
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,long>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<ushort,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ET.Client.WindowID,object>
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
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<ushort,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ET.Client.WindowID,object>
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
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,long>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<ushort,object>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.Dictionary<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.Dictionary<ET.Client.WindowID,object>
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
	// System.Collections.Generic.EqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<object,int>>
	// System.Collections.Generic.EqualityComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.EqualityComparer<UnityEngine.AnimatorControllerParameterType>
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
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
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
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
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
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>>
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
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
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
	// System.Collections.Generic.KeyValuePair<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.CoinType,int>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyInt,int>
	// System.Collections.Generic.KeyValuePair<ET.AbilityConfig.RecordKeyString,object>
	// System.Collections.Generic.KeyValuePair<ET.Client.WindowID,object>
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
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectComparer<System.Numerics.Vector3>
	// System.Collections.Generic.ObjectComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ObjectComparer<UnityEngine.EventSystems.RaycastResult>
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
	// System.Collections.Generic.ObjectEqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<object,int>>
	// System.Collections.Generic.ObjectEqualityComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.AnimatorControllerParameterType>
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
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<int,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<object,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<float,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<int,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<object,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<float,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<int,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<object,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<float,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<int,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<long,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<float,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<object,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary<float,object>
	// System.Collections.Generic.SortedDictionary<int,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedDictionary<object,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<float,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<float,object>>
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
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.Ability.SkillSlotType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagGroupType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffTagType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<ET.AbilityConfig.BuffType,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<float,object>>
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
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Comparison<System.Numerics.Vector3>
	// System.Comparison<Unity.Mathematics.float3>
	// System.Comparison<UnityEngine.EventSystems.RaycastResult>
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
	// System.Func<object,object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,object,System.ValueTuple<uint,uint>>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Linq.Buffer<ET.RpcInfo>
	// System.Linq.Buffer<object>
	// System.Nullable<ET.AbilityConfig.BuffTagGroupType>
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
	// System.ValueTuple<ET.AbilityConfig.NumericType,float>
	// System.ValueTuple<Unity.Mathematics.float3,Unity.Mathematics.float3>
	// System.ValueTuple<Unity.Mathematics.float3,object>
	// System.ValueTuple<byte,DotRecast.Core.RcVec3f>
	// System.ValueTuple<byte,ET.AbilityConfig.AnimatorMotionName>
	// System.ValueTuple<byte,Unity.Mathematics.float3>
	// System.ValueTuple<byte,byte,Unity.Mathematics.float3>
	// System.ValueTuple<byte,byte,object>
	// System.ValueTuple<byte,float>
	// System.ValueTuple<byte,object,object>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<float,object>
	// System.ValueTuple<int,object>
	// System.ValueTuple<object,int>
	// System.ValueTuple<object,object,object>
	// System.ValueTuple<uint,object>
	// System.ValueTuple<uint,uint>
	// System.WeakReference<object>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<object>
	// }}

	public void RefMethods()
	{
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagGroupType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.CoinType,int>(System.Collections.Generic.IDictionary<ET.AbilityConfig.CoinType,int>)
		// string Bright.Common.StringUtil.CollectionToString<float>(System.Collections.Generic.IEnumerable<float>)
		// string Bright.Common.StringUtil.CollectionToString<int>(System.Collections.Generic.IEnumerable<int>)
		// string Bright.Common.StringUtil.CollectionToString<object,int>(System.Collections.Generic.IDictionary<object,int>)
		// string Bright.Common.StringUtil.CollectionToString<object>(System.Collections.Generic.IEnumerable<object>)
		// object DG.Tweening.TweenSettingsExtensions.OnComplete<object>(object,DG.Tweening.TweenCallback)
		// object DG.Tweening.TweenSettingsExtensions.SetEase<object>(object,DG.Tweening.Ease)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,object>(ET.ETTaskCompleted&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,object>(System.Runtime.CompilerServices.TaskAwaiter&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>,object>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,object>(System.Runtime.CompilerServices.TaskAwaiter<object>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!0>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<Unity.Mathematics.float3,object>>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,object>(System.Runtime.CompilerServices.TaskAwaiter<object>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.AfterCreateClientScene>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.AfterCreateClientScene>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.BattleSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.BattleSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent1>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent1>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent2>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent2>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent3>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.EntryEvent3>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.HallSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.HallSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginFinish>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginOutFinish>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginOutFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginSceneEnterStart>>(ET.EventSystem.<PublishAsync>d__26<object,ET.EventType.LoginSceneEnterStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<!!0>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<Unity.Mathematics.float3,object>>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<byte,object>>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.Start<object>(object&)
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
		// ET.PlayerGameMode ET.EnumHelper.FromString<ET.PlayerGameMode>(string)
		// ET.PlayerStatus ET.EnumHelper.FromString<ET.PlayerStatus>(string)
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
		// System.Void ET.EventSystem.Publish<object,ET.EventType.BattleSceneEnterFinish>(object,ET.EventType.BattleSceneEnterFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangePosition>(object,ET.EventType.ChangePosition)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangeRotation>(object,ET.EventType.ChangeRotation)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.EnterMapFinish>(object,ET.EventType.EnterMapFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.GamePlayChg>(object,ET.EventType.GamePlayChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.GamePlayCoinChg>(object,ET.EventType.GamePlayCoinChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStart>(object,ET.EventType.MoveByPathStart)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStop>(object,ET.EventType.MoveByPathStop)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MovePointList>(object,ET.EventType.MovePointList)
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
		// string ET.StringHelper.ArrayToString<float>(float[])
		// object MongoDB.Bson.Serialization.BsonSerializer.Deserialize<object>(MongoDB.Bson.IO.IBsonReader,System.Action<MongoDB.Bson.Serialization.BsonDeserializationContext.Builder>)
		// object MongoDB.Bson.Serialization.BsonSerializer.Deserialize<object>(string,System.Action<MongoDB.Bson.Serialization.BsonDeserializationContext.Builder>)
		// MongoDB.Bson.Serialization.IBsonSerializer<object> MongoDB.Bson.Serialization.BsonSerializer.LookupSerializer<object>()
		// object MongoDB.Bson.Serialization.IBsonSerializerExtensions.Deserialize<object>(MongoDB.Bson.Serialization.IBsonSerializer<object>,MongoDB.Bson.Serialization.BsonDeserializationContext)
		// !!0 ReferenceCollector.Get<!!0>(string)
		// object ReferenceCollector.Get<object>(string)
		// !0 System.Activator.CreateInstance<!0>()
		// object System.Activator.CreateInstance<object>()
		// ET.RpcInfo[] System.Linq.Enumerable.ToArray<ET.RpcInfo>(System.Collections.Generic.IEnumerable<ET.RpcInfo>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,object>(ET.ETTaskCompleted&,object&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,object>(ET.ETTaskCompleted&,object&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<object>(object&)
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
		// string string.Join<float>(string,System.Collections.Generic.IEnumerable<float>)
		// string string.Join<int>(string,System.Collections.Generic.IEnumerable<int>)
		// string string.Join<object>(string,System.Collections.Generic.IEnumerable<object>)
		// string string.JoinCore<ET.AbilityConfig.BuffTagGroupType>(System.Char*,int,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string string.JoinCore<ET.AbilityConfig.BuffTagType>(System.Char*,int,System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string string.JoinCore<float>(System.Char*,int,System.Collections.Generic.IEnumerable<float>)
		// string string.JoinCore<int>(System.Char*,int,System.Collections.Generic.IEnumerable<int>)
		// string string.JoinCore<object>(System.Char*,int,System.Collections.Generic.IEnumerable<object>)
	}
}