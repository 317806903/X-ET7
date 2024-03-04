
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgKnapsack))]
	[EnableMethod]
	public class DlgKnapsackViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_Sprite_BGImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Sprite_BGImage == null )
				{
					this.m_E_Sprite_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Sprite_BG");
				}
				return this.m_E_Sprite_BGImage;
			}
		}

		public UnityEngine.UI.Button E_ReturnButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnButton == null )
				{
					this.m_E_ReturnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Return");
				}
				return this.m_E_ReturnButton;
			}
		}

		public UnityEngine.UI.Image E_ReturnImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_ReturnImage == null )
				{
					this.m_E_ReturnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Return");
				}
				return this.m_E_ReturnImage;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_LoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_LoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_LoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "E_Return/ELoopScrollList_");
				}
				return this.m_ELoopScrollList_LoopVerticalScrollRect;
			}
		}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_MyCardLoopVerticalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_MyCardLoopVerticalScrollRect == null )
				{
					this.m_ELoopScrollList_MyCardLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "ELoopScrollList_MyCard");
				}
				return this.m_ELoopScrollList_MyCardLoopVerticalScrollRect;
			}
		}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_CardLoopHorizontalScrollRect
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELoopScrollList_CardLoopHorizontalScrollRect == null )
				{
					this.m_ELoopScrollList_CardLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject, "ELoopScrollList_Card");
				}
				return this.m_ELoopScrollList_CardLoopHorizontalScrollRect;
			}
		}

		public UnityEngine.UI.Button EButton_GetButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GetButton == null )
				{
					this.m_EButton_GetButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "OperatorRoot/GetList/EButton_Get");
				}
				return this.m_EButton_GetButton;
			}
		}

		public UnityEngine.UI.Image EButton_GetImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_GetImage == null )
				{
					this.m_EButton_GetImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/GetList/EButton_Get");
				}
				return this.m_EButton_GetImage;
			}
		}

		public UnityEngine.UI.Text ELabel_GetText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_GetText == null )
				{
					this.m_ELabel_GetText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "OperatorRoot/GetList/EButton_Get/ELabel_Get");
				}
				return this.m_ELabel_GetText;
			}
		}

		public TMPro.TMP_Dropdown EDropdown_TypeTMP_Dropdown
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EDropdown_TypeTMP_Dropdown == null )
				{
					this.m_EDropdown_TypeTMP_Dropdown = UIFindHelper.FindDeepChild<TMPro.TMP_Dropdown>(this.uiTransform.gameObject, "OperatorRoot/GetList/EDropdown_Type");
				}
				return this.m_EDropdown_TypeTMP_Dropdown;
			}
		}

		public UnityEngine.UI.Image EDropdown_TypeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EDropdown_TypeImage == null )
				{
					this.m_EDropdown_TypeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/GetList/EDropdown_Type");
				}
				return this.m_EDropdown_TypeImage;
			}
		}

		public UnityEngine.UI.Button EButton_AddButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_AddButton == null )
				{
					this.m_EButton_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "OperatorRoot/Add/EButton_Add");
				}
				return this.m_EButton_AddButton;
			}
		}

		public UnityEngine.UI.Image EButton_AddImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_AddImage == null )
				{
					this.m_EButton_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/Add/EButton_Add");
				}
				return this.m_EButton_AddImage;
			}
		}

		public UnityEngine.UI.Text ELabel_AddText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_AddText == null )
				{
					this.m_ELabel_AddText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "OperatorRoot/Add/EButton_Add/ELabel_Add");
				}
				return this.m_ELabel_AddText;
			}
		}

		public TMPro.TMP_InputField EInputField_AddNumTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EInputField_AddNumTMP_InputField == null )
				{
					this.m_EInputField_AddNumTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "OperatorRoot/Add/EInputField_AddNum");
				}
				return this.m_EInputField_AddNumTMP_InputField;
			}
		}

		public UnityEngine.UI.Image EInputField_AddNumImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EInputField_AddNumImage == null )
				{
					this.m_EInputField_AddNumImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/Add/EInputField_AddNum");
				}
				return this.m_EInputField_AddNumImage;
			}
		}

		public UnityEngine.UI.Image E_SetNodeImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SetNodeImage == null )
				{
					this.m_E_SetNodeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode");
				}
				return this.m_E_SetNodeImage;
			}
		}

		public UnityEngine.UI.Button EButton_SetButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SetButton == null )
				{
					this.m_EButton_SetButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode/EButton_Set");
				}
				return this.m_EButton_SetButton;
			}
		}

		public UnityEngine.UI.Image EButton_SetImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_SetImage == null )
				{
					this.m_EButton_SetImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode/EButton_Set");
				}
				return this.m_EButton_SetImage;
			}
		}

		public UnityEngine.UI.Text ELabel_ModifyText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ModifyText == null )
				{
					this.m_ELabel_ModifyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode/EButton_Set/ELabel_Modify");
				}
				return this.m_ELabel_ModifyText;
			}
		}

		public TMPro.TMP_InputField EInputField_SetNumTMP_InputField
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EInputField_SetNumTMP_InputField == null )
				{
					this.m_EInputField_SetNumTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode/EInputField_SetNum");
				}
				return this.m_EInputField_SetNumTMP_InputField;
			}
		}

		public UnityEngine.UI.Image EInputField_SetNumImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EInputField_SetNumImage == null )
				{
					this.m_EInputField_SetNumImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/E_SetNode/EInputField_SetNum");
				}
				return this.m_EInputField_SetNumImage;
			}
		}

		public UnityEngine.UI.Button EButton_DeleteButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_DeleteButton == null )
				{
					this.m_EButton_DeleteButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "OperatorRoot/Delete/EButton_Delete");
				}
				return this.m_EButton_DeleteButton;
			}
		}

		public UnityEngine.UI.Image EButton_DeleteImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_DeleteImage == null )
				{
					this.m_EButton_DeleteImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/Delete/EButton_Delete");
				}
				return this.m_EButton_DeleteImage;
			}
		}

		public UnityEngine.UI.Text ELabel_DeleteText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_DeleteText == null )
				{
					this.m_ELabel_DeleteText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "OperatorRoot/Delete/EButton_Delete/ELabel_Delete");
				}
				return this.m_ELabel_DeleteText;
			}
		}

		public UnityEngine.UI.Button EButton_ClearButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearButton == null )
				{
					this.m_EButton_ClearButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "OperatorRoot/EButton_Clear");
				}
				return this.m_EButton_ClearButton;
			}
		}

		public UnityEngine.UI.Image EButton_ClearImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_ClearImage == null )
				{
					this.m_EButton_ClearImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "OperatorRoot/EButton_Clear");
				}
				return this.m_EButton_ClearImage;
			}
		}

		public UnityEngine.UI.Text ELabel_ClearText
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_ClearText == null )
				{
					this.m_ELabel_ClearText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "OperatorRoot/EButton_Clear/ELabel_Clear");
				}
				return this.m_ELabel_ClearText;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_Sprite_BGImage = null;
			this.m_E_ReturnButton = null;
			this.m_E_ReturnImage = null;
			this.m_ELoopScrollList_LoopVerticalScrollRect = null;
			this.m_ELoopScrollList_MyCardLoopVerticalScrollRect = null;
			this.m_ELoopScrollList_CardLoopHorizontalScrollRect = null;
			this.m_EButton_GetButton = null;
			this.m_EButton_GetImage = null;
			this.m_ELabel_GetText = null;
			this.m_EDropdown_TypeTMP_Dropdown = null;
			this.m_EDropdown_TypeImage = null;
			this.m_EButton_AddButton = null;
			this.m_EButton_AddImage = null;
			this.m_ELabel_AddText = null;
			this.m_EInputField_AddNumTMP_InputField = null;
			this.m_EInputField_AddNumImage = null;
			this.m_E_SetNodeImage = null;
			this.m_EButton_SetButton = null;
			this.m_EButton_SetImage = null;
			this.m_ELabel_ModifyText = null;
			this.m_EInputField_SetNumTMP_InputField = null;
			this.m_EInputField_SetNumImage = null;
			this.m_EButton_DeleteButton = null;
			this.m_EButton_DeleteImage = null;
			this.m_ELabel_DeleteText = null;
			this.m_EButton_ClearButton = null;
			this.m_EButton_ClearImage = null;
			this.m_ELabel_ClearText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_Sprite_BGImage = null;
		private UnityEngine.UI.Button m_E_ReturnButton = null;
		private UnityEngine.UI.Image m_E_ReturnImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_LoopVerticalScrollRect = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_MyCardLoopVerticalScrollRect = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_CardLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_GetButton = null;
		private UnityEngine.UI.Image m_EButton_GetImage = null;
		private UnityEngine.UI.Text m_ELabel_GetText = null;
		private TMPro.TMP_Dropdown m_EDropdown_TypeTMP_Dropdown = null;
		private UnityEngine.UI.Image m_EDropdown_TypeImage = null;
		private UnityEngine.UI.Button m_EButton_AddButton = null;
		private UnityEngine.UI.Image m_EButton_AddImage = null;
		private UnityEngine.UI.Text m_ELabel_AddText = null;
		private TMPro.TMP_InputField m_EInputField_AddNumTMP_InputField = null;
		private UnityEngine.UI.Image m_EInputField_AddNumImage = null;
		private UnityEngine.UI.Image m_E_SetNodeImage = null;
		private UnityEngine.UI.Button m_EButton_SetButton = null;
		private UnityEngine.UI.Image m_EButton_SetImage = null;
		private UnityEngine.UI.Text m_ELabel_ModifyText = null;
		private TMPro.TMP_InputField m_EInputField_SetNumTMP_InputField = null;
		private UnityEngine.UI.Image m_EInputField_SetNumImage = null;
		private UnityEngine.UI.Button m_EButton_DeleteButton = null;
		private UnityEngine.UI.Image m_EButton_DeleteImage = null;
		private UnityEngine.UI.Text m_ELabel_DeleteText = null;
		private UnityEngine.UI.Button m_EButton_ClearButton = null;
		private UnityEngine.UI.Image m_EButton_ClearImage = null;
		private UnityEngine.UI.Text m_ELabel_ClearText = null;
		public Transform uiTransform = null;
	}
}
