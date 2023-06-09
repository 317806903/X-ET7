
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

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TowerLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TankLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
