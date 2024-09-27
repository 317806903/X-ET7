using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine.Bindings;

namespace TATools.FBXFormatSetting
{
    public class FBXImportSettingTemplate
    {
        public ModelImporter fbxImporter { get; set; }

        public FBXImportSettingTemplate(ModelImporter fbxImporter)
        {
            this.fbxImporter = fbxImporter;
        }
    }

    public class FBXImportSettingTemplateHelper
    {
        public FBXImportSettingTemplate template;

        public FBXImportSettingTemplateHelper(FBXCatalog fbxCatalog, ModelImporter modelImporter)
        {
            this.template = GetImportSettingTemplate(fbxCatalog, modelImporter);
        }

        FBXImportSettingTemplate GetImportSettingTemplate(FBXCatalog fbxCatalog, ModelImporter fbxImporter)
        {
            switch (fbxCatalog)
            {
                case FBXCatalog.NotMatch:
                    return null;
                case FBXCatalog.NotDeal:
                    return null;
            }

            string path = fbxImporter.assetPath;
            FBXImportSettingTemplate temp = new FBXImportSettingTemplate(fbxImporter);

            this.SetDefaultFBXSetting(path, fbxImporter);
            switch (fbxCatalog)
            {
                case FBXCatalog.Animation:
                    this.SetAnimationFBX(path, fbxImporter);
                    break;
                case FBXCatalog.Character:break;
                case FBXCatalog.EffectUI:break;
                case FBXCatalog.EffectScene:break;
                case FBXCatalog.Item:break;
                case FBXCatalog.Scene:
                    this.SetAniDefaultFBX(path, fbxImporter);
                    break;
            }

            return temp;
        }

        //通用的低配设置
        private void SetDefaultFBXSetting(string path, ModelImporter modelImporter)
        {
            //model
            modelImporter.globalScale = 1;
            modelImporter.useFileScale = true;
            modelImporter.meshCompression = ModelImporterMeshCompression.Low;
            modelImporter.isReadable = false;
            modelImporter.optimizeMesh = true;
            modelImporter.importBlendShapes = false;
            modelImporter.addCollider = false;
            modelImporter.keepQuads = false;
            modelImporter.indexFormat = ModelImporterIndexFormat.UInt16;

            modelImporter.generateSecondaryUV = false;
            modelImporter.swapUVChannels = false;
            modelImporter.weldVertices = true;

            modelImporter.importVisibility = false;
            modelImporter.importCameras = false;
            modelImporter.importLights = false;

            modelImporter.preserveHierarchy = false;
            modelImporter.swapUVChannels = false;

            modelImporter.importNormals = ModelImporterNormals.Import;
            modelImporter.normalCalculationMode = ModelImporterNormalCalculationMode.AreaAndAngleWeighted;
            modelImporter.normalSmoothingSource = ModelImporterNormalSmoothingSource.PreferSmoothingGroups;
            modelImporter.normalSmoothingAngle = 60;
            modelImporter.importTangents = ModelImporterTangents.CalculateLegacy;

            //material
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            RemoveMaterials(modelImporter);
            //animation
            modelImporter.animationType = ModelImporterAnimationType.None;
            modelImporter.importConstraints = false;
            modelImporter.importAnimation = false;
        }

        //通用的动画低配设置
        protected void SetAniDefaultFBX(string path, ModelImporter modelImporter)
        {
            modelImporter.animationType = ModelImporterAnimationType.None;
            modelImporter.importConstraints = false;
            modelImporter.importAnimation = false;
        }

        protected void SetModelSkin(string path, ModelImporter modelImporter)
        {
            modelImporter.animationType = ModelImporterAnimationType.Generic;
            modelImporter.optimizeGameObjects = true;

            modelImporter.importAnimation = false;
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            RemoveMaterials(modelImporter);
        }

        protected void SetAnimationFBX(string path, ModelImporter modelImporter)
        {
            modelImporter.animationType = ModelImporterAnimationType.Generic;
            modelImporter.importAnimation = true;
            modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            RemoveMaterials(modelImporter);

            modelImporter.resampleCurves = true;
            modelImporter.animationCompression = ModelImporterAnimationCompression.Optimal;
            //if (!isErrorAdjustFilterPath(path))
            {
                modelImporter.animationPositionError = 0.5f;
                modelImporter.animationRotationError = 0.5f;
                modelImporter.animationScaleError = 1.0f;
            }
        }

        /// <summary>
        /// 移除掉fbx的所有材质引用
        /// </summary>
        /// <param name="modelImporter"></param>
        protected void RemoveMaterials(ModelImporter modelImporter)
        {
            foreach (var item in modelImporter.GetExternalObjectMap())
            {
                if (item.Key.type == typeof(Material))
                {
                    modelImporter.RemoveRemap(item.Key);
                }
            }
        }

        //------------------------------------

        // public static bool IsFbxFirstImport(string path)
        // {
        //     GameObject go = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
        //     if (null == go) return true;
        //     return false;
        // }
        //
        // public static bool isSkinModelFBX(string path)
        // {
        //     if (!path.ToLower().Contains(".fbx")) return false;
        //     GameObject go = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
        //     if (go == null) return false;
        //     Transform root = go.transform.Find("Root");
        //     Transform bipRoot = go.transform.Find("Bip_Root");
        //
        //     bool bHasRoot = root != null || bipRoot != null;
        //
        //     if (bHasRoot) return true;
        //
        //     return false;
        // }
        //
        // public static bool isSkinModelFBXEX(string path)
        // {
        //     if (!path.ToLower().Contains(".fbx")) return false;
        //     GameObject go = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
        //     if (go == null) return false;
        //     bool bContainRoot = false;
        //     foreach(Transform item  in go.transform)
        //     {
        //         string name = item.name;
        //         if (name.Contains("Root") || name.Contains("root")) bContainRoot = true;
        //     }
        //
        //     Transform root = go.transform.Find("Root");
        //     Transform bipRoot = go.transform.Find("Bip_Root");
        //
        //     bool bHasRoot = root != null || bipRoot != null || bContainRoot;
        //
        //     if (bHasRoot) return true;
        //
        //     return false;
        // }
        //
        // public static bool isEnviromentAni(string path)
        // {
        //     if (!path.ToLower().Contains(".fbx")) return false;
        //     GameObject go = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
        //     if (go == null) return false;
        //     bool bIsAni = false;
        //     foreach (Transform item in go.transform)
        //     {
        //         string name = item.name;
        //         if (name.Contains("Root")|| name.Contains("Bone")) bIsAni = true;
        //     }
        //
        //
        //     if (bIsAni) return true;
        //
        //     return false;
        //
        // }
        //
        // //动画导出带了模型，这个一般会放在路径Animation的文件夹下，如果不是则不符合规范
        // public static bool isAnimationModelFBX(string path)
        // {
        //     if (path.Contains("/Animation")) return true;
        //
        //     return false;
        // }
        //------------------------------------
    }
}