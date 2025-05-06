using System;
using ET.AbilityConfig;
using UnityEngine;
using ET.Client;
using Unity.Mathematics;

namespace ET.Ability.Client
{
	[FriendOf(typeof(AnimatorShowComponent))]
	public static class AnimatorShowComponentSystem
	{
		[ObjectSystem]
		public class AnimatorShowComponentAwakeSystem : AwakeSystem<AnimatorShowComponent>
		{
			protected override void Awake(AnimatorShowComponent self)
			{
				self.Awake().Coroutine();
			}
		}

		[ObjectSystem]
		public class AnimatorShowComponentUpdateSystem : UpdateSystem<AnimatorShowComponent>
		{
			protected override void Update(AnimatorShowComponent self)
			{
				self.Update();
			}
		}

		[ObjectSystem]
		public class AnimatorShowComponentDestroySystem : DestroySystem<AnimatorShowComponent>
		{
			protected override void Destroy(AnimatorShowComponent self)
			{
				self.animationClips = null;
				self.Parameter = null;
				self.mainAnimator = null;
				self.curRecordAnimatorName = null;
			}
		}

		public static async ETTask Awake(this AnimatorShowComponent self)
		{
			bool bRet = await ET.Client.UnitViewHelper.ChkGameObjectShowReady(self, self.GetUnit());
			if (bRet == false)
			{
				return;
			}
			GameObjectShowComponent gameObjectShowComponent = self.GetUnit().GetComponent<GameObjectShowComponent>();

			RecordAnimatorName recordAnimatorName = gameObjectShowComponent.GetUnitResRoot().GetComponent<RecordAnimatorName>();
			if (recordAnimatorName == null)
			{
				recordAnimatorName = gameObjectShowComponent.GetUnitResRoot().GetComponentInChildren<RecordAnimatorName>();
			}

			if (recordAnimatorName == null)
			{
				return;
			}

			Animator animator = recordAnimatorName.gameObject.GetComponent<Animator>();
			if (animator == null)
			{
				return;
			}

			if (animator.runtimeAnimatorController == null)
			{
				return;
			}

			if (animator.runtimeAnimatorController.animationClips == null)
			{
				return;
			}

			self.curRecordAnimatorName = recordAnimatorName;

			self.mainAnimator = animator;
			var list = gameObjectShowComponent.GetUnitResRoot().GetComponentsInChildren<RecordAnimatorName>();
			if (list.Length > 0)
			{
				for (int i = 0; i < list.Length; i++)
				{
					if (list[i] == self.curRecordAnimatorName)
					{
						continue;
					}
					if (list[i].gameObject.GetComponent<Animator>() == null)
					{
						continue;
					}

					if (self.animatorOtherList == null)
					{
						self.animatorOtherList = new();
					}
					self.animatorOtherList.Add(list[i].gameObject.GetComponent<Animator>());
				}
			}

			self.CurMotionType = AnimatorMotionName.None;
			self.NextMotionType = AnimatorMotionName.None;

			self.animationClips = new ();
			self.Parameter = new ();
			self.animatorMotion2Length = new ();
			self.animatorMotionIsLoop = new ();

			foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
			{
				if (animationClip == null)
				{
					continue;
				}
				self.animationClips[animationClip.name] = animationClip;
			}


			foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
			{
				self.Parameter.Add(animatorControllerParameter.name, animatorControllerParameter.type);
			}

		}

		public static Unit GetUnit(this AnimatorShowComponent self)
		{
			return self.GetParent<Unit>();
		}

		public static void ResetAnimatorComponent(this AnimatorShowComponent self)
		{
			if (self.animatorComponent == null)
			{
				AnimatorComponent animatorComponent = self.GetUnit().GetComponent<AnimatorComponent>();
				if (animatorComponent != null)
				{
					self.animatorComponent = animatorComponent;
				}
			}
		}

		public static void ChkAnimator(this AnimatorShowComponent self)
		{
			AnimatorComponent animatorComponent = self.animatorComponent;
			if (animatorComponent == null)
			{
				return;
			}

			if (animatorComponent.isOnlySelfShow)
			{
				long playerId = ET.GamePlayHelper.GetPlayerIdByUnitId(self.GetUnit());
				if (playerId != (long)ET.PlayerId.PlayerNone)
				{
					long myPlayerId = ET.Client.PlayerStatusHelper.GetMyPlayerId(self.DomainScene());
					if (myPlayerId != playerId)
					{
						return;
					}
				}
			}

			if (animatorComponent.isStoppingAnimator != self.isStop)
			{
				if (animatorComponent.isStoppingAnimator)
				{
					self.PauseAnimator();
				}
				else
				{
					self.RunAnimator();
				}
			}

			if (self.isStop)
			{
				return;
			}

			AnimatorMotionName nextAnimatorMotionName = AnimatorMotionName.None;
			float motionSpeed = 1f;
			long motionTickTime = 0;
			bool isAnimatorLoop = false;

			if (animatorComponent.controlStateName != AnimatorMotionName.None)
			{
				nextAnimatorMotionName = animatorComponent.controlStateName;
				motionTickTime = animatorComponent.controlAnimatorTickTime;
				isAnimatorLoop = animatorComponent.isControlAnimatorLoop;
			}
			else
			{
				nextAnimatorMotionName = animatorComponent.name;
				animatorComponent.name = AnimatorMotionName.None;
				motionTickTime = animatorComponent.animatorTickTime;
				isAnimatorLoop = animatorComponent.isAnimatorLoop;
			}

			if (self.CurMotionType != AnimatorMotionName.None)
			{
				if (self.ChkIsLoop(self.CurMotionType))
				{
				}
				else
				{
					if (self.ChkIsPlayFinished(self.CurMotionType))
					{
						if (nextAnimatorMotionName == self.CurMotionType)
						{
							if (isAnimatorLoop)
							{
								motionTickTime = 0;
							}
						}
						else
						{
							self.CurMotionType = AnimatorMotionName.None;
						}
					}
				}
			}

			if (nextAnimatorMotionName != AnimatorMotionName.None)
			{
				if (motionTickTime == 0)
				{
				}
				else
				{
					long time = TimeHelper.ServerNow() - motionTickTime;
					float length = self.GetAnimationLength(nextAnimatorMotionName);
					if (time > length * 1000)
					{
						nextAnimatorMotionName = AnimatorMotionName.None;
					}
				}
			}

			if (nextAnimatorMotionName == AnimatorMotionName.None &&
			    ET.Ability.AnimatorHelper.ChkIsIdleOrMove(self.CurMotionType))
			{
				motionTickTime = 0;
				float dis = 0.0001f;
				if (self.CurMotionType == AnimatorMotionName.Idle)
				{
					dis = 0.01f;
				}

				float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
				dis *= clientResScale;

				float3 unitPos = self.GetUnit().Position;
				float3 goPos = self.GetUnit().GetUnitClientPos();
				if (math.abs(unitPos.x - goPos.x) <= dis
				    && math.abs(unitPos.z - goPos.z) <= dis
				   )
				{
					//nextAnimatorMotionName = AnimatorMotionName.Idle;
					nextAnimatorMotionName = AnimatorMotionName.None;
					if (self.chgToIdleTime == -1)
					{
						if (UnitHelper.ChkIsPlayer(self.GetUnit())
						    || UnitHelper.ChkIsCameraPlayer(self.GetUnit())
						    || UnitHelper.ChkIsSkillCaster(self.GetUnit()))
						{
							self.chgToIdleTime = TimeHelper.ServerNow();
						}
						else
						{
							self.chgToIdleTime = TimeHelper.ServerNow() + 1000;
						}
					}
				}
				else
				{
					nextAnimatorMotionName = AnimatorMotionName.Move;

					Unit unit = self.GetUnit();
					if (UnitHelper.ChkIsFly(unit))
					{
						if (self.ChkAnimationExist(AnimatorMotionName.Fly))
						{
							nextAnimatorMotionName = AnimatorMotionName.Fly;
						}
					}
					UnitCfg unitCfg = unit.model;
					ResUnitCfg resUnitCfg = unitCfg.ResId_Ref;
					float curMoveSpeed = UnitHelper.GetMoveSpeed(unit);

					float moveShowNatureSpeed = resUnitCfg.MoveShowNatureSpeed;
					moveShowNatureSpeed *= clientResScale;
					motionSpeed = curMoveSpeed / (moveShowNatureSpeed * unitCfg.ResScale);

					self.chgToIdleTime = -1;
				}
			}
			else
			{
				self.chgToIdleTime = -1;
			}

			if (nextAnimatorMotionName != AnimatorMotionName.None)
			{
				if (nextAnimatorMotionName != self.CurMotionType || motionTickTime == 0)
				{
					self.Play(nextAnimatorMotionName, motionSpeed, motionTickTime);
				}
				else if(Math.Abs(self.mainAnimator.speed - motionSpeed) > 0.01f)
				{
					motionSpeed = (self.mainAnimator.speed + motionSpeed) * 0.5f;
					self.SetAnimatorSpeed(motionSpeed);
				}
			}
		}

		public static void Update(this AnimatorShowComponent self)
		{
			if (self.mainAnimator == null)
			{
				return;
			}

			self.ResetAnimatorComponent();
			self.ChkAnimator();
			self.UpdateNextMotionType();
		}

		public static void UpdateNextMotionType(this AnimatorShowComponent self)
		{
			if (self.isStop)
			{
				return;
			}

			if (self.chgToIdleTime != -1 && self.chgToIdleTime < TimeHelper.ServerNow())
			{
				self.NextMotionType = AnimatorMotionName.Idle;
				self.chgToIdleTime = -1;
			}

			if (self.NextMotionType == AnimatorMotionName.None)
			{
				return;
			}

			if (self.CurMotionType == self.NextMotionType)
			{
				if (self.ChkIsLoop(self.CurMotionType))
				{
					return;
				}
			}

			try
			{
				float crossTime = 0.1f;

				AnimatorMotionName CurMotionTypeOld = self.CurMotionType;
				self.CurMotionType = self.NextMotionType;
				self.NextMotionType = AnimatorMotionName.None;

				float normalizedTime;
				if (self.NextMotionTickTime == 0)
				{
					normalizedTime = 0;
				}
				else
				{
					float length = self.GetAnimationLength(self.CurMotionType);
					if (length > 0)
					{
						long time = TimeHelper.ServerNow() - self.NextMotionTickTime;
						normalizedTime = time*0.001f / length;
						if (normalizedTime > 1)
						{
							if (CurMotionTypeOld == AnimatorMotionName.Idle)
							{
								self.CurMotionType = AnimatorMotionName.Idle;
								return;
							}
							self.CurMotionType = AnimatorMotionName.Idle;
							normalizedTime = 0;
						}
					}
					else
					{
						normalizedTime = 0;
					}
				}

				if (CurMotionTypeOld == AnimatorMotionName.Move
				    || CurMotionTypeOld == AnimatorMotionName.Fly)
				{
					self.mainAnimator.Play(self.CurMotionType.ToString(), 0, normalizedTime);
				}
				else
				{
					self.mainAnimator.ForceCrossFade(self.CurMotionType.ToString(), crossTime, 0, normalizedTime);
				}

				if (self.animatorOtherList != null)
				{
					foreach (Animator animator in self.animatorOtherList)
					{
						if (CurMotionTypeOld == AnimatorMotionName.Move
						    || CurMotionTypeOld == AnimatorMotionName.Fly)
						{
							animator.Play(self.CurMotionType.ToString(), 0, normalizedTime);
						}
						else
						{
							animator.ForceCrossFade(self.CurMotionType.ToString(), crossTime, 0, normalizedTime);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"动作播放失败: {self.CurMotionType}", ex);
			}
		}

		public static void ForceCrossFade(this Animator animator, string name, float transitionDuration, int layer = 0, float normalizedTime = float.NegativeInfinity)
		{
			animator.Update(0);

			int fullPathHash = animator.GetNextAnimatorStateInfo(layer).fullPathHash;
			if (fullPathHash == 0)
			{
				animator.CrossFade(name, transitionDuration, layer, normalizedTime);
			}
			else
			{
				animator.Play(fullPathHash, layer);
				animator.Update(0);
				animator.CrossFade(name, transitionDuration, layer, normalizedTime);
			}
		}

		public static bool HasParameter(this AnimatorShowComponent self, string parameter)
		{
			return self.Parameter.ContainsKey(parameter);
		}

		public static void PlayInTime(this AnimatorShowComponent self, AnimatorMotionName motionType, float time)
		{
			float motionSpeed = 1;
			if (self.curRecordAnimatorName == null)
			{
				throw new Exception($"self.curRecordAnimatorName == null: {self.mainAnimator.name}");
			}
			else
			{
				AnimationClip animationClip;

				string clipName = self.curRecordAnimatorName.GetRecordValue(motionType.ToString());
				if (string.IsNullOrEmpty(clipName))
				{
					throw new Exception($"{self.mainAnimator.name} 找不到该动作: {motionType}");
				}

				if (!self.animationClips.TryGetValue(clipName, out animationClip))
				{
					throw new Exception($"{self.mainAnimator.name} 找不到该动作: {clipName}");
				}

				motionSpeed = animationClip.length / time;
				if (motionSpeed < 0.01f || motionSpeed > 1000f)
				{
					Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
					return;
				}
			}

			self.NextMotionType = motionType;

			self.SetAnimatorSpeed(motionSpeed);
		}

		public static void Play(this AnimatorShowComponent self, AnimatorMotionName motionType, float motionSpeed = 1f, long motionTickTime = 0)
		{
			// AnimationClip animationClip;
			// if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			// {
			// 	throw new Exception($"找不到该动作: {motionType}");
			// }
			self.NextMotionType = motionType;
			self.NextMotionTickTime = motionTickTime;
			self.SetAnimatorSpeed(motionSpeed);
		}

		public static bool ChkIsLoop(this AnimatorShowComponent self, AnimatorMotionName animatorMotionName)
		{
			if (self.animatorMotionIsLoop.TryGetValue(animatorMotionName, out bool isLoop))
			{
				return isLoop;
			}

			if (animatorMotionName == AnimatorMotionName.Idle
			    || animatorMotionName == AnimatorMotionName.Move
			    || animatorMotionName == AnimatorMotionName.Fly)
			{
				self.animatorMotionIsLoop[animatorMotionName] = true;
			}
			else
			{
				AnimatorStateInfo stateInfo = self.mainAnimator.GetCurrentAnimatorStateInfo(0);
				if (stateInfo.IsName(animatorMotionName.ToString()) == false)
				{
					self.animatorMotionIsLoop[animatorMotionName] = false;
				}
				else if (stateInfo.loop)
				{
					self.animatorMotionIsLoop[animatorMotionName] = true;
				}
				else
				{
					self.animatorMotionIsLoop[animatorMotionName] = false;
				}
			}
			return self.animatorMotionIsLoop[animatorMotionName];
		}

		public static bool ChkIsPlayFinished(this AnimatorShowComponent self, AnimatorMotionName animatorMotionName)
		{
			AnimatorStateInfo stateInfo = self.mainAnimator.GetCurrentAnimatorStateInfo(0);
			if (stateInfo.IsName(animatorMotionName.ToString()) == false)
			{
				return true;
			}
			else if (stateInfo.loop)
			{
				return false;
			}
			else
			{
				if (stateInfo.normalizedTime >= 1.0f)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static bool ChkAnimationExist(this AnimatorShowComponent self, AnimatorMotionName animatorMotionName)
		{
			string clipName = self.curRecordAnimatorName.GetRecordValue(animatorMotionName.ToString());
			if (string.IsNullOrEmpty(clipName))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static float GetAnimationLength(this AnimatorShowComponent self, AnimatorMotionName animatorMotionName)
		{
			if (self.animatorMotion2Length.TryGetValue(animatorMotionName, out float length))
			{
				return length;
			}
			AnimationClip animationClip;

			if (self.curRecordAnimatorName == null)
			{
				throw new Exception($"self.curRecordAnimatorName == null: {self.mainAnimator.name}");
			}
			else
			{
				string clipName = self.curRecordAnimatorName.GetRecordValue(animatorMotionName.ToString());
				if (string.IsNullOrEmpty(clipName))
				{
#if UNITY_EDITOR
					Log.Error($"{self.mainAnimator.name} 找不到该动作: {animatorMotionName.ToString()}");
#endif
					self.animatorMotion2Length[animatorMotionName] = 0;
					return 0;
				}

				if (!self.animationClips.TryGetValue(clipName, out animationClip))
				{
#if UNITY_EDITOR
					Log.Error($"{self.mainAnimator.name} 找不到该动作: {clipName}");
#endif
					self.animatorMotion2Length[animatorMotionName] = 0;
					return 0;
				}
				length = animationClip.length;
				self.animatorMotion2Length[animatorMotionName] = length;
				return length;
			}
		}

		/// <summary>
		/// 暂停动作播放
		/// </summary>
		/// <param name="self"></param>
		public static void PauseAnimator(this AnimatorShowComponent self)
		{
			if (self.isStop)
			{
				return;
			}
			self.isStop = true;

			if (self.mainAnimator == null)
			{
				return;
			}
			self.SetAnimatorSpeed(0);
		}

		/// <summary>
		/// 恢复动作播放
		/// </summary>
		/// <param name="self"></param>
		public static void RunAnimator(this AnimatorShowComponent self)
		{
			if (!self.isStop)
			{
				return;
			}

			self.isStop = false;

			if (self.mainAnimator == null)
			{
				return;
			}
			self.ResetAnimatorSpeed();
		}

		public static void SetBoolValue(this AnimatorShowComponent self, string name, bool state)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.mainAnimator.SetBool(name, state);
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.SetBool(name, state);
				}
			}
		}

		public static void SetFloatValue(this AnimatorShowComponent self, string name, float state)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.mainAnimator.SetFloat(name, state);
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.SetFloat(name, state);
				}
			}
		}

		public static void SetIntValue(this AnimatorShowComponent self, string name, int value)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.mainAnimator.SetInteger(name, value);
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.SetInteger(name, value);
				}
			}
		}

		public static void SetTrigger(this AnimatorShowComponent self, string name)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.mainAnimator.SetTrigger(name);
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.SetTrigger(name);
				}
			}
		}

		/// <summary>
		/// 修改动画播放速度
		/// </summary>
		/// <param name="self"></param>
		/// <param name="speed"></param>
		public static void SetAnimatorSpeed(this AnimatorShowComponent self, float speed)
		{
			self.stopSpeed = self.mainAnimator.speed;
			self.mainAnimator.speed = speed;
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.speed = speed;
				}
			}
		}

		/// <summary>
		/// 恢复动画播放速度
		/// </summary>
		/// <param name="self"></param>
		public static void ResetAnimatorSpeed(this AnimatorShowComponent self)
		{
			self.mainAnimator.speed = self.stopSpeed;
			if (self.animatorOtherList != null)
			{
				foreach (Animator animator in self.animatorOtherList)
				{
					animator.speed = self.stopSpeed;
				}
			}
		}
	}
}