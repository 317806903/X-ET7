Shader "Unlit/HealthBar"
{
    Properties
    {
        _ColorGreen ("_ColorGreen", Color) = (0.127, 0.689, 0.1965, 1)
        _ColorDelay ("_ColorDelay", Color) = (1, 0, 0, 1)
        _ColorLost ("_ColorLost", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Pass
        {
            //Tags { "RenderType" = "Opaque"}
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                uint instanceID: SV_InstanceID; // 添加实例 ID
            };

            fixed4 _ColorGreen;
            fixed4 _ColorLost;
            fixed4 _ColorDelay;

            float _CurHpPers[250];   // Max instanced batch size.
            float _CurDelayHpPers[250];   // Max instanced batch size.

            v2f vert (appdata v, uint instanceID: SV_InstanceID)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v)
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.instanceID = instanceID;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                float x = i.uv.x;
                float _CurHpPer = _CurHpPers[i.instanceID];
                float _CurDelayHpPer = _CurDelayHpPers[i.instanceID];
                 if (x < _CurHpPer)
                 {
                     col = _ColorGreen;
                 }
                 else if (x < _CurDelayHpPer)
                 {
                     col = _ColorDelay;
                 }
                 else
                 {
                     col = _ColorLost;
                 }

                return col;
            }
            ENDCG
        }
    }
}
