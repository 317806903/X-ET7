﻿using System;
using CommandLine;

namespace ET
{
	public static class Init
	{
		public static void Start()
		{
			try
			{	
				AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
				{
					Log.Error(e.ExceptionObject.ToString());
				};
				
				// 异步方法全部会回掉到主线程
				Game.AddSingleton<MainThreadSynchronizationContext>();

				// 命令行参数
				Parser.Default.ParseArguments<Options>(System.Environment.GetCommandLineArgs())
					.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
					.WithParsed(Game.AddSingleton);
				
				Game.AddSingleton<TimeInfo>();
				Game.AddSingleton<Logger>().ILog = new NLogger(Options.Instance.AppType.ToString(), Options.Instance.Process, "../Config/NLog/NLog.config");
				Game.AddSingleton<ObjectPool>();
				Game.AddSingleton<IdGenerater>();
				Game.AddSingleton<EventSystem>();
				Game.AddSingleton<TimerComponent>();
				Game.AddSingleton<CoroutineLockComponent>();
				
				ETTask.ExceptionHandler += Log.Error;
				
				Log.Console($"{Parser.Default.FormatCommandLine(Options.Instance)}");

				Game.AddSingleton<CodeLoader>().Start();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void Update()
		{
			Game.Update();
			ChkFixedUpdate();
		}

		static long lastChkTimer = 0;
		static long logicTimer = 0;
		private static void ChkFixedUpdate()
		{
			if (lastChkTimer > 0)
			{
				logicTimer += TimeHelper.ClientFrameTime() - lastChkTimer;
			}
			lastChkTimer = TimeHelper.ClientFrameTime();
			while (logicTimer >= TimeHelper.FixedDetalTimeLong)
            {     
                logicTimer -= TimeHelper.FixedDetalTimeLong;

				FixedUpdate();
			}
		}

		public static void FixedUpdate()
		{
			Game.FixedUpdate();
		}

		public static void LateUpdate()
		{
			Game.LateUpdate();
		}

		public static void FrameFinishUpdate()
		{
			Game.FrameFinishUpdate();
		}
	}
}
