Shader "CSC3223/DepthDisplayEffectNoTest"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            sampler2D _CameraDepthNormalsTexture;
            int useLinear;

            fixed4 frag(v2f i) : SV_Target
            {

            float d = 0.0f;

            if (useLinear > 0) {
                d = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
            }
            else {
                d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
            }
             half4 depth = half4(d,d,d,1);
             return depth;
         }
         ENDCG
     }
    }
}
