Shader "Custom/UIGray"
{
    Properties
    {
        [PerRendererData]_MainTex("MainTex", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IngnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert;
            #pragma fragment frag;

            sampler2D _MainTex;
            float4 _MainTex_ST; // 纹理无需编辑，可以不配置ST

            struct a2v
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(a2v v)
            {
                v2f f;
                f.pos = UnityObjectToClipPos(v.vertex);
                f.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                return f;
            }

            fixed4 frag(v2f f) : SV_Target
            {
                // 置灰
                fixed3 grayRGB = dot(tex2D(_MainTex, f.uv), fixed3(0.299, 0.587, 0.114));
                // 透明度
                fixed grayA = tex2D(_MainTex, f.uv).a;
                return fixed4(grayRGB, grayA);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

