// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:Particles/Additive,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:34001,y:32700,varname:node_1,prsc:2|emission-17-OUT,alpha-90-OUT,voffset-54-OUT;n:type:ShaderForge.SFN_Color,id:3,x:33583,y:32664,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3027,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_NormalVector,id:4,x:33013,y:33247,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:9,x:33687,y:33292,varname:node_9,prsc:2|A-35-OUT,B-7078-OUT;n:type:ShaderForge.SFN_Multiply,id:17,x:33748,y:32847,varname:node_17,prsc:2|A-44-OUT,B-9-OUT;n:type:ShaderForge.SFN_Power,id:35,x:33444,y:33148,varname:node_35,prsc:2|VAL-36-OUT,EXP-38-OUT;n:type:ShaderForge.SFN_Dot,id:36,x:33177,y:33137,varname:node_36,prsc:2,dt:3|A-39-OUT,B-4-OUT;n:type:ShaderForge.SFN_Vector1,id:38,x:33177,y:33291,varname:node_38,prsc:2,v1:10;n:type:ShaderForge.SFN_ViewVector,id:39,x:33013,y:33121,varname:node_39,prsc:2;n:type:ShaderForge.SFN_ScreenPos,id:40,x:32691,y:32464,varname:node_40,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:44,x:33583,y:32817,varname:node_44,prsc:2|A-3-RGB,B-73-OUT,C-67-OUT;n:type:ShaderForge.SFN_Multiply,id:54,x:33891,y:33391,varname:node_54,prsc:2|A-4-OUT,B-87-OUT;n:type:ShaderForge.SFN_Slider,id:55,x:33277,y:33469,ptovrint:False,ptlb:NormalOffset,ptin:_NormalOffset,varname:node_3072,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Panner,id:56,x:32703,y:32730,varname:node_56,prsc:2,spu:0,spv:-0.2|UVIN-40-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:63,x:33050,y:32560,varname:node_1259,prsc:2,tex:e36dd20ec6445b94098b8c15fb1a4ed8,ntxv:0,isnm:False|UVIN-476-OUT,TEX-64-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:64,x:32713,y:33000,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_558,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e36dd20ec6445b94098b8c15fb1a4ed8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:67,x:33301,y:32924,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:node_2733,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Power,id:73,x:33301,y:32988,varname:node_73,prsc:2|VAL-163-RGB,EXP-84-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:84,x:33619,y:33148,varname:node_84,prsc:2,a:3,b:-0.5|IN-7078-OUT;n:type:ShaderForge.SFN_Multiply,id:87,x:33687,y:33438,varname:node_87,prsc:2|A-55-OUT,B-88-OUT;n:type:ShaderForge.SFN_Vector1,id:88,x:33434,y:33542,varname:node_88,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Multiply,id:90,x:33773,y:32988,varname:node_90,prsc:2|A-163-A,B-9-OUT,C-91-OUT,D-9-OUT,E-3-A;n:type:ShaderForge.SFN_ValueProperty,id:91,x:33583,y:33084,ptovrint:False,ptlb:Exp Alpha,ptin:_ExpAlpha,varname:node_6273,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Lerp,id:101,x:33050,y:32422,varname:node_101,prsc:2|A-40-UVOUT,B-63-R,T-122-OUT;n:type:ShaderForge.SFN_Slider,id:122,x:32546,y:32654,ptovrint:False,ptlb:Deformation,ptin:_Deformation,varname:node_9805,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:0.4;n:type:ShaderForge.SFN_Tex2d,id:163,x:33301,y:32697,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_3920,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:862aaf9cd040f1648ab79e988ae59157,ntxv:0,isnm:False|UVIN-101-OUT;n:type:ShaderForge.SFN_Multiply,id:476,x:33050,y:32714,varname:node_476,prsc:2|A-56-UVOUT,B-519-OUT;n:type:ShaderForge.SFN_Slider,id:519,x:32546,y:32902,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_2924,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.18,max:1;n:type:ShaderForge.SFN_Vector1,id:7078,x:33434,y:33380,varname:node_7078,prsc:2,v1:1;proporder:3-55-163-67-91-64-122-519;pass:END;sub:END;*/

Shader "Custom/Aura2" {
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _NormalOffset ("NormalOffset", Range(0, 1)) = 1
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Exp ("Exp", Float ) = 3
        _ExpAlpha ("Exp Alpha", Float ) = 3
        _Noise ("Noise", 2D) = "white" {}
        _Deformation ("Deformation", Range(0, 0.4)) = 0.4
        _Scale ("Scale", Range(0, 1)) = 0.18
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _NormalOffset;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Exp;
            uniform float _ExpAlpha;
            uniform float _Deformation;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                v.vertex.xyz += (v.normal*(_NormalOffset*0.6));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_818 = _Time + _TimeEditor;
                float2 node_476 = ((i.screenPos.rg+node_818.g*float2(0,-0.2))*_Scale);
                float4 node_1259 = tex2D(_Noise,TRANSFORM_TEX(node_476, _Noise));
                float2 node_101 = lerp(i.screenPos.rg,float2(node_1259.r,node_1259.r),_Deformation);
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_101, _MainTexture));
                float node_7078 = 1.0;
                float node_9 = (pow(abs(dot(viewDirection,normalDirection)),10.0)*node_7078);
                float3 emissive = ((_Color.rgb*pow(_MainTexture_var.rgb,lerp(3,-0.5,node_7078))*_Exp)*node_9);
                float3 finalColor = emissive;
                return fixed4(finalColor,(_MainTexture_var.a*node_9*_ExpAlpha*node_9*_Color.a));
            }
            ENDCG
        }
    }
    FallBack "Particles/Additive"
    CustomEditor "ShaderForgeMaterialInspector"
}
