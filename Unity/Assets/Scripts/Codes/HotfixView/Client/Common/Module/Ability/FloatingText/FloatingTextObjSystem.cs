using System;
using System.Collections.Generic;
using DamageNumbersPro;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [FriendOf(typeof (FloatingTextObj))]
    public static class FloatingTextObjSystem
    {
        [ObjectSystem]
        public class FloatingTextObjAwakeSystem: AwakeSystem<FloatingTextObj>
        {
            protected override void Awake(FloatingTextObj self)
            {
            }
        }

        [ObjectSystem]
        public class FloatingTextObjDestroySystem: DestroySystem<FloatingTextObj>
        {
            protected override void Destroy(FloatingTextObj self)
            {
                if (self.go != null)
                {
                    if (self.damageNumber != null)
                    {
                        DamageNumbersPro.Internal.DNPUpdater.UnregisterPopup(self.damageNumber.unscaledTime, self.damageNumber.updateDelay, self.damageNumber);

                        self.damageNumber.enabled = false;
                    }
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }
            }
        }

        [ObjectSystem]
        public class FloatingTextObjFixedUpdate: FixedUpdateSystem<FloatingTextObj>
        {
            protected override void FixedUpdate(FloatingTextObj self)
            {
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask<DamageNumber> _Init(this FloatingTextObj self, ActionCfg_FloatingText _ActionCfg_FloatingText)
        {
            Unit unit = self.GetParent<FloatingTextComponent>()?.GetParent<Unit>();
            if (unit == null)
            {
                Log.Error($"FloatingTextObjSystem.Init unit == null");
            }
            else
            {
                bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, unit);
                if (bRet == false)
                {
                    return null;
                }
                GameObjectShowComponent gameObjectShowComponent = unit.GetComponent<GameObjectShowComponent>();

                string resName = _ActionCfg_FloatingText.ResEffectId_Ref.ResName;
                GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);
                if (go == null)
                {
                    Log.Error($"FloatingTextObjSystem.Init go == null when resName={resName}");
                }
                self.go = go;
                Transform tran = gameObjectShowComponent.GetEffectResScaleRoot().transform;
                go.transform.SetParent(tran);
                go.transform.localScale = UnityEngine.Vector3.one;
                go.transform.localPosition = UnityEngine.Vector3.zero;
                go.transform.localEulerAngles = UnityEngine.Vector3.zero;

                DamageNumber damageNumber = self.go.GetComponentInChildren<DamageNumber>();
                self.damageNumber = damageNumber;
                damageNumber.orthographicCamera = CameraHelper.GetMainCamera(self.DomainScene());
                damageNumber.enabled = true;
                damageNumber.permanent = true;
                damageNumber.Restart();
                damageNumber.enablePooling = false;
                damageNumber.PrewarmPool();
                damageNumber.SetFollowedTarget(tran);
                damageNumber.SetPosition(tran.position + Vector3.up * ET.Ability.UnitHelper.GetBodyHeight(unit) + new Vector3(0, _ActionCfg_FloatingText.VerticalOffset, 0));
                return damageNumber;
            }

            return null;
        }

        public static void FixedUpdate(this FloatingTextObj self, float fixedDeltaTime)
        {
            // if (self.go == null)
            // {
            //     return;
            // }
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;

            if (self.duration <= 0)
            {
                self.Dispose();
            }
        }

        public static async ETTask CreateFloatingText(this FloatingTextObj self, ActionCfg_FloatingText _ActionCfg_FloatingText, int showNum)
        {
            DamageNumber damageNumber = await self._Init(_ActionCfg_FloatingText);

            if (self.IsDisposed || self.go == null)
            {
                return;
            }

            self.timeElapsed = 0;
            self.permanent = _ActionCfg_FloatingText.Duration == -1? true : false;
            self.duration = _ActionCfg_FloatingText.Duration == -1? 1 : _ActionCfg_FloatingText.Duration;

            string leftText = _ActionCfg_FloatingText.LeftText;
            string rightText = _ActionCfg_FloatingText.RightText;
            string topText = _ActionCfg_FloatingText.TopText;
            string bottomText = _ActionCfg_FloatingText.BottomText;

            if (showNum == 0)
            {
                damageNumber.enableNumber = false;
                damageNumber.leftText = leftText;
            }
            else
            {
                damageNumber.enableNumber = true;
                damageNumber.number = showNum;
            }

            if (string.IsNullOrEmpty(leftText) == false)
            {
                damageNumber.enableLeftText = true;
                damageNumber.leftText = leftText;
            }
            else
            {
                damageNumber.enableLeftText = false;
            }

            if (string.IsNullOrEmpty(rightText) == false)
            {
                damageNumber.enableRightText = true;
                damageNumber.rightText = rightText;
            }
            else
            {
                damageNumber.enableRightText = false;
            }

            if (string.IsNullOrEmpty(topText) == false)
            {
                damageNumber.enableTopText = true;
                damageNumber.topText = topText;
            }
            else
            {
                damageNumber.enableTopText = false;
            }

            if (string.IsNullOrEmpty(bottomText) == false)
            {
                damageNumber.enableBottomText = true;
                damageNumber.bottomText = bottomText;
            }
            else
            {
                damageNumber.enableBottomText = false;
            }
        }

    }
}