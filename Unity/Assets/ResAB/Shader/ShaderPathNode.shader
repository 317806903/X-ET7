Shader "MyShader/PathNode"  { // 路径上的节点移动特效
    Properties {
        _MainTex("MainTex", 2D) = "white" {} // 节点贴图
        _Speed("Speed", Range(0.1, 3)) = 2 // 节点移动速度
        [HDR]_Color("Color", Color) = (1, 1, 1, 1) // 节点颜色
    }

    SubShader {
        tags{"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
        Blend  SrcAlpha One // 混合
        // Cull off // 双面
        ZWrite Off

        Pass {
            CGPROGRAM

            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex; // 节点贴图
            float _Speed; // 节点移动速度
            float4 _Color; // 节点颜色

            v2f_img vert(appdata_img v) {
                v2f_img o;
                o.pos = UnityObjectToClipPos(v.vertex); // 模型空间顶点坐标变换到裁剪空间, 等价于: mul(UNITY_MATRIX_MVP, v.vertex)
                o.uv = v.texcoord;
                o.uv.x -= _Speed * _Time.y; // 通过uv纹理坐标的移动实现节点的移动
                return o;
            }

            fixed4 frag(v2f_img i) : SV_Target {
                return tex2D(_MainTex, i.uv) * _Color;
            }

            ENDCG
        }
    }
}