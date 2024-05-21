using System.IO;

namespace ET.Client
{
    [Invoke]
    public class GetFPSInvoke: AInvokeHandler<GetFPS, int>
    {
        public override int Handle(GetFPS args)
        {
            return DebugShowComponent.Instance.m_FPS;
        }
    }
}