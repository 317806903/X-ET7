using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using YooAsset;

namespace ET
{
	public class CodeLoader: Singleton<CodeLoader>
	{
		private Assembly model;

		public void Start()
		{
			Log.Debug($"ET.CodeLoader.Start In]");
			if (Define.EnableCodes)
			{
				if (GlobalConfig.Instance.CodeMode != CodeMode.ClientServer)
				{
					//throw new Exception("ENABLE_CODES mode must use ClientServer code mode!");
				}

				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				Dictionary<string, Type> types = AssemblyHelper.GetAssemblyTypes(assemblies);
				EventSystem.Instance.Add(types);
				foreach (Assembly ass in assemblies)
				{
					string name = ass.GetName().Name;
					if (name == "Unity.Model.Codes")
					{
						this.model = ass;
					}
				}
			}
			else
			{
				Log.Debug($"---LoadRawFile Model_{GlobalConfig.Instance.ModelVersion}.dll");
				byte[] assBytes = MonoResComponent.Instance.LoadRawFile($"Model_{GlobalConfig.Instance.ModelVersion}.dll");
#if DEVELOPMENT_BUILD || UNITY_EDITOR
				Log.Debug($"---LoadRawFile Model_{GlobalConfig.Instance.ModelVersion}.pdb");
				byte[] pdbBytes = MonoResComponent.Instance.LoadRawFile($"Model_{GlobalConfig.Instance.ModelVersion}.pdb");
#else
				byte[] pdbBytes = null;
#endif

				if (!Define.IsEditor)
				{
					if (Define.EnableIL2CPP)
					{
						HybridCLRHelper.Load();
					}
				}

				Log.Debug($"---CodeLoader---Model_{GlobalConfig.Instance.ModelVersion}.dll pdbBytes==null[{pdbBytes == null}]");
				this.model = Assembly.Load(assBytes, pdbBytes);
				this.LoadHotfix();
			}

			Log.Debug($"ET.Entry.Start Before]");
			IStaticMethod start = new StaticMethod(this.model, "ET.Entry", "Start");
			start.Run();
		}

		// 热重载调用该方法
		public void LoadHotfix()
		{
			Log.Debug($"---LoadRawFile Hotfix_{GlobalConfig.Instance.HotFixVersion}.dll");
			byte[] assBytes = MonoResComponent.Instance.LoadRawFile($"Hotfix_{GlobalConfig.Instance.HotFixVersion}.dll");
#if DEVELOPMENT_BUILD || UNITY_EDITOR
			Log.Debug($"---LoadRawFile Hotfix_{GlobalConfig.Instance.HotFixVersion}.pdb");
			byte[] pdbBytes = MonoResComponent.Instance.LoadRawFile($"Hotfix_{GlobalConfig.Instance.HotFixVersion}.pdb");
#else
			byte[] pdbBytes = null;
#endif

			Log.Debug($"---CodeLoader---Hotfix_{GlobalConfig.Instance.HotFixVersion}.dll pdbBytes==null[{pdbBytes == null}]");
			Assembly hotfixAssembly = Assembly.Load(assBytes, pdbBytes);

			Log.Debug($"Assembly.Load finished");
			Dictionary<string, Type> types = AssemblyHelper.GetAssemblyTypes(typeof (Game).Assembly, typeof(Init).Assembly, this.model, hotfixAssembly);
			Log.Debug($"AssemblyHelper.GetAssemblyTypes finished");

			EventSystem.Instance.Add(types);
		}
	}
}