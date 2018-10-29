// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|normal-42-RGB,custl-5286-OUT;n:type:ShaderForge.SFN_Tex2d,id:6589,x:32374,y:32649,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Set,id:3814,x:32925,y:32682,varname:MainTexColor,prsc:2|IN-6589-RGB;n:type:ShaderForge.SFN_Get,id:8809,x:32048,y:33727,varname:node_8809,prsc:2|IN-3814-OUT;n:type:ShaderForge.SFN_LightVector,id:7928,x:31992,y:32929,varname:node_7928,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:348,x:31976,y:33157,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:3946,x:32222,y:32929,varname:node_3946,prsc:2,dt:1|A-348-OUT,B-7928-OUT;n:type:ShaderForge.SFN_Vector1,id:5424,x:32246,y:32809,varname:node_5424,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:3610,x:32421,y:32899,varname:node_3610,prsc:2|A-5424-OUT,B-3946-OUT;n:type:ShaderForge.SFN_Multiply,id:5271,x:32478,y:33094,varname:node_5271,prsc:2|A-3610-OUT,B-3610-OUT;n:type:ShaderForge.SFN_Slider,id:816,x:32615,y:33157,ptovrint:False,ptlb:MainTexIntensity,ptin:_MainTexIntensity,varname:node_816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2366521,max:1;n:type:ShaderForge.SFN_Multiply,id:5734,x:32932,y:33034,varname:node_5734,prsc:2|A-93-OUT,B-816-OUT;n:type:ShaderForge.SFN_HalfVector,id:8447,x:32055,y:33344,varname:node_8447,prsc:2;n:type:ShaderForge.SFN_Dot,id:5594,x:32355,y:33291,varname:node_5594,prsc:2,dt:1|A-348-OUT,B-8447-OUT;n:type:ShaderForge.SFN_Power,id:3939,x:32564,y:33301,varname:node_3939,prsc:2|VAL-5594-OUT,EXP-952-OUT;n:type:ShaderForge.SFN_Exp,id:952,x:32481,y:33480,varname:node_952,prsc:2,et:1|IN-7256-OUT;n:type:ShaderForge.SFN_Slider,id:7256,x:32022,y:33561,ptovrint:False,ptlb:SpecularRange,ptin:_SpecularRange,varname:node_7256,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.396862,max:5;n:type:ShaderForge.SFN_Multiply,id:8645,x:32842,y:33315,varname:node_8645,prsc:2|A-3939-OUT,B-3976-OUT;n:type:ShaderForge.SFN_Slider,id:3976,x:32622,y:33547,ptovrint:False,ptlb:SpecularIntensity,ptin:_SpecularIntensity,varname:node_3976,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.012547,max:5;n:type:ShaderForge.SFN_Add,id:5286,x:33186,y:33260,varname:node_5286,prsc:2|A-5734-OUT,B-8813-OUT,C-2896-OUT,D-6958-OUT;n:type:ShaderForge.SFN_Desaturate,id:8242,x:32241,y:33717,varname:node_8242,prsc:2|COL-8809-OUT;n:type:ShaderForge.SFN_RemapRange,id:9596,x:32481,y:33685,varname:node_9596,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8242-OUT;n:type:ShaderForge.SFN_Clamp01,id:7838,x:32673,y:33685,varname:node_7838,prsc:2|IN-9596-OUT;n:type:ShaderForge.SFN_Multiply,id:8813,x:33119,y:33425,varname:node_8813,prsc:2|A-8645-OUT,B-7838-OUT,C-8610-RGB,D-5892-OUT;n:type:ShaderForge.SFN_Color,id:8610,x:32795,y:33773,ptovrint:False,ptlb:specularColor,ptin:_specularColor,varname:node_8610,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1209493,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:5892,x:33028,y:33729,varname:node_5892,prsc:2,v1:2;n:type:ShaderForge.SFN_RemapRange,id:564,x:32257,y:33927,varname:node_564,prsc:2,frmn:0,frmx:1,tomn:2,tomx:-2|IN-8242-OUT;n:type:ShaderForge.SFN_Color,id:5512,x:32469,y:34094,ptovrint:False,ptlb:SpecularColor,ptin:_SpecularColor,varname:node_5512,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.930985,c3:1,c4:1;n:type:ShaderForge.SFN_Clamp01,id:1592,x:32469,y:33896,varname:node_1592,prsc:2|IN-564-OUT;n:type:ShaderForge.SFN_Multiply,id:2896,x:32724,y:33960,varname:node_2896,prsc:2|A-1592-OUT,B-5512-RGB,C-1499-OUT;n:type:ShaderForge.SFN_Vector1,id:1499,x:32680,y:34127,varname:node_1499,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:42,x:32863,y:32757,ptovrint:False,ptlb:normalTex,ptin:_normalTex,varname:node_42,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:True,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:1974,x:33193,y:33783,varname:node_1974,prsc:2;n:type:ShaderForge.SFN_Exp,id:6714,x:33274,y:33985,varname:node_6714,prsc:2,et:1|IN-2840-OUT;n:type:ShaderForge.SFN_Slider,id:9381,x:32779,y:34254,ptovrint:False,ptlb:RimRange,ptin:_RimRange,varname:node_9381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:6.3746,max:10;n:type:ShaderForge.SFN_Color,id:7895,x:33404,y:33620,ptovrint:False,ptlb:RimColor,ptin:_RimColor,varname:node_7895,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1273585,c2:0.175602,c3:1,c4:1;n:type:ShaderForge.SFN_Power,id:7044,x:33454,y:33802,varname:node_7044,prsc:2|VAL-1974-OUT,EXP-6714-OUT;n:type:ShaderForge.SFN_Multiply,id:6958,x:33738,y:33730,varname:node_6958,prsc:2|A-7895-RGB,B-7044-OUT,C-2388-OUT;n:type:ShaderForge.SFN_Vector1,id:2388,x:33631,y:33983,varname:node_2388,prsc:2,v1:2;n:type:ShaderForge.SFN_RemapRange,id:2840,x:33188,y:34230,varname:node_2840,prsc:2,frmn:0,frmx:10,tomn:10,tomx:0|IN-9381-OUT;n:type:ShaderForge.SFN_Multiply,id:93,x:32693,y:32946,varname:node_93,prsc:2|A-6589-RGB,B-5271-OUT;proporder:6589-42-816-7256-3976-9381-8610-5512-7895;pass:END;sub:END;*/

Shader "Shader Forge/Blinn" {
    Properties {
        _MainTex ("MainTex", 2D) = "bump" {}
        [Normal]_normalTex ("normalTex", 2D) = "bump" {}
        _MainTexIntensity ("MainTexIntensity", Range(0, 1)) = 0.2366521
        _SpecularRange ("SpecularRange", Range(0, 5)) = 3.396862
        _SpecularIntensity ("SpecularIntensity", Range(0, 5)) = 1.012547
        _RimRange ("RimRange", Range(0, 10)) = 6.3746
        [HDR]_specularColor ("specularColor", Color) = (0.1209493,0,1,1)
        [HDR]_SpecularColor ("SpecularColor", Color) = (0,0.930985,1,1)
        [HDR]_RimColor ("RimColor", Color) = (0.1273585,0.175602,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _MainTexIntensity;
            uniform float _SpecularRange;
            uniform float _SpecularIntensity;
            uniform float4 _specularColor;
            uniform float4 _SpecularColor;
            uniform sampler2D _normalTex; uniform float4 _normalTex_ST;
            uniform float _RimRange;
            uniform float4 _RimColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _normalTex_var = tex2D(_normalTex,TRANSFORM_TEX(i.uv0, _normalTex));
                float3 normalLocal = _normalTex_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_3610 = (0.5+max(0,dot(normalDirection,lightDirection)));
                float node_5271 = (node_3610*node_3610);
                float3 MainTexColor = _MainTex_var.rgb;
                float node_8242 = dot(MainTexColor,float3(0.3,0.59,0.11));
                float3 finalColor = (((_MainTex_var.rgb*node_5271)*_MainTexIntensity)+((pow(max(0,dot(normalDirection,halfDirection)),exp2(_SpecularRange))*_SpecularIntensity)*saturate((node_8242*2.0+-1.0))*_specularColor.rgb*2.0)+(saturate((node_8242*-4.0+2.0))*_SpecularColor.rgb*2.0)+(_RimColor.rgb*pow((1.0-max(0,dot(normalDirection, viewDirection))),exp2((_RimRange*-1.0+10.0)))*2.0));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _MainTexIntensity;
            uniform float _SpecularRange;
            uniform float _SpecularIntensity;
            uniform float4 _specularColor;
            uniform float4 _SpecularColor;
            uniform sampler2D _normalTex; uniform float4 _normalTex_ST;
            uniform float _RimRange;
            uniform float4 _RimColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _normalTex_var = tex2D(_normalTex,TRANSFORM_TEX(i.uv0, _normalTex));
                float3 normalLocal = _normalTex_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_3610 = (0.5+max(0,dot(normalDirection,lightDirection)));
                float node_5271 = (node_3610*node_3610);
                float3 MainTexColor = _MainTex_var.rgb;
                float node_8242 = dot(MainTexColor,float3(0.3,0.59,0.11));
                float3 finalColor = (((_MainTex_var.rgb*node_5271)*_MainTexIntensity)+((pow(max(0,dot(normalDirection,halfDirection)),exp2(_SpecularRange))*_SpecularIntensity)*saturate((node_8242*2.0+-1.0))*_specularColor.rgb*2.0)+(saturate((node_8242*-4.0+2.0))*_SpecularColor.rgb*2.0)+(_RimColor.rgb*pow((1.0-max(0,dot(normalDirection, viewDirection))),exp2((_RimRange*-1.0+10.0)))*2.0));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
