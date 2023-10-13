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
                ET.Client.GamePlayTowerDefenseHelper.SendClearMyTower(self.DomainScene()).Coroutine();
            });

            self.View.EButton_ClearAllMonsterButton.AddListener(() =>
            {
                ET.Client.GamePlayTowerDefenseHelper.SendClearAllMonster(self.DomainScene()).Coroutine();
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
            self.CheckIfPlaceSuccess();

            int countTower = self.towerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            int countTank = self.monsterList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTanks, countTank);
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.SetVisible(true, countTank);

            self._groundLayerMask = LayerMask.GetMask("Map");

            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleFrameTimer, self);

            self.ShowMesh().Coroutine();

            self.PlayMusic();
        }

        public static void PlayMusic(this DlgBattle self)
        {
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
            List<string> musicList = gamePlayComponent.GetGamePlayBattleConfig().MusicList;
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), musicList);
        }

        public static void HideWindow(this DlgBattle self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static async ETTask QuitBattle(this DlgBattle self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioBack(self.DomainScene());

            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_IsQuitBattle");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self._QuitBattle().Coroutine();
            }, null);
        }

        public static async ETTask _QuitBattle(this DlgBattle self)
        {
            await RoomHelper.MemberQuitBattleAsync(self.ClientScene());
            await SceneHelper.EnterHall(self.ClientScene());
        }

        public static string GetUnitPrefabName(this DlgBattle self, UnitCfg unitCfg)
        {
            ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
            return resUnitCfg.ResName;
        }

        public static string GetUnitIcon(this DlgBattle self, UnitCfg unitCfg)
        {
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(unitCfg.Icon);
            return resIconCfg.ResName;
        }

        public static void OnSelectMonster(this DlgBattle self, string monsterCfgId)
        {
            self.selectCfgType = UISelectCfgType.Monster;
            self.selectCfgId = monsterCfgId;

            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(monsterCfg.UnitId);

            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectTower(this DlgBattle self, string towerCfgId)
        {
            self.selectCfgType = UISelectCfgType.Tower;
            self.selectCfgId = towerCfgId;

            self.currentPlaceObj = new GameObject("currentPlaceObj");

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;

            for (int i = 0; i < towerCfg.UnitId.Count; i++)
            {
                string unitCfgId = towerCfg.UnitId[i];
                float3 releativePos = float3.zero;
                if (towerCfg.RelativePosition.Count > i)
                {
                    releativePos = new float3(towerCfg.RelativePosition[i].X, towerCfg.RelativePosition[i].Y, towerCfg.RelativePosition[i].Z);
                }
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                float resScale = unitCfg.ResScale;

                float3 forward = new float3(0, 0, 1);
                string pathName = self.GetUnitPrefabName(unitCfg);
                GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

                GameObject goTmp = GameObject.Instantiate(go);
                goTmp.transform.SetParent(self.currentPlaceObj.transform);
                goTmp.transform.localPosition = releativePos;
                goTmp.transform.localScale = Vector3.one * resScale;
                goTmp.transform.forward = forward;

            }

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.towerList[index]);
            string towerName = towerCfg.Name;
            if (string.IsNullOrEmpty(towerName))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                towerName = unitCfg.Name;
            }

            string icon = "";
            if (string.IsNullOrEmpty(towerCfg.Icon))
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                icon = self.GetUnitIcon(unitCfg);
            }
            else
            {
                ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(towerCfg.Icon);
                icon = resIconCfg.ResName;
            }

            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTower.ELabel_NumTextMeshProUGUI.text = "1";

            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";
            itemTower.EButton_TowerIcoImage.sprite = sprite;
            SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () => { self.OnSelectTower(towerCfg.Id); };

            itemTower.EG_IconStarRectTransform.SetVisible(true);
            int starCount = towerCfg.Level[0];
            itemTower.E_IconStar1Image.gameObject.SetActive(starCount>=1);
            itemTower.E_IconStar2Image.gameObject.SetActive(starCount>=2);
            itemTower.E_IconStar3Image.gameObject.SetActive(starCount>=3);
        }

        public static void AddTankItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTank = self.ScrollItemTanks[index].BindTrans(transform);

            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(self.monsterList[index]);
            string monsterName = monsterCfg.Name;
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(monsterCfg.UnitId);
            if (string.IsNullOrEmpty(monsterName))
            {
                monsterName = unitCfg.Name;
            }

            string icon = self.GetUnitIcon(unitCfg);

            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTank.ELabel_NumTextMeshProUGUI.text = $"1";
            itemTank.ELabel_NameTextMeshProUGUI.text = $"{monsterName}";
            itemTank.EButton_TowerIcoImage.sprite = sprite;
            SelectImage selectImage = itemTank.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () =>
            {
                self.OnSelectMonster(monsterCfg.Id);
            };
            itemTank.EG_IconStarRectTransform.SetVisible(false);
        }

        public static void ChgScrollRectMoveStatus(this DlgBattle self, bool status)
        {
            self.View.ELoopScrollList_TankLoopHorizontalScrollRect.enabled = status;
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.enabled = status;
        }

        public static void Update(this DlgBattle self)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }

            //Log.Debug($"--- Update 44 self.currentPlaceObj[{self.currentPlaceObj}] self.isDragging[{self.isDragging}] self.isPlaceSuccess[{self.isPlaceSuccess}]");
            if (self.CheckUserInput())
            {
                self.isClickUGUI = ET.UGUIHelper.IsClickUGUI();
                if (self.isClickUGUI)
                {
                    return;
                }

                self.isDragging = true;
                self.ChgScrollRectMoveStatus(false);
                self.MoveCurrentPlaceObj();

                bool canPut = self.ChkCanPut(self.currentPlaceObj.transform.position);
                if (canPut)
                {
                    self.View.E_TipNodeImage.SetVisible(false);
                    self.ChgCurrentPlaceObj(true);
                }
                else
                {
                    //self.currentPlaceObj.gameObject.SetActive(false);
                    self.ChgCurrentPlaceObj(false);
                }
            }
            else if (self.isDragging)
            {
                if (self.isClickUGUI == false)
                {
                    if (self.isRaycast == false)
                    {
                        string tipMsg = $"当前放置位置 没有投射点";
                        ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }

                    if (self.isCliffy)
                    {
                        string tipMsg = $"当前放置位置 太陡峭";
                        ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }

                    var position = self.currentPlaceObj.transform.position;
                    if (self.selectCfgType == UISelectCfgType.Tower)
                    {
                        ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

                        ET.Client.GamePlayTowerDefenseHelper.SendCallTower(self.ClientScene(), self.selectCfgId, position).Coroutine();
                    }
                    else if (self.selectCfgType == UISelectCfgType.Monster)
                    {
                        ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioTowerPush(self.DomainScene());

                        int count = int.Parse(self.View.E_InputFieldTMP_InputField.text);
                        if (count > 50)
                        {
                            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeUITip()
                            {
                                tipMsg = "太多了",
                            });
                            self.CheckIfPlaceSuccess();
                            return;
                        }
                        ET.Client.GamePlayTowerDefenseHelper.SendCallMonster(self.ClientScene(), self.selectCfgId, position, count).Coroutine();
                    }
                }

                self.CheckIfPlaceSuccess();
            }
            else if (self.currentPlaceObj != null)
            {
                self.CheckIfPlaceSuccess();
            }
        }

        public static void ChgCurrentPlaceObj(this DlgBattle self, bool canPut)
        {
            Color colorNew;
            if (canPut)
            {
                colorNew = Color.white;
            }
            else
            {
                colorNew = Color.red;
            }

            MeshRenderer[] rendererList = self.currentPlaceObj.gameObject.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in rendererList)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                }
            }
            SkinnedMeshRenderer[] rendererList2 = self.currentPlaceObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (SkinnedMeshRenderer renderer in rendererList2)
            {
                foreach (var material in renderer.materials)
                {
                    Color color = material.color;
                    material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                }
            }
        }

        public static bool ChkCanPut(this DlgBattle self, Vector3 position)
        {
            long leftTime = self.curTipTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return false;
            }
            //self.curTipTime = TimeHelper.ClientNow() + 800;
            self.curTipTime = TimeHelper.ClientNow();

            if (self.isClickUGUI)
            {
                string tipMsg = $"当前手指在UI上,请挪开";
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isRaycast == false)
            {
                string tipMsg = $"当前放置位置 没有投射点";
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isCliffy)
            {
                string tipMsg = $"当前放置位置 太陡峭";
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            return true;
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattle self)
        {
            return Input.GetMouseButton(0);
        }

        public static void ShowPutTipMsg(this DlgBattle self, string tipMsg)
        {
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattle self)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }
            Vector3 screenPosition = Input.mousePosition;
            screenPosition += new Vector3(-130, 30, 0);

            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            Vector3 point = Vector3.zero;
            self.isRaycast = false;
            self.isCliffy = false;
            if (Physics.Raycast(ray, out hitInfo, 10000, self._groundLayerMask))
            {
                self.isRaycast = true;
                Vector3 normal = hitInfo.normal;
                //大概是66.6度
                if (Vector3.Dot(normal, Vector3.up) < 0.5f)
                {
                    self.isCliffy = true;
                }

                point = hitInfo.point;
            }

            if (self.isRaycast)
            {
                if (self.currentPlaceObj.gameObject.activeSelf == false)
                {
                    self.currentPlaceObj.gameObject.SetActive(true);
                }

                self.currentPlaceObj.transform.position = point + new Vector3(0, self._YOffset, 0);
                self.currentPlaceObj.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                if (self.currentPlaceObj.gameObject.activeSelf)
                {
                    self.currentPlaceObj.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        ///检测是否放置成功
        /// </summary>
        public static void CheckIfPlaceSuccess(this DlgBattle self)
        {
            self.isDragging = false;
            if (self.currentPlaceObj != null)
            {
                GameObject.Destroy(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            self.View.E_TipNodeImage.SetVisible(false);
            self.ChgScrollRectMoveStatus(true);
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
            }
            else if(gamePlayComponent.ChkIsAR() == false)
            {
                return;
            }

            // Draco bytes
            byte[] bytes = await gamePlayComponent.DownloadFileAsync(gamePlayComponent._ARMeshDownLoadUrl);
            MeshHelper.MeshData meshData = MeshHelper.GetMeshDataFromBytes(bytes);
            CreateMeshFromData(meshData, gamePlayComponent.GetARScale());
        }

        public static Mesh CreateMeshFromData(MeshHelper.MeshData meshData, float3 scale)
        {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            if (meshData.vertices != null)
            {
                var vertices = new Vector3[meshData.vertices.Length];
                for (int i = 0; i < meshData.vertices.Length; i++)
                {
                    vertices[i] = meshData.vertices[i] * scale;
                }
                mesh.vertices = vertices;
            }
            if (meshData.triangles != null)
            {
                mesh.triangles = meshData.triangles;
            }
            if (meshData.normals != null)
            {
                var normals = new Vector3[meshData.normals.Length];
                for (int i = 0; i < meshData.normals.Length; i++)
                {
                    normals[i] = meshData.normals[i];
                }

                mesh.normals = normals;
            }
            if (meshData.uv != null)
            {
                var uv = new Vector2[meshData.uv.Length];
                for (int i = 0; i < meshData.uv.Length; i++)
                {
                    uv[i] = meshData.uv[i];
                }
                mesh.uv = uv;
            }

            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);

            GameObject go = new GameObject("zpb");
            GameObject.DontDestroyOnLoad(go);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.layer = LayerMask.NameToLayer("Map");
            go.AddComponent<MeshCollider>().sharedMesh = mesh;
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>().sharedMesh = mesh;
            // Mesh without wireframe data.
            return mesh;
        }

    }
}