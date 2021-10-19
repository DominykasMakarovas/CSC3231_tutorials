Shader "CSC3223/BasicTutorialShaderNoDepth"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
                float2 uv       : TEXCOORD0;
                float4 colour   : COLOR0;
            };

            struct v2f
            {                
                float4 vertex   : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float4 colour   : COLOR0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex    = UnityObjectToClipPos(v.vertex);
                o.uv        = TRANSFORM_TEX(v.uv, _MainTex);
                o.colour    = v.colour;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColour    = tex2D(_MainTex, i.uv);
                fixed4 vColour      = i.colour;

                return texColour * vColour;
            }
            ENDCG
        }
    }
}
