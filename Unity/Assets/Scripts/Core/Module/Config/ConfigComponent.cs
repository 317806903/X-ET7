﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bright.Serialization;

namespace ET
{
	/// <summary>
    /// Config组件会扫描所有的有ConfigAttribute标签的配置,加载进来
    /// </summary>
    public class ConfigComponent: Singleton<ConfigComponent>
    {
	    public struct GetCodeMode
	    {
	    }

	    public struct GetLocalDBSavePath
	    {
	    }

	    public struct GetLocalMeshSavePath
	    {
	    }

        public struct GetAllConfigBytes
        {
        }

        public struct GetOneConfigBytes
        {
            public string ConfigName;
            public string ConfigFullName;
        }

        public struct GetRes
        {
            public string ResName;
        }

        public struct GetRouterHttpHostAndPort
        {
        }

        private readonly object lockObj = new object();
        private readonly Dictionary<string, IConfigSingleton> allConfig = new Dictionary<string, IConfigSingleton>(20);

        private bool isFinishLoad;

		public override void Dispose()
		{
			foreach (var kv in this.allConfig)
			{
				kv.Value.Destroy();
			}
		}

		public bool ChkFinishLoad()
		{
			return this.isFinishLoad;
		}

		public object LoadOneConfig(Type configType)
		{
			object category;
			lock (lockObj)
			{
				this.allConfig.TryGetValue(configType.Name, out IConfigSingleton oneConfig);
				if (oneConfig != null)
				{
					oneConfig.Destroy();
				}

				ByteBuf oneConfigBytes = EventSystem.Instance.Invoke<GetOneConfigBytes, ByteBuf>(new GetOneConfigBytes()
				{
					ConfigName = configType.Name,
					ConfigFullName = configType.FullName,
				});

				category = Activator.CreateInstance(configType, oneConfigBytes);
				IConfigSingleton singleton = category as IConfigSingleton;
				singleton.Register();

				this.allConfig[configType.Name] = singleton;
			}
			return category;
		}

		public void Load()
		{
			this.isFinishLoad = false;
			foreach (var configSingleton in this.allConfig)
			{
				configSingleton.Value.Destroy();
			}
			this.allConfig.Clear();
			Dictionary<Type, ByteBuf> configBytes = EventSystem.Instance.Invoke<GetAllConfigBytes, Dictionary<Type, ByteBuf>>(new GetAllConfigBytes());

			foreach (Type type in configBytes.Keys)
			{
				ByteBuf oneConfigBytes = configBytes[type];
				this.LoadOneInThread(type, oneConfigBytes);
			}

			foreach (IConfigSingleton category in this.allConfig.Values)
			{
				category.Register();
				category.Resolve(allConfig);
			}
			this.isFinishLoad = true;
		}

		public async ETTask LoadAsync(Action<float> process)
		{
			Dictionary<Type, ByteBuf> configBytes;
			lock (lockObj)
			{
				this.isFinishLoad = false;
				foreach (var configSingleton in this.allConfig)
				{
					configSingleton.Value.Destroy();
				}
				this.allConfig.Clear();
				configBytes = EventSystem.Instance.Invoke<GetAllConfigBytes, Dictionary<Type, ByteBuf>>(new GetAllConfigBytes());

			}

			using ListComponent<Task> listTasks = ListComponent<Task>.Create();

			foreach (Type type in configBytes.Keys)
			{
				ByteBuf oneConfigBytes = configBytes[type];
				Task task = Task.Run(() => LoadOneInThread(type, oneConfigBytes));
				listTasks.Add(task);
			}

			while (process != null)
			{
				int total = listTasks.Count;
				int finishNum = 0;
				foreach (Task task in listTasks)
				{
					if (task.IsCompleted)
					{
						finishNum++;
					}
				}

				if (finishNum == total)
				{
					break;
				}
				process?.Invoke((float)finishNum / total);
			}
			await Task.WhenAll(listTasks.ToArray());

			foreach (IConfigSingleton category in this.allConfig.Values)
			{
				category.Register();
			}

			foreach (IConfigSingleton category in this.allConfig.Values)
			{
				category.Resolve(allConfig);
			}
			this.isFinishLoad = true;
		}

		private void LoadOneInThread(Type configType, ByteBuf oneConfigBytes)
		{
			object category = Activator.CreateInstance(configType, oneConfigBytes);
			lock (this)
			{
				this.allConfig[configType.Name] = category as IConfigSingleton;
			}
		}

		public void TranslateText(Func<string, string, string> translator)
		{
			foreach (IConfigSingleton category in this.allConfig.Values)
			{
				category.TranslateText(translator);
			}
		}
	}
}