using ET.Ability;
using ET.AbilityConfig;
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
            if (Ability.UnitHelper.ChkIsPlayer(unit))
            {
                resName = ResUnitCfgCategory.Instance.Get(unit.model.ResId).ResName;
            }
            else if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                resName = ResUnitCfgCategory.Instance.Get(unit.GetComponent<BulletObj>().model.ResId).ResName;
            }
            else
            {
                resName = ResUnitCfgCategory.Instance.Get(unit.model.ResId).ResName;
            }
            // Unit View层
            GameObject prefab = await ResComponent.Instance.LoadAssetAsync<GameObject>(resName);
	        Log.Debug(" AfterUnitCreate_CreateUnitView after LoadAssetAsync 1");
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            Log.Debug($" AfterUnitCreate_CreateUnitView after LoadAssetAsync 2 0 {prefab} {go}");
            go.transform.position = unit.Position;
            unit.AddComponent<GameObjectComponent>().GameObject = go;
            Log.Debug($" AfterUnitCreate_CreateUnitView after LoadAssetAsync 3 0 {prefab} {go}");
            unit.AddComponent<AnimatorComponent>();
            Log.Debug(" AfterUnitCreate_CreateUnitView after LoadAssetAsync 4");
            unit.AddComponent<ET.Ability.Client.EffectShowComponent>();
            Log.Debug(" AfterUnitCreate_CreateUnitView after LoadAssetAsync 5");
            await ETTask.CompletedTask;
        }
    }
}