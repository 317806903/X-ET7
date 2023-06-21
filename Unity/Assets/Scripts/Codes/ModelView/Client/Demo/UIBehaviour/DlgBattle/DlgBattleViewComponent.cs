
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgBattle))]
	[EnableMethod]
	public  class DlgBattleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TowerLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TowerLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"GameObject/ELoopScrollList_Tower");
     			}
     			return this.m_ELoopScrollList_TowerLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TankLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TankLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TankLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"GameObject/ELoopScrollList_Tank");
     			}
     			return this.m_ELoopScrollList_TankLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button E_QuitBattleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuitBattleButton == null )
     			{
		    		this.m_E_QuitBattleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"GameObject/E_QuitBattle");
     			}
     			return this.m_E_QuitBattleButton;
     		}
     	}

		public UnityEngine.UI.Image E_QuitBattleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuitBattleImage == null )
     			{
		    		this.m_E_QuitBattleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"GameObject/E_QuitBattle");
     			}
     			return this.m_E_QuitBattleImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
			this.m_E_QuitBattleButton = null;
			this.m_E_QuitBattleImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_QuitBattleButton = null;
		private UnityEngine.UI.Image m_E_QuitBattleImage = null;
		public Transform uiTransform = null;
	}
}
