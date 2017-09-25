// Shader created with Shader Forge Beta 0.30 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.30;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:True,frtr:False,vitr:False,dbil:False,rmgx:True,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:32521,y:32700|emission-17-OUT,alpha-234-OUT;n:type:ShaderForge.SFN_Color,id:3,x:33120,y:32647,ptlb:Color,ptin:_Color,glob:False,c1:0.1960784,c2:0.1960784,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:10,x:33241,y:33204,ptlb:Intensity,ptin:_Intensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:17,x:32825,y:32802|A-103-OUT,B-10-OUT,C-67-OUT;n:type:ShaderForge.SFN_Panner,id:21,x:33448,y:32704,spu:0.3,spv:-1|UVIN-85-UVOUT,DIST-41-OUT;n:type:ShaderForge.SFN_Tex2d,id:22,x:33241,y:32812,tex:6ea735aa16299c5409cf0dfdf576a9e0,ntxv:0,isnm:False|UVIN-21-UVOUT,TEX-64-TEX;n:type:ShaderForge.SFN_Multiply,id:41,x:33623,y:32720|A-42-TSL,B-43-OUT;n:type:ShaderForge.SFN_Time,id:42,x:33711,y:32595;n:type:ShaderForge.SFN_Vector1,id:43,x:33822,y:32748,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:64,x:33448,y:32872,ptlb:Texture,ptin:_Texture,glob:False,tex:6ea735aa16299c5409cf0dfdf576a9e0;n:type:ShaderForge.SFN_ValueProperty,id:67,x:33072,y:33185,ptlb:Exp,ptin:_Exp,glob:False,v1:5;n:type:ShaderForge.SFN_TexCoord,id:85,x:33711,y:32456,uv:0;n:type:ShaderForge.SFN_Multiply,id:103,x:32825,y:32664|A-3-RGB,B-22-RGB,C-107-RGB;n:type:ShaderForge.SFN_Panner,id:105,x:33448,y:33032,spu:-1,spv:-1|UVIN-85-UVOUT,DIST-41-OUT;n:type:ShaderForge.SFN_Tex2d,id:107,x:33241,y:32976,tex:6ea735aa16299c5409cf0dfdf576a9e0,ntxv:0,isnm:False|UVIN-105-UVOUT,TEX-64-TEX;n:type:ShaderForge.SFN_Tex2d,id:108,x:33072,y:32838,ptlb:Alpha,ptin:_Alpha,tex:18bfce7794338ea488b7abcb10ce750d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:143,x:33072,y:33000|A-108-A,B-10-OUT;n:type:ShaderForge.SFN_Multiply,id:234,x:32825,y:32932|A-22-A,B-107-A,C-143-OUT;proporder:3-10-64-67-108;pass:END;sub:END;*/

Shader "Custom/RimLightNormalMove" {
    Properties {
        _Color ("Color", Color) = (0.1960784,0.1960784,1,1)
        _Intensity ("Intensity", Range(0, 1)) = 1
        _Texture ("Texture", 2D) = "white" {}
        _Exp ("Exp", Float ) = 5
        _Alpha ("Alpha", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers flash 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _Intensity;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Exp;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_42 = _Time + _TimeEditor;
                float node_41 = (node_42.r*2.0);
                float2 node_85 = i.uv0;
                float2 node_21 = (node_85.rg+node_41*float2(0.3,-1));
                float4 node_22 = tex2D(_Texture,TRANSFORM_TEX(node_21, _Texture));
                float2 node_105 = (node_85.rg+node_41*float2(-1,-1));
                float4 node_107 = tex2D(_Texture,TRANSFORM_TEX(node_105, _Texture));
                float3 emissive = ((_Color.rgb*node_22.rgb*node_107.rgb)*_Intensity*_Exp);
                float3 finalColor = emissive;
                float2 node_395 = i.uv0;
                float node_234 = (node_22.a*node_107.a*(tex2D(_Alpha,TRANSFORM_TEX(node_395.rg, _Alpha)).a*_Intensity));
/// Final Color:
                return fixed4(finalColor,node_234);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
