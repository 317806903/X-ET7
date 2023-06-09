using System.Collections.Generic;

namespace ET.Ability
{
	public class Action_CreateAoE: IActionHandler
	{

		public override async ETTask Run(Unit unit, string actionId, float delayTime, SelectHandle selectHandle, ActionContext actionContext)
		{
			if (delayTime > 0)
			{
				await TimerComponent.Instance.WaitTillAsync(TimeHelper.ClientFrameTime() + (long)(1000 * delayTime));
			}
			await ETTask.CompletedTask;
		}
	}
}


