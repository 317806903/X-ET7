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
            self.RegisterSkill();
            Log.Debug($"ET.Client.DlgBattleSystem.RegisterUIEvent 33");
        }

        public static void RegisterSkill(this DlgBattle self)
        {
            Unit myUnit = UnitHelper.GetMyPlayerUnit(self.DomainScene());
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
        }

        public static void ShowWindow(this DlgBattle self, ShowWindowData contextData = null)
        {
            int countTower = self.towerList.Count;
            self.AddUIScrollItems(ref self.ScrollItemTowers, countTower);
            self.View.ELoopScrollList_TowerLoopHorizontalScrollRect.SetVisible(true, countTower);

            int countTank = self.tankList.Count;
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

            string msg = "是否确认退出战斗?";
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

        public static (string cfgId, string name, UnitCfg unitCfg) GetCfgId(this DlgBattle self, bool isTower, int index)
        {
            string cfgId;
            string name;
            string unitCfgId;
            if (isTower)
            {
                cfgId = self.towerList[index];
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(cfgId);
                name = towerCfg.Name;
                unitCfgId = towerCfg.UnitId;
            }
            else
            {
                cfgId = self.tankList[index];
                TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(cfgId);
                name = monsterCfg.Name;
                unitCfgId = monsterCfg.UnitId;
            }

            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
            if (string.IsNullOrEmpty(name))
            {
                name = unitCfg.Name;
            }

            return (cfgId, name, unitCfg);
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

        public static void OnSelectItem(this DlgBattle self, bool isTower, int index)
        {
            if (isTower)
            {
                self.selectCfgType = UISelectCfgType.Tower;
            }
            else
            {
                self.selectCfgType = UISelectCfgType.Tanker;
            }

            UnitCfg unitCfg;
            string name;
            (self.selectCfgId, name, unitCfg) = self.GetCfgId(isTower, index);

            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void AddTowerItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTower = self.ScrollItemTowers[index].BindTrans(transform);
            UnitCfg unitCfg;
            string towerName;
            (_, towerName, unitCfg) = self.GetCfgId(true, index);
            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);

            itemTower.ELabel_NumTextMeshProUGUI.text = $"";
            itemTower.ELabel_NameTextMeshProUGUI.text = $"{towerName}";

            itemTower.EButton_SelectImage.sprite = sprite;
            SelectImage selectImage = itemTower.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () =>
            {
                self.OnSelectItem(true, index);
            };
        }

        public static void AddTankItemRefreshListener(this DlgBattle self, Transform transform, int index)
        {
            Scroll_Item_Tower itemTank = self.ScrollItemTanks[index].BindTrans(transform);
            UnitCfg unitCfg;
            string towerName;
            (_, towerName, unitCfg) = self.GetCfgId(false, index);
            string icon = self.GetUnitIcon(unitCfg);
            Sprite sprite = ResComponent.Instance.LoadAsset<Sprite>(icon);
            itemTank.ELabel_NumTextMeshProUGUI.text = $"";
            itemTank.ELabel_NameTextMeshProUGUI.text = $"{towerName}";
            itemTank.EButton_SelectImage.sprite = sprite;
            SelectImage selectImage = itemTank.EButton_SelectButton.GetComponent<SelectImage>();
            selectImage.onPointerDown = () =>
            {
                self.OnSelectItem(false, index);
            };
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

                self.CheckCanPut(self.currentPlaceObj.transform.position);
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
                    else if (self.selectCfgType == UISelectCfgType.Tanker)
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

        public static bool CheckCanPut(this DlgBattle self, Vector3 position)
        {
            long leftTime = self.curTipTime - TimeHelper.ClientNow();
            if (leftTime > 0)
            {
                return false;
            }
            self.curTipTime = TimeHelper.ClientNow() + 800;

            if (self.isClickUGUI)
            {
                string tipMsg = $"当前手指在UI上,请挪开";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (self.isRaycast == false)
            {
                string tipMsg = $"当前放置位置 没有投射点";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (self.isCliffy)
            {
                string tipMsg = $"当前放置位置 太陡峭";
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
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
            screenPosition += new Vector3(-160, 30, 0);

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
                self.currentPlaceObj.transform.localEulerAngles = new Vector3(0, 60, 0);
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
            self.ChgScrollRectMoveStatus(true);
        }

        public static async ETTask ShowMesh(this DlgBattle self)
        {
#if !UNITY_EDITOR
            return;
#endif
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