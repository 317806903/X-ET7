
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLoading))]
	[EnableMethod]
	public class DlgLoadingViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGImage == null )
				{
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "Sprite_BackGround/E_BG");
				}
				return this.m_E_BGImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_BGImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BGImage = null;
		public Transform uiTransform = null;
	}
}
