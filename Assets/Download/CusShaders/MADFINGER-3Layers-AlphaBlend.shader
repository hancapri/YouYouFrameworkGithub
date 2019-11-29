// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "MADFINGER/Environment/Scroll 3 Layers AlphaBlended" {
Properties {
	_MainTex ("Base layer (RGB)", 2D) = "white" {}
	_DetailTex ("2nd layer (RGB)", 2D) = "white" {}
	_Tex3 ("3nd layer (RGB)", 2D) = "white" {}
	_ScrollX ("1nd layer Scroll speed X", Float) = 1.0
	_ScrollY ("1nd layer Scroll speed Y", Float) = 0.0
	_Scroll2X ("2nd layer Scroll speed X", Float) = 1.0
	_Scroll2Y ("2nd layer Scroll speed Y", Float) = 0.0
	_Scroll3X ("3nd layer Scroll speed X", Float) = 1.0
	_Scroll3Y ("3nd layer Scroll speed Y", Float) = 0.0
	_Color("Color", Color) = (1,1,1,1)
	_MMultiplier ("Layer Multiplier", Float) = 1.0
}

	
SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off Lighting Off ZWrite Off Fog { Color (1,1,1,0) }
	
	LOD 100
	
	
	
	CGINCLUDE
	#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
	#pragma exclude_renderers molehill    
	#include "UnityCG.cginc"
	sampler2D _MainTex;
	sampler2D _DetailTex;
	sampler2D _Tex3;

	float4 _MainTex_ST;
	float4 _DetailTex_ST;
	float4 _Tex3_ST;
	
	float _ScrollX;
	float _ScrollY;
	float _Scroll2X;
	float _Scroll2Y;
	float _Scroll3X;
	float _Scroll3Y;
	float _MMultiplier;
	float4 _Color;

	struct v2f {
		float4 pos : SV_POSITION;
		float4 uv : TEXCOORD0;
		float4 uv2 : TEXCOORD2;
		fixed4 color : TEXCOORD1;
	};

	
	v2f vert (appdata_full v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv.xy = TRANSFORM_TEX(v.texcoord.xy,_MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time);
		o.uv.zw = TRANSFORM_TEX(v.texcoord.xy,_DetailTex) + frac(float2(_Scroll2X, _Scroll2Y) * _Time);
		o.uv2.xy = TRANSFORM_TEX(v.texcoord.xy,_Tex3) + frac(float2(_Scroll3X, _Scroll3Y) * _Time);
		o.uv2.zw = TRANSFORM_TEX(v.texcoord.xy,_Tex3) + frac(float2(_Scroll3X, _Scroll3Y) * _Time*2
		);
		o.color = _MMultiplier * _Color * v.color;
		return o;
	}
	ENDCG


	Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
//		#pragma fragmentoption ARB_precision_hint_fastest		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 o;
			fixed4 tex = tex2D (_MainTex, i.uv.xy);
			fixed4 tex2 = tex2D (_DetailTex, i.uv.zw);
			fixed4 tex3 = tex2D (_Tex3,i.uv2.xy);
			fixed4 tex4 = tex2D (_Tex3,i.uv2.zw);
			
			o = tex * tex2 * tex3 * tex4 * i.color;
						
			return o;
		}
		ENDCG 
	}	
}
}
