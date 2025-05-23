﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace YooAsset
{
	internal class EditorSimulateModeImpl : IPlayModeServices, IBundleServices
	{
		private PackageManifest _activeManifest;
		private bool _locationToLower;

		/// <summary>
		/// 异步初始化
		/// </summary>
		public InitializationOperation InitializeAsync(bool locationToLower, string simulateManifestFilePath)
		{
			_locationToLower = locationToLower;
			var operation = new EditorSimulateModeInitializationOperation(this, simulateManifestFilePath);
			OperationSystem.StartOperation(operation);
			return operation;
		}

		#region IPlayModeServices接口
		public PackageManifest ActiveManifest
		{
			set
			{
				_activeManifest = value;
				_activeManifest.InitAssetPathMapping(_locationToLower);
			}
			get
			{
				return _activeManifest;
			}
		}

		public void FlushManifestVersionFile()
		{
		}

		public void ReLoadMap()
		{
			_activeManifest.ResetInitAssetPathMapping();
			_activeManifest.InitAssetPathMapping(_locationToLower);
		}

		UpdatePackageVersionOperation IPlayModeServices.UpdatePackageVersionAsync(bool appendTimeTicks, int timeout)
		{
			var operation = new EditorPlayModeUpdatePackageVersionOperation();
			OperationSystem.StartOperation(operation);
			return operation;
		}
		UpdatePackageManifestOperation IPlayModeServices.UpdatePackageManifestAsync(string packageVersion, bool autoSaveVersion, int timeout)
		{
			var operation = new EditorPlayModeUpdatePackageManifestOperation();
			OperationSystem.StartOperation(operation);
			return operation;
		}
		PreDownloadContentOperation IPlayModeServices.PreDownloadContentAsync(string packageVersion, int timeout)
		{
			var operation = new EditorPlayModePreDownloadContentOperation();
			OperationSystem.StartOperation(operation);
			return operation;
		}

		ResourceDownloaderOperation IPlayModeServices.CreateResourceDownloaderByAll(int downloadingMaxNumber, int failedTryAgain, int timeout)
		{
			return ResourceDownloaderOperation.CreateEmptyDownloader(downloadingMaxNumber, failedTryAgain, timeout);
		}
		ResourceDownloaderOperation IPlayModeServices.CreateResourceDownloaderByTags(string[] tags, int downloadingMaxNumber, int failedTryAgain, int timeout)
		{
			return ResourceDownloaderOperation.CreateEmptyDownloader(downloadingMaxNumber, failedTryAgain, timeout);
		}
		ResourceDownloaderOperation IPlayModeServices.CreateResourceDownloaderByPaths(AssetInfo[] assetInfos, int downloadingMaxNumber, int failedTryAgain, int timeout)
		{
			return ResourceDownloaderOperation.CreateEmptyDownloader(downloadingMaxNumber, failedTryAgain, timeout);
		}

		ResourceUnpackerOperation IPlayModeServices.CreateResourceUnpackerByAll(int upackingMaxNumber, int failedTryAgain, int timeout)
		{
			return ResourceUnpackerOperation.CreateEmptyUnpacker(upackingMaxNumber, failedTryAgain, timeout);
		}
		ResourceUnpackerOperation IPlayModeServices.CreateResourceUnpackerByTags(string[] tags, int upackingMaxNumber, int failedTryAgain, int timeout)
		{
			return ResourceUnpackerOperation.CreateEmptyUnpacker(upackingMaxNumber, failedTryAgain, timeout);
		}
		#endregion

		#region IBundleServices接口
		private BundleInfo CreateBundleInfo(PackageBundle packageBundle, AssetInfo assetInfo)
		{
			if (packageBundle == null)
				throw new Exception("Should never get here !");

			BundleInfo bundleInfo = new BundleInfo(packageBundle, BundleInfo.ELoadMode.LoadFromEditor, assetInfo.AssetPath);
			return bundleInfo;
		}
		BundleInfo IBundleServices.GetBundleInfo(AssetInfo assetInfo)
		{
			if (assetInfo.IsInvalid)
				throw new Exception("Should never get here !");

			// 注意：如果清单里未找到资源包会抛出异常！
			var packageBundle = _activeManifest.GetMainPackageBundle(assetInfo.AssetPath);
			return CreateBundleInfo(packageBundle, assetInfo);
		}
		BundleInfo[] IBundleServices.GetAllDependBundleInfos(AssetInfo assetInfo)
		{
			if (assetInfo.IsInvalid)
				throw new Exception("Should never get here !");

			// 注意：如果清单里未找到资源包会抛出异常！
			var depends = _activeManifest.GetAllDependencies(assetInfo.AssetPath);
			List<BundleInfo> result = new List<BundleInfo>(depends.Length);
			foreach (var packageBundle in depends)
			{
				BundleInfo bundleInfo = CreateBundleInfo(packageBundle, assetInfo);
				result.Add(bundleInfo);
			}
			return result.ToArray();
		}
		string IBundleServices.GetBundleName(int bundleID)
		{
			return _activeManifest.GetBundleName(bundleID);
		}
		bool IBundleServices.IsServicesValid()
		{
			return _activeManifest != null;
		}
		#endregion
	}
}