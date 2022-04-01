// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-9374-OUT;n:type:ShaderForge.SFN_TexCoord,id:5951,x:31238,y:32760,varname:node_5951,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3951,x:31845,y:33438,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5440-UVOUT;n:type:ShaderForge.SFN_Time,id:9415,x:31008,y:33239,varname:node_9415,prsc:2;n:type:ShaderForge.SFN_Vector4Property,id:3249,x:30995,y:33006,ptovrint:False,ptlb:AnimSpeed,ptin:_AnimSpeed,varname:node_3249,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Add,id:9717,x:31471,y:32913,varname:node_9717,prsc:2|A-5951-U,B-729-OUT;n:type:ShaderForge.SFN_Add,id:4254,x:31482,y:33199,varname:node_4254,prsc:2|A-5951-V,B-5561-OUT;n:type:ShaderForge.SFN_Append,id:100,x:31691,y:33022,varname:node_100,prsc:2|A-9717-OUT,B-4254-OUT;n:type:ShaderForge.SFN_Multiply,id:6365,x:32091,y:33074,varname:node_6365,prsc:2|A-2221-RGB,B-2468-RGB,C-285-OUT;n:type:ShaderForge.SFN_Color,id:2468,x:31872,y:33110,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_2468,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2221,x:31872,y:32928,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_2221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-100-OUT;n:type:ShaderForge.SFN_Multiply,id:9374,x:32248,y:33160,varname:node_9374,prsc:2|A-6365-OUT,B-3951-RGB;n:type:ShaderForge.SFN_Multiply,id:729,x:31235,y:33021,varname:node_729,prsc:2|A-3249-X,B-9415-T;n:type:ShaderForge.SFN_Multiply,id:5561,x:31235,y:33212,varname:node_5561,prsc:2|A-3249-Y,B-9415-T;n:type:ShaderForge.SFN_TexCoord,id:5440,x:31591,y:33394,varname:node_5440,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:285,x:31896,y:33298,ptovrint:False,ptlb:ColorIntensity,ptin:_ColorIntensity,varname:node_285,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:2468-285-3951-2221-3249;pass:END;sub:END;*/

Shader "Custom/CustomFXShader" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _ColorIntensity ("ColorIntensity", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _AnimSpeed ("AnimSpeed", Vector) = (0,0,0,0)
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _AnimSpeed;
            uniform float4 _MainColor;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _ColorIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_9415 = _Time;
                float2 node_100 = float2((i.uv0.r+(_AnimSpeed.r*node_9415.g)),(i.uv0.g+(_AnimSpeed.g*node_9415.g)));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_100, _Mask));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv1, _MainTex));
                float3 node_9374 = ((_Mask_var.rgb*_MainColor.rgb*_ColorIntensity)*_MainTex_var.rgb);
                float3 emissive = node_9374;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
