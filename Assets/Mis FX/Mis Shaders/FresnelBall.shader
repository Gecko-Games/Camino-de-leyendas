// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:Particles/Additive,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:1,x:33364,y:32698,varname:node_1,prsc:2|emission-13-OUT,alpha-14-OUT;n:type:ShaderForge.SFN_Fresnel,id:2,x:33076,y:32820,varname:node_2,prsc:2|NRM-7-OUT,EXP-9-OUT;n:type:ShaderForge.SFN_NormalVector,id:7,x:32748,y:32698,prsc:2,pt:True;n:type:ShaderForge.SFN_Vector1,id:9,x:32748,y:32861,varname:node_9,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:11,x:33076,y:32961,varname:node_11,prsc:2|A-2-OUT,B-12-A;n:type:ShaderForge.SFN_Color,id:12,x:32810,y:33048,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1663,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5220588,c2:0.5220588,c3:0.5220588,c4:1;n:type:ShaderForge.SFN_Multiply,id:13,x:33076,y:33083,varname:node_13,prsc:2|A-11-OUT,B-16-OUT;n:type:ShaderForge.SFN_Multiply,id:14,x:33166,y:32689,varname:node_14,prsc:2|A-2-OUT,B-12-A;n:type:ShaderForge.SFN_Multiply,id:16,x:33076,y:33212,varname:node_16,prsc:2|A-12-RGB,B-17-OUT;n:type:ShaderForge.SFN_ValueProperty,id:17,x:32810,y:33226,ptovrint:False,ptlb:ModColorValue,ptin:_ModColorValue,varname:node_6480,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:12-17;pass:END;sub:END;*/

Shader "Custom/FresnelBall" {
    Properties {
        _Color ("Color", Color) = (0.5220588,0.5220588,0.5220588,1)
        _ModColorValue ("ModColorValue", Float ) = 1
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
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 2.0
            uniform float4 _Color;
            uniform float _ModColorValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_2 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float3 emissive = ((node_2*_Color.a)*(_Color.rgb*_ModColorValue));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_2*_Color.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
