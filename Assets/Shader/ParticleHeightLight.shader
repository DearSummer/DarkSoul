// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33283,y:32608,varname:node_9361,prsc:2|custl-376-OUT;n:type:ShaderForge.SFN_Tex2d,id:9387,x:32478,y:32751,ptovrint:False,ptlb:tex,ptin:_tex,varname:node_9387,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7656,x:32746,y:32775,varname:node_7656,prsc:2|A-9387-RGB,B-9387-A;n:type:ShaderForge.SFN_Color,id:5509,x:32478,y:32955,ptovrint:False,ptlb:color,ptin:_color,varname:node_5509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:376,x:32998,y:32849,varname:node_376,prsc:2|A-7656-OUT,B-5509-RGB,C-564-OUT,D-2518-OUT;n:type:ShaderForge.SFN_ValueProperty,id:564,x:32677,y:33037,ptovrint:False,ptlb:value,ptin:_value,varname:node_564,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_VertexColor,id:7408,x:32519,y:33181,varname:node_7408,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2518,x:32751,y:33132,varname:node_2518,prsc:2|A-7408-RGB,B-7408-A;proporder:9387-5509-564;pass:END;sub:END;*/

Shader "Shader Forge/ParticleHeightLight" {
    Properties {
        _tex ("tex", 2D) = "white" {}
        _color ("color", Color) = (1,1,1,1)
        _value ("value", Float ) = 2
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
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _tex; uniform float4 _tex_ST;
            uniform float4 _color;
            uniform float _value;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 _tex_var = tex2D(_tex,TRANSFORM_TEX(i.uv0, _tex));
                float3 finalColor = ((_tex_var.rgb*_tex_var.a)*_color.rgb*_value*(i.vertexColor.rgb*i.vertexColor.a));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
