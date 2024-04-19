using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    public static class UIManagerHelper
    {
        public static UIComponent GetUIComponent(Scene scene)
        {
            Scene currentScene = null;
            Scene clientScene = null;
            if (scene == scene.ClientScene())
            {
                currentScene = scene.GetComponent<CurrentScenesComponent>().Scene;
                clientScene = scene;
            }
            else
            {
                currentScene = scene;
                clientScene = scene.ClientScene();
            }

            UIComponent _UIComponent = clientScene.GetComponent<UIComponent>();
            if (_UIComponent == null)
            {
                _UIComponent = clientScene.AddComponent<UIComponent>();
            }
            return _UIComponent;
        }

        public static void ShowCommonLoading(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            if (_DlgCommonLoading == null)
            {
                _UIComponent.ShowWindow<DlgCommonLoading>();
                _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            }
            if (_DlgCommonLoading != null)
            {
                _DlgCommonLoading.Show();
            }
        }

        public static void HideCommonLoading(Scene scene, bool bForceHide)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonLoading _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            if (_DlgCommonLoading == null)
            {
                _UIComponent.ShowWindow<DlgCommonLoading>();
                _DlgCommonLoading = _UIComponent.GetDlgLogic<DlgCommonLoading>(true);
            }
            if (_DlgCommonLoading != null)
            {
                _DlgCommonLoading.Hide(bForceHide);
            }
        }

        public static void ShowTip(Scene scene, string tipMsg)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonTip _DlgCommonTip = _UIComponent.GetDlgLogic<DlgCommonTip>(true);
            if (_DlgCommonTip == null)
            {
                _UIComponent.ShowWindow<DlgCommonTip>();
                _DlgCommonTip = _UIComponent.GetDlgLogic<DlgCommonTip>(true);
            }
            if (_DlgCommonTip != null)
            {
                _DlgCommonTip.ShowTip(tipMsg);
            }
        }

        public static void ShowTipTopShow(Scene scene, string tipMsg)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonTipTopShow _DlgCommonTipTopShow = _UIComponent.GetDlgLogic<DlgCommonTipTopShow>(true);
            if (_DlgCommonTipTopShow == null)
            {
                _UIComponent.ShowWindow<DlgCommonTipTopShow>();
                _DlgCommonTipTopShow = _UIComponent.GetDlgLogic<DlgCommonTipTopShow>(true);
            }
            if (_DlgCommonTipTopShow != null)
            {
                _DlgCommonTipTopShow.ShowTip(tipMsg);
            }
        }

        public static void HideConfirm(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.HideWindow<DlgCommonConfirm>();
        }

        public static void PreLoadConfirm(Scene scene)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            if (_UIComponent != null)
            {
                DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(false);
                if (_DlgCommonConfirm == null)
                {
                    _UIComponent.ShowWindow<DlgCommonConfirm>();
                    _UIComponent.HideWindow<DlgCommonConfirm>();
                }
            }
        }

        public static void ShowConfirmNoClose(Scene scene, string confirmMsg, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowConfirmNoClose(confirmMsg, sureText, titleText);
            }
        }

        public static void ShowConfirmNoCloseHighest(Scene scene, string confirmMsg, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowConfirmNoClose(confirmMsg, sureText, titleText);
            }
        }

        public static void ShowOnlyConfirm(Scene scene, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowOnlyConfirm(confirmMsg, confirmCallBack, sureText, titleText);
            }
        }

        public static void ShowOnlyConfirmHighest(Scene scene, string confirmMsg, Action confirmCallBack, string sureText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowOnlyConfirm(confirmMsg, confirmCallBack, sureText, titleText);
            }
        }

        public static void ShowConfirm(Scene scene, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirm _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            if (_DlgCommonConfirm == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirm>();
                _DlgCommonConfirm = _UIComponent.GetDlgLogic<DlgCommonConfirm>(true);
            }
            if (_DlgCommonConfirm != null)
            {
                _DlgCommonConfirm.ShowConfirm(confirmMsg, confirmCallBack, cancelCallBack, sureText, cancelText, titleText);
            }
        }

        public static void ShowConfirmHighest(Scene scene, string confirmMsg, Action confirmCallBack, Action cancelCallBack, string sureText = null, string cancelText = null, string titleText = null)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            DlgCommonConfirmHighest _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            if (_DlgCommonConfirmHighest == null)
            {
                _UIComponent.ShowWindow<DlgCommonConfirmHighest>();
                _DlgCommonConfirmHighest = _UIComponent.GetDlgLogic<DlgCommonConfirmHighest>(true);
            }
            if (_DlgCommonConfirmHighest != null)
            {
                _DlgCommonConfirmHighest.ShowConfirm(confirmMsg, confirmCallBack, cancelCallBack, sureText, cancelText, titleText);
            }
        }

        public static async ETTask<Sprite> LoadSprite(string imgPath)
        {
            if (string.IsNullOrEmpty(imgPath))
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.LoadSprite imgPath is null");
#endif
                return null;
            }
            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            return sprite;
        }

        public static async ETTask SetMyIcon(this Image image, Scene scene)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            List<string> avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();
            await image.SetImageByPath(avatarIconList[playerBaseInfoComponent.IconIndex]);
        }

        public static async ETTask SetPlayerIcon(this Image image, Scene scene, long playerId)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(scene, playerId);
            List<string> avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();
            await image.SetImageByPath(avatarIconList[playerBaseInfoComponent.IconIndex]);
        }

        public static async ETTask SetImageByPath(this Image image, string imgPath, bool needSetNativeSize = false)
        {
            if (string.IsNullOrEmpty(imgPath))
            {
                image.sprite = null;
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.SetImageByPath imgPath is null");
#endif
                return;
            }
            Sprite sprite = await ResComponent.Instance.LoadAssetAsync<Sprite>(imgPath);
            image.sprite = sprite;
            if (needSetNativeSize)
            {
                image.SetNativeSize();
            }
        }

        public static void SetImageGray(this Image image, bool isGray)
        {
            Material material = ResComponent.Instance.LoadAsset<Material>("UIGray");
            image.material = isGray ? material : null;
        }

        public static void SetTowerItemClick(Scene scene, string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ItemHelper.ChkIsTower(itemCfgId) == false)
            {
                return;
            }

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            _UIComponent.ShowWindow<DlgDetails>();
            DlgDetails _DlgDetails = _UIComponent.GetDlgLogic<DlgDetails>(true);
            if (_DlgDetails != null)
            {
                _DlgDetails.SetCurItemCfgId(itemCfgId);
            }
        }

        public static void SetMonsterItemClick(Scene scene, string itemCfgId, Vector3 pos)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }
            if (ItemHelper.ChkIsMonster(itemCfgId) == false)
            {
                return;
            }

            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
            string itemDesc = ItemHelper.GetItemDesc(itemCfgId);
            DlgDescTips_ShowWindowData _DlgDescTips_ShowWindowData = new()
            {
                Pos = pos + Vector3.up,
                Desc = itemDesc,
            };
            _UIComponent.ShowWindowAsync<DlgDescTips>(_DlgDescTips_ShowWindowData).Coroutine();
        }

        public static async ETTask EnterRoom(Scene scene)
        {
            UIAudioManagerHelper.PlayUIAudio(scene, SoundEffectType.JoinRoom);
            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            PlayerStatusComponent playerStatusComponent = PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            if (playerStatusComponent.PlayerStatus != PlayerStatus.Room || playerStatusComponent.RoomId == 0)
            {
                if (playerStatusComponent.RoomType == RoomType.Normal)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
                }
                else if (playerStatusComponent.RoomType == RoomType.AR)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
                }
                return;
            }

            if (playerStatusComponent.RoomType == RoomType.Normal)
            {
                if (playerStatusComponent.SubRoomType == SubRoomType.NormalPVE)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVE>();
                }
                else if (playerStatusComponent.SubRoomType == SubRoomType.NormalPVP)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVP>();
                }
                else if (playerStatusComponent.SubRoomType == SubRoomType.NormalEndlessChallenge)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>();
                }
            }
            else if (playerStatusComponent.RoomType == RoomType.AR && playerStatusComponent.SubRoomType == SubRoomType.ARPVP)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVP>();
            }
            else if (playerStatusComponent.RoomType == RoomType.AR && playerStatusComponent.SubRoomType == SubRoomType.ARPVE)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoomPVE>();
            }
            else
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
            }
        }

        public static async ETTask ExitRoom(Scene scene)
        {
            ET.Client.ARSessionHelper.ResetMainCamera(scene, false);

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();

            if (DebugConnectComponent.Instance.IsDebugMode)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            }
            // PlayerStatusComponent playerStatusComponent = PlayerHelper.GetMyPlayerStatusComponent(scene);
            // if (playerStatusComponent.RoomType == RoomType.Normal)
            // {
            //     await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            // }
            // else if (playerStatusComponent.RoomType == RoomType.AR)
            // {
            //     await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            // }
        }

        public static async ETTask<bool> ChkPhsicalAndShowtip(Scene scene, int takePhsicalStrength)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            if(playerBaseInfoComponent._ChkPhysicalStrength(-takePhsicalStrength) == false)
            {
                UIComponent _UIComponent = UIManagerHelper.GetUIComponent(scene);
                _UIComponent.ShowWindow<DlgPhysicalStrengthTip>();
                DlgPhysicalStrengthTip _DlgPhysicalStrengthTip = _UIComponent.GetDlgLogic<DlgPhysicalStrengthTip>(true);
                _DlgPhysicalStrengthTip.SetText(takePhsicalStrength.ToString());
                return false;
            }
            return true;
        }

        public static async ETTask ShowFunctionMenuLockOne(Scene scene, string functionMenuCfgId, Transform transformLock)
        {
            if (transformLock == null)
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.ShowFunctionMenuLockOne transformLock == null");
#endif
                return;
            }

            FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
            if (functionMenuCfg == null)
            {
#if UNITY_EDITOR
                Log.Error($"ET.Client.UIManagerHelper.ShowFunctionMenuLockOne functionMenuCfg[{functionMenuCfgId}] == null");
#endif
                return;
            }
            PlayerFunctionMenuComponent playerFunctionMenuComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerFunctionMenu(scene);
            FunctionMenuStatus functionMenuStatus = playerFunctionMenuComponent.GetStatus(functionMenuCfgId);
            if (functionMenuCfg.IsOpenSoon)
            {
                transformLock.gameObject.SetActive(true);

                ET.EventTriggerListener.Get(transformLock.gameObject).onClick.AddListener((go, xx) =>
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                    UIManagerHelper.ShowTip(scene, tipMsg);
                });

                Transform lockTextTrans = transformLock.Find("Text");
                if (lockTextTrans != null)
                {
                    var tmpText = lockTextTrans.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    if (tmpText != null)
                    {
                        tmpText.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                    }
                    else
                    {
                        var text = lockTextTrans.gameObject.GetComponent<Text>();
                        if (text != null)
                        {
                            text.text = LocalizeComponent.Instance.GetTextValue("TextCode_Key_FunctionMenu_IsOpenSoon");
                        }
                    }
                }
            }
            else if (functionMenuStatus == FunctionMenuStatus.Lock)
            {
                transformLock.gameObject.SetActive(true);

                ET.EventTriggerListener.Get(transformLock.gameObject).onClick.AddListener((go, xx) =>
                {
                    string tipMsg = GetFunctionMenuConditionDesc(functionMenuCfgId);
                    UIManagerHelper.ShowTip(scene, tipMsg);
                });

                Transform lockTextTrans = transformLock.Find("Text");
                if (lockTextTrans != null)
                {
                    var tmpText = lockTextTrans.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                    if (tmpText != null)
                    {
                        tmpText.text = GetFunctionMenuConditionDesc(functionMenuCfgId);
                    }
                    else
                    {
                        var text = lockTextTrans.gameObject.GetComponent<Text>();
                        if (text != null)
                        {
                            text.text = GetFunctionMenuConditionDesc(functionMenuCfgId);
                        }
                    }
                }
            }
            else
            {
                transformLock.gameObject.SetActive(false);
            }
        }

        public static string GetFunctionMenuConditionDesc(string functionMenuCfgId)
        {
            FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
            string descKey = "";
            switch (functionMenuCfg.OpenCondition)
            {
                case FunctionMenuConditionDefault functionMenuConditionDefault:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_Default";
                    return LocalizeComponent.Instance.GetTextValue(descKey);
                    break;
                case FunctionMenuConditionTutorialFirstFinished functionMenuConditionTutorialFirstFinished:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_TutorialFirstFinished";
                    break;
                case FunctionMenuConditionBattleNumARAny functionMenuConditionBattleNumARAny:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARAny";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumARAny.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumARPVE functionMenuConditionBattleNumArpve:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARPVE";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumArpve.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumARPVP functionMenuConditionBattleNumArpvp:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumARPVP";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumArpvp.BattleNum);
                    break;
                case FunctionMenuConditionBattleNumAREndlessChallenge functionMenuConditionBattleNumAREndlessChallenge:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_BattleNumAREndlessChallenge";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionBattleNumAREndlessChallenge.BattleNum);
                    break;
                case FunctionMenuConditionIndexWhenARPVE functionMenuConditionIndexWhenArpve:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_IndexWhenARPVE";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionIndexWhenArpve.Index);
                    break;
                case FunctionMenuConditionIndexWhenAREndlessChallenge functionMenuConditionIndexWhenAREndlessChallenge:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_IndexWhenAREndlessChallenge";
                    return LocalizeComponent.Instance.GetTextValue(descKey, functionMenuConditionIndexWhenAREndlessChallenge.Index);
                    break;
                default:
                    descKey = "Text_Key_FunctionMenu_OpenCondition_Default";
                    return LocalizeComponent.Instance.GetTextValue(descKey);
                    break;
            }
            return "";
        }

        #region 金币、体力 显示
        public static async ETTask ShowCoinCostText(this Transform textTrans, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.physicalStrength >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowCoinCostTextInBattleTower(this Transform textTrans, Scene scene, int costValue)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            long playerId = PlayerStatusHelper.GetMyPlayerId(scene);
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);

            bool isEnough = curGoldValue >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowPhysicalCostText(this Transform textTrans, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.GetPhysicalStrength() >= costValue;
            textTrans.ChgTMPColor(isEnough);
            textTrans.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowCoinCostText(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.physicalStrength >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowCoinCostTextInBattleTower(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(scene);
            long playerId = PlayerStatusHelper.GetMyPlayerId(scene);
            int curGoldValue = (int)gamePlayComponent.GetPlayerCoin(playerId, CoinType.Gold);

            bool isEnough = curGoldValue >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        public static async ETTask ShowPhysicalCostText(this TextMeshProUGUI textMeshProUGUI, Scene scene, int costValue)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            bool isEnough = playerBaseInfoComponent.GetPhysicalStrength() >= costValue;
            textMeshProUGUI.ChgTMPColor(isEnough);
            textMeshProUGUI.ChgTMPText($"{costValue}");
        }

        #endregion

        #region 在client展示ARMesh

        public static async ETTask ShowARMesh(Scene scene)
        {
            if (Application.isEditor == false)
            {
                return;
            }

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                return;
            }

            if (gamePlayComponent.isTestARMesh)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARMeshUrl;

                // Draco bytes
                byte[] bytes = await gamePlayComponent.DownloadFileBytesAsync(gamePlayComponent._ARMeshDownLoadUrl);
                MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);
                CreateMeshFromData(meshData, gamePlayComponent.GetARScale());
            }
            else if (gamePlayComponent.isTestARObj)
            {
                gamePlayComponent._ARMeshDownLoadUrl = gamePlayComponent.isTestARObjUrl;

                string content = await gamePlayComponent.DownloadFileTextAsync(gamePlayComponent._ARMeshDownLoadUrl);
                ET.LoadMesh.ObjMesh objInstace = new ET.LoadMesh.ObjMesh();
                objInstace = objInstace.LoadFromObj(content);

                CreateMesh(objInstace.VertexArray, objInstace.TriangleArray, objInstace.NormalArray, objInstace.UVArray, gamePlayComponent.GetARScale());
            }
            else if(gamePlayComponent.ChkIsAR() == false)
            {
                return;
            }

        }

        public static Mesh CreateMeshFromData(MeshHelper.MeshData meshData, float3 scale)
        {
            var vertices = new Vector3[meshData.vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = meshData.vertices[i];
            }
            var normals = new Vector3[meshData.normals.Length];
            for (int i = 0; i < meshData.normals.Length; i++)
            {
                normals[i] = meshData.normals[i];
            }
            var uv = new Vector2[meshData.uv.Length];
            for (int i = 0; i < meshData.uv.Length; i++)
            {
                uv[i] = meshData.uv[i];
            }

            return CreateMesh(vertices, meshData.triangles, normals, uv, scale);
        }

        public static Mesh CreateMesh(Vector3[] verticesIn, int[] trianglesIn, Vector3[] normalsIn, Vector2[] uvIn, float3 scale)
        {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            if (verticesIn != null)
            {
                var vertices = new Vector3[verticesIn.Length];
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = verticesIn[i] * scale;
                }
                mesh.vertices = vertices;
            }
            if (trianglesIn != null)
            {
                mesh.triangles = trianglesIn;
            }
            if (normalsIn != null)
            {
                var normals = new Vector3[normalsIn.Length];
                for (int i = 0; i < normalsIn.Length; i++)
                {
                    normals[i] = normalsIn[i];
                }

                mesh.normals = normals;
            }
            if (uvIn != null)
            {
                var uv = new Vector2[uvIn.Length];
                for (int i = 0; i < uvIn.Length; i++)
                {
                    uv[i] = uvIn[i];
                }
                mesh.uv = uv;
            }

            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);

            GameObject zpbTestObj = GameObject.Find("zpbTestObj");
            if (zpbTestObj != null)
            {
                GameObject.Destroy(zpbTestObj);
            }
            zpbTestObj = new GameObject("zpbTestObj");
            GameObject.DontDestroyOnLoad(zpbTestObj);
            zpbTestObj.transform.localScale = new Vector3(1, 1, 1);
            zpbTestObj.layer = LayerMask.NameToLayer("Map");
            zpbTestObj.AddComponent<MeshCollider>().sharedMesh = mesh;
            zpbTestObj.AddComponent<MeshRenderer>();
            zpbTestObj.AddComponent<MeshFilter>().sharedMesh = mesh;
            // Mesh without wireframe data.
            return mesh;
        }

        #endregion
    }
}