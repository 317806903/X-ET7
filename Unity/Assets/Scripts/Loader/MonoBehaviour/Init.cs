using System;
using System.Collections;
using System.Threading.Tasks;
using CommandLine;
using UnityEngine;

namespace ET
{
	public class Init: MonoBehaviour
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
			GlobalConfig.Instance = UnityEditor.AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Bundles/Config/GlobalConfig/GlobalConfig.asset");
#endif
			// 命令行参数
			string[] args = $"--StartConfig=StartConfig/{GlobalConfig.Instance.StartConfig}".Split(" ");
			Parser.Default.ParseArguments<Options>(args)
					.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
					.WithParsed(Game.AddSingleton);

			Game.AddSingleton<ET.CodeLoader>().Start();

#if UNITY_EDITOR

			while (Game.ChkIsExistSingleton<ConfigComponent>() == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}
			ConfigComponent configComponent = Game.GetExistSingleton<ConfigComponent>();
			while (configComponent.ChkFinishLoad() == false)
			{
				await TimerComponent.Instance.WaitFrameAsync();
			}

			(string RouterHttpHost, int RouterHttpPort) = EventSystem.Instance.Invoke<ConfigComponent.GetRouterHttpHostAndPortWhenEditor, (string, int)>(new ConfigComponent.GetRouterHttpHostAndPortWhenEditor());
			ResConfig.Instance.RouterHttpHost = RouterHttpHost;
			ResConfig.Instance.RouterHttpPort = RouterHttpPort;
#endif

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