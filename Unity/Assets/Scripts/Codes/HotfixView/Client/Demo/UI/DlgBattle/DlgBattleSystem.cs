using System.Collections;
using System.Collections.Generic;
using System;
using ET.Ability.Client;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleFrameTimer)]
    public class DlgBattleTimer: ATimer<DlgBattle>
    {
        protected override void Run(DlgBattle self)
        {
            try
            {
                self.Update();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof (DlgBattle))]
    public static class DlgBattleSystem
    {
        public static void RegisterUIEvent(this DlgBattle self)
        {
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 11");
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Tower";
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTowerItemRefreshListener(transform, i));
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddTankItemRefreshListener(transform, i));
            self.View.E_QuitBattleButton.AddListenerAsync(self.QuitBattle);

            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 22");
            self.RegisterClear().Coroutine();
            self.RegisterSkill().Coroutine();
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 33");
        }

        public static async ETTask RegisterClear(this DlgBattle self)
        {
            Unit myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            while (myUnit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            }
            UnitCfg unitCfg = myUnit.model;

            self.View.EButton_ClearMyTowerButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearMyTower(self.DomainScene()).Coroutine();
            });

            self.View.EButton_ClearAllMonsterButton.AddListener(() =>
            {
                ET.Client.GamePlayPKHelper.SendClearAllMonster(self.DomainScene()).Coroutine();
            });
        }

        public static async ETTask RegisterSkill(this DlgBattle self)
        {
            Unit myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            while (myUnit == null)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
            }
            UnitCfg unitCfg = myUnit.model;
            int count = unitCfg.SkillList.Count;
            if (count > 0)
            {
                self.View.EButton_Skill1Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), unitCfg.SkillList[0]);
                });
            }
            if (count > 1)
            {
                self.View.EButton_Skill2Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), unitCfg.SkillList[1]);
                });
            }
            if (count > 2)
            {
                self.View.EButton_Skill3Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), unitCfg.SkillList[2]);
                });
            }
            if (count > 3)
            {
                self.View.EButton_Skill4Button.AddListener(() =>
                {
                    SkillHelper.CastSkill(self.DomainScene(), unitCfg.SkillList[3]);
                });
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgBattle self, ShowWindowData contextData = null)
        {
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Game);
            int countTower = self.towerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            int countTank = self.monsterList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTanks, countTank);
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.SetVisible(true, countTank);

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleFrameTimer, self);

            self.ShowMesh().Coroutine();
            self.ShowPutTipMsg("");

            //self.PlayMusic();
        }

        // public static void PlayMusic(this DlgBattle self)
        // {
        //     GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
        //     List<string> musicList = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
        //     UIAudioManagerHelper.PlayMusic(self.DomainScene(), musicList);
        // }

        public static void HideWindow(this DlgBattle self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static async ETTask QuitBattle(this DlgBattle self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(),SoundEffectType.Back);

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Des");
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_LeaveTheGame_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () =>
            {
                self._QuitBattle().Coroutine();
            }, null, sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask _QuitBattle(this DlgBattle self)
        {
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            string itemCfgId = self.towerList[index];
            string towerName = ItemHelper.GetItemName(itemCfgId);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTower.EButton_TowerIcoImage.sprite = sprite;
            }
            itemTower.ELabel_NumTextMeshProUGUI.text = "1";

            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";

            ET.EventTriggerListener.Get(itemTower.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                DlgBattleDragItem_ShowWindowData showWindowData = new()
                {
                    battleDragItemType = BattleDragItemType.PKTower,
                    battleDragItemParam = itemCfgId,
                    callBack = (scene) =>
                    {
                    },
                };
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
            });

            itemTower.SetLevel(itemCfgId);
            itemTower.SetLabels(itemCfgId);
            itemTower.SetQuality(itemCfgId);
        }

        public static void AddTankItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTank = self.ScrollItemTanks[index].BindTrans(transform);

            string itemCfgId = self.monsterList[index];
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(itemCfgId);
            string monsterName = ItemHelper.GetItemName(itemCfgId);

            string icon = ItemHelper.GetItemIcon(itemCfgId);
            if (string.IsNullOrEmpty(icon) == false)
            {
                Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
                itemTank.EButton_TowerIcoImage.sprite = sprite;
            }
            itemTank.ELabel_NumTextMeshProUGUI.text = $"1";
            itemTank.ELabel_NameTextMeshProUGUI.text = $"{monsterName}";

            ET.EventTriggerListener.Get(itemTank.EButton_SelectButton.gameObject).onPress.AddListener((go, xx) =>
            {
                DlgBattleDragItem_ShowWindowData showWindowData = new()
                {
                    battleDragItemType = BattleDragItemType.PKMonster,
                    battleDragItemParam = monsterCfg.Id,
                    countOnce = int.Parse(self.View.E_InputFieldTMP_InputField.text),
                    callBack = (scene) =>
                    {
                    },
                };
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgBattleDragItem>(showWindowData).Coroutine();
            });

            itemTank.EG_IconStarRectTransform.SetVisible(false);
        }

        public static void ChgScrollRectMoveStatus(this DlgBattle self, bool status)
        {
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.enabled = status;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.enabled = status;
        }

        public static void Update(this DlgBattle self)
        {
        }

        public static void ShowPutTipMsg(this DlgBattle self, string tipMsg)
        {
            if (string.IsNullOrEmpty(tipMsg))
            {
                self.HidePutTipMsg();
                return;
            }
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattle self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static async ETTask ShowMesh(this DlgBattle self)
        {
            if (Application.isEditor == false)
            {
                return;
            }

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(self.DomainScene());
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

    }
}