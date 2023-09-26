using System;
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
                    resName = ResUnitCfgCategory.Instance.Get(unit.GetComponent<BulletObj>().model.ResId).ResName;
                    resScale = unit.GetComponent<BulletObj>().model.ResScale;
                }
                else if (Ability.UnitHelper.ChkIsAoe(unit))
                {
                    resName = ResUnitCfgCategory.Instance.Get(unit.GetComponent<AoeObj>().model.ResId).ResName;
                    resScale = unit.GetComponent<AoeObj>().model.ResScale;
                }
                else
                {
                    resName = ResUnitCfgCategory.Instance.Get(unit.model.ResId).ResName;
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
                    Vector3 dis = transform.position - (Vector3)unit.Position;
                    if (dis.sqrMagnitude > moveSpeed * moveSpeed * 2 * 2)
                    {
                        transform.position = unit.Position;
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

        }

        public static GameObject GetGo(this GameObjectComponent self)
        {
            return self.gameObject;
        }

        public static Unit GetUnit(this GameObjectComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}