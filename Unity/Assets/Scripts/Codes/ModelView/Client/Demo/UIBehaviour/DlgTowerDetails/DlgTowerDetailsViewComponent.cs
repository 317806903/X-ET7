
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgTowerDetails))]
	[EnableMethod]
	public class DlgTowerDetailsViewComponent : Entity, IAwake, IDestroy
	{
		public UnityEngine.UI.Image E_DetailsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DetailsImage == null )
				{
					this.m_E_DetailsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details");
				}
				return this.m_E_DetailsImage;
			}
		}

		public UnityEngine.UI.Button EButton_BgButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BgButton == null )
				{
					this.m_EButton_BgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/EButton_Bg");
				}
				return this.m_EButton_BgButton;
			}
		}

		public UnityEngine.UI.Image EButton_BgImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_BgImage == null )
				{
					this.m_EButton_BgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EButton_Bg");
				}
				return this.m_EButton_BgImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxLowImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxLowImage == null )
				{
					this.m_EImage_BoxLowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxLow");
				}
				return this.m_EImage_BoxLowImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxMiddleImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxMiddleImage == null )
				{
					this.m_EImage_BoxMiddleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxMiddle");
				}
				return this.m_EImage_BoxMiddleImage;
			}
		}

		public UnityEngine.UI.Image EImage_BoxHighImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_BoxHighImage == null )
				{
					this.m_EImage_BoxHighImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/EImage_BoxHigh");
				}
				return this.m_EImage_BoxHighImage;
			}
		}

		public UnityEngine.UI.Image E_videoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_videoImage == null )
				{
					this.m_E_videoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video");
				}
				return this.m_E_videoImage;
			}
		}

		public UnityEngine.Video.VideoPlayer E_videoVideoPlayer
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_videoVideoPlayer == null )
				{
					this.m_E_videoVideoPlayer = UIFindHelper.FindDeepChild<UnityEngine.Video.VideoPlayer>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video");
				}
				return this.m_E_videoVideoPlayer;
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
					this.m_E_VideoShowRawImage = UIFindHelper.FindDeepChild<UnityEngine.UI.RawImage>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow");
				}
				return this.m_E_VideoShowRawImage;
			}
		}

		public UnityEngine.UI.Button EButton_VideoButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_VideoButton == null )
				{
					this.m_EButton_VideoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/EButton_Video");
				}
				return this.m_EButton_VideoButton;
			}
		}

		public UnityEngine.UI.Image EButton_VideoImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_VideoImage == null )
				{
					this.m_EButton_VideoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/EButton_Video");
				}
				return this.m_EButton_VideoImage;
			}
		}

		public UnityEngine.UI.Slider E_SliderSlider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_SliderSlider == null )
				{
					this.m_E_SliderSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_video/video/VideoShow/mask/E_VideoShow/E_Slider");
				}
				return this.m_E_SliderSlider;
			}
		}

		public UnityEngine.UI.Slider E_Slider01Slider
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Slider01Slider == null )
				{
					this.m_E_Slider01Slider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/E_Slider01");
				}
				return this.m_E_Slider01Slider;
			}
		}

		public UnityEngine.UI.Button EButton_Pause01Button
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Pause01Button == null )
				{
					this.m_EButton_Pause01Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause01");
				}
				return this.m_EButton_Pause01Button;
			}
		}

		public UnityEngine.UI.Image EButton_Pause01Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_Pause01Image == null )
				{
					this.m_EButton_Pause01Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause01");
				}
				return this.m_EButton_Pause01Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_PauseTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_PauseTextMeshProUGUI == null )
				{
					this.m_ELabel_PauseTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause01/ELabel_Pause");
				}
				return this.m_ELabel_PauseTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_PauseButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PauseButton == null )
				{
					this.m_EButton_PauseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause");
				}
				return this.m_EButton_PauseButton;
			}
		}

		public UnityEngine.UI.Image EButton_PauseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_PauseImage == null )
				{
					this.m_EButton_PauseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause");
				}
				return this.m_EButton_PauseImage;
			}
		}

		public UnityEngine.UI.Image E_Image_PlayImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Image_PlayImage == null )
				{
					this.m_E_Image_PlayImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause/E_Image_Play");
				}
				return this.m_E_Image_PlayImage;
			}
		}

		public UnityEngine.UI.Image E_Image_PauseImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_Image_PauseImage == null )
				{
					this.m_E_Image_PauseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/E_Tutorials/EButton_Pause/E_Image_Pause");
				}
				return this.m_E_Image_PauseImage;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_NameTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_NameTextMeshProUGUI == null )
				{
					this.m_ELabel_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Name/ELabel_Name");
				}
				return this.m_ELabel_NameTextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_IconStarRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_IconStarRectTransform == null )
				{
					this.m_EG_IconStarRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Details/info/EG_IconStar");
				}
				return this.m_EG_IconStarRectTransform;
			}
		}

		public UnityEngine.UI.Image E_IconStar1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar1Image == null )
				{
					this.m_E_IconStar1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_IconStar/E_IconStar1");
				}
				return this.m_E_IconStar1Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar2Image == null )
				{
					this.m_E_IconStar2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_IconStar/E_IconStar2");
				}
				return this.m_E_IconStar2Image;
			}
		}

		public UnityEngine.UI.Image E_IconStar3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_IconStar3Image == null )
				{
					this.m_E_IconStar3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_IconStar/E_IconStar3");
				}
				return this.m_E_IconStar3Image;
			}
		}

		public UnityEngine.RectTransform EG_LabelsRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LabelsRectTransform == null )
				{
					this.m_EG_LabelsRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Details/info/EG_Labels");
				}
				return this.m_EG_LabelsRectTransform;
			}
		}

		public UnityEngine.UI.Image EG_LabelsImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_LabelsImage == null )
				{
					this.m_EG_LabelsImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Labels");
				}
				return this.m_EG_LabelsImage;
			}
		}

		public UnityEngine.UI.Image EImage_Label1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label1Image == null )
				{
					this.m_EImage_Label1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Labels/EImage_Label1");
				}
				return this.m_EImage_Label1Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label1TextMeshProUGUI == null )
				{
					this.m_ELabel_Label1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Labels/EImage_Label1/ELabel_Label1");
				}
				return this.m_ELabel_Label1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image EImage_Label2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EImage_Label2Image == null )
				{
					this.m_EImage_Label2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Labels/EImage_Label2");
				}
				return this.m_EImage_Label2Image;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_Label2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_Label2TextMeshProUGUI == null )
				{
					this.m_ELabel_Label2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Labels/EImage_Label2/ELabel_Label2");
				}
				return this.m_ELabel_Label2TextMeshProUGUI;
			}
		}

		public UnityEngine.RectTransform EG_AttributeRectTransform
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EG_AttributeRectTransform == null )
				{
					this.m_EG_AttributeRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute");
				}
				return this.m_EG_AttributeRectTransform;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute1Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute1Image == null )
				{
					this.m_ENode_Attribute1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute1");
				}
				return this.m_ENode_Attribute1Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute1TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute1/Elabel_Attribute1");
				}
				return this.m_Elabel_Attribute1TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue1TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue1TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue1TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute1/Elabel_Attribute1/Elabel_AttributeValue1");
				}
				return this.m_Elabel_AttributeValue1TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_AttributeLine2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_AttributeLine2Image == null )
				{
					this.m_ENode_AttributeLine2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_AttributeLine2");
				}
				return this.m_ENode_AttributeLine2Image;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute2Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute2Image == null )
				{
					this.m_ENode_Attribute2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute2");
				}
				return this.m_ENode_Attribute2Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute2TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute2/Elabel_Attribute2");
				}
				return this.m_Elabel_Attribute2TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue2TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue2TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue2TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute2/Elabel_Attribute2/Elabel_AttributeValue2");
				}
				return this.m_Elabel_AttributeValue2TextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Image ENode_AttributeLine3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_AttributeLine3Image == null )
				{
					this.m_ENode_AttributeLine3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_AttributeLine3");
				}
				return this.m_ENode_AttributeLine3Image;
			}
		}

		public UnityEngine.UI.Image ENode_Attribute3Image
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ENode_Attribute3Image == null )
				{
					this.m_ENode_Attribute3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute3");
				}
				return this.m_ENode_Attribute3Image;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_Attribute3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_Attribute3TextMeshProUGUI == null )
				{
					this.m_Elabel_Attribute3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute3/Elabel_Attribute3");
				}
				return this.m_Elabel_Attribute3TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI Elabel_AttributeValue3TextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_Elabel_AttributeValue3TextMeshProUGUI == null )
				{
					this.m_Elabel_AttributeValue3TextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EG_Attribute/ENode_Attribute3/Elabel_Attribute3/Elabel_AttributeValue3");
				}
				return this.m_Elabel_AttributeValue3TextMeshProUGUI;
			}
		}

		public TMPro.TextMeshProUGUI ELabel_DescriptionTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELabel_DescriptionTextMeshProUGUI == null )
				{
					this.m_ELabel_DescriptionTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/Scroll View/Viewport/Content/ELabel_Description");
				}
				return this.m_ELabel_DescriptionTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_UnlockButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_UnlockButton == null )
				{
					this.m_EButton_UnlockButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/EButton_Unlock");
				}
				return this.m_EButton_UnlockButton;
			}
		}

		public UnityEngine.UI.Image EButton_UnlockImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_UnlockImage == null )
				{
					this.m_EButton_UnlockImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EButton_Unlock");
				}
				return this.m_EButton_UnlockImage;
			}
		}

		public TMPro.TextMeshProUGUI ELable_UnLockShowTipTextMeshProUGUI
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_ELable_UnLockShowTipTextMeshProUGUI == null )
				{
					this.m_ELable_UnLockShowTipTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject, "E_Details/info/EButton_Unlock/ELable_UnLockShowTip");
				}
				return this.m_ELable_UnLockShowTipTextMeshProUGUI;
			}
		}

		public UnityEngine.UI.Button EButton_LeftButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_LeftButton == null )
				{
					this.m_EButton_LeftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/EButton_Left");
				}
				return this.m_EButton_LeftButton;
			}
		}

		public UnityEngine.UI.Image EButton_LeftImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_LeftImage == null )
				{
					this.m_EButton_LeftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EButton_Left");
				}
				return this.m_EButton_LeftImage;
			}
		}

		public UnityEngine.UI.Button EButton_RightButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RightButton == null )
				{
					this.m_EButton_RightButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Details/info/EButton_Right");
				}
				return this.m_EButton_RightButton;
			}
		}

		public UnityEngine.UI.Image EButton_RightImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EButton_RightImage == null )
				{
					this.m_EButton_RightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Details/info/EButton_Right");
				}
				return this.m_EButton_RightImage;
			}
		}

		public UnityEngine.UI.Toggle EToggle_Lv1Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EToggle_Lv1Toggle == null )
				{
					this.m_EToggle_Lv1Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "E_Details/info/ToggleGroup/EToggle_Lv1");
				}
				return this.m_EToggle_Lv1Toggle;
			}
		}

		public UnityEngine.UI.Toggle EToggle_Lv2Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EToggle_Lv2Toggle == null )
				{
					this.m_EToggle_Lv2Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "E_Details/info/ToggleGroup/EToggle_Lv2");
				}
				return this.m_EToggle_Lv2Toggle;
			}
		}

		public UnityEngine.UI.Toggle EToggle_Lv3Toggle
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_EToggle_Lv3Toggle == null )
				{
					this.m_EToggle_Lv3Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject, "E_Details/info/ToggleGroup/EToggle_Lv3");
				}
				return this.m_EToggle_Lv3Toggle;
			}
		}

		public UnityEngine.UI.Button E_DebugButton
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugButton == null )
				{
					this.m_E_DebugButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject, "E_Debug");
				}
				return this.m_E_DebugButton;
			}
		}

		public UnityEngine.UI.Image E_DebugImage
		{
			get
			{
				if (this.uiTransform == null)
				{
					Log.Error("uiTransform is null.");
					return null;
				}
				if( this.m_E_DebugImage == null )
				{
					this.m_E_DebugImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Debug");
				}
				return this.m_E_DebugImage;
			}
		}

		public void DestroyWidget()
		{
			this.m_E_DetailsImage = null;
			this.m_EButton_BgButton = null;
			this.m_EButton_BgImage = null;
			this.m_EImage_BoxLowImage = null;
			this.m_EImage_BoxMiddleImage = null;
			this.m_EImage_BoxHighImage = null;
			this.m_E_videoImage = null;
			this.m_E_videoVideoPlayer = null;
			this.m_E_VideoShowRawImage = null;
			this.m_EButton_VideoButton = null;
			this.m_EButton_VideoImage = null;
			this.m_E_SliderSlider = null;
			this.m_E_Slider01Slider = null;
			this.m_EButton_Pause01Button = null;
			this.m_EButton_Pause01Image = null;
			this.m_ELabel_PauseTextMeshProUGUI = null;
			this.m_EButton_PauseButton = null;
			this.m_EButton_PauseImage = null;
			this.m_E_Image_PlayImage = null;
			this.m_E_Image_PauseImage = null;
			this.m_ELabel_NameTextMeshProUGUI = null;
			this.m_EG_IconStarRectTransform = null;
			this.m_E_IconStar1Image = null;
			this.m_E_IconStar2Image = null;
			this.m_E_IconStar3Image = null;
			this.m_EG_LabelsRectTransform = null;
			this.m_EG_LabelsImage = null;
			this.m_EImage_Label1Image = null;
			this.m_ELabel_Label1TextMeshProUGUI = null;
			this.m_EImage_Label2Image = null;
			this.m_ELabel_Label2TextMeshProUGUI = null;
			this.m_EG_AttributeRectTransform = null;
			this.m_ENode_Attribute1Image = null;
			this.m_Elabel_Attribute1TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue1TextMeshProUGUI = null;
			this.m_ENode_AttributeLine2Image = null;
			this.m_ENode_Attribute2Image = null;
			this.m_Elabel_Attribute2TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue2TextMeshProUGUI = null;
			this.m_ENode_AttributeLine3Image = null;
			this.m_ENode_Attribute3Image = null;
			this.m_Elabel_Attribute3TextMeshProUGUI = null;
			this.m_Elabel_AttributeValue3TextMeshProUGUI = null;
			this.m_ELabel_DescriptionTextMeshProUGUI = null;
			this.m_EButton_UnlockButton = null;
			this.m_EButton_UnlockImage = null;
			this.m_ELable_UnLockShowTipTextMeshProUGUI = null;
			this.m_EButton_LeftButton = null;
			this.m_EButton_LeftImage = null;
			this.m_EButton_RightButton = null;
			this.m_EButton_RightImage = null;
			this.m_EToggle_Lv1Toggle = null;
			this.m_EToggle_Lv2Toggle = null;
			this.m_EToggle_Lv3Toggle = null;
			this.m_E_DebugButton = null;
			this.m_E_DebugImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_DetailsImage = null;
		private UnityEngine.UI.Button m_EButton_BgButton = null;
		private UnityEngine.UI.Image m_EButton_BgImage = null;
		private UnityEngine.UI.Image m_EImage_BoxLowImage = null;
		private UnityEngine.UI.Image m_EImage_BoxMiddleImage = null;
		private UnityEngine.UI.Image m_EImage_BoxHighImage = null;
		private UnityEngine.UI.Image m_E_videoImage = null;
		private UnityEngine.Video.VideoPlayer m_E_videoVideoPlayer = null;
		private UnityEngine.UI.RawImage m_E_VideoShowRawImage = null;
		private UnityEngine.UI.Button m_EButton_VideoButton = null;
		private UnityEngine.UI.Image m_EButton_VideoImage = null;
		private UnityEngine.UI.Slider m_E_SliderSlider = null;
		private UnityEngine.UI.Slider m_E_Slider01Slider = null;
		private UnityEngine.UI.Button m_EButton_Pause01Button = null;
		private UnityEngine.UI.Image m_EButton_Pause01Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_PauseTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_PauseButton = null;
		private UnityEngine.UI.Image m_EButton_PauseImage = null;
		private UnityEngine.UI.Image m_E_Image_PlayImage = null;
		private UnityEngine.UI.Image m_E_Image_PauseImage = null;
		private TMPro.TextMeshProUGUI m_ELabel_NameTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_IconStarRectTransform = null;
		private UnityEngine.UI.Image m_E_IconStar1Image = null;
		private UnityEngine.UI.Image m_E_IconStar2Image = null;
		private UnityEngine.UI.Image m_E_IconStar3Image = null;
		private UnityEngine.RectTransform m_EG_LabelsRectTransform = null;
		private UnityEngine.UI.Image m_EG_LabelsImage = null;
		private UnityEngine.UI.Image m_EImage_Label1Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_EImage_Label2Image = null;
		private TMPro.TextMeshProUGUI m_ELabel_Label2TextMeshProUGUI = null;
		private UnityEngine.RectTransform m_EG_AttributeRectTransform = null;
		private UnityEngine.UI.Image m_ENode_Attribute1Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute1TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue1TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_AttributeLine2Image = null;
		private UnityEngine.UI.Image m_ENode_Attribute2Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute2TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue2TextMeshProUGUI = null;
		private UnityEngine.UI.Image m_ENode_AttributeLine3Image = null;
		private UnityEngine.UI.Image m_ENode_Attribute3Image = null;
		private TMPro.TextMeshProUGUI m_Elabel_Attribute3TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_Elabel_AttributeValue3TextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_ELabel_DescriptionTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_UnlockButton = null;
		private UnityEngine.UI.Image m_EButton_UnlockImage = null;
		private TMPro.TextMeshProUGUI m_ELable_UnLockShowTipTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_EButton_LeftButton = null;
		private UnityEngine.UI.Image m_EButton_LeftImage = null;
		private UnityEngine.UI.Button m_EButton_RightButton = null;
		private UnityEngine.UI.Image m_EButton_RightImage = null;
		private UnityEngine.UI.Toggle m_EToggle_Lv1Toggle = null;
		private UnityEngine.UI.Toggle m_EToggle_Lv2Toggle = null;
		private UnityEngine.UI.Toggle m_EToggle_Lv3Toggle = null;
		private UnityEngine.UI.Button m_E_DebugButton = null;
		private UnityEngine.UI.Image m_E_DebugImage = null;
		public Transform uiTransform = null;
	}
}
