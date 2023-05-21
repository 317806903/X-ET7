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
                resName = "Character1";
            }
            else if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                resName = "Bullet1";
            }
            else
            {
                resName = "Monster1";
            }
            // Unit View层
            GameObject prefab = await ResComponent.Instance.LoadAssetAsync<GameObject>(resName);
	        
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            go.transform.position = unit.Position;
            unit.AddComponent<GameObjectComponent>().GameObject = go;
            unit.AddComponent<AnimatorComponent>();
            await ETTask.CompletedTask;
        }
    }
}