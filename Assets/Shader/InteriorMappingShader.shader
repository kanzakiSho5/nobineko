// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Adapted to Unity from http://www.humus.name/index.php?page=3D&ID=80
Shader "Custom/InteriorMapping - Cubemap"
{

    Properties
    {
        _RoomCube ("Room Cube Map", Cube) = "white" {}
        [NoScaleOffset]_FrameTex ("Frame Texture", 2D) = "white" {}
        _GlassTex ("Glass Texture", 2D) = "while" {}
        _Rooms ("Room Atlas Rows&Cols (XY)", Vector) = (1,1,0,0)
        _Smoothness ("Smoothness", Range(0, 1)) = 1
        _BrendDistance("Brend Dist", Range(0.0, 1.0)) = 0.8
        _FrameTilling ("Frame Tilling", float) = 1.0
        _FrameSize ("FrameSize", Range(0.0,1.0)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _USEOBJECTSPACE

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            samplerCUBE _RoomCube;
            sampler2D _FrameTex;
            sampler2D _GlassTex;
            float _BrendDistance;
            float _FrameTilling;
            float _FrameSize;
            float2 _Rooms;
            float4 _RoomCube_ST;

            // psuedo random
            float3 rand3(float co){
                return frac(sin(co * float3(12.9898,78.233,43.2316)) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // uvs
                o.uv = TRANSFORM_TEX(v.uv, _RoomCube);

                // get tangent space camera vector
                float4 objCam = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0));
                float3 viewDir = v.vertex.xyz - objCam.xyz;
                float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                float3 bitangent = cross(v.normal.xyz, v.tangent.xyz) * tangentSign;
                o.viewDir = float3(
                    dot(viewDir, v.tangent.xyz),
                    dot(viewDir, bitangent),
                    dot(viewDir, v.normal)
                    );

                // adjust for tiling
                o.viewDir *= _RoomCube_ST.xyx;
                return o;
            }

            float box(float2 st, float size)
            {
                size = 0.5 +size * 0.5;
                st = step(st, size) * step(1.0 - st,size);
                return !(st.x * st.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // room uvs
                float2 roomUV = frac(i.uv);
                float2 frameUV = frac(i.uv * _FrameTilling);
                float2 roomIndexUV = floor(i.uv);

                //randomize the room
                float2 n = floor(rand3(roomIndexUV.x + roomIndexUV.y * (roomIndexUV.x + 1)).xy * _Rooms.xy);
                roomIndexUV += n;

                // get room depth from room stlas alpha
                fixed farFrac = 0.5;
                float depthScale = 1.0 / (1.0 - farFrac) - 1.0;

                // raytrace box from tangent view dir
                float3 pos = float3(roomUV * 2.0 - 1.0, 1.0);
                float3 id = 1.0 / i.viewDir;
                float3 k = abs(id) - pos * id;
                float kMin = min(min(k.x, k.y), k.z);
                pos += kMin * i.viewDir;

                float frame = box(frameUV, _FrameSize);
                float interp = pos.z * 0.5 + 0.5;
				/*
                // randomly flip & rotate cube map for some variety
                float2 flooredUV = floor(i.uv);
                float3 r = rand3(flooredUV.x + 1.0 + flooredUV.y * (flooredUV.x + 1));
                float2 cubeflip = floor(r.xy * 2.0) * 2.0 - 1.0;
                pos.xz *= cubeflip;
                pos.xz = r.z > 0.5 ? pos.xz : pos.zx;
				*/

                float realZ = saturate(interp) / depthScale + 1;
                interp = 1.0 - (1.0 / realZ);
                interp *= depthScale + 1.0;

                float2 interiorUV = pos.xy * lerp(1.0, farFrac, interp);
                interiorUV = interiorUV * 0.5 + 0.5;
                // glass uvs
                float2 glassUV = frac(i.uv);

                // sample room cube map
                fixed4 room = texCUBE(_RoomCube, pos.xyz);//float3((roomIndexUV + interiorUV.xy) / _Rooms,0.5));//pos.xyz);

                //add frame UV
                fixed4 glasscol = tex2D(_GlassTex, glassUV);
                fixed4 framecol = tex2D(_FrameTex, frameUV);
                room = lerp(room, glasscol, _BrendDistance);
                room = lerp(room, framecol, frame);
                return fixed4(room.rgb, 1.0);
            }
            ENDCG
        }

		CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard alpha

			half _Smoothness;

			struct Input {
				fixed null;
			};

			void surf (Input IN, inout SurfaceOutputStandard o) {
				o.Smoothness = _Smoothness;
			}
		ENDCG
    }
}
