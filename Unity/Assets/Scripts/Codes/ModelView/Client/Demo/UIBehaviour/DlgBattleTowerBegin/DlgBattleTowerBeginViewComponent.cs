
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerBegin))]
	[EnableMethod]
	public class DlgBattleTowerBeginViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_EffectImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EffectImage == null )
     			{
		    		this.m_E_EffectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Effect");
     			}
     			return this.m_E_EffectImage;
     		}
     	}

		public UnityEngine.UI.Button Effect_GameBeginButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_Effect_GameBeginButton == null )
     			{
		    		this.m_Effect_GameBeginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Effect/Effect_GameBegin");
     			}
     			return this.m_Effect_GameBeginButton;
     		}
     	}

		public UnityEngine.UI.Image Effect_GameBeginImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_Effect_GameBeginImage == null )
     			{
		    		this.m_Effect_GameBeginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Effect/Effect_GameBegin");
     			}
     			return this.m_Effect_GameBeginImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_EffectImage = null;
			this.m_Effect_GameBeginButton = null;
			this.m_Effect_GameBeginImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_EffectImage = null;
		private UnityEngine.UI.Button m_Effect_GameBeginButton = null;
		private UnityEngine.UI.Image m_Effect_GameBeginImage = null;
		public Transform uiTransform = null;
	}
}
