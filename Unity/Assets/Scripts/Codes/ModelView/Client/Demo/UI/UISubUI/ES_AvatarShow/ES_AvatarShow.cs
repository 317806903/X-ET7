namespace ET.Client
{
	public class ES_AvatarShow : Entity, IAwake<UnityEngine.Transform>, IDestroy
	{
		public ES_AvatarShowViewComponent View { get => this.GetComponent<ES_AvatarShowViewComponent>(); }

	}
}
