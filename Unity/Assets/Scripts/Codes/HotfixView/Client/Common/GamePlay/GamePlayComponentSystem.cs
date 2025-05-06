using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof (GamePlayComponent))]
    public static class GamePlayComponentSystem
    {
		[ObjectSystem]
		public class GamePlayComponentUpdateSystem : FixedUpdateSystem<GamePlayComponent>
		{
			protected override void FixedUpdate(GamePlayComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Current)
				{
					return;
				}

				self.DoUpdate();
			}
		}

		public static void DoUpdate(this GamePlayComponent self)
		{
			if (++self.curFrameSyncPos >= self.waitFrameSyncPos)
			{
				self.curFrameSyncPos = 0;

				self.SendGetNumericUnit();
			}
		}

		public static void RecordNeedGetNumericUnit(this GamePlayComponent self, Unit unit)
		{
			if (self.RecordSendGetNumericUnit == null)
			{
				self.RecordSendGetNumericUnit = new();
			}

			if (self.RecordSendGetNumericUnit.Contains(unit.Id))
			{
				return;
			}
			self.RecordSendGetNumericUnit.Add(unit.Id);
		}

		public static void SendGetNumericUnit(this GamePlayComponent self)
		{
			if (self.RecordSendGetNumericUnit == null || self.RecordSendGetNumericUnit.Count == 0)
			{
				return;
			}
			C2M_GetNumericUnit _C2M_GetNumericUnit = new ()
			{
				UnitIdList = self.RecordSendGetNumericUnit.ToList(),
				NumericKeyList = new List<int>(){NumericType.Speed},
			};
			self.RecordSendGetNumericUnit.Clear();
			ET.Client.SessionHelper.GetSession(self.DomainScene()).Send(_C2M_GetNumericUnit);
		}

        public static void PlayBattleStartMusic(this GamePlayComponent self)
        {
            Dictionary<string, float> audioList = self.GetGamePlayBattleConfig().MusicList;
            UIAudioManagerComponent _UIAudioManagerComponent = UIAudioManagerHelper.GetUIAudioManagerComponent(self.DomainScene());
            _UIAudioManagerComponent.PlayMusic(audioList);
        }
    }
}
