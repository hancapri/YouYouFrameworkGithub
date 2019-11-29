// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Water_Texture"
{
  Properties {
		_WaveScale ("整体缩放", range(0.01,10)) = 0.05
		_difScale ("水波缩放", Range(0,50)) = 1
		_Opacity("透明度",range(0,1)) = 1
		_GroundMap("水波图", 2D) = "balck"{}
		_BumpMap ("法线贴图", 2D) = "bump" {}
		_BumpDepth ("法线明度", Range(1,2)) = 1
		_Reflection( "反射贴图", 2D) = "black" {}
		_RefScale ("反射强度", Range(0,10)) = 1
		_WaveOffset ("双层水波方向,速度 (x,y代表第一层; z,w第二层)", Vector) = (7,4,-6,-3)
//		_Color0("Shallow Color", Color ) = (0.6,0.9,1,1)
		_Color1("主色调", Color ) = (1,1,1,1)
//		_Shininess ("Shininess", Float) = 100
//		_InvRange("inverse Alpha, Depth, and Ccolor ", Vector) = (1, 0.17,0.17,0.25)
		_Fresnel ("Fresnel ", 2D) = "gray" {}
	}
	SubShader {
		Pass {
			Tags{ "Queue"="Transparent" "IgnoreProjector"="true	" "LightMode"="ForwardBase" "RenderType"="Transparent"}
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 100

//			Blend  One SrcAlpha


//					Cull Back
		    ZWrite off
//		ZTest LEqual
//		ColorMask RGBA
//          Fog{Mode Linear Color(0.8,0.8,0.8,1) Density 0.1 Range 0,30}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_fog

//			#pragma exclude_renderers d3d11 xbox360
			#include "UnityCG.cginc"

			//user defined variables
			
			half _WaveScale;
			half _difScale;
			half _Opacity;
			half4 _WaveOffset;
	
			sampler2D _BumpMap;
			sampler2D _GroundMap;
			sampler2D _Fresnel;
			
			fixed4 _SpecColor;
			fixed4 _RimColor;
//			half _Shininess;
			fixed _BumpDepth;
			fixed _RefScale;
			
//			fixed4 _Color0;
			fixed4 _Color1;
			
//			half4 _InvRange;
			
			sampler2D _Reflection;
			
			//base input structs
			struct vertexInput{
				half4 vertex : POSITION;
				half3 normal : NORMAL;
				half4 texcoord : TEXCOORD0;
				half4 tangent : TANGENT;
				half4 color:COLOR;
			};
			struct vertexOutput{
				half4 pos : SV_POSITION;
				half4 col : COLOR;
				half2 tex : TEXCOORD0;
				fixed4 lightDirection : TEXCOORD1;
				fixed4 viewDirection : TEXCOORD2;
				fixed4 normalWorld : TEXCOORD3;
				fixed4 tangentWorld : TEXCOORD4;
				//fixed3 binormalWorld : TEXCOORD5;
				fixed4  bumpuv : TEXCOORD5;
				UNITY_FOG_COORDS(6)

			};
			
			//vertex Function
			
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.col = v.color;
				o.normalWorld.xyz = normalize( mul( half4( v.normal, 0.0 ), unity_WorldToObject ).xyz );
				o.tangentWorld.xyz = normalize( mul( unity_ObjectToWorld, v.tangent ).xyz );
				fixed3 binormalWorld = normalize( cross(o.normalWorld.xyz, o.tangentWorld.xyz) * v.tangent.w );
				o.viewDirection.w = binormalWorld.x;
				o.normalWorld.w = binormalWorld.y;
				o.tangentWorld.w = binormalWorld.z;
				
				half4 posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.tex = v.texcoord.xy;

				half4 temp;
				temp.xyzw = (v.vertex.xzxz * _WaveScale * 0.2 + _WaveOffset * _Time.xxxx*0.1);
		
				o.bumpuv = temp;
				
				o.viewDirection.xyz = normalize(  posWorld.xyz  - _WorldSpaceCameraPos.xyz);
				
				half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				
				o.lightDirection = fixed4(
					normalize( lerp(_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w) ),
					lerp(1.0 , 1.0/length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);
				UNITY_TRANSFER_FOG(o,o.pos);

				return o;
			}
			
			//fragment function
			
			fixed4 frag(vertexOutput i) : COLOR
			{
				//Texture Maps
				fixed4 texN = (tex2D(_BumpMap, i.bumpuv.xy) + tex2D(_BumpMap, i.bumpuv.zw));
				fixed4 ground = tex2D(_GroundMap, (i.tex.xy +(texN-.5)*0.07) *_difScale );
				//(i.tex.xy +(texN-.5)*0.07)* 
				fixed depth = ground.a;
				
				//unpackNormal function
				fixed3 localCoords = fixed3( 1,1,1);
				#if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) && defined(SHADER_API_MOBILE)
					localCoords = normalize( fixed3(2.0 * texN.rg - fixed2(1.0, 1.0), 0) );
				#else
					localCoords = normalize( fixed3(2.0 * texN.ag - fixed2(1.0, 1.0), 0) );
				#endif
				fixed3 reflectCoord = fixed3( localCoords.rg, localCoords.b * _RefScale);
				
				
				
				//normal transpose matrix
				fixed3 binormalWorld = fixed3(i.viewDirection.w, i.normalWorld.w, i.tangentWorld.w);
				fixed3x3 local2WorldTranspose = fixed3x3(
					i.tangentWorld.xyz,
					binormalWorld,
					i.normalWorld.xyz
				);
				
				//calculate normal direction
				fixed3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) ) ;

				fixed3 reflectNormal = normalize( mul( reflectCoord, local2WorldTranspose ) );
				half fresnel = dot( normalize(-i.viewDirection), normalDirection);	
				fresnel = tex2D(_Fresnel, fixed2( fresnel, 0) );
				
				half3 ranges = saturate( half3(1,0,0) * depth  );
				ranges.y = 1 - ranges.y;
				
				fixed nDotL = saturate(dot(normalDirection, i.lightDirection.xyz)) ;
				
				fixed3 specularReflection = nDotL * pow(saturate(dot(reflect(i.lightDirection.xyz, normalDirection), i.viewDirection.xyz)) , 100) ;
		
				half3 refraction = ground.rgb;
		
				refraction =  lerp( refraction, refraction , ranges.z);
				refraction =  lerp( lerp( half3(1,1,1),refraction, ranges.y), refraction, ranges.y);

				fixed4 ref = tex2D(_Reflection,i.viewDirection.xyz);

				fixed4 envColor = tex2D( _Reflection, i.viewDirection.xyz) * _RefScale  ;
				fixed3 aa = lerp(refraction, envColor, fresnel)*_BumpDepth;
				fixed4 col = fixed4( aa , (i.col.a)/1)  * _Opacity * i.col.a * _Color1;
				UNITY_APPLY_FOG(i.fogCoord, col);

				return  col;
			}
			ENDCG
		}
	}
Fallback "Legacy Shaders/Transparent/VertexLit"
}