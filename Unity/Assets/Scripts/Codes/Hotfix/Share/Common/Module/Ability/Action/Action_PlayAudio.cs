using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
	public class Action_PlayAudio: IActionHandler
	{
		public override async ETTask Run(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ServerFrameTime() + (long)(1000 * delayTime));
				if (unit == null || unit.DomainScene() == null || unit.DomainScene().IsDisposed)
				{
					return;
				}
			}

			ET.Ability.AudioPlayHelper.DoAudioPlay(unit, actionId, selectHandle, ref actionContext);

			await ETTask.CompletedTask;
		}
	}
}
