Shader "UI/BGShader" {
    Properties
    {
        [PerRendererData]_MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("BG Color",Color) = (0,0,0,0)
        _PattenTex ("PattenTex", 2D) = "white" {}
        _ScrollX ("Scroll X", Float) = 0
        _ScrollY ("Scroll Y", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
           #pragma vertex vert_img
           #pragma fragment frag
            
           #include "UnityCG.cginc"

            sampler2D _PattenTex;
            float4 _Color;
            float _ScrollX;
            float _ScrollY;

            float4 FixToAspectRatio(fixed4 uv)
            {
                uv.x = frac(uv.x * 3);
                uv.y = frac(uv.y * 4);
                return uv;
            }

            
            float4 frag (v2f_img i) : SV_Target
            {
                float scroll = float2(_ScrollX,_ScrollY) * _Time.y;
                float4 BGUV = float4(i.uv.x,i.uv.y,0,1) + scroll;
                float4 ResultUV = _Color;
                BGUV = FixToAspectRatio(BGUV);

                ResultUV = tex2D(_PattenTex,BGUV);
                ResultUV = lerp(ResultUV,_Color,ResultUV.a);
                return ResultUV;
                //return tex2Dproj(_MainTex, i.pos);
            }
            ENDCG
        }
    }
}
