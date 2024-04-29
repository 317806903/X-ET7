namespace ET
{
	public enum PayStatus
	{
		Default,
		GetWXUrlSuccessed,
		PaySuccessed,
		AddCoinNumSuccessed,
		PayFailed,
	}

	[ChildOf(typeof(PayManagerComponent))]
	public class PayComponent : Entity, IAwake
	{
		public long orderId;
		public long playerId;
		public long createOrderTime;
		public long confirmOrderTime;
		public PayStatus payStatus;
		public string sWXUrl;
		public int coinNum;
		public int moneyValue;
		public string payResultMsg;
	}
}