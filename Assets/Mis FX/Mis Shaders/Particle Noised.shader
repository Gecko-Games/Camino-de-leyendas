// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-2393-OUT,alpha-798-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-3571-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5220588,c2:0.5220588,c3:0.5220588,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32495,y:32923,varname:node_798,prsc:2|A-347-OUT,B-2053-A,C-797-A;n:type:ShaderForge.SFN_Lerp,id:7859,x:32269,y:32294,varname:node_7859,prsc:2|A-3874-OUT,B-7020-R,T-8696-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:8856,x:32060,y:32226,ptovrint:False,ptlb:FirstNoise,ptin:_FirstNoise,varname:node_8856,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:afd02814a93355e4fa3ff2bc1477c94e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:3391,x:31770,y:32452,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3391,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:463f3e17448c6f147a7b7811acdfb56f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:9636,x:31770,y:32265,varname:node_9636,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7020,x:32520,y:32125,varname:node_7020,prsc:2,tex:afd02814a93355e4fa3ff2bc1477c94e,ntxv:0,isnm:False|UVIN-8320-UVOUT,TEX-8856-TEX;n:type:ShaderForge.SFN_Panner,id:8320,x:32326,y:32125,varname:node_8320,prsc:2,spu:-0.2,spv:-1|UVIN-5100-UVOUT,DIST-9636-TSL;n:type:ShaderForge.SFN_Tex2d,id:3571,x:32060,y:32425,varname:node_3571,prsc:2,tex:463f3e17448c6f147a7b7811acdfb56f,ntxv:0,isnm:False|UVIN-7859-OUT,TEX-3391-TEX;n:type:ShaderForge.SFN_Slider,id:8696,x:31692,y:32153,ptovrint:False,ptlb:LerpPotency,ptin:_LerpPotency,varname:node_8696,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:0.4;n:type:ShaderForge.SFN_Multiply,id:347,x:32235,y:32631,varname:node_347,prsc:2|A-3571-A,B-5467-A;n:type:ShaderForge.SFN_Multiply,id:3874,x:32326,y:31993,varname:node_3874,prsc:2|A-5056-OUT,B-5100-UVOUT;n:type:ShaderForge.SFN_Slider,id:5056,x:31971,y:31886,ptovrint:False,ptlb:NoiseScale,ptin:_NoiseScale,varname:node_5056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:5467,x:31770,y:32637,ptovrint:False,ptlb:AlphaFilter,ptin:_AlphaFilter,varname:node_5467,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:5100,x:32050,y:31990,varname:node_5100,prsc:2,uv:0;proporder:3391-797-8856-5056-8696-5467;pass:END;sub:END;*/

Shader "Custom/Particle Noised" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5220588,0.5220588,0.5220588,1)
        _FirstNoise ("FirstNoise", 2D) = "white" {}
        _NoiseScale ("NoiseScale", Range(0.1, 1)) = 1
        _LerpPotency ("LerpPotency", Range(0, 0.4)) = 0.1
        _AlphaFilter ("AlphaFilter", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform sampler2D _FirstNoise; uniform float4 _FirstNoise_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _LerpPotency;
            uniform float _NoiseScale;
            uniform sampler2D _AlphaFilter; uniform float4 _AlphaFilter_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_9636 = _Time + _TimeEditor;
                float2 node_8320 = (i.uv0+node_9636.r*float2(-0.2,-1));
                float4 node_7020 = tex2D(_FirstNoise,TRANSFORM_TEX(node_8320, _FirstNoise));
                float2 node_7859 = lerp((_NoiseScale*i.uv0),float2(node_7020.r,node_7020.r),_LerpPotency);
                float4 node_3571 = tex2D(_MainTex,TRANSFORM_TEX(node_7859, _MainTex));
                float3 emissive = (node_3571.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                float4 _AlphaFilter_var = tex2D(_AlphaFilter,TRANSFORM_TEX(i.uv0, _AlphaFilter));
                fixed4 finalRGBA = fixed4(finalColor,((node_3571.a*_AlphaFilter_var.a)*i.vertexColor.a*_TintColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
