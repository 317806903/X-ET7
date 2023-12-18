using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgVideoShow : Entity, IAwake, IUILogic
	{
		public DlgVideoShowViewComponent View { get => this.GetComponent<DlgVideoShowViewComponent>(); }
		public string videoPath;
	}
}
