
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public class ES_AvatarShow : Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
	{
		public UnityEngine.UI.Image E_ImgLineImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ImgLineImage == null )
				{
					this.m_E_ImgLineImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_ImgLine");
				}
				return this.m_E_ImgLineImage;
			}
		}

		public UnityEngine.UI.Image E_AvatarIconImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_AvatarIconImage == null )
				{
					this.m_E_AvatarIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_AvatarIcon");
				}
				return this.m_E_AvatarIconImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_ImgLineImage = null;
			this.m_E_AvatarIconImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_ImgLineImage = null;
		private UnityEngine.UI.Image m_E_AvatarIconImage = null;
		public Transform uiTransform = null;
	}
}
