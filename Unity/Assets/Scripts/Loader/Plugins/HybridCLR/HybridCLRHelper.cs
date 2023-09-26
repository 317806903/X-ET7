using System.Collections.Generic;
using HybridCLR;
using UnityEngine;

namespace ET
{
    public static class HybridCLRHelper
    {
        public static void Load()
        {
            //HybridCLRSettings.Instance.patchAOTAssemblies
            string[] addresses = MonoResComponent.Instance.GetAddressesByTag("aotdlls");
            foreach (string address in addresses)
            {
                byte[] bytes = MonoResComponent.Instance.LoadRawFile(address);
                Log.Debug($"ET.HybridCLRHelper.Load {address} before LoadMetadataForAOTAssembly");
                //RuntimeApi.LoadMetadataForAOTAssembly(bytes, HomologousImageMode.Consistent);
                RuntimeApi.LoadMetadataForAOTAssembly(bytes, HomologousImageMode.SuperSet);
                Log.Debug($"ET.HybridCLRHelper.Load {address} after LoadMetadataForAOTAssembly");
            }
        }
    }
}