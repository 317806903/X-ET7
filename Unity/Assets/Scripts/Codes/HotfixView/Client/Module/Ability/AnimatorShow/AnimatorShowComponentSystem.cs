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
				self.Awake();
			}
		}

		[ObjectSystem]
		public class AnimatorShowComponentUpdateSystem : UpdateSystem<AnimatorShowComponent>
		{
			protected override void Update(AnimatorShowComponent self)
			{
				self.ChkAnimator();
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
				self.Animator = null;
			}
		}

		public static void Awake(this AnimatorShowComponent self)
		{
			Animator animator = null;
			GameObjectComponent gameObjectComponent = self.GetUnit().GetComponent<GameObjectComponent>();
			if (gameObjectComponent != null && gameObjectComponent.GetGo() != null)
			{
				animator = gameObjectComponent.GetGo().GetComponent<Animator>();
			}

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
			self.Animator = animator;

			self.CurMotionType = AnimatorMotionName.None;
			self.NextMotionType = AnimatorMotionName.None;

			self.animationClips = new ();
			self.Parameter = new ();

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

		public static void ChkAnimator(this AnimatorShowComponent self)
		{
			if (self.Animator == null)
			{
				return;
			}

			AnimatorComponent animatorComponent;
			if (self.animatorComponent == null)
			{
				animatorComponent = self.GetUnit().GetComponent<AnimatorComponent>();
				if (animatorComponent != null)
				{
					self.animatorComponent = animatorComponent;
				}
			}
			else
			{
				animatorComponent = self.animatorComponent;
			}

			if (self.CurMotionType != AnimatorMotionName.None)
			{
				if (self.CurMotionType == AnimatorMotionName.Idle || self.CurMotionType == AnimatorMotionName.Move)
				{
				}
				else
				{
					AnimatorStateInfo stateInfo = self.Animator.GetCurrentAnimatorStateInfo(0);
					if (stateInfo.IsName(self.CurMotionType.ToString()) == false)
					{
						self.CurMotionType = AnimatorMotionName.None;
					}
					else if (stateInfo.loop == false && stateInfo.normalizedTime >= 0.95f)
					{
						self.CurMotionType = AnimatorMotionName.None;
					}
				}
			}

			if (animatorComponent != null)
			{
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
			}

			AnimatorMotionName nextAnimatorMotionName = AnimatorMotionName.None;
			float motionSpeed = 1f;
			if (animatorComponent != null)
			{
				if (animatorComponent.controlStateName != AnimatorMotionName.None)
				{
					nextAnimatorMotionName = animatorComponent.controlStateName;
				}
				else
				{
					nextAnimatorMotionName = animatorComponent.name;
				}
			}
			if (nextAnimatorMotionName == AnimatorMotionName.None)
			{
				float dis = 0.0001f;
				if (self.CurMotionType == AnimatorMotionName.Idle)
				{
					dis = 0.01f;
				}
				float3 unitPos = self.GetUnit().Position;
				float3 goPos = self.Animator.transform.position;
				if (math.abs(unitPos.x - goPos.x) <= dis
				    && math.abs(unitPos.z - goPos.z) <= dis
				   )
				{
					//nextAnimatorMotionName = AnimatorMotionName.Idle;
					nextAnimatorMotionName = AnimatorMotionName.None;
					if (self.chgToIdleTime == -1)
					{
						self.chgToIdleTime = TimeHelper.ServerNow() + 1000;
					}
				}
				else
				{
					nextAnimatorMotionName = AnimatorMotionName.Move;

					Unit unit = self.GetUnit();
					UnitCfg unitCfg = unit.model;
					ResUnitCfg resUnitCfg = unitCfg.ResId_Ref;
					float curMoveSpeed = UnitHelper.GetMoveSpeed(unit);

					motionSpeed = curMoveSpeed / (resUnitCfg.MoveShowNatureSpeed * unitCfg.ResScale);

					self.chgToIdleTime = -1;
				}
			}
			else
			{
				self.chgToIdleTime = -1;
			}

			if (nextAnimatorMotionName != AnimatorMotionName.None)
			{
				if (nextAnimatorMotionName != self.CurMotionType)
				{
					self.Play(nextAnimatorMotionName, motionSpeed);
				}
				else if(Math.Abs(self.Animator.speed - motionSpeed) > 0.01f)
				{
					motionSpeed = (self.Animator.speed + motionSpeed) * 0.5f;
					self.SetAnimatorSpeed(motionSpeed);
				}
			}
		}

		public static void Update(this AnimatorShowComponent self)
		{
			if (self.Animator == null)
			{
				return;
			}

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

			try
			{
				float crossTime = 0.2f;

				self.CurMotionType = self.NextMotionType;
				self.NextMotionType = AnimatorMotionName.None;

				//self.Animator.Play(self.CurMotionType.ToString());
				self.Animator.ForceCrossFade(self.CurMotionType.ToString(), crossTime);
			}
			catch (Exception ex)
			{
				throw new Exception($"动作播放失败: {self.NextMotionType}", ex);
			}
		}

		public static void ForceCrossFade(this Animator animator, string name, float transitionDuration, int layer = 0, float normalizedTime = float.NegativeInfinity)
		{
			animator.Update(0);

			if (animator.GetNextAnimatorStateInfo(layer).fullPathHash == 0)
			{
				animator.CrossFade(name, transitionDuration, layer, normalizedTime);
			}
			else
			{
				animator.Play(animator.GetNextAnimatorStateInfo(layer).fullPathHash, layer);
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
			AnimationClip animationClip;
			if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}

			float motionSpeed = animationClip.length / time;
			if (motionSpeed < 0.01f || motionSpeed > 1000f)
			{
				Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
				return;
			}
			self.NextMotionType = motionType;

			self.SetAnimatorSpeed(motionSpeed);
		}

		public static void Play(this AnimatorShowComponent self, AnimatorMotionName motionType, float motionSpeed = 1f)
		{
			// AnimationClip animationClip;
			// if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			// {
			// 	throw new Exception($"找不到该动作: {motionType}");
			// }
			self.NextMotionType = motionType;

			self.SetAnimatorSpeed(motionSpeed);
		}

		public static float AnimationTime(this AnimatorShowComponent self, AnimatorMotionName motionType)
		{
			AnimationClip animationClip;
			if (!self.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}
			return animationClip.length;
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

			if (self.Animator == null)
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

			if (self.Animator == null)
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

			self.Animator.SetBool(name, state);
		}

		public static void SetFloatValue(this AnimatorShowComponent self, string name, float state)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.Animator.SetFloat(name, state);
		}

		public static void SetIntValue(this AnimatorShowComponent self, string name, int value)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.Animator.SetInteger(name, value);
		}

		public static void SetTrigger(this AnimatorShowComponent self, string name)
		{
			if (!self.HasParameter(name))
			{
				return;
			}

			self.Animator.SetTrigger(name);
		}

		/// <summary>
		/// 修改动画播放速度
		/// </summary>
		/// <param name="self"></param>
		/// <param name="speed"></param>
		public static void SetAnimatorSpeed(this AnimatorShowComponent self, float speed)
		{
			self.stopSpeed = self.Animator.speed;
			self.Animator.speed = speed;
		}

		/// <summary>
		/// 恢复动画播放速度
		/// </summary>
		/// <param name="self"></param>
		public static void ResetAnimatorSpeed(this AnimatorShowComponent self)
		{
			self.Animator.speed = self.stopSpeed;
		}
	}
}