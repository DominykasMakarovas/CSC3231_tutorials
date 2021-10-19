Shader "CSC3223/FragmentExampleHLSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StaticTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        //Blend One One
        //ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex      vert
            #pragma fragment    frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
                float2 uv       : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv       : TEXCOORD0;
                float4 vertex   : SV_POSITION;
            };

            void ARecursiveFunction(int a) {
                if (a > 0) {
                    ARecursiveFunction(a - 1);
                }
            }

            v2f vert (appdata v)
            {
                v2f output;
                output.vertex    = UnityObjectToClipPos(v.vertex);
                output.uv        = v.uv;
                return output;
            }

            sampler2D   _MainTex;
            sampler2D   _StaticTex;

            float movement;

            fixed4 frag (v2f input, bool front : SV_IsFrontFace) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, input.uv);

                if (col.r > 0.25f && col.g > 0.25f) {
                    col = tex2D(_StaticTex, input.uv);
                }
                if (col.a < 0.9f) {
                    discard;
                }

                return col;
            }
            ENDCG
        }
    }
}
