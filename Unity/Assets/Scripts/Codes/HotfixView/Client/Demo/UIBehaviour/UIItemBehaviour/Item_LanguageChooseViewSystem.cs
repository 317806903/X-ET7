
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_LanguageChooseDestroySystem : DestroySystem<Scroll_Item_LanguageChoose> 
	{
		protected override void Destroy( Scroll_Item_LanguageChoose self )
		{
			self.DestroyWidget();
		}
	}
}
