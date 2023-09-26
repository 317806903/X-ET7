
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgLoading))]
	[EnableMethod]
	public class DlgLoadingViewComponent : Entity, IAwake, IDestroy
	{
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
		    		this.m_E_SliderSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject,"Sprite_BackGround/E_Slider");
     			}
     			return this.m_E_SliderSlider;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SliderSlider = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Slider m_E_SliderSlider = null;
		public Transform uiTransform = null;
	}
}
