
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgQuestionnaire))]
	[EnableMethod]
	public class DlgQuestionnaireViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EGRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EGRectTransform == null )
				{
					this.m_EGRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG");
				}
				return this.m_EGRectTransform;
			}
		}

		public UnityEngine.UI.Button E_BGButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_BGButton == null )
				{
					this.m_E_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG/E_BG");
				}
				return this.m_E_BGButton;
			}
		}

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
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG/E_BG");
				}
				return this.m_E_BGImage;
			}
		}

		public UnityEngine.RectTransform EG_titleRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleRectTransform == null )
				{
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG/Root/EG_title");
				}
				return this.m_EG_titleRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_titleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_titleImage == null )
				{
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG/Root/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public TMPro.TextMeshProUGUI E_titleTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_titleTextMeshProUGUI == null )
				{
					this.m_E_titleTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG/Root/E_title");
				}
				return this.m_E_titleTextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI E_infoTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_infoTextMeshProUGUI == null )
				{
					this.m_E_infoTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG/Root/E_info");
				}
				return this.m_E_infoTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_ItemLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_ItemLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "EG/Root/Gifts/ELoopScrollList_Item");
				}
				return this.m_ELoopScrollList_ItemLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button E_StartButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_StartButton == null )
				{
					this.m_E_StartButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG/Root/OperatorRoot/E_Start");
				}
				return this.m_E_StartButton;
			}
		}

		public UnityEngine.UI.Image E_StartImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_StartImage == null )
				{
					this.m_E_StartImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG/Root/OperatorRoot/E_Start");
				}
				return this.m_E_StartImage;
			}
		}

		public UnityEngine.UI.Button E_CloseComplainButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CloseComplainButton == null )
				{
					this.m_E_CloseComplainButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG/Root/OperatorRoot/E_CloseComplain");
				}
				return this.m_E_CloseComplainButton;
			}
		}

		public UnityEngine.UI.Image E_CloseComplainImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CloseComplainImage == null )
				{
					this.m_E_CloseComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG/Root/OperatorRoot/E_CloseComplain");
				}
				return this.m_E_CloseComplainImage;
			}
		}

		public UnityEngine.RectTransform EG_OpenAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OpenAnimationRectTransform == null )
				{
					this.m_EG_OpenAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OpenAnimation");
				}
				return this.m_EG_OpenAnimationRectTransform;
			}
		}

		public UnityEngine.RectTransform EG_CloseAnimationRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_CloseAnimationRectTransform == null )
				{
					this.m_EG_CloseAnimationRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_CloseAnimation");
				}
				return this.m_EG_CloseAnimationRectTransform;
			}
		}

		public void DestroyWidget()
		{
			this.m_EGRectTransform = null;
			this.m_E_BGButton = null;
			this.m_E_BGImage = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_titleTextMeshProUGUI = null;
			this.m_E_infoTextMeshProUGUI = null;
			this.m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
			this.m_E_StartButton = null;
			this.m_E_StartImage = null;
			this.m_E_CloseComplainButton = null;
			this.m_E_CloseComplainImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGRectTransform = null;
		private UnityEngine.UI.Button m_E_BGButton = null;
		private UnityEngine.UI.Image m_E_BGImage = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private TMPro.TextMeshProUGUI m_E_titleTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_infoTextMeshProUGUI = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_ItemLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_StartButton = null;
		private UnityEngine.UI.Image m_E_StartImage = null;
		private UnityEngine.UI.Button m_E_CloseComplainButton = null;
		private UnityEngine.UI.Image m_E_CloseComplainImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
