public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	// System.Core.dll
	// System.dll
	// Unity.Core.dll
	// Unity.Loader.dll
	// Unity.ThirdParty.dll
	// UnityEngine.CoreModule.dll
	// mscorlib.dll
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// ET.AEvent<object,ET.EventType.EntryEvent1>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BulletOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnBulletLeave>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnBulletEnter>
	// ET.AEvent<object,ET.Client.NetClientComponentOnRead>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnCharacterLeave>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnCharacterEnter>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>
	// ET.AEvent<object,ET.EventType.EntryEvent3>
	// ET.AEvent<object,ET.EventType.AfterCreateClientScene>
	// ET.AEvent<object,ET.EventType.AfterCreateCurrentScene>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>
	// ET.AEvent<object,ET.EventType.BattleSceneEnterStart>
	// ET.AEvent<object,ET.EventType.EnterMapFinish>
	// ET.AEvent<object,ET.EventType.GamePlayChg>
	// ET.AEvent<object,ET.EventType.AppStartInitFinish>
	// ET.AEvent<object,ET.EventType.LoginFinish>
	// ET.AEvent<object,ET.EventType.BeKickedRoom>
	// ET.AEvent<object,ET.EventType.RoomInfoChg>
	// ET.AEvent<object,ET.EventType.OnPatchDownloadProgress>
	// ET.AEvent<object,ET.EventType.OnPatchDownlodFailed>
	// ET.AEvent<object,ET.EventType.AfterUnitCreate>
	// ET.AEvent<object,ET.EventType.ChangePosition>
	// ET.AEvent<object,ET.EventType.ChangeRotation>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>
	// ET.AEvent<object,ET.EventType.SwitchLanguage>
	// ET.AEvent<object,ET.EventType.NumbericChange>
	// ET.AEvent<object,ET.EventType.HallSceneEnterStart>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>
	// ET.AInvokeHandler<ET.ConfigComponent.GetAllConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetOneConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetRes,object>
	// ET.ATimer<object>
	// ET.AwakeSystem<object>
	// ET.AwakeSystem<object,System.Net.Sockets.AddressFamily>
	// ET.AwakeSystem<object,int>
	// ET.AwakeSystem<object,object>
	// ET.AwakeSystem<object,ET.Ability.TeamFlagType>
	// ET.AwakeSystem<object,object,int>
	// ET.AwakeSystem<object,int,Unity.Mathematics.float3>
	// ET.ConfigSingleton<object>
	// ET.DestroySystem<object>
	// ET.EntityRef<object>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>
	// ET.ETAsyncTaskMethodBuilder<byte>
	// ET.ETAsyncTaskMethodBuilder<int>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>
	// ET.ETAsyncTaskMethodBuilder<uint>
	// ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>
	// ET.ETAsyncTaskMethodBuilder<object>
	// ET.ETTask<object>
	// ET.ETTask<System.ValueTuple<uint,object>>
	// ET.ETTask<uint>
	// ET.ETTask<ET.Client.Wait_CreateMyUnit>
	// ET.ETTask<ET.Client.Wait_SceneChangeFinish>
	// ET.ETTask<int>
	// ET.ETTask<UnityEngine.SceneManagement.Scene>
	// ET.ETTask<ET.Client.Wait_UnitStop>
	// ET.ETTask<byte>
	// ET.FixedUpdateSystem<object>
	// ET.HashSetComponent<object>
	// ET.IAwake<ET.Ability.TeamFlagType>
	// ET.IAwake<int>
	// ET.IAwake<System.Net.Sockets.AddressFamily>
	// ET.IAwake<object>
	// ET.IAwake<int,Unity.Mathematics.float3>
	// ET.IAwake<object,int>
	// ET.LateUpdateSystem<object>
	// ET.ListComponent<long>
	// ET.ListComponent<object>
	// ET.ListComponent<Unity.Mathematics.float3>
	// ET.ListComponent<ET.Ability.TeamFlagType>
	// ET.LoadSystem<object>
	// ET.MultiDictionary<int,int,object>
	// ET.MultiDictionary<long,object,int>
	// ET.MultiMap<ET.AbilityConfig.ControlState,object>
	// ET.MultiMap<ET.AbilityConfig.BuffTagType,object>
	// ET.MultiMap<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// ET.MultiMap<ET.AbilityConfig.BuffTagGroupType,object>
	// ET.MultiMap<long,object>
	// ET.MultiMap<int,object>
	// ET.MultiMap<ET.AbilityConfig.BuffType,object>
	// ET.MultiMap<float,object>
	// ET.MultiMap<ET.Ability.SkillSlotType,object>
	// ET.MultiMap<long,byte>
	// ET.Singleton<object>
	// ET.UpdateSystem<object>
	// System.Action<long>
	// System.Action<int>
	// System.Action<float>
	// System.Action<object,int>
	// System.Action<long,int>
	// System.Action<long,long,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<ET.Client.WindowID,object>
	// System.Collections.Generic.Dictionary<ET.Ability.AbilityAoeMonitorTriggerEvent,object>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary<object,float>
	// System.Collections.Generic.Dictionary<long,long>
	// System.Collections.Generic.Dictionary<object,long>
	// System.Collections.Generic.Dictionary<System.ValueTuple<object,int>,object>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<long,int>
	// System.Collections.Generic.Dictionary<object,byte>
	// System.Collections.Generic.Dictionary<int,float>
	// System.Collections.Generic.Dictionary<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary<ushort,object>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,float>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSet<long>
	// System.Collections.Generic.HashSet<ushort>
	// System.Collections.Generic.HashSet.Enumerator<long>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.KeyValuePair<int,float>
	// System.Collections.Generic.KeyValuePair<float,object>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<long,int>
	// System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<object,byte>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<ET.Ability.TeamFlagType>
	// System.Collections.Generic.List<byte>
	// System.Collections.Generic.List<ET.Client.WindowID>
	// System.Collections.Generic.List<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<Unity.Mathematics.float3>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>
	// System.Collections.Generic.List<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.List.Enumerator<ET.AbilityConfig.BuffTagGroupType>
	// System.Collections.Generic.List.Enumerator<ET.AbilityConfig.BuffTagType>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.Queue<ET.Client.WindowID>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.ControlState,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffType,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagGroupType,object>
	// System.Collections.Generic.SortedDictionary<float,object>
	// System.Collections.Generic.SortedDictionary<ET.AbilityConfig.BuffTagType,object>
	// System.Collections.Generic.SortedDictionary<object,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<object,object>
	// System.Comparison<object>
	// System.Func<object>
	// System.Func<int,object>
	// System.Func<object,object,object>
	// System.Nullable<ET.AbilityConfig.BuffTagGroupType>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.Task<System.ValueTuple<uint,uint>>
	// System.Threading.Tasks.Task<object>
	// System.ValueTuple<Unity.Mathematics.float3,Unity.Mathematics.float3>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<ET.AbilityConfig.NumericType,float>
	// System.ValueTuple<object,int>
	// System.ValueTuple<uint,object>
	// System.ValueTuple<object,object>
	// System.ValueTuple<uint,uint>
	// System.ValueTuple<int,object>
	// System.ValueTuple<byte,byte,object>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityEvent<object>
	// UnityEngine.Events.UnityEvent<byte>
	// }}

	public void RefMethods()
	{
		// string Bright.Common.StringUtil.CollectionToString<object>(System.Collections.Generic.IEnumerable<object>)
		// string Bright.Common.StringUtil.CollectionToString<int>(System.Collections.Generic.IEnumerable<int>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagType>)
		// string Bright.Common.StringUtil.CollectionToString<ET.AbilityConfig.BuffTagGroupType>(System.Collections.Generic.IEnumerable<ET.AbilityConfig.BuffTagGroupType>)
		// string Bright.Common.StringUtil.CollectionToString<float>(System.Collections.Generic.IEnumerable<float>)
		// object ET.Entity.AddChild<object>(bool)
		// object ET.Entity.AddChild<object,object>(object,bool)
		// object ET.Entity.AddChildWithId<object,int>(long,int,bool)
		// object ET.Entity.AddChildWithId<object,object>(long,object,bool)
		// object ET.Entity.AddChildWithId<object>(long,bool)
		// object ET.Entity.AddComponent<object>(bool)
		// object ET.Entity.AddComponent<object,System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily,bool)
		// object ET.Entity.AddComponent<object,ET.Ability.TeamFlagType>(ET.Ability.TeamFlagType,bool)
		// object ET.Entity.AddComponent<object,int,Unity.Mathematics.float3>(int,Unity.Mathematics.float3,bool)
		// object ET.Entity.AddComponent<object,object>(object,bool)
		// object ET.Entity.AddComponent<object,object,int>(object,int,bool)
		// object ET.Entity.GetChild<object>(long)
		// object ET.Entity.GetComponent<object>()
		// object ET.Entity.GetParent<object>()
		// System.Void ET.Entity.RemoveComponent<object>()
		// ET.SceneType ET.EnumHelper.FromString<ET.SceneType>(string)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_PathfindingResultHandler.<Run>d__0>(object&,ET.Client.M2C_PathfindingResultHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_StopHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_StopHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.MoveHelper.<MoveToAsync>d__1>(object&,ET.Client.MoveHelper.<MoveToAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PingComponentAwakeSystem.<PingAsync>d__1>(object&,ET.Client.PingComponentAwakeSystem.<PingAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<LoginOut>d__1>(object&,ET.Client.LoginHelper.<LoginOut>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<GetRoomListAsync>d__0>(object&,ET.Client.RoomHelper.<GetRoomListAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_CreateMyUnitHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_CreateMyUnitHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<GetRoomInfoAsync>d__1>(object&,ET.Client.RoomHelper.<GetRoomInfoAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_CreateUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_CreateUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<CreateRoomAsync>d__2>(object&,ET.Client.RoomHelper.<CreateRoomAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<JoinRoomAsync>d__3>(object&,ET.Client.RoomHelper.<JoinRoomAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<QuitRoomAsync>d__4>(object&,ET.Client.RoomHelper.<QuitRoomAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__5>(object&,ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<ReLogin>d__2>(object&,ET.Client.LoginHelper.<ReLogin>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ChgRoomSeatAsync>d__6>(object&,ET.Client.RoomHelper.<ChgRoomSeatAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<BeKickedOutRoomAsync>d__8>(object&,ET.Client.RoomHelper.<BeKickedOutRoomAsync>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneHelper.<EnterBattle>d__1>(object&,ET.Client.SceneHelper.<EnterBattle>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneChangeHelper.<SceneChangeTo>d__0>(object&,ET.Client.SceneChangeHelper.<SceneChangeTo>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_StartSceneChangeHandler.<Run>d__0>(object&,ET.Client.M2C_StartSceneChangeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>&,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(object&,ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3>(object&,ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2>(object&,ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RouterAddressComponentSystem.<Init>d__1>(object&,ET.Client.RouterAddressComponentSystem.<Init>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<ReturnBackBattle>d__10>(object&,ET.Client.RoomHelper.<ReturnBackBattle>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.NetClientComponentOnReadEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.NetClientComponentOnReadEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginHelper.<Login>d__0>(object&,ET.Client.LoginHelper.<Login>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(object&,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__9>(object&,ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.RoomHelper.<MemberQuitBattleAsync>d__7>(object&,ET.Client.RoomHelper.<MemberQuitBattleAsync>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SceneHelper.<EnterHall>d__0>(object&,ET.Client.SceneHelper.<EnterHall>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_RemoveEffect.<Run>d__0>(object&,ET.Ability.Action_RemoveEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_SummonUnit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_SummonUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_CreateAoE.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_CreateAoE.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_CreateAoE.<Run>d__0>(object&,ET.Ability.Action_CreateAoE.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffResetDuration.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffResetDuration.<Run>d__0>(object&,ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffReduceStackCount.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffReduceStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffReduceStackCount.<Run>d__0>(object&,ET.Ability.Action_BuffReduceStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_BuffAddStackCount.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_BuffAddStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_BuffAddStackCount.<Run>d__0>(object&,ET.Ability.Action_BuffAddStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_AttackArea.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_AttackArea.<Run>d__0>(object&,ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_AddBuff.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_AddBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_AddBuff.<Run>d__0>(object&,ET.Ability.Action_AddBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.ETTaskCompleted&,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ConsoleComponentSystem.<Start>d__3>(object&,ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.ConsoleComponentSystem.<Start>d__3>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6>(object&,ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_CreateEffect.<Run>d__0>(object&,ET.Ability.Action_CreateEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EnterMapHelper.<EnterMapAsync>d__0>(object&,ET.Client.EnterMapHelper.<EnterMapAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_CreateEffect.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_CreateEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_DamageUnit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_SummonUnit.<Run>d__0>(object&,ET.Ability.Action_SummonUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_RemoveEffect.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_RemoveEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_RemoveUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_RemoveUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_RemoveBuff.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_RemoveBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_RemoveBuff.<Run>d__0>(object&,ET.Ability.Action_RemoveBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_PlayAudio.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_PlayAudio.<Run>d__0>(object&,ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_PlayAnimator.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_PlayAnimator.<Run>d__0>(object&,ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_ForceMove.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_ForceMove.<Run>d__0>(object&,ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_FireBullet.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_FireBullet.<Run>d__0>(object&,ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_FaceTo.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_FaceTo.<Run>d__0>(object&,ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.Action_DeathShow.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_DeathShow.<Run>d__0>(object&,ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Ability.Action_DamageUnit.<Run>d__0>(object&,ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendCallOwnTower>d__5>(object&,ET.Client.GamePlayHelper.<SendCallOwnTower>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.SwitchLanguageEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.SwitchLanguageEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.OnPatchDownlodFailedEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.OnPatchDownlodFailedEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.OnPatchDownloadProgressEvent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.OnPatchDownloadProgressEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.RoomInfoChg_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.RoomInfoChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<ShowWindowAsync>d__10>(object&,ET.Client.UIComponentSystem.<ShowWindowAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<ChgRoomSeat>d__8>(object&,ET.Client.DlgRoomSystem.<ChgRoomSeat>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__7>(object&,ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<QuitRoom>d__6>(object&,ET.Client.DlgRoomSystem.<QuitRoom>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<KickOutRoom>d__5>(object&,ET.Client.DlgRoomSystem.<KickOutRoom>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgRoomSystem.<GetRoomInfo>d__3>(object&,ET.Client.DlgRoomSystem.<GetRoomInfo>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.LoginFinish_UI.<Run>d__0>(object&,ET.Client.LoginFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(object&,ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__26>(object&,ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__26&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d>(object&,ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d>(object&,ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBullet.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBullet.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBullet.EventHandler_BulletOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBullet.EventHandler_BulletOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBullet.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBullet.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_UnitOnRemoved.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_UnitOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_UnitOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_UnitOnCreate.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnKill.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnHit.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerBuff.EventHandler_SkillOnCast.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerBuff.EventHandler_SkillOnCast.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletLeave.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletLeave.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletEnter.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterLeave.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterLeave.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterEnter.<Run>d__0>(ET.ETTaskCompleted&,ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLobbySystem.<ReturnLogin>d__3>(object&,ET.Client.DlgLobbySystem.<ReturnLogin>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgLobbySystem.<EnterMap>d__2>(object&,ET.Client.DlgLobbySystem.<EnterMap>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<ReturnLogin>d__6>(object&,ET.Client.DlgHallSystem.<ReturnLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<Run>d__0>(object&,ET.Client.EntryEvent3_InitClient.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4>(object&,ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ARSessionComponentSystem.<LoadARSession>d__3>(object&,ET.Client.ARSessionComponentSystem.<LoadARSession>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SkillHelper.<CastSkill>d__1>(object&,ET.Client.SkillHelper.<CastSkill>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.SkillHelper.<LearnSkill>d__0>(object&,ET.Client.SkillHelper.<LearnSkill>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(object&,ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__1>(object&,ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendCallTank>d__7>(object&,ET.Client.GamePlayHelper.<SendCallTank>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GamePlayComponentSystem.<DownloadMapRecast>d__14>(ET.ETTaskCompleted&,ET.GamePlayComponentSystem.<DownloadMapRecast>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendRefreshBuyPlayerTower>d__4>(object&,ET.Client.GamePlayHelper.<SendRefreshBuyPlayerTower>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendBuyPlayerTower>d__3>(object&,ET.Client.GamePlayHelper.<SendBuyPlayerTower>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendPutMonsterCall>d__2>(object&,ET.Client.GamePlayHelper.<SendPutMonsterCall>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendPutHome>d__1>(object&,ET.Client.GamePlayHelper.<SendPutHome>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(object&,ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.GamePlayHelper.<SendCallTower>d__6>(object&,ET.Client.GamePlayHelper.<SendCallTower>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__2>(object&,ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EntryEvent3_InitClient.<EnterGame>d__3>(object&,ET.Client.EntryEvent3_InitClient.<EnterGame>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<RefreshRoomList>d__5>(object&,ET.Client.DlgHallSystem.<RefreshRoomList>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<CreateRoom>d__4>(object&,ET.Client.DlgHallSystem.<CreateRoom>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<GetRoomList>d__2>(object&,ET.Client.DlgHallSystem.<GetRoomList>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<ReturnLogin>d__5>(object&,ET.Client.DlgGameModeSystem.<ReturnLogin>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterARRoomMode>d__4>(object&,ET.Client.DlgGameModeSystem.<EnterARRoomMode>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3>(object&,ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2>(object&,ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__3>(object&,ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.GamePlayChg_RefreshUI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.GamePlayChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleTowerSystem.<QuitBattle>d__9>(object&,ET.Client.DlgBattleTowerSystem.<QuitBattle>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.EnterMapFinish_UI.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.EnterMapFinish_UI.<Run>d__0>(object&,ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgBattleSystem.<QuitBattle>d__3>(object&,ET.Client.DlgBattleSystem.<QuitBattle>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.HallSceneEnterStart_UI.<Run>d__0>(object&,ET.Client.HallSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1>(object&,ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0>(object&,ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0>(ET.ETTaskCompleted&,ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.DlgHallSystem.<JoinRoom>d__7>(object&,ET.Client.DlgHallSystem.<JoinRoom>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.EntryEvent1_InitShare.<Run>d__0>(ET.ETTaskCompleted&,ET.EntryEvent1_InitShare.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.AFsmNodeHandler.<OnEnter>d__4>(ET.ETTaskCompleted&,ET.AFsmNodeHandler.<OnEnter>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Entry.<StartAsync>d__2>(object&,ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<QuitRoom>d__6>(ET.Client.DlgRoomSystem.<QuitRoom>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendPutMonsterCall>d__2>(ET.Client.GamePlayHelper.<SendPutMonsterCall>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendPutHome>d__1>(ET.Client.GamePlayHelper.<SendPutHome>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffReduceStackCount.<Run>d__0>(ET.Ability.Action_BuffReduceStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0>(ET.Client.R2C_RoomInfoChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0>(ET.Client.M2C_SyncUnitEffectsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0>(ET.Client.M2C_SyncPosUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0>(ET.Client.M2C_SyncNumericUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_RemoveUnitsHandler.<Run>d__0>(ET.Client.M2C_RemoveUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_CreateUnitsHandler.<Run>d__0>(ET.Client.M2C_CreateUnitsHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_CreateMyUnitHandler.<Run>d__0>(ET.Client.M2C_CreateMyUnitHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0>(ET.Client.G2C_PlayerStatusChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0>(ET.Client.G2C_EnterBattleNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffResetDuration.<Run>d__0>(ET.Ability.Action_BuffResetDuration.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0>(ET.Client.G2C_BeKickMemberOutRoomHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.NetClientComponentOnReadEvent.<Run>d__0>(ET.Client.NetClientComponentOnReadEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendBuyPlayerTower>d__3>(ET.Client.GamePlayHelper.<SendBuyPlayerTower>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_BuffAddStackCount.<Run>d__0>(ET.Ability.Action_BuffAddStackCount.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendCallOwnTower>d__5>(ET.Client.GamePlayHelper.<SendCallOwnTower>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_CreateAoE.<Run>d__0>(ET.Ability.Action_CreateAoE.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnKill.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_UnitOnHit.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_UnitOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBullet.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandlerBullet.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBullet.EventHandler_BulletOnHit.<Run>d__0>(ET.Ability.EventHandlerBullet.EventHandler_BulletOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBullet.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandlerBullet.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendRefreshBuyPlayerTower>d__4>(ET.Client.GamePlayHelper.<SendRefreshBuyPlayerTower>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SkillHelper.<CastSkill>d__1>(ET.Client.SkillHelper.<CastSkill>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SkillHelper.<LearnSkill>d__0>(ET.Client.SkillHelper.<LearnSkill>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0>(ET.Client.M2C_GamePlayChgNoticeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_AddBuff.<Run>d__0>(ET.Ability.Action_AddBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendCallTank>d__7>(ET.Client.GamePlayHelper.<SendCallTank>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_AttackArea.<Run>d__0>(ET.Ability.Action_AttackArea.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayHelper.<SendCallTower>d__6>(ET.Client.GamePlayHelper.<SendCallTower>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_CreateEffect.<Run>d__0>(ET.Ability.Action_CreateEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneHelper.<EnterBattle>d__1>(ET.Client.SceneHelper.<EnterBattle>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneHelper.<EnterHall>d__0>(ET.Client.SceneHelper.<EnterHall>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<Init>d__1>(ET.Client.RouterAddressComponentSystem.<Init>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginHelper.<ReLogin>d__2>(ET.Client.LoginHelper.<ReLogin>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ReturnBackBattle>d__10>(ET.Client.RoomHelper.<ReturnBackBattle>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__9>(ET.Client.RoomHelper.<MemberReturnRoomFromBattleAsync>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<BeKickedOutRoomAsync>d__8>(ET.Client.RoomHelper.<BeKickedOutRoomAsync>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<MemberQuitBattleAsync>d__7>(ET.Client.RoomHelper.<MemberQuitBattleAsync>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomSeatAsync>d__6>(ET.Client.RoomHelper.<ChgRoomSeatAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_PathfindingResultHandler.<Run>d__0>(ET.Client.M2C_PathfindingResultHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__5>(ET.Client.RoomHelper.<ChgRoomMemberStatusAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<QuitRoomAsync>d__4>(ET.Client.RoomHelper.<QuitRoomAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<JoinRoomAsync>d__3>(ET.Client.RoomHelper.<JoinRoomAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<CreateRoomAsync>d__2>(ET.Client.RoomHelper.<CreateRoomAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<GetRoomInfoAsync>d__1>(ET.Client.RoomHelper.<GetRoomInfoAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomHelper.<GetRoomListAsync>d__0>(ET.Client.RoomHelper.<GetRoomListAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_StopHandler.<Run>d__0>(ET.Client.M2C_StopHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2>(ET.Client.RouterAddressComponentSystem.<GetAllRouter>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3>(ET.Client.RouterAddressComponentSystem.<WaitTenMinGetAllRouter>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginHelper.<LoginOut>d__1>(ET.Client.LoginHelper.<LoginOut>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1>(ET.Client.RouterCheckComponentAwakeSystem.<CheckAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_DamageUnit.<Run>d__0>(ET.Ability.Action_DamageUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_DeathShow.<Run>d__0>(ET.Ability.Action_DeathShow.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<KickOutRoom>d__5>(ET.Client.DlgRoomSystem.<KickOutRoom>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_FaceTo.<Run>d__0>(ET.Ability.Action_FaceTo.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_FireBullet.<Run>d__0>(ET.Ability.Action_FireBullet.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SceneChangeHelper.<SceneChangeTo>d__0>(ET.Client.SceneChangeHelper.<SceneChangeTo>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_ForceMove.<Run>d__0>(ET.Ability.Action_ForceMove.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnKill.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnKill.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_PlayAnimator.<Run>d__0>(ET.Ability.Action_PlayAnimator.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_RemoveBuff.<Run>d__0>(ET.Ability.Action_RemoveBuff.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.M2C_StartSceneChangeHandler.<Run>d__0>(ET.Client.M2C_StartSceneChangeHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_RemoveEffect.<Run>d__0>(ET.Ability.Action_RemoveEffect.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_SummonUnit.<Run>d__0>(ET.Ability.Action_SummonUnit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Client.EffectShowObjSystem.<Init>d__2>(ET.Ability.Client.EffectShowObjSystem.<Init>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EnterMapHelper.<EnterMapAsync>d__0>(ET.Client.EnterMapHelper.<EnterMapAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginHelper.<Login>d__0>(ET.Client.LoginHelper.<Login>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.Action_PlayAudio.<Run>d__0>(ET.Ability.Action_PlayAudio.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnHit.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_DamageAfterOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_SkillOnCast.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_SkillOnCast.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PingComponentAwakeSystem.<PingAsync>d__1>(ET.Client.PingComponentAwakeSystem.<PingAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterARRoomMode>d__4>(ET.Client.DlgGameModeSystem.<EnterARRoomMode>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<ReturnLogin>d__5>(ET.Client.DlgGameModeSystem.<ReturnLogin>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<GetRoomList>d__2>(ET.Client.DlgHallSystem.<GetRoomList>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnHit.<Run>d__0>(ET.Ability.EventHandlerBuff.EventHandler_DamageBeforeOnHit.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<RefreshRoomList>d__5>(ET.Client.DlgHallSystem.<RefreshRoomList>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<ReturnLogin>d__6>(ET.Client.DlgHallSystem.<ReturnLogin>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<JoinRoom>d__7>(ET.Client.DlgHallSystem.<JoinRoom>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6>(ET.MonsterWaveCallOnceComponentSystem.<CallMonster>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GamePlayComponentSystem.<DownloadMapRecast>d__14>(ET.GamePlayComponentSystem.<DownloadMapRecast>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<EnterMap>d__2>(ET.Client.DlgLobbySystem.<EnterMap>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgLobbySystem.<ReturnLogin>d__3>(ET.Client.DlgLobbySystem.<ReturnLogin>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0>(ET.Client.AppStartInitFinish_CreateLoginUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.LoginFinish_UI.<Run>d__0>(ET.Client.LoginFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<RefreshUI>d__2>(ET.Client.DlgRoomSystem.<RefreshUI>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3>(ET.Client.DlgGameModeSystem.<EnterRoomMode>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<GetRoomInfo>d__3>(ET.Client.DlgRoomSystem.<GetRoomInfo>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__26>(ET.Client.UIComponentSystem.<LoadBaseWindowsAsync>d__26&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.UIComponentSystem.<ShowWindowAsync>d__10>(ET.Client.UIComponentSystem.<ShowWindowAsync>d__10&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AFsmNodeHandler.<OnEnter>d__4>(ET.AFsmNodeHandler.<OnEnter>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.SwitchLanguageEvent.<Run>d__0>(ET.Client.SwitchLanguageEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Entry.<StartAsync>d__2>(ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0>(ET.Client.ChangeRotation_SyncGameObjectRotation.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0>(ET.Client.ChangePosition_SyncGameObjectPos.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0>(ET.Client.AfterUnitCreate_CreateUnitView.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.OnPatchDownlodFailedEvent.<Run>d__0>(ET.Client.OnPatchDownlodFailedEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.OnPatchDownloadProgressEvent.<Run>d__0>(ET.Client.OnPatchDownloadProgressEvent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.RoomInfoChg_RefreshUI.<Run>d__0>(ET.Client.RoomInfoChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BeKickedRoom_RefreshUI.<Run>d__0>(ET.Client.BeKickedRoom_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ChgRoomSeat>d__8>(ET.Client.DlgRoomSystem.<ChgRoomSeat>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__7>(ET.Client.DlgRoomSystem.<ChgRoomMemberStatus>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EntryEvent1_InitShare.<Run>d__0>(ET.EntryEvent1_InitShare.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2>(ET.Client.DlgGameModeSystem.<EnterSingleMapMode>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgHallSystem.<CreateRoom>d__4>(ET.Client.DlgHallSystem.<CreateRoom>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.GamePlayChg_RefreshUI.<Run>d__0>(ET.Client.GamePlayChg_RefreshUI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0>(ET.Client.AfterCreateClientScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ConsoleComponentSystem.<Start>d__3>(ET.ConsoleComponentSystem.<Start>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<EnterGame>d__3>(ET.Client.EntryEvent3_InitClient.<EnterGame>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__2>(ET.Client.EntryEvent3_InitClient.<DownloadPatch>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__1>(ET.Client.EntryEvent3_InitClient.<HotUpdateAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EntryEvent3_InitClient.<Run>d__0>(ET.Client.EntryEvent3_InitClient.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4>(ET.Client.CameraComponentSystem.<SetFollowPlayer>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ARSessionComponentSystem.<LoadARSession>d__3>(ET.Client.ARSessionComponentSystem.<LoadARSession>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_UnitOnCreate.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_UnitOnCreate.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_UnitOnRemoved.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_UnitOnRemoved.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterEnter.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterLeave.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_AoeOnCharacterLeave.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletEnter.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletEnter.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletLeave.<Run>d__0>(ET.Ability.EventHandlerAoe.EventHandler_AoeOnBulletLeave.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__3>(ET.Client.DlgBattleTowerEndSystem.<OnReturnRoom>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0>(ET.Client.AfterCreateCurrentScene_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.MoveHelper.<MoveToAsync>d__1>(ET.Client.MoveHelper.<MoveToAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EnterMapFinish_UI.<Run>d__0>(ET.Client.EnterMapFinish_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__22>(ET.Client.DlgBattleTowerSystem.<RefreshBuyTower>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1>(ET.Client.BattleSceneEnterStart_AddComponent.<SetMainCamera>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<BuyTower>d__21>(ET.Client.DlgBattleTowerSystem.<BuyTower>d__21&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d>(ET.Client.EUIHelper.<>c__DisplayClass13_0.<<AddListenerAsync>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<NotTowerBuyShowWhenBattle>d__15>(ET.Client.DlgBattleTowerSystem.<NotTowerBuyShowWhenBattle>d__15&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<CloseTowerBuyShow>d__14>(ET.Client.DlgBattleTowerSystem.<CloseTowerBuyShow>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.HallSceneEnterStart_UI.<Run>d__0>(ET.Client.HallSceneEnterStart_UI.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleSystem.<QuitBattle>d__3>(ET.Client.DlgBattleSystem.<QuitBattle>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<TowerBuyShow>d__13>(ET.Client.DlgBattleTowerSystem.<TowerBuyShow>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<QuitBattle>d__9>(ET.Client.DlgBattleTowerSystem.<QuitBattle>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d>(ET.Client.EUIHelper.<>c__DisplayClass12_0.<<AddListenerAsyncWithId>g__clickActionAsync|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0>(ET.Client.BattleSceneEnterStart_AddComponent.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.DlgBattleTowerSystem.<RefreshUI>d__8>(ET.Client.DlgBattleTowerSystem.<RefreshUI>d__8&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.IconHelper.<LoadIconSpriteAsync>d__1>(object&,ET.Client.IconHelper.<LoadIconSpriteAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4>(object&,ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__3>(object&,ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__4>(object&,ET.SessionSystem.<Call>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3>(object&,ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14>(object&,ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13>(object&,ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadSceneAsync>d__12>(object&,ET.Client.ResComponentSystem.<LoadSceneAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6>(object&,ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17>(object&,ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResComponentSystem.<LoadAssetAsync>d__11>(object&,ET.Client.ResComponentSystem.<LoadAssetAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<GetRouterAddress>d__1>(object&,ET.Client.RouterHelper.<GetRouterAddress>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,ET.Client.MoveHelper.<MoveToAsync>d__0>(object&,ET.Client.MoveHelper.<MoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.Client.HttpClientHelper.<Get>d__0>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.Client.HttpClientHelper.<Get>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<CreateRouterSession>d__0>(object&,ET.Client.RouterHelper.<CreateRouterSession>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.AwaitUnsafeOnCompleted<object,ET.Client.RouterHelper.<Connect>d__2>(object&,ET.Client.RouterHelper.<Connect>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.SceneFactory.<CreateClientScene>d__0>(object&,ET.Client.SceneFactory.<CreateClientScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.MoveByPathComponentSystem.<MoveToAsync>d__5>(object&,ET.MoveByPathComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.MoveHelper.<MoveToAsync>d__0>(ET.Client.MoveHelper.<MoveToAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.MoveByPathComponentSystem.<MoveToAsync>d__5>(ET.MoveByPathComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<object>>(ET.Client.ResComponentSystem.<LoadAssetAsync>d__10<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_SceneChangeFinish>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_SceneChangeFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.SceneFactory.<CreateClientScene>d__0>(ET.Client.SceneFactory.<CreateClientScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_CreateMyUnit>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_CreateMyUnit>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.Start<ET.Client.RouterHelper.<Connect>d__2>(ET.Client.RouterHelper.<Connect>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14>(ET.Client.ResComponentSystem.<LoadRawFileTextAsync>d__14&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13>(ET.Client.ResComponentSystem.<LoadRawFileDataAsync>d__13&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>.Start<ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_UnitStop>>(ET.ObjectWaitSystem.<Wait>d__4<ET.Client.Wait_UnitStop>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResComponentSystem.<LoadAssetAsync>d__11>(ET.Client.ResComponentSystem.<LoadAssetAsync>d__11&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.IconHelper.<LoadIconSpriteAsync>d__1>(ET.Client.IconHelper.<LoadIconSpriteAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.HttpClientHelper.<Get>d__0>(ET.Client.HttpClientHelper.<Get>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__4>(ET.SessionSystem.<Call>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.RouterHelper.<CreateRouterSession>d__0>(ET.Client.RouterHelper.<CreateRouterSession>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__3>(ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.Start<ET.Client.ResComponentSystem.<LoadSceneAsync>d__12>(ET.Client.ResComponentSystem.<LoadSceneAsync>d__12&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3>(ET.Client.ResComponentSystem.<UpdateVersionAsync>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17>(ET.Client.UIComponentSystem.<ShowBaseWindowAsync>d__17&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4>(ET.Client.ResComponentSystem.<UpdateManifestAsync>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6>(ET.Client.ResComponentSystem.<DonwloadWebFilesAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.Start<ET.Client.RouterHelper.<GetRouterAddress>d__1>(ET.Client.RouterHelper.<GetRouterAddress>d__1&)
		// object ET.EventSystem.Invoke<ET.NavmeshComponent.RecastFileLoader,object>(ET.NavmeshComponent.RecastFileLoader)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStop>(object,ET.EventType.MoveByPathStop)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NoticeGamePlayToClient>(object,ET.EventType.NoticeGamePlayToClient)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStart>(object,ET.EventType.MoveByPathStart)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitEnterSightRange>(object,ET.EventType.UnitEnterSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitLeaveSightRange>(object,ET.EventType.UnitLeaveSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.Client.NetClientComponentOnRead>(object,ET.Client.NetClientComponentOnRead)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SwitchLanguage>(object,ET.EventType.SwitchLanguage)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.GamePlayChg>(object,ET.EventType.GamePlayChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NumbericChange>(object,ET.EventType.NumbericChange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncNumericUnits>(object,ET.EventType.SyncNumericUnits)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.RoomInfoChg>(object,ET.EventType.RoomInfoChg)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.EnterMapFinish>(object,ET.EventType.EnterMapFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.BeKickedRoom>(object,ET.EventType.BeKickedRoom)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownloadProgress>(object,ET.EventType.OnPatchDownloadProgress)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownlodFailed>(object,ET.EventType.OnPatchDownlodFailed)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>(object,ET.Ability.AbilityTriggerEventType.SkillOnCast)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncUnitEffects>(object,ET.EventType.SyncUnitEffects)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SyncPosUnits>(object,ET.EventType.SyncPosUnits)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>(object,ET.Ability.AbilityTriggerEventType.UnitOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BulletOnHit>(object,ET.Ability.AbilityTriggerEventType.BulletOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>(object,ET.Ability.AbilityTriggerEventType.UnitOnCreate)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangeRotation>(object,ET.EventType.ChangeRotation)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.BattleSceneEnterFinish>(object,ET.EventType.BattleSceneEnterFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterCreateCurrentScene>(object,ET.EventType.AfterCreateCurrentScene)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangePosition>(object,ET.EventType.ChangePosition)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterUnitCreate>(object,ET.EventType.AfterUnitCreate)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>(object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.HallSceneEnterStart>(object,ET.EventType.HallSceneEnterStart)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent1>(object,ET.EventType.EntryEvent1)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent2>(object,ET.EventType.EntryEvent2)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent3>(object,ET.EventType.EntryEvent3)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.BattleSceneEnterStart>(object,ET.EventType.BattleSceneEnterStart)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginFinish>(object,ET.EventType.LoginFinish)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginOutFinish>(object,ET.EventType.LoginOutFinish)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.AfterCreateClientScene>(object,ET.EventType.AfterCreateClientScene)
		// object ET.Game.AddSingleton<object>()
		// object ET.JsonHelper.FromJson<object>(string)
		// object ET.MongoHelper.Deserialize<object>(byte[])
		// System.Void ET.ObjectHelper.Swap<object>(object&,object&)
		// System.Void ET.RandomGenerator.BreakRank<object>(System.Collections.Generic.List<object>)
		// string ET.StringHelper.ArrayToString<float>(float[])
		// object ReferenceCollector.Get<object>(string)
		// ET.Client.Wait_UnitStop System.Activator.CreateInstance<ET.Client.Wait_UnitStop>()
		// ET.Client.Wait_CreateMyUnit System.Activator.CreateInstance<ET.Client.Wait_CreateMyUnit>()
		// ET.Client.Wait_SceneChangeFinish System.Activator.CreateInstance<ET.Client.Wait_SceneChangeFinish>()
		// ET.RpcInfo[] System.Linq.Enumerable.ToArray<ET.RpcInfo>(System.Collections.Generic.IEnumerable<ET.RpcInfo>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.TaskFactory.StartNew<object>(System.Func<object>,System.Threading.CancellationToken)
		// object UnityEngine.Component.GetComponent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>()
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.Object.Instantiate<object>(object)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
	}
}