
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgGameJudgeChoose))]
	[EnableMethod]
	public class DlgGameJudgeChooseViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.RectTransform EG_OperatorMenuRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_OperatorMenuRectTransform == null )
				{
					this.m_EG_OperatorMenuRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_OperatorMenu");
				}
				return this.m_EG_OperatorMenuRectTransform;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGMenuImage == null )
				{
					this.m_E_Sprite_BGMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/E_Sprite_BGMenu");
				}
				return this.m_E_Sprite_BGMenuImage;
			}
		}

		public UnityEngine.UI.Button E_LoveItMenuButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoveItMenuButton == null )
				{
					this.m_E_LoveItMenuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/Root/OperatorRoot/E_LoveItMenu");
				}
				return this.m_E_LoveItMenuButton;
			}
		}

		public UnityEngine.UI.Image E_LoveItMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoveItMenuImage == null )
				{
					this.m_E_LoveItMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Root/OperatorRoot/E_LoveItMenu");
				}
				return this.m_E_LoveItMenuImage;
			}
		}

		public UnityEngine.UI.Button E_ComplainMenuButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ComplainMenuButton == null )
				{
					this.m_E_ComplainMenuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/Root/OperatorRoot/E_ComplainMenu");
				}
				return this.m_E_ComplainMenuButton;
			}
		}

		public UnityEngine.UI.Image E_ComplainMenuImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ComplainMenuImage == null )
				{
					this.m_E_ComplainMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_OperatorMenu/Root/OperatorRoot/E_ComplainMenu");
				}
				return this.m_E_ComplainMenuImage;
			}
		}

		public UnityEngine.UI.Button E_CloseMenuButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CloseMenuButton == null )
				{
					this.m_E_CloseMenuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_OperatorMenu/Root/OperatorRoot/E_CloseMenu");
				}
				return this.m_E_CloseMenuButton;
			}
		}

		public UnityEngine.RectTransform EG_LoveItRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LoveItRectTransform == null )
				{
					this.m_EG_LoveItRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_LoveIt");
				}
				return this.m_EG_LoveItRectTransform;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGLoveItImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGLoveItImage == null )
				{
					this.m_E_Sprite_BGLoveItImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_LoveIt/E_Sprite_BGLoveIt");
				}
				return this.m_E_Sprite_BGLoveItImage;
			}
		}

		public UnityEngine.UI.Button E_LoveItButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoveItButton == null )
				{
					this.m_E_LoveItButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_LoveIt/Root/OperatorRoot/E_LoveIt");
				}
				return this.m_E_LoveItButton;
			}
		}

		public UnityEngine.UI.Image E_LoveItImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_LoveItImage == null )
				{
					this.m_E_LoveItImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_LoveIt/Root/OperatorRoot/E_LoveIt");
				}
				return this.m_E_LoveItImage;
			}
		}

		public UnityEngine.UI.Button E_CloseLoveItButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_CloseLoveItButton == null )
				{
					this.m_E_CloseLoveItButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_LoveIt/Root/OperatorRoot/E_CloseLoveIt");
				}
				return this.m_E_CloseLoveItButton;
			}
		}

		public UnityEngine.RectTransform EG_ComplainRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_ComplainRectTransform == null )
				{
					this.m_EG_ComplainRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "EG_Complain");
				}
				return this.m_EG_ComplainRectTransform;
			}
		}

		public UnityEngine.UI.Image E_Sprite_BGComplainImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGComplainImage == null )
				{
					this.m_E_Sprite_BGComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Complain/E_Sprite_BGComplain");
				}
				return this.m_E_Sprite_BGComplainImage;
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
					this.m_E_numInputTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_numInput");
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
					this.m_E_InputFieldComplainTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_InputFieldComplain");
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
					this.m_E_InputFieldComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_InputFieldComplain");
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
					this.m_E_SendComplainButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_SendComplain");
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
					this.m_E_SendComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_SendComplain");
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
					this.m_E_CloseComplainButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_CloseComplain");
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
					this.m_E_CloseComplainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "EG_Complain/Root/OperatorRoot/E_CloseComplain");
				}
				return this.m_E_CloseComplainImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_EG_OperatorMenuRectTransform = null;
			this.m_E_Sprite_BGMenuImage = null;
			this.m_E_LoveItMenuButton = null;
			this.m_E_LoveItMenuImage = null;
			this.m_E_ComplainMenuButton = null;
			this.m_E_ComplainMenuImage = null;
			this.m_E_CloseMenuButton = null;
			this.m_EG_LoveItRectTransform = null;
			this.m_E_Sprite_BGLoveItImage = null;
			this.m_E_LoveItButton = null;
			this.m_E_LoveItImage = null;
			this.m_E_CloseLoveItButton = null;
			this.m_EG_ComplainRectTransform = null;
			this.m_E_Sprite_BGComplainImage = null;
			this.m_E_numInputTextMeshProUGUI = null;
			this.m_E_InputFieldComplainTMP_InputField = null;
			this.m_E_InputFieldComplainImage = null;
			this.m_E_SendComplainButton = null;
			this.m_E_SendComplainImage = null;
			this.m_E_CloseComplainButton = null;
			this.m_E_CloseComplainImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_OperatorMenuRectTransform = null;
		private UnityEngine.UI.Image m_E_Sprite_BGMenuImage = null;
		private UnityEngine.UI.Button m_E_LoveItMenuButton = null;
		private UnityEngine.UI.Image m_E_LoveItMenuImage = null;
		private UnityEngine.UI.Button m_E_ComplainMenuButton = null;
		private UnityEngine.UI.Image m_E_ComplainMenuImage = null;
		private UnityEngine.UI.Button m_E_CloseMenuButton = null;
		private UnityEngine.RectTransform m_EG_LoveItRectTransform = null;
		private UnityEngine.UI.Image m_E_Sprite_BGLoveItImage = null;
		private UnityEngine.UI.Button m_E_LoveItButton = null;
		private UnityEngine.UI.Image m_E_LoveItImage = null;
		private UnityEngine.UI.Button m_E_CloseLoveItButton = null;
		private UnityEngine.RectTransform m_EG_ComplainRectTransform = null;
		private UnityEngine.UI.Image m_E_Sprite_BGComplainImage = null;
		private TMPro.TextMeshProUGUI m_E_numInputTextMeshProUGUI = null;
		private TMPro.TMP_InputField m_E_InputFieldComplainTMP_InputField = null;
		private UnityEngine.UI.Image m_E_InputFieldComplainImage = null;
		private UnityEngine.UI.Button m_E_SendComplainButton = null;
		private UnityEngine.UI.Image m_E_SendComplainImage = null;
		private UnityEngine.UI.Button m_E_CloseComplainButton = null;
		private UnityEngine.UI.Image m_E_CloseComplainImage = null;
		public Transform uiTransform = null;
	}
}
