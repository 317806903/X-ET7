using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, EventType.AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, EventType.AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            string resName = "";
            float resScale = 1f;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                resName = ResUnitCfgCategory.Instance.Get(unit.GetComponent<BulletObj>().model.ResId).ResName;
                resScale = unit.GetComponent<BulletObj>().model.ResScale;
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
                unit.AddComponent<GameObjectComponent>().GameObject = go;

                if (Ability.UnitHelper.ChkIsPlayer(unit) || Ability.UnitHelper.ChkIsActor(unit))
                {
                    //GameObject HealthBarPrefab = await ResComponent.Instance.LoadAssetAsync<GameObject>("HealthBar");
                    //GameObject HealthBarGo = UnityEngine.Object.Instantiate(HealthBarPrefab, go.transform, true);
                    GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool("HealthBar",true,10);
                    HealthBarGo.transform.SetParent(go.transform);
                    HealthBarGo.transform.localPosition = new float3(0, 5, 0);
                    HealthBarGo.transform.localScale = Vector3.one;
                    unit.AddComponent<HealthBarComponent, GameObject>(HealthBarGo);
                }
                unit.AddComponent<AnimatorComponent>();
            }
            unit.AddComponent<ET.Ability.Client.EffectShowComponent>();
            await ETTask.CompletedTask;
        }
    }
}