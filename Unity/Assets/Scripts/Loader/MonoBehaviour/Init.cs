using System;
using System.Collections;
using System.Threading.Tasks;
using CommandLine;
using UnityEngine;

namespace ET
{
	public class Init : MonoBehaviour
	{
		private void Start()
		{
			this._Start().Coroutine();
		}

		private async ETTask _Start()
		{
			DontDestroyOnLoad(gameObject);

			AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
			{
				Log.Error(e.ExceptionObject.ToString());
			};

			Game.AddSingleton<MainThreadSynchronizationContext>();

			Game.AddSingleton<TimeInfo>();
			Game.AddSingleton<Logger>().ILog = new UnityLogger();
			Game.AddSingleton<ObjectPool>();
			Game.AddSingleton<IdGenerater>();
			Game.AddSingleton<EventSystem>();
			Game.AddSingleton<TimerComponent>();
			Game.AddSingleton<CoroutineLockComponent>();

			ETTask.ExceptionHandler += Log.Error;

			await MonoResComponent.Instance.InitAsync();

#if UNITY_EDITOR
			var modelVersion = GlobalConfig.Instance.ModelVersion;
			var hotFixVersion = GlobalConfig.Instance.HotFixVersion;
			GlobalConfig.Instance = UnityEditor.AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
			GlobalConfig.Instance.ModelVersion = modelVersion;
			GlobalConfig.Instance.HotFixVersion = hotFixVersion;
#endif
			// 命令行参数
			string[] args = $"--StartConfig=StartConfig/{GlobalConfig.Instance.StartConfig}".Split(" ");
			Parser.Default.ParseArguments<Options>(args)
					.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
					.WithParsed(Game.AddSingleton);

#if UNITY_EDITOR
			Options.Instance.NeedDB = GlobalConfig.Instance.NeedDB?1:0;
#endif

			Game.AddSingleton<ET.CodeLoader>().Start();

// #if UNITY_EDITOR
// 			while (Game.ChkIsExistSingleton<ConfigComponent>() == false)
// 			{
// 				await TimerComponent.Instance.WaitFrameAsync();
// 			}
// 			ConfigComponent configComponent = Game.GetExistSingleton<ConfigComponent>();
// 			while (configComponent.ChkFinishLoad() == false)
// 			{
// 				await TimerComponent.Instance.WaitFrameAsync();
// 			}
// #endif

		}

		public async ETTask Restart()
		{
			Log.Info("-------Restart!");

			Game.Close();

			Game.AddSingleton<MainThreadSynchronizationContext>();

			// 命令行参数
			string[] args = $"--StartConfig=StartConfig/{GlobalConfig.Instance.StartConfig}".Split(" ");
			Parser.Default.ParseArguments<Options>(args)
					.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
					.WithParsed(Game.AddSingleton);

			Game.AddSingleton<TimeInfo>();
			Game.AddSingleton<Logger>().ILog = new UnityLogger();
			Game.AddSingleton<ObjectPool>();
			Game.AddSingleton<IdGenerater>();
			Game.AddSingleton<EventSystem>();
			Game.AddSingleton<TimerComponent>();
			Game.AddSingleton<CoroutineLockComponent>();

			await MonoResComponent.Instance.InitAsync();

			Log.Debug($"Restart CodeLoader.Start Before]");
			Game.AddSingleton<ET.CodeLoader>().Start();
		}

		private void Update()
		{
			Game.Update();
			//Log.Debug($"Update {TimeHelper.ClientFrameTime()}");
			this.ChkFixedUpdate();
		}

		protected long lastChkTimer = 0;
		protected long logicTimer = 0;
		private void ChkFixedUpdate()
		{
			//Log.Debug($"....11 logicTimer={logicTimer}");
			if (lastChkTimer > 0)
			{
				logicTimer += TimeHelper.ClientFrameTime() - lastChkTimer;
			}
			//Log.Debug($"....22 logicTimer={logicTimer}");
			lastChkTimer = TimeHelper.ClientFrameTime();
			while (logicTimer >= TimeHelper.FixedDetalTimeLong)
			{
				logicTimer -= TimeHelper.FixedDetalTimeLong;

				this.DoFixedUpdate();
			}
		}

		private void DoFixedUpdate()
		{
			//Log.Debug($"====FixedUpdate {TimeHelper.ClientFrameTime()}");
			Game.FixedUpdate();
		}

		private void LateUpdate()
		{
			Game.LateUpdate();
			Game.FrameFinishUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Close();
		}
	}
}