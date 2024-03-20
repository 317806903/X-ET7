
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgVideoShowSmall))]
	[EnableMethod]
	public class DlgVideoShowSmallViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Button E_VideoShowButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoShowButton == null )
				{
					this.m_E_VideoShowButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "VideoShow/mask/E_VideoShow");
				}
				return this.m_E_VideoShowButton;
			}
		}

		public UnityEngine.UI.RawImage E_VideoShowRawImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_VideoShowRawImage == null )
				{
					this.m_E_VideoShowRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "VideoShow/mask/E_VideoShow");
				}
				return this.m_E_VideoShowRawImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_VideoShowButton = null;
			this.m_E_VideoShowRawImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_VideoShowButton = null;
		private UnityEngine.UI.RawImage m_E_VideoShowRawImage = null;
		public Transform uiTransform = null;
	}
}
