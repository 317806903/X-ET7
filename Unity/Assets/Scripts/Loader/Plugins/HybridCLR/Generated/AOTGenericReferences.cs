public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	// System.Core.dll
	// System.dll
	// Unity.Core.dll
	// Unity.ThirdParty.dll
	// UnityEngine.CoreModule.dll
	// mscorlib.dll
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// ET.AEvent<object,ET.EventType.AfterCreateClientScene>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BuffOnDestroy>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BuffOnRemoved>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BuffOnRefresh>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BuffOnStart>
	// ET.AEvent<object,ET.EventType.EntryEvent1>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.BuffOnAwake>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnBulletLeave>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnBulletEnter>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnCharacterLeave>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.AoeOnCharacterEnter>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>
	// ET.AEvent<object,ET.EventType.NumbericChange>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>
	// ET.AEvent<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>
	// ET.AEvent<object,ET.EventType.SceneChangeStart>
	// ET.AEvent<object,ET.Client.NetClientComponentOnRead>
	// ET.AEvent<object,ET.EventType.SwitchLanguage>
	// ET.AEvent<object,ET.EventType.EntryEvent3>
	// ET.AEvent<object,ET.EventType.OnPatchDownloadProgress>
	// ET.AEvent<object,ET.EventType.OnPatchDownlodFailed>
	// ET.AEvent<object,ET.EventType.LoginFinish>
	// ET.AEvent<object,ET.EventType.AfterCreateCurrentScene>
	// ET.AEvent<object,ET.EventType.ChangeRotation>
	// ET.AEvent<object,ET.EventType.AfterUnitCreate>
	// ET.AEvent<object,ET.EventType.ChangePosition>
	// ET.AInvokeHandler<ET.ConfigComponent.GetAllConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigComponent.GetOneConfigBytes,object>
	// ET.ATimer<object>
	// ET.AwakeSystem<object>
	// ET.AwakeSystem<object,int>
	// ET.AwakeSystem<object,System.Net.Sockets.AddressFamily>
	// ET.AwakeSystem<object,object>
	// ET.AwakeSystem<object,ET.Ability.TeamFlagType>
	// ET.AwakeSystem<object,int,Unity.Mathematics.float3>
	// ET.AwakeSystem<object,object,int>
	// ET.ConfigSingleton<object>
	// ET.DestroySystem<object>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>
	// ET.ETAsyncTaskMethodBuilder<byte>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>
	// ET.ETAsyncTaskMethodBuilder<object>
	// ET.ETAsyncTaskMethodBuilder<uint>
	// ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>
	// ET.ETAsyncTaskMethodBuilder<int>
	// ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>
	// ET.ETTask<ET.Client.Wait_SceneChangeFinish>
	// ET.ETTask<System.ValueTuple<uint,object>>
	// ET.ETTask<uint>
	// ET.ETTask<ET.Client.Wait_CreateMyUnit>
	// ET.ETTask<int>
	// ET.ETTask<object>
	// ET.ETTask<UnityEngine.SceneManagement.Scene>
	// ET.ETTask<byte>
	// ET.ETTask<ET.Client.Wait_UnitStop>
	// ET.FixedUpdateSystem<object>
	// ET.HashSetComponent<object>
	// ET.IAwake<int>
	// ET.IAwake<object>
	// ET.IAwake<System.Net.Sockets.AddressFamily>
	// ET.IAwake<ET.Ability.TeamFlagType>
	// ET.IAwake<int,Unity.Mathematics.float3>
	// ET.IAwake<object,int>
	// ET.LateUpdateSystem<object>
	// ET.ListComponent<object>
	// ET.ListComponent<Unity.Mathematics.float3>
	// ET.ListComponent<long>
	// ET.LoadSystem<object>
	// ET.MultiMap<float,object>
	// ET.MultiMap<ET.Ability.SkillSlotType,object>
	// ET.MultiMap<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// ET.Singleton<object>
	// ET.UpdateSystem<object>
	// System.Action<object>
	// System.Action<float>
	// System.Action<long,int>
	// System.Action<long,long,object>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<ET.Ability.TeamFlagType,int>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<ushort,object>
	// System.Collections.Generic.Dictionary<object,float>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<object,long>
	// System.Collections.Generic.Dictionary<object,ET.Client.PanelInfo>
	// System.Collections.Generic.Dictionary<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary<ET.Client.PanelId,object>
	// System.Collections.Generic.Dictionary<ET.Ability.AbilityAoeMonitorTriggerEvent,object>
	// System.Collections.Generic.Dictionary<ET.Client.PanelId,ET.Client.PanelInfo>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.HashSet<ushort>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSet<long>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet.Enumerator<long>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<float,object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.KeyValuePair<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.List<ET.Client.PanelId>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.List<ET.Ability.TeamFlagType>
	// System.Collections.Generic.List<Unity.Mathematics.float3>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary<object,object>
	// System.Collections.Generic.SortedDictionary<int,object>
	// System.Collections.Generic.SortedDictionary<float,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedDictionary<ET.Ability.SkillSlotType,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<float,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<ET.Ability.AbilityBuffMonitorTriggerEvent,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<object,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Stack<ET.Client.PanelId>
	// System.Comparison<object>
	// System.Func<object>
	// System.Func<object,object>
	// System.Func<object,object,object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.Task<System.ValueTuple<uint,uint>>
	// System.ValueTuple<uint,uint>
	// System.ValueTuple<uint,object>
	// System.ValueTuple<byte,object>
	// System.ValueTuple<object,object>
	// }}

	public void RefMethods()
	{
		// string Bright.Common.StringUtil.CollectionToString<int>(System.Collections.Generic.IEnumerable<int>)
		// string Bright.Common.StringUtil.CollectionToString<object>(System.Collections.Generic.IEnumerable<object>)
		// object ET.Entity.AddChild<object,object>(object,bool)
		// object ET.Entity.AddChild<object>(bool)
		// object ET.Entity.AddChildWithId<object,int>(long,int,bool)
		// object ET.Entity.AddChildWithId<object,object>(long,object,bool)
		// object ET.Entity.AddChildWithId<object>(long,bool)
		// object ET.Entity.AddComponent<object,System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily,bool)
		// object ET.Entity.AddComponent<object,object,int>(object,int,bool)
		// object ET.Entity.AddComponent<object,int>(int,bool)
		// object ET.Entity.AddComponent<object,int,Unity.Mathematics.float3>(int,Unity.Mathematics.float3,bool)
		// object ET.Entity.AddComponent<object,ET.Ability.TeamFlagType>(ET.Ability.TeamFlagType,bool)
		// object ET.Entity.AddComponent<object>(bool)
		// object ET.Entity.AddComponent<object,object>(object,bool)
		// object ET.Entity.GetChild<object>(long)
		// object ET.Entity.GetComponent<object>()
		// object ET.Entity.GetParent<object>()
		// System.Void ET.Entity.RemoveComponent<object>()
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,object>(System.Runtime.CompilerServices.TaskAwaiter<object>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,object>(ET.ETTaskCompleted&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>,object>(System.Runtime.CompilerServices.TaskAwaiter<System.ValueTuple<uint,uint>>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,object>(System.Runtime.CompilerServices.TaskAwaiter<object>&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<uint>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<uint,object>>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_SceneChangeFinish>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_UnitStop>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.Client.Wait_CreateMyUnit>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<UnityEngine.SceneManagement.Scene>.Start<object>(object&)
		// System.Void ET.EventSystem.Invoke<ET.SyncUnits>(ET.SyncUnits)
		// object ET.EventSystem.Invoke<ET.NavmeshComponent.RecastFileLoader,object>(ET.NavmeshComponent.RecastFileLoader)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.EnterMapFinish>(object,ET.EventType.EnterMapFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStop>(object,ET.EventType.MoveByPathStop)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownlodFailed>(object,ET.EventType.OnPatchDownlodFailed)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SwitchLanguage>(object,ET.EventType.SwitchLanguage)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SceneChangeStart>(object,ET.EventType.SceneChangeStart)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.SceneChangeFinish>(object,ET.EventType.SceneChangeFinish)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.MoveByPathStart>(object,ET.EventType.MoveByPathStart)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.OnPatchDownloadProgress>(object,ET.EventType.OnPatchDownloadProgress)
		// System.Void ET.EventSystem.Publish<object,ET.Client.NetClientComponentOnRead>(object,ET.Client.NetClientComponentOnRead)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnCreate>(object,ET.Ability.AbilityTriggerEventType.UnitOnCreate)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved>(object,ET.Ability.AbilityTriggerEventType.UnitOnRemoved)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitEnterSightRange>(object,ET.EventType.UnitEnterSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BuffOnAwake>(object,ET.Ability.AbilityTriggerEventType.BuffOnAwake)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BuffOnStart>(object,ET.Ability.AbilityTriggerEventType.BuffOnStart)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BuffOnDestroy>(object,ET.Ability.AbilityTriggerEventType.BuffOnDestroy)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.BuffOnRemoved>(object,ET.Ability.AbilityTriggerEventType.BuffOnRemoved)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterUnitCreate>(object,ET.EventType.AfterUnitCreate)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.UnitOnHit>(object,ET.Ability.AbilityTriggerEventType.UnitOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnHit)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.UnitLeaveSightRange>(object,ET.EventType.UnitLeaveSightRange)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageAfterOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.SkillOnCast>(object,ET.Ability.AbilityTriggerEventType.SkillOnCast)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangeRotation>(object,ET.EventType.ChangeRotation)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.AfterCreateCurrentScene>(object,ET.EventType.AfterCreateCurrentScene)
		// System.Void ET.EventSystem.Publish<object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill>(object,ET.Ability.AbilityTriggerEventType.DamageBeforeOnKill)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.NumbericChange>(object,ET.EventType.NumbericChange)
		// System.Void ET.EventSystem.Publish<object,ET.EventType.ChangePosition>(object,ET.EventType.ChangePosition)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent2>(object,ET.EventType.EntryEvent2)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.AfterCreateClientScene>(object,ET.EventType.AfterCreateClientScene)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent1>(object,ET.EventType.EntryEvent1)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.EntryEvent3>(object,ET.EventType.EntryEvent3)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EventType.LoginFinish>(object,ET.EventType.LoginFinish)
		// object ET.Game.AddSingleton<object>()
		// object ET.JsonHelper.FromJson<object>(string)
		// object ET.MongoHelper.Deserialize<object>(byte[])
		// System.Void ET.ObjectHelper.Swap<object>(object&,object&)
		// System.Void ET.RandomGenerator.BreakRank<object>(System.Collections.Generic.List<object>)
		// string ET.StringHelper.ArrayToString<float>(float[])
		// ET.Client.Wait_SceneChangeFinish System.Activator.CreateInstance<ET.Client.Wait_SceneChangeFinish>()
		// ET.Client.Wait_CreateMyUnit System.Activator.CreateInstance<ET.Client.Wait_CreateMyUnit>()
		// ET.Client.Wait_UnitStop System.Activator.CreateInstance<ET.Client.Wait_UnitStop>()
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
		// ET.RpcInfo[] System.Linq.Enumerable.ToArray<ET.RpcInfo>(System.Collections.Generic.IEnumerable<ET.RpcInfo>)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.TaskFactory.StartNew<object>(System.Func<object>,System.Threading.CancellationToken)
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// object YooAsset.AssetOperationHandle.GetAssetObject<object>()
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetAsync<object>(string)
		// YooAsset.AssetOperationHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
	}
}