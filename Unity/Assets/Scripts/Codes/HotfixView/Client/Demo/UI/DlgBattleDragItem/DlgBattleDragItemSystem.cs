using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.BattleDragItemFrameTimer)]
    public class DlgBattleDragItemTimer: ATimer<DlgBattleDragItem>
    {
        protected override void Run(DlgBattleDragItem self)
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

	[FriendOf(typeof(DlgBattleDragItem))]
	public static class DlgBattleDragItemSystem
	{
		public static void RegisterUIEvent(this DlgBattleDragItem self)
		{
		}

		public static void ShowWindow(this DlgBattleDragItem self, ShowWindowData contextData = null)
        {
            DlgBattleDragItem_ShowWindowData showWindowData = contextData as DlgBattleDragItem_ShowWindowData;
            if (showWindowData == null)
            {
                return;
            }
            self.battleDragItemType = showWindowData.battleDragItemType;
            self.battleDragItemParam = showWindowData.battleDragItemParam;
            self.moveTowerUnitId = showWindowData.moveTowerUnitId;
            self.countOnce = showWindowData.countOnce;
            self.createActionIds = showWindowData.createActionIds;
            self.callBack = showWindowData.callBack;
            self.sceneIn = showWindowData.sceneIn;

            self.isConfirming = false;
            self.isClickUGUI = false;
            self.isDragging = false;
            self.isCliffy = false;
            self.isRaycast = false;
            self.isChkPutMonsterCall = true;
            self.canPutMonsterCall = true;

            self._groundLayerMask = LayerMask.GetMask("Map");
            self.Timer = TimerComponent.Instance.NewFrameTimer(TimerInvokeType.BattleDragItemFrameTimer, self);

            self.View.E_TipNodeImage.SetVisible(false);
		}

        public static void InitPrefab(this DlgBattleDragItem self)
        {
            switch (self.battleDragItemType)
            {
                case BattleDragItemType.PKTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMonster:
                    self.OnSelectMonster(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMoveTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.PKMovePlayer:
                    self.OnSelectPlayer(self.battleDragItemParam);
                    break;
                case BattleDragItemType.HeadQuarter:
                    self.battleDragItemParam = "Unit_HeadQuarter";
                    self.OnSelectHeadQuarter(self.battleDragItemParam);
                    break;
                case BattleDragItemType.MonsterCall:
                    self.battleDragItemParam = "Unit_MonsterCall";
                    self.OnSelectMonsterCall(self.battleDragItemParam);
                    break;
                case BattleDragItemType.Tower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                case BattleDragItemType.MoveTower:
                    self.OnSelectTower(self.battleDragItemParam);
                    break;
                default:
                    self.CheckIfPlaceSuccess();
                    break;
            }
        }

		public static void Update(this DlgBattleDragItem self)
		{
            if (self.currentPlaceObj == null)
            {
                self.InitPrefab();
                return;
            }

            if (self.isConfirming)
            {
                return;
            }

            if (self.battleDragItemType == BattleDragItemType.Tower)
            {
                if (self.isDragging)
                {
                    ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), true);
                }
            }
            else
            {
                ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), true);
            }

            if (self.CheckUserInput())
            {
                self.isClickUGUI = ET.UGUIHelper.IsClickUGUI();
                // if (self.isClickUGUI)
                // {
                //     return;
                // }

                self.isDragging = true;
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
                if (!self.ChkCanPut(self.currentPlaceObj.transform.position))
                {
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.BattleForbidden);
                }
                if (self.isClickUGUI == false)
                {
                    if (self.isRaycast == false)
                    {
                        //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                        //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }
                    if (self.isCliffy)
                    {
                        //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                        //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                        self.CheckIfPlaceSuccess();
                        return;
                    }

                    var position = self.currentPlaceObj.transform.position;
                    if (self.battleDragItemType == BattleDragItemType.PKTower)
                    {
                        self.DoPutPKTower(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMonster)
                    {
                        self.DoPutPKMonster(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMoveTower)
                    {
                        self.DoPutPKMoveTower(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.PKMovePlayer)
                    {
                        self.DoPutPKMovePlayer(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.HeadQuarter)
                    {
                        self.DoPutHome(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.MonsterCall)
                    {
                        self.DoPutMonsterCall(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.Tower)
                    {
                        self.DoPutOwnTower(position).Coroutine();
                    }
                    else if (self.battleDragItemType == BattleDragItemType.MoveTower)
                    {
                        self.DoPutMoveTower(position).Coroutine();
                    }
                }

                self.CheckIfPlaceSuccess();
            }
            else if (self.currentPlaceObj != null)
            {
                self.CheckIfPlaceSuccess();
            }
		}

        public static string GetUnitPrefabName(this DlgBattleDragItem self, UnitCfg unitCfg)
        {
            ResUnitCfg resUnitCfg = ResUnitCfgCategory.Instance.Get(unitCfg.ResId);
            return resUnitCfg.ResName;
        }

        public static void OnSelectHeadQuarter(this DlgBattleDragItem self, string headQuarterUnitCfgId)
        {
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(headQuarterUnitCfgId);
            float resScale = unitCfg.ResScale;

            if (self.currentPlaceObj != null)
            {
                GameObject.DestroyImmediate(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.transform.localScale = Vector3.one * resScale;
            self.currentPlaceObj.SetActive(false);

        }

        public static void OnSelectMonsterCall(this DlgBattleDragItem self, string monsterCallUnitCfgId)
        {
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(monsterCallUnitCfgId);
            float resScale = unitCfg.ResScale;

            if (self.currentPlaceObj != null)
            {
                GameObject.DestroyImmediate(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }
            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.transform.localScale = Vector3.one * resScale;
            self.currentPlaceObj.SetActive(false);

        }

        public static void OnSelectTower(this DlgBattleDragItem self, string towerCfgId)
        {
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

                if (isTower)
                {
                    ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_TowerShow");
                    GameObject resEffectGo = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);

                    GameObject resEffectGoTmp = GameObject.Instantiate(resEffectGo);
                    resEffectGoTmp.transform.SetParent(self.currentPlaceObj.transform);
                    resEffectGoTmp.transform.localPosition = Vector3.zero;
                    resEffectGoTmp.transform.localScale = Vector3.one * resScale;

                    Transform attackAreaTran = resEffectGoTmp.transform.Find("AttackArea");
                    Transform defaultShowTran = resEffectGoTmp.transform.Find("DefaultShow");
                    for (int i1 = 0; i1 < resEffectGoTmp.transform.childCount; i1++)
                    {
                        Transform child = resEffectGoTmp.transform.GetChild(i1);
                        if (child == attackAreaTran || child == defaultShowTran)
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                    float skillDis = ET.Ability.UnitHelper.GetMaxSkillDis(unitCfg, ET.AbilityConfig.SkillSlotType.NormalAttack);
                    attackAreaTran.localScale = Vector3.one * skillDis*2 / resScale;

                    long playerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                    GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(self.DomainScene());
                    float3 colorValue = gamePlayComponent.GetPlayerColor(playerId);
                    Color color = new Color(colorValue.x, colorValue.y, colorValue.z);
                    ParticleSystem[] psList = attackAreaTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }
                    psList = defaultShowTran.gameObject.GetComponentsInChildren<ParticleSystem>(true);
                    foreach (ParticleSystem particleSystem in psList)
                    {
                        ParticleSystem.MainModule mainModule = particleSystem.main;
                        float alpha = mainModule.startColor.color.a;
                        color.a = alpha;
                        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
                    }

                }
            }

            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectMonster(this DlgBattleDragItem self, string monsterCfgId)
        {
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(monsterCfg.UnitId);
            float resScale = unitCfg.ResScale;

            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.transform.localScale = Vector3.one * resScale;
            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectPlayer(this DlgBattleDragItem self, string monsterCfgId)
        {
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get("Unit_PlayerPK");
            float resScale = unitCfg.ResScale;

            string pathName = self.GetUnitPrefabName(unitCfg);
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>(pathName);

            self.currentPlaceObj = GameObject.Instantiate(go);
            self.currentPlaceObj.transform.localScale = Vector3.one * resScale;
            self.currentPlaceObj.gameObject.SetActive(false);
        }

        public static void OnSelectMoveTower(this DlgBattleDragItem self, string moveTowerUnitId)
        {
        }

        public static void ChgCurrentPlaceObj(this DlgBattleDragItem self, bool canPut)
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
                    if (material == null)
                    {
                        continue;
                    }
                    if (material.HasColor("_Color"))
                    {
                        Color color = material.color;
                        material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                    }
                }
            }
            SkinnedMeshRenderer[] rendererList2 = self.currentPlaceObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (SkinnedMeshRenderer renderer in rendererList2)
            {
                foreach (var material in renderer.materials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    if (material.HasColor("_Color"))
                    {
                        Color color = material.color;
                        material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                    }
                }
            }
        }

        public static bool ChkCanPut(this DlgBattleDragItem self, Vector3 position)
        {
            if (self.isClickUGUI)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnUI");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isRaycast == false)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsRaycast");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.isCliffy)
            {
                string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsCliffy");
                self.ShowPutTipMsg(tipMsg);
                return false;
            }

            if (self.battleDragItemType == BattleDragItemType.MonsterCall)
            {
                if (self.canPutMonsterCall == false)
                {
                    string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsReachHome");
                    self.ShowPutTipMsg(tipMsg);
                    return false;
                }
            }
            else if (self.battleDragItemType == BattleDragItemType.Tower)
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.battleDragItemParam);
                float radius = 0;
                if (towerCfg.Radius <= 0)
                {
                    UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                    radius = unitCfg.BodyRadius * unitCfg.ResScale;
                }
                else
                {
                    radius = towerCfg.Radius;
                }

                bool isTower = towerCfg.Type is PlayerTowerType.Tower;
                bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
                if (gamePlayTowerDefenseComponent != null)
                {
                    (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.battleDragItemParam);
                    if (bRet == false)
                    {
                        string tipMsg = msg;
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }

                if (isTower)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }

                    if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }
                if (isCallMonster)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position) == false)
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNotNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }
            }
            else if (self.battleDragItemType == BattleDragItemType.MoveTower)
            {
                TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.battleDragItemParam);
                float radius = 0;
                if (towerCfg.Radius <= 0)
                {
                    UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                    radius = unitCfg.BodyRadius * unitCfg.ResScale;
                }
                else
                {
                    radius = towerCfg.Radius;
                }

                bool isTower = towerCfg.Type is PlayerTowerType.Tower;
                bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());

                if (isTower)
                {
                    if (self.ChkIsNearTower(self.battleDragItemParam, position))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }

                    if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                    {
                        string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                        self.ShowPutTipMsg(tipMsg);
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ChkIsNearTower(this DlgBattleDragItem self, string towerCfgId, float3 pos)
        {
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            float3 targetPos = pos;
            float radius = 0;
            if (towerCfg.Radius <= 0)
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                radius = unitCfg.BodyRadius * unitCfg.ResScale;
            }
            else
            {
                radius = towerCfg.Radius;
            }

            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return true;
            }

            if (isTower)
            {
                if (self.battleDragItemType == BattleDragItemType.MoveTower)
                {
                    return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius, -1, self.moveTowerUnitId);
                }
                else
                {
                    return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius, -1);
                }
            }
            if (isCallMonster)
            {
                long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
                return gamePlayTowerDefenseComponent.ChkIsNearTower(targetPos, radius + 0.3f, myPlayerId);
            }
            return false;
        }

        public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return gamePlayTowerDefenseComponent;
        }

        /// <summary>
        ///检测用户当前输入
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserInput(this DlgBattleDragItem self)
        {
            return Input.GetMouseButton(0);
        }

        public static void ShowPutTipMsg(this DlgBattleDragItem self, string tipMsg)
        {
            self.View.E_TipNodeImage.SetVisible(true);
            self.View.E_TipTextTextMeshProUGUI.text = tipMsg;
        }

        public static void HidePutTipMsg(this DlgBattleDragItem self)
        {
            self.View.E_TipNodeImage.SetVisible(false);
        }

        public static async ETTask<bool> DoPutHome(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            self.isConfirming = true;
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattlePut_IsOK");
            /*ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self.isConfirming = false;
                ET.Client.GamePlayTowerDefenseHelper.SendPutHome(self.ClientScene(), self.battleDragItemParam, position).Coroutine();
                self.CheckIfPlaceSuccess();
            }, () =>
            {
                self.isConfirming = false;
                self.CheckIfPlaceSuccess();
            });*/
            self.isConfirming = false;

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            ET.Client.GamePlayTowerDefenseHelper.SendPutHome(self.ClientScene(), self.battleDragItemParam, position).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutMonsterCall(this DlgBattleDragItem self, float3 position)
        {
            if (self.isChkPutMonsterCall == false)
            {
                return false;
            }
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            if (self.canPutMonsterCall == false)
            {
                //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsReachHome");
                //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return false;
            }

            self.isConfirming = true;
            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BattlePut_IsOK");
            /*ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msg, () =>
            {
                self.isConfirming = false;
                ET.Client.GamePlayTowerDefenseHelper.SendPutMonsterCall(self.ClientScene(), self.battleDragItemParam, position).Coroutine();
                self.CheckIfPlaceSuccess();
            }, () =>
            {
                self.isConfirming = false;
                self.CheckIfPlaceSuccess();
            });*/
            self.isConfirming = false;

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            ET.Client.GamePlayTowerDefenseHelper.SendPutMonsterCall(self.ClientScene(), self.battleDragItemParam, position).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKTower(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            ET.Client.GamePlayPKHelper.SendCallTower(self.ClientScene(), self.battleDragItemParam, position, self.createActionIds).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMonster(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);
            int count = self.countOnce;
            if (count > 50)
            {
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeUITip()
                {
                    tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Tip_TooMuch"),
                });
                return false;
            }
            ET.Client.GamePlayPKHelper.SendCallMonster(self.ClientScene(), self.battleDragItemParam, position, count, self.createActionIds).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutOwnTower(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkCallPlayerTower(myPlayerId, self.battleDragItemParam);
            if (bRet == false)
            {
                //string tipMsg = msg;
                //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return false;
            }

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.battleDragItemParam);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

            float radius = 0;
            if (towerCfg.Radius <= 0)
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                radius = unitCfg.BodyRadius * unitCfg.ResScale;
            }
            else
            {
                radius = towerCfg.Radius;
            }

            if (isTower)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return false;
                }

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return false;
                }
            }

            if (isCallMonster)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position) == false)
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNotNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return false;
                }
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayTowerDefenseHelper.SendCallOwnTower(self.ClientScene(), self.battleDragItemParam, position).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutMoveTower(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Forbidden);

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            (bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkMovePlayerTower(myPlayerId, self.moveTowerUnitId, position);
            if (bRet == false)
            {
                string tipMsg = msg;
                ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                self.CheckIfPlaceSuccess();
                return false;
            }

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(self.battleDragItemParam);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;

            float radius = 0;
            if (towerCfg.Radius <= 0)
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(towerCfg.UnitId[0]);
                radius = unitCfg.BodyRadius * unitCfg.ResScale;
            }
            else
            {
                radius = towerCfg.Radius;
            }

            if (isTower)
            {
                if (self.ChkIsNearTower(self.battleDragItemParam, position))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsNearTower");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return false;
                }

                if (ET.Client.PathLineRendererComponent.Instance.ChkIsHitPath(position, radius))
                {
                    //string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Battle_ChkPutMesh_IsOnRoad");
                    //ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                    self.CheckIfPlaceSuccess();
                    return false;
                }
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayTowerDefenseHelper.SendMovePlayerTower(self.ClientScene(), self.moveTowerUnitId, position).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMoveTower(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayPKHelper.SendMovePKTower(self.ClientScene(), self.moveTowerUnitId, position).Coroutine();
            return true;
        }

        public static async ETTask<bool> DoPutPKMovePlayer(this DlgBattleDragItem self, float3 position)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.TowerPush);

            ET.Client.GamePlayPKHelper.SendMovePKPlayer(self.ClientScene(), self.moveTowerUnitId, position).Coroutine();
            return true;
        }

        /// <summary>
        ///让当前对象跟随鼠标移动
        /// </summary>
        public static void MoveCurrentPlaceObj(this DlgBattleDragItem self)
        {
            if (self.currentPlaceObj == null)
            {
                return;
            }
            Vector3 screenPosition = Input.mousePosition;
            screenPosition += new Vector3(-100, 20, 0);

            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(screenPosition);
            RaycastHit hitInfo;
            Vector3 point = Vector3.zero;
            self.isRaycast = false;
            self.isCliffy = false;
            if (Physics.Raycast(ray, out hitInfo, 10000, self._groundLayerMask))
            {
                self.isRaycast = true;
                point = hitInfo.point;

                Vector3 normal = hitInfo.normal;
                //大概是66.6度
                if (Vector3.Dot(normal, Vector3.up) < 0.5f)
                {
                    float totalHeight = 0;
                    int hitCount = 0;
                    float hitPointHeight = 0;
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0.5f, 0, 0));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(-0.5f, 0, 0));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0, 0, 0.5f));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }
                    hitPointHeight = self.GetHitPointHeight(ray.origin + new Vector3(0, 0, -0.5f));
                    if (hitPointHeight != 0)
                    {
                        hitCount++;
                    }

                    if (math.abs(totalHeight / hitCount - point.y) > 0.5f)
                    {
                        self.isCliffy = true;
                    }
                }
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

            self.DrawMonsterCall2HeadQuarter().Coroutine();
        }

        public static float GetHitPointHeight(this DlgBattleDragItem self, Vector3 startPos)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(startPos, Vector3.down, out hitInfo, 10000, self._groundLayerMask))
            {
                return hitInfo.point.y;
            }
            return 0;
        }

        public static async ETTask DrawMonsterCall2HeadQuarter(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }
            GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
            if (gamePlayTowerDefenseStatus != GamePlayTowerDefenseStatus.PutMonsterPoint)
            {
                return;
            }

            if (self.newShowLineRendererTime > TimeHelper.ClientNow())
            {
                return;
            }

            self.newShowLineRendererTime = TimeHelper.ClientNow() + 100;

            if (self.currentPlaceObj.gameObject.activeSelf == false)
            {
                await self._HideMonsterCall2HeadQuarter();
                return;
            }

            float disX = math.abs(self.lineRendererPos.x - self.currentPlaceObj.transform.position.x);
            float disZ = math.abs(self.lineRendererPos.z - self.currentPlaceObj.transform.position.z);
            if (disX > 0.3f || disZ > 0.3f)
            {
                self.lineRendererPos = self.currentPlaceObj.transform.position;
                self.canShowLineRendererNear = true;
                return;
            }
            if (disX < 0.1f && disZ < 0.1f && self.canShowLineRendererNear == false)
            {
                return;
            }
            self.lineRendererPos = self.currentPlaceObj.transform.position;
            self.canShowLineRendererNear = false;

            if (self.lineRendererReqing)
            {
                return;
            }

            self.lineRendererReqing = true;
            bool canArrive = await self._DrawMonsterCall2HeadQuarter(self.lineRendererPos);
            if (canArrive)
            {
                self.canPutMonsterCall = true;
            }
            else
            {
                self.canPutMonsterCall = false;
            }
            self.lineRendererReqing = false;
            self.isChkPutMonsterCall = true;
        }

        public static async ETTask<bool> _DrawMonsterCall2HeadQuarter(this DlgBattleDragItem self, float3 pos)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            return await gamePlayTowerDefenseComponent.DoDrawMyMonsterCall2HeadQuarter(pos);
        }

        public static async ETTask _HideMonsterCall2HeadQuarter(this DlgBattleDragItem self)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = Client.GamePlayHelper.GetGamePlayTowerDefense(self.DomainScene());
            await gamePlayTowerDefenseComponent.DoHideMyMonsterCall2HeadQuarter();
        }

        /// <summary>
        ///检测是否放置成功
        /// </summary>
        public static void CheckIfPlaceSuccess(this DlgBattleDragItem self)
        {
            if (self.IsDisposed)
            {
                return;
            }

            if (self.isConfirming)
            {
                return;
            }

            ET.Client.ARSessionHelper.ShowARMesh(self.DomainScene(), false);
            self.isConfirming = false;
            self.isDragging = false;
            if (self.currentPlaceObj != null)
            {
                GameObject.Destroy(self.currentPlaceObj);
                self.currentPlaceObj = null;
            }

            try
            {
                self.callBack?.Invoke(self.sceneIn);
                self.callBack = null;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            self.sceneIn = null;

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBattleDragItem>();
        }

        public static void HideWindow(this DlgBattleDragItem self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

	}
}
