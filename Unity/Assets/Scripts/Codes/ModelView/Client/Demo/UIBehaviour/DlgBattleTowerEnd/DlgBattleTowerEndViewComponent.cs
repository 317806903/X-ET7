
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattleTowerEnd))]
	[EnableMethod]
	public  class DlgBattleTowerEndViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_ReturnRoomButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReturnRoomButton == null )
     			{
		    		this.m_E_ReturnRoomButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"GameObject/E_ReturnRoom");
     			}
     			return this.m_E_ReturnRoomButton;
     		}
     	}

		public UnityEngine.UI.Image E_ReturnRoomImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReturnRoomImage == null )
     			{
		    		this.m_E_ReturnRoomImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"GameObject/E_ReturnRoom");
     			}
     			return this.m_E_ReturnRoomImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_GameEndButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_GameEndButton == null )
     			{
		    		this.m_EButton_GameEndButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"GameObject/EButton_GameEnd");
     			}
     			return this.m_EButton_GameEndButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_GameEndImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_GameEndImage == null )
     			{
		    		this.m_EButton_GameEndImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"GameObject/EButton_GameEnd");
     			}
     			return this.m_EButton_GameEndImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_GameEndText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_GameEndText == null )
     			{
		    		this.m_ELabel_GameEndText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"GameObject/EButton_GameEnd/ELabel_GameEnd");
     			}
     			return this.m_ELabel_GameEndText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ReturnRoomButton = null;
			this.m_E_ReturnRoomImage = null;
			this.m_EButton_GameEndButton = null;
			this.m_EButton_GameEndImage = null;
			this.m_ELabel_GameEndText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ReturnRoomButton = null;
		private UnityEngine.UI.Image m_E_ReturnRoomImage = null;
		private UnityEngine.UI.Button m_EButton_GameEndButton = null;
		private UnityEngine.UI.Image m_EButton_GameEndImage = null;
		private UnityEngine.UI.Text m_ELabel_GameEndText = null;
		public Transform uiTransform = null;
	}
}
