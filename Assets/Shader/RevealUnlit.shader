﻿Shader "Unlit/RevealUnlit"
{
    Properties
    {
        _MainTex ("Top texture", 2D) = "white" {}
        _SubTex  ("Bot texture", 2D) = "white" {}
        _MaskTex ("Trans (A)", 2D) = "white" {}
        _Transparency ("Transparency", Range(0.0, 0.5)) = 0.25
        _CutoutThresh("Cutout Threshold", Range(0.0, 1.0)) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent"  "RenderType"="Transparent" }
        LOD 100
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 mainTexCoord : TEXCOORD0;
                float2 subTexCoord : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D _SubTex;
            float4 _SubTex_ST;
            
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            
            float _Transparency;
            float _CutoutThresh;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.mainTexCoord = TRANSFORM_TEX(v.uv, _MainTex);
                o.subTexCoord = TRANSFORM_TEX(v.uv, _SubTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, i.mainTexCoord);
                fixed4 subCol  = tex2D(_SubTex, i.subTexCoord);
                float w = tex2D(_MaskTex, i.mainTexCoord).a;
                fixed4 col = lerp(mainCol, subCol, w);
                
                if (col.a < _CutoutThresh)
                    col.a = 0; 
                                          
                return col;
            }
            ENDCG
        }
    }
}
