
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameReport))]
	[EnableMethod]
	public class DlgGameReportViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EG_ReportRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ReportRectTransform == null )
				{
					this.m_EG_ReportRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Report");
				}
				return this.m_EG_ReportRectTransform;
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
					this.m_E_BGButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Report/E_BG");
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
					this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Report/E_BG");
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
					this.m_EG_titleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Report/Root/EG_title");
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
					this.m_EG_titleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Report/Root/EG_title");
				}
				return this.m_EG_titleImage;
			}
		}

		public UnityEngine.UI.ToggleGroup E_ToggleGroupToggleGroup
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ToggleGroupToggleGroup == null )
				{
					this.m_E_ToggleGroupToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject, "EG_Report/Root/E_ToggleGroup");
				}
				return this.m_E_ToggleGroupToggleGroup;
			}
		}

		public UnityEngine.UI.Toggle E_Toggle_1Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Toggle_1Toggle == null )
				{
					this.m_E_Toggle_1Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "EG_Report/Root/E_ToggleGroup/E_Toggle_1");
				}
				return this.m_E_Toggle_1Toggle;
			}
		}

		public UnityEngine.UI.Toggle E_Toggle_2Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Toggle_2Toggle == null )
				{
					this.m_E_Toggle_2Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "EG_Report/Root/E_ToggleGroup/E_Toggle_2");
				}
				return this.m_E_Toggle_2Toggle;
			}
		}

		public UnityEngine.UI.Toggle E_Toggle_3Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Toggle_3Toggle == null )
				{
					this.m_E_Toggle_3Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "EG_Report/Root/E_ToggleGroup/E_Toggle_3");
				}
				return this.m_E_Toggle_3Toggle;
			}
		}

		public TMPro.TextMeshProUGUI E_numInputTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_numInputTextMeshProUGUI == null )
				{
					this.m_E_numInputTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_numInput");
				}
				return this.m_E_numInputTextMeshProUGUI;
			}
		}

		public TMPro.TMP_InputField E_InputFieldComplainTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldComplainTMP_InputField == null )
				{
					this.m_E_InputFieldComplainTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_InputFieldComplain");
				}
				return this.m_E_InputFieldComplainTMP_InputField;
			}
		}

		public UnityEngine.UI.Image E_InputFieldComplainImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_InputFieldComplainImage == null )
				{
					this.m_E_InputFieldComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_InputFieldComplain");
				}
				return this.m_E_InputFieldComplainImage;
			}
		}

		public UnityEngine.UI.Button E_SendComplainButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SendComplainButton == null )
				{
					this.m_E_SendComplainButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_SendComplain");
				}
				return this.m_E_SendComplainButton;
			}
		}

		public UnityEngine.UI.Image E_SendComplainImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SendComplainImage == null )
				{
					this.m_E_SendComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_SendComplain");
				}
				return this.m_E_SendComplainImage;
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
					this.m_E_CloseComplainButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_CloseComplain");
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
					this.m_E_CloseComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Report/Root/OperatorRoot/E_CloseComplain");
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
			this.m_EG_ReportRectTransform = null;
			this.m_E_BGButton = null;
			this.m_E_BGImage = null;
			this.m_EG_titleRectTransform = null;
			this.m_EG_titleImage = null;
			this.m_E_ToggleGroupToggleGroup = null;
			this.m_E_Toggle_1Toggle = null;
			this.m_E_Toggle_2Toggle = null;
			this.m_E_Toggle_3Toggle = null;
			this.m_E_numInputTextMeshProUGUI = null;
			this.m_E_InputFieldComplainTMP_InputField = null;
			this.m_E_InputFieldComplainImage = null;
			this.m_E_SendComplainButton = null;
			this.m_E_SendComplainImage = null;
			this.m_E_CloseComplainButton = null;
			this.m_E_CloseComplainImage = null;
			this.m_EG_OpenAnimationRectTransform = null;
			this.m_EG_CloseAnimationRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ReportRectTransform = null;
		private UnityEngine.UI.Button m_E_BGButton = null;
		private UnityEngine.UI.Image m_E_BGImage = null;
		private UnityEngine.RectTransform m_EG_titleRectTransform = null;
		private UnityEngine.UI.Image m_EG_titleImage = null;
		private UnityEngine.UI.ToggleGroup m_E_ToggleGroupToggleGroup = null;
		private UnityEngine.UI.Toggle m_E_Toggle_1Toggle = null;
		private UnityEngine.UI.Toggle m_E_Toggle_2Toggle = null;
		private UnityEngine.UI.Toggle m_E_Toggle_3Toggle = null;
		private TMPro.TextMeshProUGUI m_E_numInputTextMeshProUGUI = null;
		private TMPro.TMP_InputField m_E_InputFieldComplainTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldComplainImage = null;
		private UnityEngine.UI.Button m_E_SendComplainButton = null;
		private UnityEngine.UI.Image m_E_SendComplainImage = null;
		private UnityEngine.UI.Button m_E_CloseComplainButton = null;
		private UnityEngine.UI.Image m_E_CloseComplainImage = null;
		private UnityEngine.RectTransform m_EG_OpenAnimationRectTransform = null;
		private UnityEngine.RectTransform m_EG_CloseAnimationRectTransform = null;
		public Transform uiTransform = null;
	}
}
