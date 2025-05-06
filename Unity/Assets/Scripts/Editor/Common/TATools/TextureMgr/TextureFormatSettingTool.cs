using System;
using System.Linq;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace TATools.TextureFormatSetting
{
    public class TextureFormatSettingTool : EditorWindow
    {
        private static TextureFormatSettingTool window;
        static readonly string OutFilePath = @"TAToolChkReport";

        [MenuItem("TATools/贴图格式设置/查看贴图格式设置窗口")]
        public static void GetWindow()
        {
            window = GetWindow<TextureFormatSettingTool>("贴图格式设置查看", true);
        }

        private bool getFormatFromTexture = false;
        private bool getFormatFromTextureCatalog = false;

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            //GUILayout.Label("贴图格式设置");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("根据贴图查询格式设置", GUILayout.Width(150)))
            {
                getFormatFromTexture = !getFormatFromTexture;
                if (getFormatFromTexture && getFormatFromTextureCatalog)
                {
                    getFormatFromTextureCatalog = false;
                }
            }

            if (GUILayout.Button("根据分类查询格式设置", GUILayout.Width(150)))
            {
                getFormatFromTextureCatalog = !getFormatFromTextureCatalog;
                if (getFormatFromTexture && getFormatFromTextureCatalog)
                {
                    getFormatFromTexture = false;
                }
            }


            EditorGUILayout.EndHorizontal();
            if (getFormatFromTexture)
            {
                DisplayGetTextureFormatFromTexture();
            }

            if (getFormatFromTextureCatalog)
            {
                DisplayGetTextureFormatFromTextureCatalog();
            }

            EditorGUILayout.EndVertical();
        }

        private static string defaultTexPath = "Assets/TATools/test.png";
        public static string texturePath;
        public static bool isTextureHasAlpha;
        private Texture defaultTex;
        private bool isDisplayTextureCatalogAndFormat = false;
        private TextureCatalog defaultTextureCaltalog = TextureCatalog.CommonRGB_PNG;

        private void Awake()
        {
            //defaultTex = AssetDatabase.LoadAssetAtPath<Texture>(defaultTexPath);
        }

        private void DisplayGetTextureFormatFromTexture()
        {
            EditorGUILayout.BeginVertical();
            defaultTex = EditorGUILayout.ObjectField(defaultTex, typeof(UnityEngine.Texture), true, GUILayout.Height(100), GUILayout.Width(100)) as Texture;
            if (defaultTex != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("贴图尺寸：", GUILayout.Width(100));
                GUILayout.Label(defaultTex.width + " x " + defaultTex.height, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();
                string texPath = AssetDatabase.GetAssetPath(defaultTex);
                DisplayTextureCatalogAndFormat(texPath);
            }
            else
            {
                GUILayout.Label("贴图不存在", GUILayout.Width(100));
            }


            EditorGUILayout.EndVertical();
        }

        private void DisplayGetTextureFormatFromTextureCatalog()
        {
            EditorGUILayout.BeginVertical();
            TextureImporter textureImporter = AssetImporter.GetAtPath(defaultTexPath) as TextureImporter;

            defaultTextureCaltalog = (TextureCatalog) EditorGUILayout.Popup("贴图分类", (int) defaultTextureCaltalog, Enum.GetNames(typeof(TextureCatalog)));
            string format = DisplayTextureFormat(defaultTextureCaltalog, 0, textureImporter);
            GUILayout.TextArea(format);
            EditorGUILayout.EndVertical();
        }

        private void DisplayTextureCatalogAndFormat(string texPath)
        {
            // EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("贴图资源路径：", GUILayout.Width(100));
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(texPath, style, GUILayout.Width(500));
            EditorGUILayout.EndHorizontal();

            TextureImporter textureImporter = AssetImporter.GetAtPath(texPath) as TextureImporter;
            if (textureImporter != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Alpha通道：", GUILayout.Width(100));

                isTextureHasAlpha = textureImporter.DoesSourceTextureHaveAlpha();
                if (isTextureHasAlpha)
                {
                    GUILayout.Label("贴图有Alpha通道", style, GUILayout.Width(150));
                }
                else
                {
                    GUILayout.Label("贴图没有Alpha通道", style, GUILayout.Width(150));
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                string fileName = Path.GetFileName(texPath);
                TextureCatalogHelper textureCatalogHelper = new TextureCatalogHelper(texPath, textureImporter);
                TextureCatalog textureCatalog = textureCatalogHelper.textureCatalog;
                GUILayout.Label("贴图分类", GUILayout.Width(100));
                GUILayout.Label(textureCatalog.ToString(), style, GUILayout.Width(150));
                EditorGUILayout.EndHorizontal();

                int maxSize = textureCatalogHelper.maxSize;
                string maxSizeErr = textureCatalogHelper.maxSizeErr;
                if (string.IsNullOrEmpty(maxSizeErr) == false)
                {
                    EditorGUILayout.BeginHorizontal();

                    GUILayout.Label("贴图大小超了:", GUILayout.Width(100));
                    GUILayout.Label(maxSizeErr, style, GUILayout.Width(150));
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("格式设置模板", GUILayout.Width(100));
                string format = DisplayTextureFormat(textureCatalog, maxSize, textureImporter);
                GUILayout.TextArea(format);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("不适合设置格式的贴图", GUILayout.Width(200));
            }

            // EditorGUILayout.EndVertical();
        }

        private string DisplayTextureFormat(TextureCatalog textureCatalog, int maxSize, TextureImporter textureImporter)
        {
            TextureImportSettingTemplate template = new TextureImportSettingTemplateHelper(textureCatalog, maxSize, textureImporter).template;
            string format = "";
            if (template == null)
            {
                format = "template == null";
                return format;
            }
            TextureImporterType type = template.textureImporter.textureType;
            if (type == TextureImporterType.Default)
            {
                format += "Texture Type： Default \n";
                format += "Texture Shape: " + template.textureImporter.textureShape.ToString() + "\n";
                if (template.textureImporter.textureShape == TextureImporterShape.TextureCube)
                {
                    format += CubeMappingFormat(template);
                }

                format += SRGBFormat(template);
                format += AlphaFormat(template);
                format += GeneralAdvancedFormat(template);
            }
            else if (type == TextureImporterType.NormalMap)
            {
                format += "Texture Type： Normal \n";
                format += "Texture Shape: " + template.textureImporter.textureShape.ToString() + "\n";
                format += "Create from Grayscale： 默认为不勾选";
                format += GeneralAdvancedFormat(template);
            }
            else if (type == TextureImporterType.Lightmap)
            {
                format += "Texture Type： Lightmap \n";
                format += "Texture Shape: " + template.textureImporter.textureShape.ToString() + "\n";
                format += GeneralAdvancedFormat(template);
            }
            else if (type == TextureImporterType.Sprite)
            {
                format += "Texture Type： Sprite(2D and UI) \n";
                format += "Texture Shape: " + template.textureImporter.textureShape.ToString() + "\n";
                format += "Sprite Mode: 此部分模板不做设置";
                format += SpriteAdvancedFormat(template);
            }
            else
            {
                return "TextureType超出设置范围，请联系TA组";
            }

            format += PlatformSetting(template.androidSetting);
            format += PlatformSetting(template.iphoneSetting);
            format += PlatformSetting(template.standaloneSetting);
            if (type == TextureImporterType.Sprite)
            {
                format += "提示： 如果出现了Split Alpha Chanel的选择框，请确认是否将格式选为了ETC; \n";
            }

            return format;
        }

        string CubeMappingFormat(TextureImportSettingTemplate template)
        {
            string format = "Mapping: " + template.textureImporter.generateCubemap.ToString() + "\n";
            TextureImporterSettings importerSettings = new TextureImporterSettings();
            template.textureImporter.ReadTextureSettings(importerSettings);
            format += "\tConvolution Type: " + importerSettings.cubemapConvolution.ToString() + "\n";
            format += "\tFixed up Edge Seams: " + BoolToString(importerSettings.seamlessCubemap) + "\n";
            return format;
        }

        string SRGBFormat(TextureImportSettingTemplate template)
        {
            string format = "sRGB(Color Texture): " + BoolToString(template.textureImporter.sRGBTexture) + "\n";
            return format;
        }

        string AlphaFormat(TextureImportSettingTemplate template)
        {
            string format = "Alpha Source: " + template.textureImporter.alphaSource.ToString() + "\n";
            if (template.textureImporter.alphaSource != TextureImporterAlphaSource.None)
            {
                format += "Alpha Is Transparency: " + BoolToString(template.textureImporter.alphaIsTransparency) + "\n";
            }

            return format;
        }

        string GeneralAdvancedFormat(TextureImportSettingTemplate template)
        {
            string format = "\nAdvanced \n";
            format += "\tNon Power of 2： 除UI目录(None)和spine剧情目录(To Nearest)外， 模板未做强制设定 \n";
            format += "\tRead/Write Enabled: " + BoolToString(template.textureImporter.isReadable) + "\n";
            // Jan 20, 2021 ZBJ  Streaming Mip Maps 在大地图贴图有单独设置了
            format += "\tStreaming Mip Maps：" + BoolToString(template.textureImporter.streamingMipmaps) + "\n";
            format += "\tGenerate Mip Maps: " + BoolToString(template.textureImporter.mipmapEnabled) + "\n";
            if (template.textureImporter.mipmapEnabled)
            {
                format += "\t\tBorder Mip Maps： 默认为不勾选, 模板未做强制设定 \n";
                format += "\t\tMip Maps Filtering: " + template.textureImporter.mipmapFilter.ToString() + "\n";
                format += "\t\tMip Maps Preserve Coverage： 默认为不勾选, 模板未做强制设定 \n";
                format += "\t\tFadeout Mip Maps： 默认为不勾选, 模板未做强制设定 \n";
            }

            format += "\nWrap Mode： 模板未做强制设定 \n";
            format += "Filter Mode: " + template.textureImporter.filterMode.ToString() + "\n";
            if (template.textureImporter.mipmapEnabled)
            {
                format += "Aniso Level: " + template.textureImporter.anisoLevel + "\n";
            }

            return format;
        }

        string SpriteAdvancedFormat(TextureImportSettingTemplate template)
        {
            string format = "\nAdvanced \n";
            format += "\t" + SRGBFormat(template);
            format += "\tAlpha Source: " + template.textureImporter.alphaSource.ToString() + "\n";
            if (template.textureImporter.alphaSource != TextureImporterAlphaSource.None)
            {
                format += "\tAlpha Is Transparency: " + BoolToString(template.textureImporter.alphaIsTransparency) + "\n";
            }

            format += "\tGenerate Mip Maps: " + BoolToString(template.textureImporter.mipmapEnabled) + "\n";
            if (template.textureImporter.mipmapEnabled)
            {
                format += "\t\tBorder Mip Maps： 默认为不勾选, 模板未做强制设定 \n";
                format += "\t\tMip Maps Filtering: " + template.textureImporter.mipmapFilter.ToString() + "\n";
                format += "\t\tMip Maps Preserve Coverage： 默认为不勾选, 模板未做强制设定 \n";
                format += "\t\tFadeout Mip Maps： 默认为不勾选, 模板未做强制设定 \n";
            }

            format += "\nWrap Mode： 模板未做强制设定 \n";
            format += "Filter Mode: " + template.textureImporter.filterMode.ToString() + "\n";
            if (template.textureImporter.mipmapEnabled)
            {
                format += "Aniso Level: " + template.textureImporter.anisoLevel + "\n";
            }

            return format;
        }

        string PlatformSetting(TextureImporterPlatformSettings settings)
        {
            string format = settings.name + "平台设置" + "\n";
            format += "\tMax Size: " + settings.maxTextureSize + "\n";
            format += "\tResize Algorithm: " + settings.resizeAlgorithm.ToString() + "\n";
            format += "\tFormat: " + settings.format.ToString() + "\n";
            if (settings.name.Equals("Android") || settings.name.Equals("iPhone"))
            {
                format += "\tCompress Quality: " + settings.compressionQuality + "\n";
                if (settings.name.Equals("Android"))
                {
                    format += "\tOverride ETC2 fallback: " + settings.androidETC2FallbackOverride + "\n";
                }
            }

            return format;
        }

        string BoolToString(bool bl)
        {
            if (bl)
            {
                return "勾选";
            }
            else
            {
                return "不勾选";
            }
        }

        private static Dictionary<TextureCatalog, List<TextureCatalogHelper>> textureCatalog_texturePathList_Dict = new Dictionary<TextureCatalog, List<TextureCatalogHelper>>();

        static void TextureCatalog_PathList_Dict_Init()
        {
            textureCatalog_texturePathList_Dict.Clear();
            foreach (TextureCatalog textureCatalog in Enum.GetValues(typeof(TextureCatalog)))
            {
                List<TextureCatalogHelper> pathList = new List<TextureCatalogHelper>();
                textureCatalog_texturePathList_Dict.Add(textureCatalog, pathList);
            }
        }

        static bool IsTextureFile(string fileName)
        {
            List<string> ingoreExtList = new List<string>()
            {
                ".asset", ".renderTexture", ".otf", ".cubemap", ".ttf", ".TTF"
            };
            bool textureExtension = true;
            foreach (var item in ingoreExtList)
            {
                if (fileName.EndsWith(item))
                {
                    textureExtension = false;
                }
            }

            return textureExtension;
        }

        static void GetTextureFormatDict()
        {
            TextureCatalog_PathList_Dict_Init();
            string assetDirPath = Application.dataPath;
            string[] assetDirs = new string[] {assetDirPath};
            string[] textureGuids = AssetDatabase.FindAssets("t:Texture");
            Debug.Log("图片数量" + textureGuids.Length);

            foreach (var textureGuid in textureGuids)
            {
                string texturePath = AssetDatabase.GUIDToAssetPath(textureGuid);
                if (texturePath.StartsWith("Packages/"))
                {
                    continue;
                }

                string fileName = Path.GetFileName(texturePath);
                if (IsTextureFile(fileName))
                {
                    TextureImporter textureImporter = TextureImporter.GetAtPath(texturePath) as TextureImporter;
                    //Debug.Log(texturePath);
                    //Debug.Log(textureImporter.textureType);
                    TextureCatalogHelper textureCatalogHelper = new TextureCatalogHelper(texturePath, textureImporter);
                    TextureCatalog textureCatalog = textureCatalogHelper.textureCatalog;
                    if (!textureCatalog_texturePathList_Dict.Keys.Contains(textureCatalog))
                    {
                        Debug.LogError("Texture Catalog Dict Error!!!!!!!");
                    }
                    else
                    {
                        textureCatalog_texturePathList_Dict[textureCatalog].Add(textureCatalogHelper);
                    }
                }
            }
        }

        [MenuItem("TATools/贴图格式设置/生成贴图格式列表文件")]
        static void OutPutLog()
        {
            string outFilePath = OutFilePath;
            string outFileName = Path.Combine(Application.dataPath, outFilePath, "ProjectTextureCatalogList_" + DateTime.Now.ToString("MM_dd_HH_mm") + ".txt");

            if (Directory.Exists(Path.Combine(Application.dataPath, outFilePath)) == false)
            {
                // Debug.LogError($"目录{outFilePath}不存在");
                // return;
                Directory.CreateDirectory(Path.Combine(Application.dataPath, outFilePath));
            }

            GetTextureFormatDict();

            StreamWriter sw = new StreamWriter(outFileName);
            int count = 1;
            foreach (var item in textureCatalog_texturePathList_Dict.Keys)
            {
                string textureCalalogName = item.ToString();
                int listLength = textureCatalog_texturePathList_Dict[item].Count;
                if (listLength > 0)
                {
                    sw.WriteLine(count + ": " + textureCalalogName + " \t\t " + listLength);
                    count++;
                }
            }

            count = 1;
            sw.WriteLine("贴图格式下没有对应贴图的贴图格式如下：");
            foreach (var item in textureCatalog_texturePathList_Dict.Keys)
            {
                string textureCalalogName = item.ToString();
                int listLength = textureCatalog_texturePathList_Dict[item].Count;
                if (listLength == 0)
                {
                    sw.WriteLine(count + ": " + textureCalalogName);
                    count++;
                }
            }

            foreach (var item in textureCatalog_texturePathList_Dict.Keys)
            {
                string textureCalalogName = item.ToString();
                int listLength = textureCatalog_texturePathList_Dict[item].Count;
                if (listLength > 0)
                {
                    int count2 = 1;
                    sw.WriteLine(textureCalalogName + " 类型贴图所包含的贴图数量为 " + listLength + ":");
                    foreach (var infos in textureCatalog_texturePathList_Dict[item])
                    {
                        sw.WriteLine(count2 + ": " + infos.texturePath);
                        count2++;
                    }
                }
            }

            sw.Flush();
            sw.Close();
            Debug.Log($"==============生成贴图格式列表文件结束:{outFileName}");
        }

        [MenuItem("TATools/贴图格式设置/生成贴图超过约定大小列表文件")]
        static void OutPutMaxSizeErrLog()
        {
            string outFilePath = OutFilePath;
            string outFileName = Path.Combine(Application.dataPath, outFilePath, "ProjectTextureMaxSizeErrList_" + DateTime.Now.ToString("MM_dd_HH_mm") + ".txt");

            if (Directory.Exists(Path.Combine(Application.dataPath, outFilePath)) == false)
            {
                // Debug.LogError($"目录{outFilePath}不存在");
                // return;
                Directory.CreateDirectory(Path.Combine(Application.dataPath, outFilePath));
            }

            GetTextureFormatDict();

            StreamWriter sw = new StreamWriter(outFileName);
            int count = 1;
            sw.WriteLine("以下贴图超过约定的大小：");
            foreach (var item in textureCatalog_texturePathList_Dict.Keys)
            {
                int count2 = 1;
                foreach (var infos in textureCatalog_texturePathList_Dict[item])
                {
                    if (string.IsNullOrEmpty(infos.maxSizeErr) == false)
                    {
                        sw.WriteLine(count2 + ": " + infos.maxSizeErr);
                        count2++;
                    }
                }
            }

            sw.Flush();
            sw.Close();
            Debug.Log($"==============生成贴图超过约定大小列表文件结束:{outFileName}");
        }

        // [MenuItem("TATools/贴图格式设置/输出当前选中的贴图格式设置内容")]
        // static void OutTextureFormatSetting()
        // {
        //     if (Selection.activeObject == null)
        //     {
        //         Debug.LogError($"没有选择文件");
        //         return;
        //     }
        //     string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //     //TextureImporter textureImporter = AssetImporter.GetAtPath(defaultTexPath) as TextureImporter;
        //     TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        //     if (textureImporter == null)
        //     {
        //         Debug.LogError($"当前选择的文件不是图片:{path}");
        //         return;
        //     }
        //     TextureImportSettingTemplate template = new TextureImportSettingTemplateHelper(TextureCatalog.CommonRGBA_PNG, 0, textureImporter).template;
        //
        //     PropertyInfo[] propertyInfos = typeof(TextureImportSettingTemplate).GetProperties();
        //     string formatSetting = "";
        //     // PropertyInfo[] propertyInfos2 = typeof(TextureImportSettingTemplate).GetProperties();
        //     foreach (var propertyInfo in propertyInfos)
        //     {
        //         formatSetting += propertyInfo.ToString() + ": " + propertyInfo.GetValue(template).ToString() + "\n";
        //     }
        //
        //     PropertyInfo[] propertyInfos_1 = typeof(TextureImporterPlatformSettings).GetProperties();
        //     foreach (var propertyInfo in propertyInfos_1)
        //     {
        //         formatSetting += propertyInfo.ToString() + ": " + propertyInfo.GetValue(template.androidSetting).ToString()+ "\n";
        //     }
        //
        //     Debug.Log($"==============[{path}] SetTextureImpoterGeneralPropertiesformat properties finished:\n{formatSetting}");
        // }

        private static Dictionary<string, List<string>> textureName_PathList_Dict = new Dictionary<string, List<string>>();

        [MenuItem("TATools/贴图格式设置/检查贴图重复(同个名字,不同位置)")]
        static void CheckDuplicatedTexture()
        {
            string outFilePath = OutFilePath;
            string outFileName = Path.Combine(Application.dataPath, outFilePath, "ProjectDuplicaedTextureList_" + DateTime.Now.ToString("MM_dd_HH_mm") + ".txt");

            if (Directory.Exists(Path.Combine(Application.dataPath, outFilePath)) == false)
            {
                // Debug.LogError($"目录{outFilePath}不存在");
                // return;
                Directory.CreateDirectory(Path.Combine(Application.dataPath, outFilePath));
            }


            StreamWriter sw = new StreamWriter(outFileName);

            textureName_PathList_Dict.Clear();
            string[] textureGuids = AssetDatabase.FindAssets("t:Texture");
            foreach (var textureGuid in textureGuids)
            {
                string texturePath = AssetDatabase.GUIDToAssetPath(textureGuid);
                //Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
                string fileName = Path.GetFileName(texturePath);
                if (textureName_PathList_Dict.ContainsKey(fileName))
                {
                    if (!ExceptedTexturePath(texturePath))
                    {
                        textureName_PathList_Dict[fileName].Add(texturePath);
                    }
                }
                else
                {
                    List<string> list = new List<string>();
                    if (!ExceptedTexturePath(texturePath))
                    {
                        list.Add(texturePath);
                    }

                    textureName_PathList_Dict.Add(fileName, list);
                }
            }

            foreach (var key in textureName_PathList_Dict.Keys)
            {
                if (textureName_PathList_Dict[key].Count == 2)
                {
                    if (!ExceptedDuplication(textureName_PathList_Dict[key]))
                    {
                        sw.WriteLine(key + ": " + textureName_PathList_Dict[key].Count + " Copies");

                        //Debug.Log(key + ": " + textureName_PathList_Dict[key].Count + " Copies");
                        int count = 1;
                        foreach (var path in textureName_PathList_Dict[key])
                        {
                            sw.WriteLine("\t" + count + ": " + path);
                            //Debug.Log(count + ": " + path);
                            count++;
                        }
                    }
                }

                if (textureName_PathList_Dict[key].Count > 2)
                {
                    sw.WriteLine(key + ": " + textureName_PathList_Dict[key].Count + " Copies");
                    //Debug.Log(key + ": " + textureName_PathList_Dict[key].Count + " Copies");
                    int count = 1;
                    foreach (var path in textureName_PathList_Dict[key])
                    {
                        sw.WriteLine("\t" + count + ": " + path);
                        //Debug.Log(count + ": " + path);
                        count++;
                    }
                }
            }

            sw.Flush();
            sw.Close();
            Debug.Log($"==============生成重复贴图列表结束:{outFileName}");
        }

        static bool ExceptedTexturePath(string path)
        {
            bool excepted = false;
            string[] exceptedFilters = new string[] {"(test)", "(Test)", "Lightmap-", "com.unity.", "/ThirdPart/", "ReflectionProbe-0"};
            foreach (var exceptedFilter in exceptedFilters)
            {
                if (path.Contains(exceptedFilter))
                {
                    excepted = true;
                }
            }

            return excepted;
        }

        static bool ExceptedDuplication(List<string> list)
        {
            return DuplicatedTextureNameWithMaleAndFemale(list) || DuplicatedTextureNameBigMapAnd901(list);
        }

        static bool DuplicatedTextureNameWithMaleAndFemale(List<string> list)
        {
            bool con1 = list[0].Contains("/Male/") && list[1].Contains("/Female/");
            bool con2 = list[1].Contains("/Male/") && list[0].Contains("/Female/");
            string[] bodyParts = new string[] {"hair", "face", "fbody", "ubody", "lbody", "shoe"};
            bool con3 = false;
            foreach (var bodyPart in bodyParts)
            {
                if (list[0].ToLower().Contains(bodyPart) && list[1].ToLower().Contains(bodyPart))
                {
                    con3 = true;
                }
            }

            return (con1 || con2) && con3;
        }

        static bool DuplicatedTextureNameBigMapAnd901(List<string> list)
        {
            bool con1 = list[0].Contains("/BigMap/") && list[1].Contains("/Level_901/");
            bool con2 = list[1].Contains("/BigMap/") && list[0].Contains("/Level_901/");

            return (con1 || con2);
        }
    }
}