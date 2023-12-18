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
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}

			ET.Ability.AudioPlayHelper.DoAudioPlay(unit, actionId, selectHandle, ref actionContext);

			await ETTask.CompletedTask;
		}
	}
}
