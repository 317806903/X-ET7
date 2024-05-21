using UnityEngine.Video;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public class DlgVideoShowSmall : Entity, IAwake, IUILogic
	{
		public DlgVideoShowSmallViewComponent View { get => this.GetComponent<DlgVideoShowSmallViewComponent>(); }

		public string videoPath;
		public VideoPlayer videoPlayer;
	}
}
