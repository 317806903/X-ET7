using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectComponent>
        {
            protected override void Awake(GameObjectComponent self)
            {
                Unit unit = self.GetUnit();
                string resName = "";
                float resScale = 1f;
                if (Ability.UnitHelper.ChkIsBullet(unit))
                {
                    BulletObj bulletObj = unit.GetComponent<BulletObj>();
                    resName = bulletObj.model.ResId_Ref.ResName;
                    resScale = bulletObj.model.ResScale;
                }
                else if (Ability.UnitHelper.ChkIsAoe(unit))
                {
                    AoeObj aoeObj = unit.GetComponent<AoeObj>();
                    resName = aoeObj.model.ResId_Ref.ResName;
                    resScale = aoeObj.model.ResScale;
                }
                else
                {
                    resName = unit.model.ResId_Ref.ResName;
                    resScale = unit.model.ResScale;
                }

                // Unit View层
                if (string.IsNullOrEmpty(resName) == false)
                {
                    GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);
                    go.transform.SetParent(GlobalComponent.Instance.Unit);
                    go.transform.position = unit.Position;
                    go.transform.forward = unit.Forward;
                    go.transform.localScale = Vector3.one * resScale;

                    ET.Client.GameObjectPoolHelper.TrigFromPool(go);

                    self.SetGo(go);

                }

            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectComponent>
        {
            protected override void Destroy(GameObjectComponent self)
            {
                if (self.gameObject != null)
                {
                    //UnityEngine.Object.Destroy(self.gameObject);
                    GameObjectPoolHelper.ReturnTransformToPool(self.gameObject.transform);
                    self.gameObject = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<GameObjectComponent>
        {
            protected override void Update(GameObjectComponent self)
            {
                if (self.gameObject == null)
                {
                    return;
                }
                Transform transform = self.gameObject.transform;
                Unit unit = self.GetUnit();

                // transform.position = Vector3.Lerp(transform.position, self.GetUnit().Position, Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, self.GetUnit().Position, 1);
                //if (false && ((Vector3)unit.Position - transform.position).sqrMagnitude < 0.01f)
                if (math.abs(unit.Position.x - transform.position.x) < 0.0005f
                    && math.abs(unit.Position.z - transform.position.z) < 0.0005f
                    )
                {
                    transform.position = unit.Position;
                }
                else if (false && Ability.UnitHelper.ChkIsBullet(unit))
                {
                    transform.position = Vector3.Lerp(transform.position, unit.Position, 0.8f);
                }
                else
                {
                    float moveSpeed = ET.Ability.UnitHelper.GetMoveSpeed(unit);
                    if (moveSpeed == 0)
                    {
                        transform.position = unit.Position;
                    }
                    else
                    {
                        Vector3 dis = transform.position - (Vector3)unit.Position;
                        if (dis.sqrMagnitude > moveSpeed * moveSpeed)
                        {
                            if (dis.sqrMagnitude > moveSpeed * moveSpeed * 2 * 2)
                            {
                                transform.position = unit.Position;
                                ET.Client.UnitHelper.SendGetNumericUnit(unit);
                            }
                            else
                            {
                                float timeToCompleteMove = dis.magnitude / moveSpeed;
                                float donePercentageMove = Mathf.Min(1f, 2 * Time.deltaTime / timeToCompleteMove);
                                transform.position = Vector3.Lerp(transform.position, unit.Position, donePercentageMove);
                            }
                        }
                        else if (dis.sqrMagnitude > moveSpeed * moveSpeed * Time.deltaTime * Time.deltaTime)
                        {
                            float timeToCompleteMove = dis.magnitude / moveSpeed;
                            float donePercentageMove = Mathf.Min(1f, Time.deltaTime / timeToCompleteMove);
                            transform.position = Vector3.Lerp(transform.position, unit.Position, donePercentageMove);
                        }
                        else
                        {
                            transform.position = unit.Position;
                        }
                    }
                }

                Quaternion targetRotation = unit.Rotation;
                float rotationSpeed = ET.Ability.UnitHelper.GetRotationSpeed(unit);
                if (rotationSpeed <= 0)
                {
                    transform.rotation = targetRotation;
                }
                else
                {
                    float angle = Quaternion.Angle(transform.rotation, targetRotation);
                    float timeToComplete = angle / rotationSpeed;
                    float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, donePercentage);
                    //transform.rotation = targetRotation;
                }
            }
        }

        public static void SetGo(this GameObjectComponent self, GameObject go)
        {
            self.gameObject = go;

            long myPlayerId = PlayerHelper.GetMyPlayerId(self.DomainScene());
            int gameObjectShowType = ET.Ability.BuffHelper.GetGameObjectShowType(self.GetUnit(), myPlayerId);

#if ENABLE_VIEW && UNITY_EDITOR
            ReferenceCollector referenceCollector = self.gameObject.GetComponent<ReferenceCollector>();
            if (referenceCollector == null)
            {
                referenceCollector = self.gameObject.AddComponent<ReferenceCollector>();
            }
            referenceCollector.Clear();
            referenceCollector.Add("EntityViewGO", self.GetUnit().viewGO);
#endif
        }

        public static GameObject GetGo(this GameObjectComponent self)
        {
            return self.gameObject;
        }

        public static Unit GetUnit(this GameObjectComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ChgColor(this GameObjectComponent self, bool isMoving)
        {
            TransparentSetter[] tss = self.gameObject.GetComponentsInChildren<TransparentSetter>();
            if (isMoving)
            {
                foreach (var ts in tss)
                {
                    ts.SetTransparent(true, 0.6f);
                }
            }
            else
            {
                foreach (var ts in tss)
                {
                    ts.SetTransparent(false, 1f);
                }
            }
        }

        public static void ChgColorOld(this GameObjectComponent self, bool isMoving)
        {
            if (isMoving)
            {
                Color colorNew = new Color(0.1f, 0.1f, 0.1f);
                self._ChgColor(true, colorNew);
            }
            else
            {
                Color colorNew = Color.black;
                self._ChgColor(false, colorNew);
            }
        }

        public static void _ChgColor(this GameObjectComponent self, bool enableEmission, Color emissionColor)
        {
            MeshRenderer[] rendererList = self.gameObject.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in rendererList)
            {
                foreach (var material in renderer.materials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    // if (material.HasColor("_Color"))
                    // {
                    //     Color color = material.color;
                    //     material.color = new Color(colorNew.r, colorNew.g, colorNew.b, color.a);
                    // }
                    if (enableEmission)
                    {
                        material.EnableKeyword("_EMISSION");
                        material.SetColor("_EmissionColor", emissionColor);
                    }
                    else
                    {
                        material.DisableKeyword("_EMISSION");
                    }
                }
            }

            SkinnedMeshRenderer[] rendererList2 = self.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (SkinnedMeshRenderer renderer in rendererList2)
            {
                foreach (var material in renderer.materials)
                {
                    if (material == null)
                    {
                        continue;
                    }
                    // if (material.HasColor("_Color"))
                    // {
                    //     Color color = material.color;
                    //     material.color = new Color(emissionColor.r, emissionColor.g, emissionColor.b, color.a);
                    // }
                    if (enableEmission)
                    {
                        material.EnableKeyword("_EMISSION");
                        material.SetColor("_EmissionColor", emissionColor);
                    }
                    else
                    {
                        material.DisableKeyword("_EMISSION");
                    }
                }
            }
        }
    }
}